<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="FAQChat.aspx.cs" Inherits="Krous_Ex.FAQChat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Scripts/jquery.signalR-2.4.2.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />

    <script>
    $(function () {

            var chat = $.connection.chatHub;

            chat.client.broadcastMessage = function (userType, message, messageType, sendTime) {
                AddMessage(userType, message, messageType, sendTime)
            };

            chat.client.alertEndChat = function (userType, message) {

                if (userType == $('#<%=hdUserType.ClientID%>').val()) {

                    alert(message)

                } else {
                    alert("The chat is ended by the " + userType)

                }

                window.location.href = "FAQChatView.aspx?ChatGUID=" + $('#<%=hdChatGUID.ClientID%>').val();

            };

            $.connection.hub.start(function () {
                chat.server.join($('#<%=hdChatGUID.ClientID%>').val());
            });

            $.connection.hub.start().done(function () {
                $('#btn-sendmsg').click(function () {

                    if ($('#txtMessage').val() != "") {
                        chat.server.send($('#<%=hdCurrentUserGUID.ClientID%>').val(), $('#<%=hdUserType.ClientID%>').val(), $('#txtMessage').val().replace(/\n/g, "<br>"), $('#<%=hdChatGUID.ClientID%>').val(), $('#<%=hdNewChat.ClientID%>').val(), "Text");
                        newMessageAlert()
                        $('#<%=hdNewChat.ClientID%>').val("False");

                    } else {
                        alert("Please enter message !");
                    }

                });

            });

            $(window).focus(function () {
                if (document.title == "(New Message!) Chat") {
                    document.title = "Chat";
                }
            });

        });

        function AddMessage(userType, message, messageType, sendTime) {

            var divChat = '';
            var linkColor;
            var checkDate = new Date($('#<%=hdCheckDate.ClientID%>').val());
            var todaysDate = new Date();

            if (checkDate.setHours(0, 0, 0, 0) != todaysDate.setHours(0, 0, 0, 0)) {
                divChat += '<div class="row col-lg-12 justifiy-content-center p0">';
                divChat += '<div class="media media-meta-day">Today</div>';
                divChat += '</div>';
                $('#<%=hdCheckDate.ClientID%>').val(todaysDate.toDateString())

            }

            

            if (userType == $('#<%=hdUserType.ClientID%>').val()) {

                linkColor = "pdfLinkWhite";
                clearMsgAfterSend()
                divChat += '<div class="row col-lg-12 justify-content-end p0">'
                divChat += '<div class="media media-chat media-chat-reverse mediaPadding">';

            } else {

                linkColor = "pdfLinkBlack";

                divChat += '<div class="media media-chat mediaPadding">';
            }

            divChat += '<div class="media-body">';



            if (messageType == "Text") {

                divChat += '<p>' + message + '</p>';

            } else if (messageType == "Image") {

                var imagepath = getCookie("ImagePath");

                divChat += '<p>';
                divChat += '<img src="' + imagepath + message + '" alt="Send Image" Style="cursor: pointer" width="250" height="200" onclick="enlargeImage(this.src)">';
                divChat += '</p>';

            } else {

                var imagepath = getCookie("ImagePath");
                var filename = message.split('_');

                divChat += '<p>';
                divChat += '<a class="' + linkColor + '" target="_blank" href="' + imagepath + message + '">' + filename[0] + '.pdf</a>';
                divChat += '</p>';

            }

            divChat += '<p class="meta"><time>' + sendTime + '</time></p>';
            divChat += '</div>';
            divChat += '</div>';
            divChat += '</div>';



            $('#chat-content-Message').append(divChat);

            var height = $('#chat-content')[0].scrollHeight;
            $('#chat-content').scrollTop(height);

            checkTab(userType)
            playAudio(userType)


        }

        function IsValidateFile(fileF) {
            var allowedFiles = [".pdf", ".png", ".jpg", ".jpeg"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:\(\)])+(" + allowedFiles.join('|') + ")$");
            if (!regex.test(fileF.toLowerCase())) {
                alert("Only allow image and pdf file.");
                return false;
            }
            return true;
        }

        function IsImageFile(fileF) {
            var ImageFiles = [".png", ".jpg", ".jpeg"];
            var regex = new RegExp("(" + ImageFiles.join('|') + ")$");
            if (!regex.test(fileF.toLowerCase())) {
                return false;
            }
            return true;
        }

        function uploadComplete(sender, args) {

            var chat = $.connection.chatHub;

            var msg;

            var messageType;

            if (IsValidateFile(args.get_fileName())) {
                if (IsImageFile(args.get_fileName())) {

                    messageType = "Image";

                } else {

                    messageType = "File";
                }

                msg = getCookie("ChatImageName")

                chat.server.send($('#<%=hdCurrentUserGUID.ClientID%>').val(), $('#<%=hdUserType.ClientID%>').val(), msg, $('#<%=hdChatGUID.ClientID%>').val(), $('#<%=hdNewChat.ClientID%>').val(), messageType);
                newMessageAlert()
                $('#<%=hdNewChat.ClientID%>').val("False");

            }
        }

        function getCookie(cname) {
            var name = cname + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        function enlargeImage(src) {
            var modal = document.getElementById("imgModal");
            var modalImg = document.getElementById("enlargeImg");
            modal.style.display = "block";
            modalImg.src = src;
        }

        function CloseModal() {
            var modal = document.getElementById("imgModal");
            modal.style.display = "none";
        }

        function ClickFileUpload() {
            document.getElementById("ctl00_body_AsyncFileUploadChat_ctl02").click()
        }

        function ScrollToBottom() {
            $("#chat-content").animate({ scrollTop: $('#chat-content').prop("scrollHeight") }, 1000);
        }


        function clearMsgAfterSend() {
            document.getElementById("txtMessage").value = "";
        }

        function playAudio(userType) {

            if (userType != $('#<%=hdUserType.ClientID%>').val()) {

                audio.play();

            }
        }

        function checkTab(userType) {

            if (userType != $('#<%=hdUserType.ClientID%>').val()) {
                if (document.hidden) {

                    document.title = "(New Message!) Chat";
                }
            }
        }

        function newMessageAlert() {
            if ($('#<%=hdNewChat.ClientID%>').val() == "True") {
                $('#myPopup').fadeIn();
                 $('#myPopup').delay(10000).fadeOut();
            }
        }

        function endChat() {
            if ($('#<%=hdNewChat.ClientID%>').val() != "True") {
                if (confirm("Are sure want to end this chat ? \n You no longer can send and receive message from this chat.")) {
                    $.ajax({
                        type: "POST",
                        url: "KrousExWebService.asmx/endChat",
                        data: { ChatGUID: $('#<%=hdChatGUID.ClientID%>').val() },
                        dataType: "xml",
                    }).done(function (chartData2) {
                        $(chartData2).find('string').each(function () {

                            var message = $(this).text();
                            var chat = $.connection.chatHub;

                            chat.server.alertEndChatMsg($('#<%=hdChatGUID.ClientID%>').val(), $('#<%=hdUserType.ClientID%>').val(), message);


                        });
                    });
                }
            } else {
                alert("You cannot end a empty chat !");
            }
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="card">
        <div class="card-body">
            <div class="col-lg-12">

                <div class="card-header">
                    <h4 class="card-title"><strong>Chat Room <asp:Literal ID="litStudName" runat="server"></asp:Literal></strong></h4>
                    <a class="btn btn-sm btn-secondary" id="endChatBtn" onclick="endChat()" href="#" data-abc="true">End Chat</a>
                </div>

                <div class="ps-container ps-theme-default ps-active-y" id="chat-content" style="overflow-y: scroll !important; height: 520px !important;">
                    <div id="chat-content-Message">
                        <asp:Literal runat="server" ID="litMessage"></asp:Literal>
                    </div>

                    <div class="popup" id="myPopup">
                        <div class="row col-lg-12 p0">
                            <div class="popuptext">
                                Your message has successfully send to the staff.
                                                <br />
                                You can choose to wait for the reply here or leave the page and check it later on the Chat List 
                            </div>
                        </div>
                    </div>

                </div>

                <div class="box-footer mt-2">
                    <div class="input-group justify-content-end" style="float: right;">
                        <textarea id="txtMessage" class="form-control" onkeydown=""></textarea>
                        <button type="button" id="btn-sendmsg" class="btn btn-primary rounded-circle ml-1 mr-1"><i class="fa fa-paper-plane fa-lg" style="width:40px"></i></button>
                        <button type="button" id="btn-sendImg" class="btn btn-primary rounded-circle" onclick="ClickFileUpload();"><i class="fa fa-paperclip fa-lg" style="width:40px"></i></button>
                        <ajaxToolkit:AsyncFileUpload OnClientUploadComplete="uploadComplete" runat="server" ID="AsyncFileUploadChat" OnUploadedComplete="FileUploadComplete"  Style="display: none"/>
                    </div>
                        
                    <img id="imgDisplay" src="" class="user-image" style="height: 100px;" />

                </div>
            </div>
        </div>
    </div>
    <div id="imgModal" onclick="CloseModal()">
        <img class="modal-content" id="enlargeImg" src="" />
    </div>


    <asp:HiddenField ID="hdCurrentUserGUID" runat="server" />
    <asp:HiddenField ID="hdUserType" runat="server" />
    <asp:HiddenField ID="hdChatGUID" runat="server" />
    <asp:HiddenField ID="hdNewChat" runat="server" />
    <asp:HiddenField ID="hdCheckDate" runat="server" />

    <script>
        const textbox = document.getElementById("txtMessage");
        textbox.addEventListener("keypress", function onEvent(event) {
            if (event.key === "Enter") {
                document.getElementById("btn-sendmsg").click();
            }
        });

        var myDiv = document.getElementById("chat-content");
        myDiv.scrollTop = myDiv.scrollHeight;

    </script>

</asp:Content>
