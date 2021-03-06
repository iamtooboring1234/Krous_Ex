<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Krous_Ex.Chat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/LiveChat.css" rel="stylesheet" />
    <script src="Scripts/date.format.js"></script>
    <script src="Scripts/jquery.signalR-2.4.2.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>

<%--    <script>
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
    </script>--%>

    <script>
        var IntervalVal;
        $(function () {
            // Declare a proxy to reference the hub.
            var chatHub = $.connection.chatHub;
            registerClientMethods(chatHub);
            // Start Hub
            $.connection.hub.start().done(function () {
                registerEvents(chatHub);
            });
            // Reset Message Counter on Hover
            $("#divChatWindow").mouseover(function () {
                $("#MsgCountMain").html('0');
                $("#MsgCountMain").attr("title", '0 New Messages');
                clearInterval(IntervalVal);
                document.title = 'Live Chat';
            });
            // Stop Title Alert
            window.onfocus = function (event) {
                if (event.explicitOriginalTarget === window) {
                    clearInterval(IntervalVal);
                    document.title = 'SignalR Chat App';
                }
            };
        });
        // Show Title Alert
        function ShowTitleAlert(newMessageTitle, pageTitle) {
            if (document.title == pageTitle) {
                document.title = newMessageTitle;
            }
            else {
                document.title = pageTitle;
            }
        }
        function registerEvents(chatHub) {
    
            var name = $('#<%=hdUserName.ClientID%>').val();
            if (name.length > 0) {
                chatHub.server.connect(name);
            }
            //send button click
            $('#btnSendMsg').click(function () {
                var msg = $("#txtMessage").val();
                if (document.getElementById("txtMessage").value.trim() == '') {
                    alert("Message cannot be empty!");
                } else {
                    if (msg.length > 0) {
                        var userName = $('#<%=hdUserName.ClientID%>').val();
                        var date = GetCurrentDateTime(new Date());
                        chatHub.server.sendMessageToAll(userName, msg, date);
                        $("#txtMessage").val('');
                    }
                }
            });
            // send message on enter button
            $("#txtMessage").keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    $('#btnSendMsg').click();
                }
            });
        }
        function registerClientMethods(chatHub) {
            // Calls when user successfully logged in
            chatHub.client.onConnected = function (id, userName, allUsers, messages, times) {
                $('#hdId').val(id);
                $('#<%=hdUserName.ClientID%>').val(userName);
                $('#spanUser').html(userName);
                // Add All Users
                for (i = 0; i < allUsers.length; i++) {
                    AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].UserImage, allUsers[i].LoginTime);
                }
                // Add Existing Messages
                for (i = 0; i < messages.length; i++) {
                    AddMessage(messages[i].UserName, messages[i].Message, messages[i].Time, messages[i].UserImage);
                }
            }
            // On New User Connected
            chatHub.client.onNewUserConnected = function (id, name, UserImage, loginDate) {
                AddUser(chatHub, id, name, UserImage, loginDate);
            }
            // On User Disconnected
            chatHub.client.onUserDisconnected = function (id, userName) {
                $('#Div' + id).remove();
                var ctrId = 'private_' + id;
                $('#' + ctrId).remove();
                var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');
                $(disc).hide();
                $('#divusers').prepend(disc);
                $(disc).fadeIn(200).delay(2000).fadeOut(200);
            }
            chatHub.client.messageReceived = function (userName, message, time, userimg) {
                AddMessage(userName, message, time, userimg);
                // Display Message Count and Notification
                var CurrUser1 = $('#<%=hdUserName.ClientID%>').val();
                if (CurrUser1 != userName) {
                    var msgcount = $('#MsgCountMain').html();
                    msgcount++;
                    $("#MsgCountMain").html(msgcount);
                    $("#MsgCountMain").attr("title", msgcount + ' New Messages');
                    var Notification = 'New Message From ' + userName;
                    IntervalVal = setInterval("ShowTitleAlert('Live Chat', '" + Notification + "')", 800);
                }
            }
            chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, userimg, CurrentDateTime) {
                var ctrId = 'private_' + windowId;
                if ($('#' + ctrId).length == 0) {
                    OpenPrivateChatBox(chatHub, windowId, ctrId, fromUserName, userimg);
                }
                var CurrUser = $('#<%=hdUserName.ClientID%>').val();
                var Side = 'right';
                var TimeSide = 'left';
                if (CurrUser == fromUserName) {
                    Side = 'left';
                    TimeSide = 'right';
                }
                else {
                    var Notification = 'New Message From ' + fromUserName;
                    IntervalVal = setInterval("ShowTitleAlert('Live Chat', '" + Notification + "')", 800);
                    var msgcount = $('#' + ctrId).find('#MsgCountP').html();
                    msgcount++;
                    $('#' + ctrId).find('#MsgCountP').html(msgcount);
                    $('#' + ctrId).find('#MsgCountP').attr("title", msgcount + ' New Messages');
                }
                var divChatP = '<div class="direct-chat-msg ' + Side + '">' +
                    '<div class="direct-chat-info clearfix">' +
                    '<span class="direct-chat-name pull-' + Side + '">' + fromUserName + '</span>' +
                    '<span class="direct-chat-timestamp pull-' + TimeSide + '"">' + CurrentDateTime + '</span>' +
                    '</div>' +
                    ' <img class="direct-chat-img" src="' + userimg + '" alt="Message User Image">' +
                    ' <div class="direct-chat-text" >' + message + '</div> </div>';
                $('#' + ctrId).find('#divMessage').append(divChatP);
                var htt = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
                $('#' + ctrId).find('#divMessage').slimScroll({
                    height: htt
                });
            }
        }

        function GetCurrentDateTime(now) {
            var localdate = dateFormat(now, "dddd, mmmm dS, yyyy, h:MM:ss TT");
            return localdate;
        }

        function AddUser(chatHub, id, name, UserImage, date) {
            var userId = $('#hdId').val();
            var code = "";
            var Clist = "";
            if (userId == id) {
                code = $('<div class="box-comment">' +
                    '<img class="img-circle img-sm" src="' + UserImage + '" alt="User Image" />' +
                    ' <div class="comment-text">' +
                    '<span class="username">' + name + '<span class="text-muted pull-right">' + date + '</span>  </span></div></div>');
                Clist = $(
                    '<li style="background:#494949;">' +
                    '<a href="#">' +
                    '<img class="contacts-list-img" src="' + UserImage + '" alt="User Image" />' +
                    ' <div class="contacts-list-info">' +
                    ' <span class="contacts-list-name" id="' + id + '">' + name + ' <small class="contacts-list-date pull-right">' + date + '</small> </span>' +
                    ' <span class="contacts-list-msg">How have you been? I was...</span></div></a > </li >');
            }
            else {
                code = $('<div class="box-comment" id="Div' + id + '">' +
                    '<img class="img-circle img-sm" src="' + UserImage + '" alt="User Image" />' +
                    ' <div class="comment-text">' +
                    '<span class="username">' + '<a id="' + id + '" class="user" >' + name + '<a>' + '<span class="text-muted pull-right">' + date + '</span>  </span></div></div>');
                Clist = $(
                    '<li>' +
                    '<a href="#">' +
                    '<img class="contacts-list-img" src="' + UserImage + '" alt="User Image" />' +
                    ' <div class="contacts-list-info">' +
                    '<span class="contacts-list-name" id="' + id + '">' + name + ' <small class="contacts-list-date pull-right">' + date + '</small> </span>' +
                    ' <span class="contacts-list-msg">How have you been? I was...</span></div></a > </li >');
                var UserLink = $('<a id="' + id + '" class="user" >' + name + '<a>');
                $(code).click(function () {
                    var id = $(UserLink).attr('id');
                    if (userId != id) {
                        var ctrId = 'private_' + id;
                        OpenPrivateChatBox(chatHub, id, ctrId, name);
                    }
                });
                var link = $('<span class="contacts-list-name" id="' + id + '">');
                $(Clist).click(function () {
                    var id = $(link).attr('id');
                    if (userId != id) {
                        var ctrId = 'private_' + id;
                        OpenPrivateChatBox(chatHub, id, ctrId, name);
                    }
                });
            }
            $("#divusers").append(code);
            $("#ContactList").append(Clist);
        }

        function AddMessage(userName, message, time, userimg) {
            var CurrUser = $('#<%=hdUserName.ClientID%>').val();
            var Side = 'right';
            var TimeSide = 'left';
            if (CurrUser == userName) {
                Side = 'left';
                TimeSide = 'right';
            }
            var divChat = '<div class="direct-chat-msg ' + Side + '">' +
                '<div class="direct-chat-info clearfix">' +
                '<span class="direct-chat-name pull-' + Side + '">' + userName + '</span>' +
                '<span class="direct-chat-timestamp pull-' + TimeSide + '"">' + time + '</span>' +
                '</div>' +
                ' <img class="direct-chat-img" src="' + userimg + '" alt="Message User Image">' +
                ' <div class="direct-chat-text" >' + message + '</div> </div>';
            $('#divChatWindow').append(divChat);
            var height = $('#divChatWindow')[0].scrollHeight;
            $('#divChatWindow').scrollTop(height);
        }

        // Creation and Opening Private Chat Div
        function OpenPrivateChatBox(chatHub, userId, ctrId, userName) {
            var PWClass = $('#PWCount').val();
            if ($('#PWCount').val() == 'info')
                PWClass = 'danger';
            else if ($('#PWCount').val() == 'danger')
                PWClass = 'warning';
            else
                PWClass = 'info';
            $('#PWCount').val(PWClass);
            var div1 = ' <div class="col-md-4" id="' + ctrId + '"> <div class="box box-solid box-' + PWClass + ' direct-chat direct-chat-' + PWClass + '">' +
                '<div class="box-header with-border">' +
                ' <h3 class="box-title">' + userName + '</h3>' +
                ' <div class="box-tools pull-right">' +
                ' <span data-toggle="tooltip" id="MsgCountP" title="0 New Messages" class="badge bg-' + PWClass + '">0</span>' +
                ' <button type="button" class="btn btn-box-tool" data-widget="collapse">' +
                '    <i class="fa fa-minus"></i>' +
                '  </button>' +
                '  <button id="imgDelete" type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button></div></div>' +
                ' <div class="box-body">' +
                ' <div id="divMessage" class="direct-chat-messages">' + '<p style="color: black;">This chat doesn\'t save into database.</p>' +
                ' </div>' +
                '  </div>' +
                '  <div class="box-footer">' +
                '<div class="input-group justify-content-end" style="float: right;">' +
                '    <input type="text" id="txtPrivateMessage" name="message" placeholder="Type Message ..." class="form-control"  />' +
                '       <button type="button" id="btnSendMessage" class="btn btn-' + PWClass + ' ml-1" ><i class="fa fa-paper-plane fa-lg"></i></button>' +
                '  </div>' +
                ' </div>' +
                ' </div></div>';
            var $div = $(div1);
            // Closing Private Chat Box
            $div.find('#imgDelete').click(function () {
                $('#' + ctrId).remove();
            });
            // Send Button event in Private Chat
            $div.find("#btnSendMessage").click(function () {
                $textBox = $div.find("#txtPrivateMessage");
                var msg = $textBox.val();
                if (msg.length > 0) {
                    chatHub.server.sendPrivateMessage(userId, msg);
                    $textBox.val('');
                }
            });
            // Text Box event on Enter Button
            $div.find("#txtPrivateMessage").keypress(function (e) {
                if (e.which == 13) {
                    $div.find("#btnSendMessage").click();
                }
            });
            // Clear Message Count on Mouse over           
            $div.find("#divMessage").mouseover(function () {
                $("#MsgCountP").html('0');
                $("#MsgCountP").attr("title", '0 New Messages');
            });
            // Append private chat div inside the main div
            $('#PriChatDiv').append($div);
        }
        function uploadComplete(sender, args) {
            var imgDisplay = $get("imgDisplay");
            imgDisplay.src = "img/loading.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "Display:none;";
                imgDisplay.src = img.src;
            };
            imgDisplay.src = "<%= ResolveUrl(UploadFolderPath) %>" + args.get_fileName();
            var chatHub = $.connection.chatHub;
            var userName = $('#<%=hdUserName.ClientID%>').val();
            var date = GetCurrentDateTime(new Date());
            var sizeKB = (args.get_length() / 1024).toFixed(2);
            var msg1;
            if (IsValidateFile(args.get_fileName())) {
                if (IsImageFile(args.get_fileName())) {
                    msg1 =
                        '<div class="box-body">' +
                        '<div class="attachment-block clearfix">' +
                        '<a><img id="imgC" style="width:100px;" class="attachment-img" src="' + imgDisplay.src + '" alt="Attachment Image"></a>' +
                        '<div class="attachment-pushed"> ' +
                        '<h4 class="attachment-heading"><i class="fas fa-image">  ' + args.get_fileName() + ' </i></h4> <br />' +
                        '<div id="at" class="attachment-text"> Dimensions : ' + imgDisplay.height + 'x' + imgDisplay.width + ', Type: ' + args.get_contentType() +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '<a id="btnDownload" href="' + imgDisplay.src + '" class="btn btn-default btn-xs" download="' + args.get_fileName() + '"><i class="fa fa fa-download"></i> Download</a>' +
                        '<a href="' + imgDisplay.src + '" target="_blank" class="btn btn-default btn-xs"><i class="fa fa-camera"></i> View</a>' +
                        '<span class="pull-right text-muted">File Size : ' + sizeKB + ' Kb</span>' +
                        '</div>';
                }
                else {
                    msg1 =
                        '<div class="box-body">' +
                        '<div class="attachment-block clearfix">' +
                        '<a><img id="imgC" style="width:100px;" class="attachment-img" src="img/file-icon.png" alt="Attachment Image"></a>' +
                        '<div class="attachment-pushed"> ' +
                        '<h4 class="attachment-heading"><i class="fas fa-file">  ' + args.get_fileName() + ' </i></h4> <br />' +
                        '<div id="at" class="attachment-text"> Type: ' + args.get_contentType() +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '<a id="btnDownload" href="' + imgDisplay.src + '" class="btn btn-default btn-xs" download="' + args.get_fileName() + '"><i class="fa fa fa-download"></i> Download</a>' +
                        '<a href="' + imgDisplay.src + '" target="_blank" class="btn btn-default btn-xs"><i class="fa fa-camera"></i> View</a>' +
                        '<span class="pull-right text-muted">File Size : ' + sizeKB + ' Kb</span>' +
                        '</div>';
                }
                chatHub.server.sendMessageToAll(userName, msg1, date);
            }
            imgDisplay.src = '';
        }

        function ClickFileUpload() {
            document.getElementById("ctl00_body_AsyncFileUpload1_ctl02").click()
        }

        function IsValidateFile(fileF) {
            var allowedFiles = [".doc", ".docx", ".pdf", ".txt", ".zip", ".rar", ".xlsx", ".xls", ".png", ".jpg", ".gif"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            if (!regex.test(fileF.toLowerCase())) {
                alert("Please upload files having extensions: " + allowedFiles.join(', ') + " only.");
                return false;
            }
            return true;
        }
        function IsImageFile(fileF) {
            var ImageFiles = [".png", ".jpg", ".gif"];
            var regex = new RegExp("(" + ImageFiles.join('|') + ")$");
            if (!regex.test(fileF.toLowerCase())) {
                return false;
            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">


    <div class="container-fluid page-body-wrapper">
        <div class="main-panel">
            <div class="content-wrapper">
                <div class="row">
                    <div class="col-md-8">
                        <!-- DIRECT CHAT PRIMARY -->
                        <div class="box box-primary direct-chat direct-chat-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title" style="color: dimgrey;">Welcome to Discussion Room <span id='spanUser'></span></h3>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" id="btnClearChat" data-toggle="tooltip" title="Clear Chat">
                                        <i class="fa fa-trash-o"></i>
                                    </button>

                                    <span data-toggle="tooltip" id="MsgCountMain" title="0 New Messages" class="badge bg-gray">0</span>
                                </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <!-- Conversations are loaded here -->
                                <div class="box-body" id="chat-box">
                                    <!-- Conversations are loaded here -->

                                    <div id="divChatWindow" class="direct-chat-messages" style="height: 450px;">
                                    </div>

                                    <div class="direct-chat-contacts">
                                        <ul class="contacts-list" id="ContactList">

                                            <!-- End Contact Item -->
                                        </ul>
                                        <!-- /.contatcts-list -->
                                    </div>
                                    <!-- /.direct-chat-pane -->

                                </div>


                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer">
                        
                                <div class="input-group justify-content-end" style="float: right;">
                                    <textarea id="txtMessage" class="form-control"></textarea>


                                 <button type="button" id="btnSendMsg" class="btn btn-primary rounded-circle ml-1 mr-1">
                                    <i class="fa fa-paper-plane fa-lg"></i>
                                </button>

                               <button type="button" id="btnFile" class="btn btn-primary rounded-circle" onclick="ClickFileUpload();">
                                    <i class="fa fa-paperclip fa-lg"></i>
                                </button>

                                <ajaxToolkit:AsyncFileUpload OnClientUploadComplete="uploadComplete" runat="server" ID="AsyncFileUpload1" OnUploadedComplete="FileUploadComplete"  Style="display: none"/>
      
                                </div>
                        
                                <img id="imgDisplay" src="" class="user-image" style="height: 100px;" />

                            </div>
                            <!-- /.box-footer-->
                        </div>
                        <!--/.direct-chat -->
                    </div>
                    <!-- /.col -->
                    <div class="col-md-4">

                        <div class="box box-solid box-primary">

                            <div class="box-header with-border">
                                <h3 class="box-title">Online Users <span id='UserCount'></span></h3>
                            </div>

                            <div class="box-footer box-comments" id="divusers">
                            </div>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row" id="PriChatDiv">
                        </div>
                        <textarea class="form-control" style="visibility: hidden;"></textarea>
                        <!--/.private-chat -->
                    </div>
                </div>
            </div>
        </div>
    </div>


    <span id="time"></span>
    <input id="hdId" type="hidden" />
    <input id="PWCount" type="hidden" value="info" />
    <asp:HiddenField ID="hdUserName" runat="server" />


    <style>
        .upload-btn-wrapper {
            position: relative;
            overflow: hidden;
            display: inline-block;
            margin-top: 5px;
        }
            .upload-btn-wrapper input[type=file] {
                font-size: 100px;
                position: absolute;
                left: 0;
                top: 0;
                opacity: 0;
            }
        .direct-chat-text img {
            width: 20px;
        }
    </style>


</asp:Content>
