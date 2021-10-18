<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Krous_Ex.Chat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <script src="Scripts/jquery.signalR-2.4.2.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>

    <script>
        $(function () {

            var chat = $.connection.chatHub;

            chat.client.addMessage = function (message) {
                $('#listMessages').append('<li style="color:black;">' + message + '</li>');
            };

            $("#Send").click(function () {
                chat.server.send($('#txtMessage').val());
            });

            $.connection.hub.start()

        })
    </script>

<%--    <script type="text/javascript">

        window.onload = ScrollToBottom;

        var audio = new Audio('../upload/inquiry/audio/that-was-quick.mp3');

        var keyPressCount = 0;

        $(function () {

            var chat = $.connection.chatHub;

            chat.client.broadcastMessage = function (userType, message, messageType, sendTime) {
                AddMessage(userType, message, messageType, sendTime)
            };

            chat.client.whoIsTyping = function (userType) {
                ShowTyping(userType)
            };

            chat.client.alertEndChat = function (userType, message) {

                if (userType == $('#<%=hdUserType.ClientID%>').val()) {

                    alert(message)

                } else {
                    alert("The chat is ended by the " + userType)

                }

                window.location.href = "ChatView.aspx?ChatGUID=" + $('#<%=hdChatGUID.ClientID%>').val();

            };

            $.connection.hub.start(function () {
                chat.server.join($('#<%=hdChatGUID.ClientID%>').val());
            });

            $.connection.hub.start().done(function () {
                $('#btn-sendmsg').click(function () {

                    if ($('#msgTextArea').val() != "") {
                        chat.server.send($('#<%=hdCurrentUserGUID.ClientID%>').val(), $('#<%=hdUserType.ClientID%>').val(), $('#msgTextArea').val().replace(/\n/g, "<br>"), $('#<%=hdChatGUID.ClientID%>').val(), $('#<%=hdNewChat.ClientID%>').val(), "Text");
                        newMessageAlert()
                        $('#<%=hdNewChat.ClientID%>').val("False");

                    } else {
                        alert("Please enter message !");
                    }

                });

                $('#msgTextArea').keyup(function (e) {

                    if ($(this).val() == "") {
                        $("#sendDiv").slideUp(500);

                    } else {
                        $("#sendDiv").slideDown(500);

                        if (keyPressCount++ % 5 == 0) {
                            keyPressCount = 1
                            isTypingNow()
                        }

                    }


                });

            });

            $(window).focus(function () {
                if (document.title == "(New Message!) Chat") {
                    document.title = "Chat";
                }
            });

        });


        function isTypingNow() {

            var chat = $.connection.chatHub;
            chat.server.isTyping($('#<%=hdChatGUID.ClientID%>').val(), $('#<%=hdUserType.ClientID%>').val());
        }

        function AddMessage(userType, message, messageType, sendTime) {

            var divChat = '';
            var linkColor;
            var checkDate = new Date($('#<%=hdCheckDate.ClientID%>').val());
            var todaysDate = new Date();

            if (checkDate.setHours(0, 0, 0, 0) != todaysDate.setHours(0, 0, 0, 0)) {
                divChat += '<div class="row col-lg-12 p0">';
                divChat += '<div class="media media-meta-day">Today</div>';
                divChat += '</div>';
                $('#<%=hdCheckDate.ClientID%>').val(todaysDate.toDateString())

            }

            divChat += '<div class="row col-lg-12 p0">'

            if (userType == $('#<%=hdUserType.ClientID%>').val()) {

                linkColor = "pdfLinkWhite";
                clearMsgAfterSend()
                keyPressCount = 0
                $("#sendDiv").slideUp(500);
                divChat += '<div class="media media-chat media-chat-reverse mediaPadding">';

            } else {

                linkColor = "pdfLinkBlack";

                if ($('#divIsTyping').is(":visible")) {
                    $("#divIsTyping").slideUp(500);
                }

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

        function ShowTyping(userType) {

            if (userType != $('#<%=hdUserType.ClientID%>').val()) {

                if ($('#divIsTyping').is(":hidden")) {

                    $("#divIsTyping").slideDown(500);


                    ScrollToBottom();

                }

            }
        }

        window.setInterval(function () {
            if ($('#divIsTyping').is(":visible")) {

                $("#divIsTyping").slideUp(500);

            }
        }, 8000);


        function IsValidateFile(fileF) {
            var allowedFiles = [".pdf", ".png", ".jpg", ".jpeg"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
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
            document.getElementById("ctl00_ContentPlaceHolder1_AsyncFileUploadChat_ctl02").click()
        }

        function ScrollToBottom() {
            $("#chat-content").animate({ scrollTop: $('#chat-content').prop("scrollHeight") }, 1000);
        }


        function clearMsgAfterSend() {
            document.getElementById("msgTextArea").value = "";
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
                        url: "TracoWS.asmx/endChat",
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


    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">


    <div class="container">
        <input type="text" id="txtMessage" />
        <input type="button" id="Send" value="Send" />
        <ul id="listMessages"></ul>
    </div>



</asp:Content>
