<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterAcc.aspx.cs" Inherits="Krous_Ex.RegisterAcc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Assests/main/css/AccRegistration.css" rel="stylesheet" />
    <title>Account Registration</title>

    <%--<script src="Assests/main/js/jquery.validate.min.js"></script>
    <script src="Assests/main/js/jquery.steps.min.js"></script>
    <script src="js/main.js"></script>--%>
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
                                            <div class="form-radio-item">
                                                <input type="radio" name="gender" value="male" id="male" checked="checked" />
                                                <label for="male">Male</label>

                                                <input type="radio" name="gender" value="female" id="female" />
                                                <label for="female">Female</label>
                                            </div>
                                        </div>

                                        <div class="form-date">
                                            <label for="DOB_date" style="margin-right: 27px">Date of Birth</label>
                                            <div class="form-flex">
                                                <div class="form-date-item">
                                                    <select id="expiry_date" name="day">
                                                        <option value="">DD</option>
                                                        <option value="01">01</option>
                                                        <option value="02">02</option>
                                                        <option value="03">03</option>
                                                        <option value="04">04</option>
                                                        <option value="05">05</option>
                                                        <option value="06">06</option>
                                                        <option value="07">07</option>
                                                        <option value="08">08</option>
                                                        <option value="09">09</option>
                                                        <option value="10">10</option>
                                                        <option value="11">11</option>
                                                        <option value="12">12</option>
                                                        <option value="13">13</option>
                                                        <option value="14">14</option>
                                                        <option value="15">15</option>
                                                        <option value="16">16</option>
                                                        <option value="17">17</option>
                                                        <option value="18">18</option>
                                                        <option value="19">19</option>
                                                        <option value="20">20</option>
                                                        <option value="21">21</option>
                                                        <option value="22">22</option>
                                                        <option value="23">23</option>
                                                        <option value="24">24</option>
                                                        <option value="25">25</option>
                                                        <option value="26">26</option>
                                                        <option value="27">27</option>
                                                        <option value="28">28</option>
                                                        <option value="29">29</option>
                                                        <option value="30">30</option>
                                                        <option value="31">31</option>
                                                    </select>
                                                    <span class="select-icon"><i class="zmdi zmdi-chevron-down"></i></span>
                                                </div>
                                                <div class="form-date-item">
                                                    <select id="expiry_month" name="month">
                                                        <option value="">MM</option>
                                                        <option value="01">01</option>
                                                        <option value="02">02</option>
                                                        <option value="03">03</option>
                                                        <option value="04">04</option>
                                                        <option value="05">05</option>
                                                        <option value="06">06</option>
                                                        <option value="07">07</option>
                                                        <option value="08">08</option>
                                                        <option value="09">09</option>
                                                        <option value="10">10</option>
                                                        <option value="11">11</option>
                                                        <option value="12">12</option>
                                                    </select>
                                                    <span class="select-icon"><i class="zmdi zmdi-chevron-down"></i></span>
                                                </div>
                                                <div class="form-date-item">
                                                    <select id="expiry_year" name="expiry_year" class="valid" aria-invalid="false">
                                                        <option value="">YYYY</option>
                                                        <option value="2021">2021</option>
                                                        <option value="2020">2020</option>
                                                        <option value="2019">2019</option>
                                                        <option value="2018">2018</option>
                                                        <option value="2017">2017</option>
                                                        <option value="2016">2016</option>
                                                        <option value="2015">2015</option>
                                                        <option value="2014">2014</option>
                                                        <option value="2013">2013</option>
                                                        <option value="2012">2012</option>
                                                        <option value="2011">2011</option>
                                                        <option value="2010">2010</option>
                                                        <option value="2009">2009</option>
                                                        <option value="2008">2008</option>
                                                        <option value="2007">2007</option>
                                                        <option value="2006">2006</option>
                                                        <option value="2005">2005</option>
                                                        <option value="2004">2004</option>
                                                        <option value="2003">2003</option>
                                                        <option value="2002">2002</option>
                                                        <option value="2001">2001</option>
                                                        <option value="2000">2000</option>
                                                        <option value="1999">1999</option>
                                                        <option value="1998">1998</option>
                                                        <option value="1997">1997</option>
                                                        <option value="1996">1996</option>
                                                        <option value="1995">1995</option>
                                                        <option value="1994">1994</option>
                                                        <option value="1993">1993</option>
                                                        <option value="1992">1992</option>
                                                        <option value="1991">1991</option>
                                                        <option value="1990">1990</option>
                                                        <option value="1989">1989</option>
                                                        <option value="1988">1988</option>
                                                        <option value="1987">1987</option>
                                                        <option value="1986">1986</option>
                                                        <option value="1985">1985</option>
                                                        <option value="1984">1984</option>
                                                        <option value="1983">1983</option>
                                                        <option value="1982">1982</option>
                                                        <option value="1981">1981</option>
                                                        <option value="1980">1980</option>
                                                        <option value="1979">1979</option>
                                                        <option value="1978">1978</option>
                                                        <option value="1977">1977</option>
                                                        <option value="1976">1976</option>
                                                        <option value="1975">1975</option>
                                                        <option value="1974">1974</option>
                                                        <option value="1973">1973</option>
                                                        <option value="1972">1972</option>
                                                        <option value="1971">1971</option>
                                                        <option value="1970">1970</option>
                                                        <option value="1969">1969</option>
                                                        <option value="1968">1968</option>
                                                        <option value="1967">1967</option>
                                                        <option value="1966">1966</option>
                                                        <option value="1965">1965</option>
                                                        <option value="1964">1964</option>
                                                        <option value="1963">1963</option>
                                                        <option value="1962">1962</option>
                                                        <option value="1961">1961</option>
                                                        <option value="1960">1960</option>
                                                        <option value="1959">1959</option>
                                                        <option value="1958">1958</option>
                                                        <option value="1957">1957</option>
                                                        <option value="1956">1956</option>
                                                        <option value="1955">1955</option>
                                                        <option value="1954">1954</option>
                                                        <option value="1953">1953</option>
                                                        <option value="1952">1952</option>
                                                        <option value="1951">1951</option>
                                                        <option value="1950">1950</option>
                                                        <option value="1949">1949</option>
                                                        <option value="1948">1948</option>
                                                        <option value="1947">1947</option>
                                                        <option value="1946">1946</option>
                                                        <option value="1945">1945</option>
                                                        <option value="1944">1944</option>
                                                        <option value="1943">1943</option>
                                                        <option value="1942">1942</option>
                                                        <option value="1941">1941</option>
                                                        <option value="1940">1940</option>
                                                    </select>
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
                                            <asp:Label ID="lblEmail" class="form-label" runat="server" Text="">Email</asp:Label>
                                            <asp:TextBox ID="txtEmail" type="text" runat="server" placeholder="Your Email" Style="color: white"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="fieldset-footer">
                                        <span>Step 2 of 2</span>
                                    </div>
                                    <div class="actions clearfix">
                                        <asp:Button ID="btnPrevious" CssClass="submit-button2" runat="server" Text="Previous" OnClick="btnPrevious_Click" UseSubmitBehavior="false" />
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


                <%--<div class="content clearfix">
                    <!--Login information-->
                    <h3 id="signup-form-h-0" tabindex="-1" class="title current">
                        <span class="title_text">Login Infomation</span>
                    </h3>
                    
                    <!--Personal information-->
                    <h3 id="signup-form-h-1" tabindex="-1" class="title">
                        <span class="title_text">Personal Information</span>
                    </h3>
                   
                </div>--%>
            </form>
        </div>

    </div>
</body>
</html>
