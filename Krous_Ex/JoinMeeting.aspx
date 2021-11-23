<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JoinMeeting.aspx.cs" Inherits="Krous_Ex.JoinMeeting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Assests/main/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        #zmmtg-root {
            background-color: transparent !important;
            width: 0 !important;
            height: 0 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">


        <asp:TextBox ID="txtClass" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>

        <asp:TextBox ID="txtTopic" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>


        <asp:TextBox ID="txtMeetingID" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>

        <asp:TextBox ID="txtMeetingPass" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>



        <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>


        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-rounded" Enabled="False"></asp:TextBox>

        <button type="submit" class="btn btn-primary" id="join_meeting">Join</button>
        <button type="submit" class="btn btn-primary" id="back" runat="server" onclick="btnBack_Click">Back</button>

        <asp:TextBox ID="txtRole" runat="server" Class="hidden"></asp:TextBox>
        <asp:TextBox ID="txtName" runat="server" Class="hidden"></asp:TextBox>


        <input type="text" name="meeting_role" id="meeting_role" value="" class="hidden">
        <input type="text" name="meeting_china" id="meeting_china" value="0" class="hidden">
        <input type="text" name="meeting_lang" id="meeting_lang" value="en-US" class="hidden">

        <input type="text" name="display_name" id="display_name" value="2.0.1#CDN" maxlength="100" placeholder="Name" required="" class="hidden">
        <input type="text" name="meeting_number" id="meeting_number" value="<%= this.txtMeetingID.Text %>" maxlength="200" style="width: 150px" placeholder="Meeting Number" required="" class="hidden">
        <input type="text" name="meeting_pwd" id="meeting_pwd" value="<%= this.txtMeetingPass.Text %>" style="width: 150px" maxlength="32" placeholder="Meeting Password" class="hidden">
        <input type="text" name="meeting_email" id="meeting_email" value="" style="width: 150px" maxlength="32" placeholder="Email option" class="hidden">
    </form>


    <script src="Assests/main/vendors/zoom-meeting/zoom-index.js"></script>
    <script src="Assests/main/vendors/zoom-meeting/zoom-tool.js"></script>
    <script src="Assests/main/vendors/zoom-meeting/zoom-vconsole.min.js"></script>


    <!-- For either view: import Web Meeting SDK JS dependencies -->
    <script src="https://source.zoom.us/2.0.1/lib/vendor/react.min.js"></script>
    <script src="https://source.zoom.us/2.0.1/lib/vendor/react-dom.min.js"></script>
    <script src="https://source.zoom.us/2.0.1/lib/vendor/redux.min.js"></script>
    <script src="https://source.zoom.us/2.0.1/lib/vendor/redux-thunk.min.js"></script>
    <script src="https://source.zoom.us/2.0.1/lib/vendor/lodash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

    <!-- For Component View -->


    <script>
        var meetingnum = document.getElementById('<%=txtMeetingID.ClientID%>').value;
        var meetingpwd = document.getElementById('<%=txtMeetingPass.ClientID%>').value;
        var role = document.getElementById('<%=txtRole.ClientID%>').value;
        var name = document.getElementById('<%=txtName.ClientID%>').value;
        document.getElementById('meeting_number').value = meetingnum;
        document.getElementById('meeting_pwd').value = meetingpwd;
        document.getElementById('meeting_role').value = role;
        document.getElementById('display_name').value = name;
    </script>
    <!-- For Client View -->
    <script src="https://source.zoom.us/zoom-meeting-2.0.1.min.js"></script>

</body>
</html>
