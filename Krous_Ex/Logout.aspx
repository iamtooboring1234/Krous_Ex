<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Krous_Ex.Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Assests/main/css/style.css" rel="stylesheet" />
    <link href="Assests/main/css/LoginPage.css" rel="stylesheet" />
    <link href="Assests/main/js/Login.js" rel="stylesheet" />
    <link href="Assests/main/css/all.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container vh-100 d-flex">
            <div class="content-wrapper  align-self-center justify-content-center">
                <div class="card col-md-6 mx-auto" style="border: 2px solid white;border-radius: 20px;">
                    <div class="card-body pb-0" >
                        <div class="form-group">
                            <div class="input-group justify-content-center">
                                <div style="padding: 20px;border: 2px solid white;border-radius: 100%;">
                                 <img src="Assests/main/img/exit.png" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="input-group justify-content-center">
                                <asp:Label ID="lblLogout" runat="server" Text="" Style="font-size: 33px;">You Have Been Logged Out!</asp:Label>
                            </div>
                        </div>

                        <asp:Button ID="btnBack" runat="server" type="submit" CssClass="submit-btn" Text="Back To Homepage" OnClick="btnBack_Click"></asp:Button>

                    </div>
                    <hr style="border-bottom:1px solid #DDDDDD" />
                    <div class="pb-3">
                        <div class="register-panel text-center font-semibold"> <a href="index.php">Home</a> <span class="mgl-10 mgr-10 vd_soft-grey">|</span> <a href="#">About</a> <span class="mgl-10 mgr-10 vd_soft-grey">|</span> <a href="#">FAQ</a> <span class="mgl-10 mgr-10 vd_soft-grey">|</span> <a href="#">Contact</a> </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
