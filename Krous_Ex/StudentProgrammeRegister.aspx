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
                        <p class="card-description"><strong><span style="color: red;"><strong>*Take note:</strong></span> The programme you selected and registered will be <strong style="color: red">PERMANENT </strong>once approved by the staff.</strong></p>

                        <hr />
                        <ul>
                            <li>Students are required to upload and submit the relevent documents</li>
                            <ul>
                                <li>Copy of MyKad (Front & Back)</li>
                                <li>Certified True Copy of SPM results (compulsory for all applicants with SPM results)</li>
                                <li>Certified True Copy of O Level/equivalent results (if applicable)</li>
                                <li>Certified True Copy of STPM/ A Level/ UEC/ equivalent results (if applicable)</li>
                                <li>Medical Report/letter (for applicants with physical disability/illness)</li>
                            </ul>
                        </ul>
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
                        <p class="card-description"><strong>Step 1:</strong> Select Programme</p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label6" runat="server">Programme Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgrammCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProgrammCategory_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label7" runat="server">Programme Name</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged"></asp:DropDownList>
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
                        <p class="card-description"><strong>Step 2:</strong> Select the session joined available</p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-md-2 col-form-label">
                                <asp:Label ID="lblSession" runat="server">Programme Session</asp:Label><span style="color: red;">*</span>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"></asp:DropDownList>
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
                        <p class="card-description"><strong>Step 3:</strong> Select the branch you want to join</p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-md-2 col-form-label">
                                <asp:Label ID="lblBranch" runat="server">Branch</asp:Label><span style="color: red;">*</span>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
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
                        <p class="card-description"><strong>Step 4:</strong> Upload supporting documents</p>
                        <p class="card-description"><strong>Please rename the file as (exp: YourFullName_MyKad)</strong></p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblUploadIC" runat="server">Upload your IC image</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:UpdatePanel ID="UpdatePanel1s" runat="server">
                                        <ContentTemplate>
                                            <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Mode="Auto" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="Label2" runat="server">Upload your result slip</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload2" Mode="Auto" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="Label1" runat="server">Upload medical report (optional)</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload3" Mode="Auto" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12 float-right text-right">
                                    <asp:Button Text="Register" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to register this programme?" TargetControlID="btnSave" />
                                    <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/tabs.js"></script>
    <%--    <script src="Assests/main/js/dropify.js"></script>
    <script src="Assests/vendors/dropify/dropify.min.js"></script>--%>
</asp:Content>
