<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterAcc.aspx.cs" Inherits="Krous_Ex.RegisterAcc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Assests/main/css/AccRegistration.css" rel="stylesheet" />
    <title>Account Registration</title>

    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/jquery-validation/dist/jquery.validate.min.js"></script>
    <script src="vendor/jquery-validation/dist/additional-methods.min.js"></script>
    <script src="vendor/jquery-steps/jquery.steps.min.js"></script>
    <script src="vendor/minimalist-picker/dobpicker.js"></script>
    <script src="vendor/jquery.pwstrength/jquery.pwstrength.js"></script>
    <script src="js/main.js"></script>

    <script>
        var password = document.getElementById("password"),
            confpass = document.getElementById("confpass");

        function validatePassword() {
            if (password.value != confpass.value) {
                confpass.setCustomValidity("Passwords Don't Match");
            } else {
                confpass.setCustomValidity("");
            }
        }
        password.onchange = validatePassword;
        confpass.onkeyup = validatePassword;
    </script>

    <script type="text/javascript">
        function changeText(id, action) {
            var textbox = document.getElementById(id);
            if (action == 0) {
                if (textbox.value == textbox.title)
                    textbox.value = "";
            }
            if (action == 1) {
                if (textbox.value == "")
                    textbox.value = textbox.title;
            }
        }
    </script>
</head>
<body>         
    
    <div class="main">

        <div class="container">
            <h2>Register a new account</h2>
            <form method="POST" class="signup-form wizard clearfix" novalidate="novalidate" role="application" runat="server">
                <div class="steps clearfix">
                    <ul role="tablist">
                        <li role="tab" class="first current" aria-disabled="false" aria-selected="true">
                            <a id="signup-form-t-0" href="#signup-form-h-0" aria-controls="signup-form-p-0">
                                <span class="current-info audible"></span>
                                <div class="title">
                                    <span class="number">1</span>
                                    <span class="title_text">Account Infomation</span>
                                </div>
                            </a>
                        </li>
                        <li role="tab" class="disabled" aria-disabled="true">
                            <a id="signup-form-t-1" href="#signup-form-h-1" aria-controls="signup-form-p-1">
                                <div class="title">
                                    <span class="number">2</span>
                                    <span class="title_text">Personal Information</span>
                                </div>
                            </a>
                        </li>
                        <li role="tab" class="disabled last" aria-disabled="true">
                            <a id="signup-form-t-2" href="#signup-form-h-2" aria-controls="signup-form-p-2">
                                <div class="title"><span class="number">3</span>
                                    <span class="title_text">Payment Details</span>
                                </div>
                            </a>
                        </li>
                    </ul>
                </div>

            <div class="content clearfix">
                <h3 id="signup-form-h-0" tabindex="-1" class="title current">
                    <span class="title_text">Login Infomation</span>
                </h3>

                <fieldset id="signup-form-p-0" role="tabpanel" aria-labelledby="signup-form-h-0" class="body current" aria-hidden="false">
                   <%-- <div class="fieldset-content">
                        <div class="form-group">
                            <asp:Label ID="lblUsername" class="form-label" runat="server" Text="">Username</asp:Label>
                            <asp:TextBox ID="txtUsername" type="text" runat="server" placeholder="Username"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEmail" class="form-label" runat="server" Text="">Email</asp:Label>
                            <asp:TextBox ID="txtEmail" type="text" runat="server" placeholder="Your Email"></asp:TextBox>
                        </div>
                        <div class="form-group form-password">
                            <asp:Label ID="lblPassw" class="form-label" runat="server" Text="">Password</asp:Label>
                            <asp:TextBox ID="txtPassword" type="password" runat="server" placeholder="Password" data-indicator="pwindicator"></asp:TextBox>
                            <div id="pwindicator">
                                <div class="bar-strength">
                                    <div class="bar-process">
                                        <div class="bar"></div>
                                    </div>
                                </div>
                                <div class="label"></div>
                            </div>
                        </div>
                    </div>--%>
                       <div class="fieldset-content">
                        <div class="form-group" style="margin-left:1px">
                            <asp:Label ID="lblFName" class="form-label" runat="server" Text="">Full Name</asp:Label>
                            <asp:TextBox ID="txtFullName" type="text" runat="server" placeholder="Your Name"></asp:TextBox>                       
                        </div>
   
                        <div class="form-radio">
                            <asp:Label ID="lblGender" class="form-label" runat="server" Text="">Gender</asp:Label>
                            <div class="form-radio-item">
                                <input id="male" type="radio" name="gender" value="male" checked="checked"/>
                                <asp:Label ID="lblMale" for="male" runat="server" Text="">Male</asp:Label>

                                <input id="female" type="radio" name="gender" value="female"/>
                                <asp:Label ID="lblFemale" for="female" runat="server" Text="">Female</asp:Label>
                            </div>
                        </div>

                                              
                    </div>
                    <div class="fieldset-footer">
                        <span>Step 1 of 3</span>
                    </div>
                </fieldset>

                <h3 id="signup-form-h-1" tabindex="-1" class="title">
                    <span class="title_text">Personal Information</span>
                </h3>
                <fieldset id="signup-form-p-1" role="tabpanel" aria-labelledby="signup-form-h-1" class="body" aria-hidden="true" style="display: none;">

                   <%-- <div class="fieldset-content">
                        <div class="form-group">
                            <asp:Label ID="lblFName" class="form-label" runat="server" Text="">Full Name</asp:Label>
                            <asp:TextBox ID="txtFullName" type="text" runat="server" placeholder="Your Name"></asp:TextBox>                       
                        </div>
   
                        <div class="form-radio">
                            <asp:Label ID="lblGender" class="form-label" runat="server" Text="">Gender</asp:Label>
                            <div class="form-radio-item">
                                <input id="radioMale" type="radio" name="gender" value="male" checked="checked"/>
                                <asp:Label ID="lblMale" for="male" runat="server" Text="">Make</asp:Label>

                                <input id="radioFemale" type="radio" name="gender" value="female"/>
                                <asp:Label ID="lblFemale" for="female" runat="server" Text="">Female</asp:Label>
                            </div>
                        </div>
    
                        <div class="form-textarea">
                            <label for="about_us" class="form-label">About us</label>
                            <textarea name="about_us" id="about_us" placeholder="Who are you ..."></textarea>
                        </div>
                    </div>--%>

                    <div class="fieldset-footer">
                        <span>Step 2 of 3</span>
                    </div>

                </fieldset>

                <h3 id="signup-form-h-2" tabindex="-1" class="title">
                    <span class="title_text">Payment Details</span>
                </h3>
                <fieldset id="signup-form-p-2" role="tabpanel" aria-labelledby="signup-form-h-2" class="body" aria-hidden="true" style="display: none;">
                    <div class="fieldset-content"> 
                        
                    </div>

                    <div class="fieldset-footer">
                        <span>Step 3 of 3</span>
                    </div>
                </fieldset>
            </div>

                <div class="actions clearfix">
                    <ul role="menu" aria-label="Pagination">
                        <li class="disabled" aria-disabled="true">
                            <a href="#previous" role="menuitem">Previous</a>
                        </li>
                        <li aria-hidden="false" aria-disabled="false">
                            <a href="#next" role="menuitem">Next</a>
                        </li>
                        <li aria-hidden="true" style="display: none;">
                            <a href="#finish" role="menuitem">Submit</a>
                        </li>
                    </ul>
                </div>
            </form>
        </div>

    </div>
</body>
</html>
