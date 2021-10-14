﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StaffLogin.aspx.cs" Inherits="Krous_Ex.StaffLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Login Page</title>

    <link href="Assests/main/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Assests/main/css/LoginPage.css" rel="stylesheet" />
    <link href="Assests/main/js/Login.js" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/>
   
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#eyeIcon").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("#txtPassword").attr("type", type);
            });
        });
    </script>
</head>
<body>  
    <form id="form1" runat="server">
        <div class="form-structor">
            <a href="Default.aspx">Back to Home Page</a>
            <div class="register">  
                <div class="form-title">
                    <asp:Image ID="teacherImage" runat="server" ImageURL="~/Assests/main/img/teacher.png" Height="60px" Width="68px"/>
                    <asp:Label ID="lblLogin" runat="server" Text="" Style="font-size:33px;">Staff Login</asp:Label>
                </div>
                <div class="form-holder">
                    <asp:TextBox ID="txtUsername" CssClass="input" runat="server" placeholder="Username"></asp:TextBox>
                </div>
                <div class="form-holder">  
                    <asp:TextBox ID="txtPassword" CssClass="input" type="password" runat="server" placeholder="Password"></asp:TextBox>
                    <span id="eyeIcon" class="fa fa-fw fa-eye field-icon"></span> 
                </div>
               <%-- <div class="remember-me">
                    <asp:CheckBox ID="chkRmbrMe" CssClass="remember" runat="server" Text="Remember me"/>  
                </div>--%>
                <div class="forgot-pass">
                    <a href="ForgotPassword.aspx">Forgot your password?</a>     
                </div>
                <asp:Button ID="btnLogin" runat="server" type="submit" CssClass="submit-btn" Text="Log in" OnClick="btnLogin_Click"></asp:Button>
                <div class="register-acc">              
                    <p>Don't have an account? <asp:HyperLink ID="hlRegister" runat="server"><a href="RegisterAcc.aspx">Register Now!</a></asp:HyperLink></p>
                </div>
            </div>
        </div>

    </form>
</body>
</html>