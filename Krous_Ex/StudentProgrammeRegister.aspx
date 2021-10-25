<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentProgrammeRegister.aspx.cs" Inherits="Krous_Ex.StudentCourseRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblCourseEntry" runat="server">Programme Registration</asp:Label>
                        </h3>
                        <p class="card-description"><strong><span style="color: red;">*Take note:</span> The programme you selected and registered will be permanent</strong></p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12 mt-3">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <p class="card-description"><strong>Step 1:</strong> Select the Programme</p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label3" runat="server">Programme Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <!--foundation / diploma-->
                                    <asp:DropDownList ID="ddlProgrammCategory" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgrammeName" runat="server">Programme Name</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <!--Bachelor of blabla-->
                                    <asp:DropDownList ID="ddlProgrammeName" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12 mt-3">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <p class="card-description"><strong>Step 2:</strong> Upload supporting documents</p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                         
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%--  <div class="col-lg-12 grid-margin stretch-card">
        <div class="card">
            <div class="card-body">
                <p class="card-description">Please fill in all the details for each tabs below.</p>
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" id="home-tab" data-toggle="tab" href="#studDetails" role="tab" aria-controls="home" aria-selected="true">Personal Details</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="profile-tab" data-toggle="tab" href="#addGuardian" role="tab" aria-controls="profile" aria-selected="false">Profile</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="contact-tab" data-toggle="tab" href="#displayMsg" role="tab" aria-controls="contact" aria-selected="false">Contact</a>
                    </li>
                </ul>

                <div class="tab-content">
                    <!--1st tab-->
                    <div class="tab-pane fade show active" id="studDetails" role="tabpanel" aria-labelledby="home-tab">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblStudName" runat="server">Full Name</asp:Label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label1" runat="server">Gender</asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtGender" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label2" runat="server">Date of Birth</asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtDOB " runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label3" runat="server">IC Number</asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtNRIC" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label4" runat="server">Home Address</asp:Label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCourseDesc" CssClass="form-control" Style="resize: none" TextMode="multiline" Columns="40" Rows="6" runat="server" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!--2nd tab-->
                    <div class="tab-pane fade" id="addGuardian" role="tabpanel" aria-labelledby="profile-tab">
                        <div class="media d-block d-sm-flex">
                        </div>
                    </div>
                    <!--3rd tab-->
                    <div class="tab-pane fade" id="displayMsg" role="tabpanel" aria-labelledby="contact-tab">
                        <h4>Contact us </h4>
                        <p>Feel free to contact us if you have any questions! </p>
                        <p>
                            <i class="mdi mdi-phone text-info"></i>+123456789
                        </p>
                        <p>
                            <i class="mdi mdi-email-outline text-success"></i>contactus@example.com
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

<%--    <script src="Assests/main/js/tabs.js"></script>
    <script src="Assests/main/js/dropify.js"></script>
    <script src="Assests/vendors/dropify/dropify.min.js"></script>--%>


</asp:Content>
