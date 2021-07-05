<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Krous_Ex.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Frogot Password</title>
    <link href="Assests/main/css/ForgotPassword.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
      
		<div class="forgetPassword-holder">
			<div class="form-title">
				<p>Forgot your password?</p>
				<p>Please follow the steps to change your password.</p>
			</div>
			<div class="container-password">
				<div class="input-password">
					<asp:TextBox ID="txtNewPassword" runat="server" type="password" CssClass="inputNewPass" placeholder="Your new password"></asp:TextBox>
					<div class="form-valid">
						<asp:RequiredFieldValidator id="rfvNewPassword" runat="server"
							ControlToValidate="txtNewPassword"
							ErrorMessage="This field is required!"
							ForeColor="Red"
							Display="Dynamic"
							ClientValidationFunction="ValidateTextBox">
						</asp:RequiredFieldValidator>

						<asp:regularexpressionvalidator id="revNewPassword" runat="server"
							display="Dynamic" 
							forecolor="Red" 
							margin-top="80px"
							ErrorMessage="8 - 30 characters, at least one alphabet and one digit"
							validationexpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,30})$" 
							controltovalidate="txtNewPassword"
							ClientValidationFunction="ValidateTextBox">
						</asp:regularexpressionvalidator>
					</div>
					<span class="focus-input100"></span>
					<span class="symbol-input100">
						<i class="fa fa-user"></i>
					</span>
				</div>

				<div class="input-confirm-pass">
					<asp:TextBox ID="txtConfirmPassword" runat="server" type="password" CssClass="input-conf-pass" placeholder="Confirm your new password"></asp:TextBox>
					<div class="form-valid">
						<asp:RequiredFieldValidator id="rfvConfPassword" runat="server"
							ControlToValidate="txtConfirmPassword"
							ErrorMessage="This field is required!"
							ForeColor="Red"
							Display="Dynamic"
							ClientValidationFunction="ValidateTextBox">
						</asp:RequiredFieldValidator>
						<asp:CompareValidator id="cvConfPassword" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" ForeColor="Red" ErrorMessage="Does not match with your password" Display="Dynamic"/>
					</div>
					<span class="focus-input100"></span>
					<span class="symbol-input100">
						<i class="fa fa-envelope"></i>
					</span>
				</div>

			<div class="submit-button">
				<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="contact100-form-btn" />
			</div>
		</div>
	</div>
				
    </form>
</body>
</html>
