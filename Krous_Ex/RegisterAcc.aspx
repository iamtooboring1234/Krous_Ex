<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterAcc.aspx.cs" Inherits="Krous_Ex.RegisterAcc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Assests/main/css/AccRegistration.css" rel="stylesheet" />
    <title>Account Registration</title>

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

</head>
<body>         
    
     <div class="main">
        <div class="container">
            <h2>Register a new account</h2>
            <form method="POST" id="signup-form" class="signup-form">

                <!-- top navigation -->
                <div class="steps clearfix">
	                <ul role="tablist">
		                <li role="tab" class="first current" aria-disabled="false" aria-selected="true">
			                <a id="signup-form-t-0" href="#signup-form-h-0" aria-controls="signup-form-p-0">
				                <span class="current-info audible"> </span>
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
				                <div class="title">
					                <span class="number">3</span>
					                <span class="title_text">Payment Details</span>
				                </div>
			                </a>
		                </li>
	                </ul>
                </div>
               

            <fieldset>
                <div class="fieldset-content">
                    <div class="form-group">
                        <div class="label">
                                <label for="username" class="form-label">Username</label>
                        </div>
                        <div class="input-content">
                            <input type="text" name="username" id="username" placeholder="Username" />
                        </div>     
                    </div>
                    <div class="form-group">
                        <div class="label">
                                <label for="email" class="form-label" style="margin-left:27px;">Email</label>
                        </div>
                        <div class="input-content">
                                <input type="email" name="email" id="email" placeholder="Your Email" />
                        </div>
                    </div>         
                    <div class="form-group form-password">
                        <div class="label">
                                <label for="password" class="form-label" style="margin-left:4px;">Password</label>
                        </div>
                        <div class="input-content">
                            <input type="password" name="password" placeholder="Password" id="password" required/>
                            <%--<input type="password" name="password" id="password"  />--%>
                        </div>
                    </div>
                    <div class="form-group form-conf-pass">
                        <div class="label">
                                <label for="confpass" class="form-label" style="margin-left:-54px;">Confirm Password</label>
                        </div>
                        <div class="input-content">
                            <input type="password" name="password" placeholder="Confirm Password" id="confpass" required/>
                            <%--<input type="password" name="password" id="confpass"  />--%> 
                        </div>
                    </div>
                </div>
                <div class="fieldset-footer" style="margin-top: 10px;">
                    <span>Step 1 of 3</span>
                </div>

                <div class="actions clearfix" style="float:right;">
                    <ul role="menu" aria-label="Pagination">
                        <%-- <li class="disabled" aria-disabled="true">
                            <a href="#previous" role="menuitem">Previous</a>
                        </li>--%>
                        <li aria-hidden="false" aria-disabled="false">
                            <a href="#next" role="menuitem">Next</a>
                        </li>
                        <li aria-hidden="true" style="display: none;">
                            <a href="#finish" role="menuitem">Submit</a>
                        </li>
                    </ul>
                </div>

            </fieldset>

               
             <%-- <fieldset>
                  <div class="fieldset-content">
                        <div class="form-group">
                            <label for="full_name" class="form-label">Full name</label>
                            <input type="text" name="full_name" id="full_name" placeholder="Full Name" />
                        </div>
                        <div class="form-radio">
                            <label for="gender" class="form-label">Gender</label>
                            <div class="form-radio-item">
                                <input type="radio" name="gender" value="male" id="male" checked="checked" />
                                <label for="male">Male</label>
    
                                <input type="radio" name="gender" value="female" id="female" />
                                <label for="female">Female</label>
                            </div>
                        </div>
                    </div>
                    <div class="fieldset-footer">
                        <span>Step 2 of 3</span>
                    </div>
                </fieldset>--%>
                

            </form>
        </div>
    </div>

</body>
</html>
