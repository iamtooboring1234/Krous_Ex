<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="Krous_Ex.StudentProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/style.css" rel="stylesheet" />
    <link href="Assests/main/css/Profile.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <script>
        function openCity(evt, cityName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }

        // Get the element with id="defaultOpen" and click on it
        document.getElementById("defaultOpen").click();
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Your Profile</h4>

            <!--tab layout-->
            <%--<div class="tab">
                <button class="tablinks" onclick="openCity(event, 'Personal')" id="defaultOpen">Personal Info</button>
                <button class="tablinks" onclick="openCity(event, 'University')">University Info</button>
                <button class="tablinks" onclick="openCity(event, 'Account')">Account Settings</button>
            </div>--%>

            <!--personal details tab-->
            <%--   <div id="Personal" class="tabcontent">--%>
            <p class="card-description" style="margin-top: 30px; font-size: 17px;">Personal Particulars</p>
            <div style="float: right;">
                <asp:Label ID="updateDate" runat="server" Style="font-size: 15px;" Text="Last Updated : "></asp:Label>
                <asp:Label ID="lblUpdateTime" runat="server"></asp:Label>
            </div>
            <br />
            <asp:Button ID="btnUpdate" runat="server" Text="Update Profile" CssClass="btn btn-primary me-2" style="width: 120px; float: right;" OnClick="btnUpdate_Click" />
            <!--image-->
            <div class="form-group" style="margin-left: 450px;">
                <div class="col-sm-5">
                    <div id="hiddenImg">
                        <asp:Image ID="imgProfile" runat="server" Width="220" Height="210" class="img-thumbnail img-circle" ImageUrl="~/Assests/main/img/defaultUserProfile.png"></asp:Image>
                    </div>
                    <asp:FileUpload ID="imageUpload" runat="server" onchange="loadFile(event)" Enabled="true" />
                </div>
            </div>

            <div class="form-group" style="margin-top: 25px;">
                <label for="fullname">Full Name</label>
                <asp:TextBox ID="txtFullname" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="nric">IC Number</label>
                <asp:TextBox ID="txtNRIC" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="gender">Gender</label>
                <asp:TextBox ID="txtGender" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Enabled="False" Visible="false">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="form-group">
                <label for="email">Email Address</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="contact">Contact Number</label>
                <asp:TextBox ID="txtContact" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="contact">Date of Birth</label>
                <asp:TextBox ID="txtDOB" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="contact">Joined at</label>
                <asp:TextBox ID="txtDateJoined" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>
            <%--</div>--%>


            <!-- uni details tab-->
            <%-- <div id="University" class="tabcontent">--%>
            <p class="card-description" style="font-size: 17px;">Other Particulars</p>
            <%--          <div class="form-group">
                <label for="email" style="margin-top: 9px;">Programme</label>
                <asp:TextBox ID="txtProgramme" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>--%>

            <div class="form-group">
                <label for="contact">Faculty</label>
                <asp:TextBox ID="txtFaculty" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Campus branch</label>
                <asp:TextBox ID="txtBranch" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Year Intake</label>
                <asp:TextBox ID="txtYearIntake" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray; width: 130px;"></asp:TextBox>
            </div>
            <%--</div>--%>


            <!-- account details tab-->
            <%--  <div id="Account" class="tabcontent">--%>
            <p class="card-description" style="font-size: 17px;"></p>
            <div class="form-group">
                <label for="contact">Your current password</label>
                <asp:TextBox ID="txtCurrentPass" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Your new password</label>
                <asp:TextBox ID="txtNewPass" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Confirm new password</label>
                <asp:TextBox ID="txtConfNewPass" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>
            <%-- </div>--%>



            <asp:Button ID="btnSave" runat="server" Text="Save Profile" CssClass="btn btn-primary me-2" Style="padding: 10px; width: 120px; margin-top: 10px; margin-right: 13px;" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-dark" Style="padding: 10px; width: 100px; margin-top: 10px;" />

        </div>
    </div>

</asp:Content>
