<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StaffLogin.aspx.cs" Inherits="Krous_Ex.StaffLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>

    <link href="Assests/main/css/style.css" rel="stylesheet" />

    <link href="Assests/main/css/LoginPage.css" rel="stylesheet" />
    <link href="Assests/main/js/Login.js" rel="stylesheet" />
    <link href="Assests/main/css/font-awesome/all.min.css" rel="stylesheet" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
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
        <div class="container vh-100 d-flex">
            <div class="content-wrapper  align-self-center justify-content-center">
                <div class="card col-md-6 mx-auto">
                    <div class="card-body">
                        <a href="Default.aspx">Back to Home Page</a>
                        <div class="form-title">
                            <asp:Image ID="teacherImage" runat="server" ImageUrl="~/Assests/main/img/teacher.png" Height="60px" Width="68px" />
                            <asp:Label ID="lblLogin" runat="server" Text="" Style="font-size: 33px;">Staff Login</asp:Label>
                        </div>
                        <div class="form-group">

                            <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server" placeholder="Username"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <div class="input-group">
                                <asp:TextBox ID="txtPassword" CssClass="form-control" type="password" runat="server" placeholder="Password"></asp:TextBox>
                                <div class="input-group-append">
                                    <span id="eyeIcon" class="fa fa-eye input-group-text"></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="text-right">
                                <a href="ForgotPassword.aspx?UserType=Staff">Forgot your password?</a>
                            </div>
                        </div>


                        <%-- <div class="remember-me">
                    <asp:CheckBox ID="chkRmbrMe" CssClass="remember" runat="server" Text="Remember me"/>  
                </div>--%>

                        <div class="form-group">
                        </div>

                        <asp:Button ID="btnLogin" runat="server" type="submit" CssClass="submit-btn" Text="Log in" OnClick="btnLogin_Click"></asp:Button>
                        <div class="register-acc">
                            <p>Don't have an account?
                                <asp:HyperLink ID="hlRegister" runat="server"><a href="RegisterAcc.aspx">Register Now!</a></asp:HyperLink></p>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </form>
</body>
</html>
