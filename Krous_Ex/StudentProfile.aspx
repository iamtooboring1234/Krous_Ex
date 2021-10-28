<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="Krous_Ex.StudentProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/style.css" rel="stylesheet" />
    <link href="Assests/main/css/Profile.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">
                <asp:Image ID="teacherImage" runat="server" ImageUrl="~/Assests/main/img/student.png" Height="40px" Width="45px" />My Profile</h4>

            <!--personal details-->
            <div style="float: right;">
                <asp:Label ID="updateDate" runat="server" Style="font-size: 15px;" Text="Last Updated"></asp:Label>
                <br />
                <asp:Label ID="lblUpdateTime" runat="server" Text="No update before"></asp:Label>
            </div>

            <!--image-->
            <div class="form-group" style="margin-left: 450px;">
                <div class="col-sm-5">
                    <div id="hiddenImg">
                        <asp:Image ID="imgProfile" runat="server" Width="200" Height="200" class="img-thumbnail img-circle" ImageUrl="~/Uploads/ProfileImage/defaultUserProfile.png"></asp:Image>
                    </div>
                    <asp:FileUpload ID="imageUpload" runat="server" onchange="loadFile(event)" Enabled="true" />
                </div>
            </div>

            <p class="card-description" style="margin-top: 30px; font-size: 17px;">Personal Particulars</p>
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
            </div>
            <div class="form-group">
                <label for="email">Email Address</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" type="text" runat="server" Enabled="true" Style="border-color: gray;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="contact">Contact Number</label>
                <asp:TextBox ID="txtContact" CssClass="form-control" type="text" runat="server" Enabled="true" Style="border-color: gray;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="contact">Date of Birth</label>
                <asp:TextBox ID="txtDOB" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="address">Home Address</label>
                <asp:TextBox ID="txtAddress" CssClass="form-control" type="text" runat="server" Enabled="true" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Joined at</label>
                <asp:TextBox ID="txtDateJoined" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <hr style="height: 2px; border-width: 0; color: #f4d47c; background-color: #f4d47c; margin-top: 23px;">

            <!-- uni details-->
            <p class="card-description" style="font-size: 17px;">Other Particulars</p>

            <div class="form-group">
                <label for="contact">Faculty</label>
                <asp:TextBox ID="txtFaculty" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Campus branch</label>
                <asp:TextBox ID="txtBranch" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="email" style="margin-top: 9px;">Programme</label>
                <asp:TextBox ID="txtProgramme" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Programme Session</label>
                <asp:TextBox ID="txtProgSession" CssClass="form-control" type="text" runat="server" Enabled="False" Style="border-color: gray; width: 130px;"></asp:TextBox>
            </div>

            <hr style="height: 2px; border-width: 0; color: #64f9db; background-color: #64f9db; margin-top: 23px;">

            <!-- account details-->
            <p class="card-description" style="font-size: 17px;">Account Settings</p>
            <div class="form-group">
                <label for="contact">Your current password</label>
                <asp:TextBox ID="txtCurrentPass" CssClass="form-control" type="password" runat="server" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Your new password</label>
                <asp:TextBox ID="txtNewPass" CssClass="form-control" type="password" runat="server" Style="border-color: gray;"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="contact">Confirm new password</label>
                <asp:TextBox ID="txtConfNewPass" CssClass="form-control" type="password" runat="server" Style="border-color: gray;"></asp:TextBox>
            </div>
            <asp:Button ID="btnChangePass" runat="server" Text="Change Password" CssClass="btn btn-primary me-2" Style="padding: 10px; width: 150px; float: right;" OnClick="btnChangePass_Click" />
            <br />

            <asp:Button ID="btnSave" runat="server" Text="Save Profile" CssClass="btn btn-primary me-2" Style="padding: 10px; width: 120px; margin-top: 20px; margin-right: 13px;" OnClick="btnSave_Click" />
            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to save your updated details ?" TargetControlID="btnSave" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-dark" Style="padding: 10px; width: 100px; margin-top: 20px;" OnClick="btnCancel_Click" />

        </div>
    </div>

</asp:Content>
