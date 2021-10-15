<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterAcc.aspx.cs" Inherits="Krous_Ex.RegisterAcc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Assests/main/css/AccRegistration.css" rel="stylesheet" />
    <title>Account Registration</title>

    <script src="Assests/main/js/jquery.validate.min.js"></script>
    <script src="Assests/main/js/jquery.steps.min.js"></script>
    <script src="js/main.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />

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

    <div class="main">
        <div class="container">
            <h2>Register a new account</h2>
            <form method="POST" class="signup-form wizard clearfix" novalidate="novalidate" role="application" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
              
                <asp:UpdatePanel runat="server" ID="pnlLogin" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <asp:Panel ID="pnlLoginInfo" runat="server">
                            <div class="steps clearfix">
                                <ul role="tablist">
                                    <li role="tab" class="first current" aria-disabled="false" aria-selected="true">
                                        <a aria-controls="signup-form-p-0">
                                            <span class="current-info audible"></span>
                                            <div class="title">
                                                <span class="number">1</span>
                                                <span class="title_text">Account Infomation</span>
                                            </div>
                                        </a>
                                    </li>
                                    <li role="tab" class="disabled" aria-disabled="true">
                                        <a aria-controls="signup-form-p-1">
                                            <div class="title">
                                                <span class="number">2</span>
                                                <span class="title_text">Personal Information</span>
                                            </div>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="content clearfix">
                                <fieldset id="signup-form-p-0" role="tabpanel" aria-labelledby="signup-form-h-0" class="body current" aria-hidden="false">
                                    <div class="fieldset-content">
                                        <div class="form-group">
                                            <asp:Label ID="lblUsername" class="form-label" runat="server" Text="">Username</asp:Label>
                                            <asp:TextBox ID="txtUsername" type="text" runat="server" placeholder="Username" Style="color: white"></asp:TextBox>
                                        </div>
                                        <div class="form-group form-password">
                                            <asp:Label ID="lblPassw" class="form-label" runat="server" Text="">Password</asp:Label>
                                            <asp:TextBox ID="txtPassword" type="password" runat="server" placeholder="Password" Style="color: white"></asp:TextBox>
                                            <span id="eyeIcon" class="fa fa-fw fa-eye field-icon" style="color: white;"></span>                                       
                                        </div>
                                        <div class="form-group form-password">
                                            <asp:Label ID="lblConfPass" class="form-label" runat="server" Text="">Confirm Password</asp:Label>
                                            <asp:TextBox ID="txtConfPass" type="password" runat="server" placeholder="Confirm Password" Style="color: white"></asp:TextBox>                                         
                                        </div>
                                    </div>
                                    <div class="fieldset-footer">
                                        <span>Step 1 of 2</span>
                                    </div>
                                    <div class="actions clearfix">
                                        <asp:Button ID="btnNext" CssClass="submit-button1" runat="server" OnClick="btnNext_Click" Text="Next" UseSubmitBehavior="false"/>
                                    </div>
                                </fieldset>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlPersonalInfo" runat="server" Enabled="false" Visible="false">
                            <div class="steps clearfix">
                                <ul role="tablist">
                                    <li role="tab" class="disabled" aria-disabled="true">
                                        <a aria-controls="signup-form-p-0">
                                            <span class="current-info audible"></span>
                                            <div class="title">
                                                <span class="number">1</span>
                                                <span class="title_text">Account Infomation</span>
                                            </div>
                                        </a>
                                    </li>
                                    <li role="tab" class="first current" aria-disabled="false" aria-selected="true">
                                        <a aria-controls="signup-form-p-1">
                                            <div class="title">
                                                <span class="number">2</span>
                                                <span class="title_text">Personal Information</span>
                                            </div>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="content clearfix">
                                <fieldset id="signup-form-p-1" role="tabpanel" aria-labelledby="signup-form-h-1" class="body current" aria-hidden="false" ">
                                    <div class="fieldset-content">
                                        <div class="form-group" style="margin-left: 1px">
                                            <asp:Label ID="lblFName" class="form-label" runat="server" Text="">Full Name</asp:Label>
                                            <asp:TextBox ID="txtFullName" type="text" runat="server" placeholder="Your Name" Style="color: white"></asp:TextBox>
                                        </div>

                                        <div class="form-radio">
                                            <asp:Label ID="lblGender" class="form-label" runat="server" Text="">Gender</asp:Label>
                                            <asp:RadioButtonList ID="rbGender" runat="server">
                                                <asp:ListItem Text="Male" Value="male"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="female"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-date">
                                            <label for="DOB_date" style="margin-right: 27px">Date of Birth</label>
                                            <div class="form-flex">
                                                <div class="form-date-item">
                                                    <asp:DropDownList ID="dob_date" name="day" runat="server">
                                                        <asp:ListItem Value="">DAY</asp:ListItem>  
                                                        <asp:ListItem>1</asp:ListItem>  
                                                        <asp:ListItem>2</asp:ListItem>  
                                                        <asp:ListItem>3</asp:ListItem>  
                                                        <asp:ListItem>4</asp:ListItem>  
                                                        <asp:ListItem>5</asp:ListItem>  
                                                        <asp:ListItem>6</asp:ListItem>  
                                                        <asp:ListItem>7</asp:ListItem>  
                                                        <asp:ListItem>8</asp:ListItem>  
                                                        <asp:ListItem>9</asp:ListItem>  
                                                        <asp:ListItem>10</asp:ListItem>  
                                                        <asp:ListItem>11</asp:ListItem>  
                                                        <asp:ListItem>12</asp:ListItem>  
                                                        <asp:ListItem>13</asp:ListItem>  
                                                        <asp:ListItem>14</asp:ListItem>  
                                                        <asp:ListItem>15</asp:ListItem>  
                                                        <asp:ListItem>16</asp:ListItem>  
                                                        <asp:ListItem>17</asp:ListItem>  
                                                        <asp:ListItem>18</asp:ListItem>  
                                                        <asp:ListItem>19</asp:ListItem>  
                                                        <asp:ListItem>20</asp:ListItem>  
                                                        <asp:ListItem>21</asp:ListItem>  
                                                        <asp:ListItem>22</asp:ListItem>  
                                                        <asp:ListItem>24</asp:ListItem>  
                                                        <asp:ListItem>25</asp:ListItem>  
                                                        <asp:ListItem>26</asp:ListItem>  
                                                        <asp:ListItem>27</asp:ListItem>  
                                                        <asp:ListItem>28</asp:ListItem>  
                                                        <asp:ListItem>29</asp:ListItem>  
                                                        <asp:ListItem>30</asp:ListItem>  
                                                        <asp:ListItem>31</asp:ListItem>  
                                                    </asp:DropDownList>
                                                    <span class="select-icon"><i class="zmdi zmdi-chevron-down"></i></span>
                                                </div>
                                                <div class="form-date-item">
                                                    <asp:DropDownList ID="dob_month" name="month" runat="server">
                                                        <asp:ListItem Value="">MONTH</asp:ListItem>  
                                                        <asp:ListItem>1</asp:ListItem>  
                                                        <asp:ListItem>2</asp:ListItem>  
                                                        <asp:ListItem>3</asp:ListItem>  
                                                        <asp:ListItem>4</asp:ListItem>  
                                                        <asp:ListItem>5</asp:ListItem>  
                                                        <asp:ListItem>6</asp:ListItem>  
                                                        <asp:ListItem>7</asp:ListItem>  
                                                        <asp:ListItem>8</asp:ListItem>  
                                                        <asp:ListItem>9</asp:ListItem>  
                                                        <asp:ListItem>10</asp:ListItem>  
                                                        <asp:ListItem>11</asp:ListItem>  
                                                        <asp:ListItem>12</asp:ListItem>       
                                                    </asp:DropDownList>
                                                    <span class="select-icon"><i class="zmdi zmdi-chevron-down"></i></span>
                                                </div>
                                                <div class="form-date-item">
                                                    <asp:DropDownList ID="dob_year" name="expiry_year" class="valid" aria-invalid="false" runat="server">
                                                        <asp:ListItem Value="">YEAR</asp:ListItem>  
                                                        <asp:ListItem>2021</asp:ListItem>  
                                                        <asp:ListItem>2020</asp:ListItem>  
                                                        <asp:ListItem>2019</asp:ListItem>  
                                                        <asp:ListItem>2018</asp:ListItem>  
                                                        <asp:ListItem>2017</asp:ListItem>  
                                                        <asp:ListItem>2016</asp:ListItem>  
                                                        <asp:ListItem>2015</asp:ListItem>  
                                                        <asp:ListItem>2014</asp:ListItem>  
                                                        <asp:ListItem>2013</asp:ListItem>  
                                                        <asp:ListItem>2012</asp:ListItem>  
                                                        <asp:ListItem>2011</asp:ListItem>  
                                                        <asp:ListItem>2010</asp:ListItem>   
                                                        <asp:ListItem>2009</asp:ListItem>  
                                                        <asp:ListItem>2008</asp:ListItem>  
                                                        <asp:ListItem>2007</asp:ListItem>  
                                                        <asp:ListItem>2006</asp:ListItem>  
                                                        <asp:ListItem>2005</asp:ListItem>  
                                                        <asp:ListItem>2004</asp:ListItem>  
                                                        <asp:ListItem>2003</asp:ListItem>  
                                                        <asp:ListItem>2002</asp:ListItem>  
                                                        <asp:ListItem>2001</asp:ListItem>  
                                                        <asp:ListItem>2000</asp:ListItem>  
                                                        <asp:ListItem>1999</asp:ListItem>  
                                                        <asp:ListItem>1998</asp:ListItem>        
                                                        <asp:ListItem>1997</asp:ListItem>  
                                                        <asp:ListItem>1996</asp:ListItem>  
                                                        <asp:ListItem>1995</asp:ListItem>  
                                                        <asp:ListItem>1994</asp:ListItem>  
                                                        <asp:ListItem>1993</asp:ListItem>  
                                                        <asp:ListItem>1992</asp:ListItem>  
                                                        <asp:ListItem>1991</asp:ListItem>  
                                                        <asp:ListItem>1990</asp:ListItem>  
                                                        <asp:ListItem>1989</asp:ListItem>  
                                                        <asp:ListItem>1988</asp:ListItem>  
                                                        <asp:ListItem>1987</asp:ListItem>  
                                                        <asp:ListItem>1986</asp:ListItem>                                                      
                                                        <asp:ListItem>1985</asp:ListItem>  
                                                        <asp:ListItem>1984</asp:ListItem>  
                                                        <asp:ListItem>1983</asp:ListItem>  
                                                        <asp:ListItem>1982</asp:ListItem>  
                                                        <asp:ListItem>1981</asp:ListItem>  
                                                        <asp:ListItem>1980</asp:ListItem>  
                                                    </asp:DropDownList>
                                                    <span class="select-icon"><i class="zmdi zmdi-chevron-down"></i></span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group" style="margin-left: 1px">
                                            <asp:Label ID="lblIC" class="form-label" runat="server" Text="">IC Number</asp:Label>
                                            <asp:TextBox ID="txtNRIC" type="text" runat="server" placeholder="Your NRIC" Style="color: white"></asp:TextBox>
                                        </div>

                                        <div class="form-group" style="margin-left: 1px">
                                            <asp:Label ID="lblPhone" class="form-label" runat="server" Text="">Contact Number</asp:Label>
                                            <asp:TextBox ID="txtPhoneNo" type="text" runat="server" placeholder="Your Contact Number" Style="color: white"></asp:TextBox>
                                        </div>

                                        <div class="form-group" style="margin-left: 1px">
                                            <asp:Label ID="lblEmail" class="form-label" runat="server" Text="">Email Address</asp:Label>
                                            <asp:TextBox ID="txtEmail" type="text" runat="server" placeholder="Your Email" Style="color: white"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="fieldset-footer">
                                        <span>Step 2 of 2</span>
                                    </div>
                                    <div class="actions clearfix">
                                        <asp:Button ID="btnPrevious" CssClass="submit-button2" runat="server" Text="Previous" OnClick="btnPrevious_Click" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnSubmit" CssClass="submit-button3" runat="server" Text="Submit" OnClick="btnSubmit_Click" UseSubmitBehavior="false"/>
                                    </div>
                                </fieldset>
                            </div>

                        </asp:Panel>
                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

            </form>
        </div>
    </div>
</body>
</html>
