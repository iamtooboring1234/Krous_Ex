<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Krous_Ex.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
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
                            <h4 class="card-title" style="font-size: 20px;">Forgot Password</h4>
                            <p class="card-description">Enter your registered email address and we wil send you link ro reset password.</p>

                            <div class="form-group" style="margin-top: 15px;">
                                <asp:Label ID="email" runat="server" Text="Email Address"></asp:Label>
                                <asp:TextBox ID="txtEmailAddress" type="text" placeholder="Email" CssClass="form-control" runat="server" Style="width: 400px"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ControlToValidate="txtEmailAddress"
                                    ID="rfvEmail"
                                    runat="server"
                                    ErrorMessage="This is required to fill!"
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    ClientValidationFunction="ValidateTextBox">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="button">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary me-2" Style="padding: 10px; width: 100px; margin-top: 10px; margin-right: 13px;" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-dark" Style="padding: 10px; width: 100px; margin-top: 10px;" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
