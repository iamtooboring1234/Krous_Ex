<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Krous_Ex.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <link href="Assests/main/css/ForgotPassword.css" rel="stylesheet" />
    <link href="Assests/main/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-scroller">
            <div class="main-panel">
                <div class="content-wrapper">
                    <div class="card">
                        <div class="card-body">
                            <h4 class="card-title" style="font-size: 20px;">Reset Password</h4>
                            <p class="card-description">Enter your new password to reset.</p>
                            <div class="form-group">
                                <asp:Label ID="newPassowrd" runat="server" Text="New Password"></asp:Label>
                                <asp:TextBox ID="txtNewPassword" type="password" placeholder="New passowrd" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group" style="margin-top:15px;">
                                <asp:Label ID="confrimPassword" runat="server" Text="Confirm New Password"></asp:Label>
                                <asp:TextBox ID="txtConfPassword" type="password" placeholder="Confirm new password" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="button">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary me-2" Style="padding: 10px; width: 100px; margin-top: 10px; margin-right: 13px;" OnClick="btnReset_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-dark" Style="padding: 10px; width: 100px; margin-top: 10px;" OnClick="btnCancel_Click" />
                                <%--<asp:Button ID="btnLogin" runat="server" Text="To Login Page" CssClass="btn btn-primary me-2" Style="padding: 10px; width: 160px; margin-top: 10px;" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
