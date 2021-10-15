<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Krous_Ex.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
	<link href="Assests/main/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <div class="card">
			<div class="card-body">
			<h4 class="card-title">Reset Password</h4>
			<p class="card-description">Enter your registered email address and we wil send you link ro reset password.</p>
				<div class="form-group">
                    <asp:Label ID="username" runat="server" Text="Username"></asp:Label>		
                    <asp:TextBox ID="exampleInputUsername1" type="text" placeholder="Username" CssClass="form-control" runat="server"></asp:TextBox>
				</div>

				<div class="form-group">
					<asp:Label ID="email" runat="server" Text="Email Address"></asp:Label>		
                    <asp:TextBox ID="exampleInputEmail1"  type="text" placeholder="Email" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
						ControlToValidate="exampleInputEmail1"
						ID="rfvEmail" 
						runat="server" 
						ErrorMessage="This is required to fill!"
						ForeColor="Red"
						Display="Dynamic"
						ClientValidationFunction="ValidateTextBox">
                    </asp:RequiredFieldValidator>
				</div>

				<div class="button">
					<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary me-2" style="padding:10px; width:100px; margin-top:10px; margin-right:13px;"/>
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-dark" style="padding:10px; width:100px; margin-top:10px; "/>
				</div>
               
			</div>
		</div>
    </form>
</body>
</html>
