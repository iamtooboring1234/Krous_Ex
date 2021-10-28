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
                        <%--<p class="card-description">Form allow students to register programme</p>--%>
                        <p class="card-description"><strong><span style="color: red;"><strong>*Take note:</strong></span> The programme you selected and registered will be <strong style="color: red">PERMANENT </strong>once approved by the staff.</strong></p>

                        <hr />
                        <ul>
                            <li>Students are only allowed to choose one programme available to register.</li>
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
                        <p class="card-description"><strong>Step 1:</strong> Select the programme available</p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-12 col-sm-3">
                                <ul class="nav nav-tabs nav-tabs-vertical" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" id="foundation-tab" data-toggle="tab" href="#foundation" role="tab" aria-controls="home" aria-selected="true">Foundation</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="diploma-tab" data-toggle="tab" href="#diploma" role="tab" aria-controls="profile" aria-selected="false">Diploma</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="degree-tab" data-toggle="tab" href="#degree" role="tab" aria-controls="profile" aria-selected="false">Bachelor Degree</a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-12 col-sm-8">
                                <div class="tab-content tab-content-vertical">

                                    <div class="tab-pane fade show active" id="foundation" role="tabpanel" aria-labelledby="foundation-tab">
                                        <p class="card-description">The following list the available for <strong>Foundation Programme </strong>(Choose one only)</p>
                                        <asp:RadioButtonList ID="rblFoundation" runat="server" CssClass="rdBtn" OnSelectedIndexChanged="rblFoundation_SelectedIndexChanged1">
                                        </asp:RadioButtonList>

                                    </div>
                                    <div class="tab-pane fade" id="diploma" role="tabpanel" aria-labelledby="diploma-tab">
                                        <p class="card-description">The following list the available for <strong>Diploma Programme </strong>(Choose one only)</p>
                                        <asp:RadioButtonList ID="rblDiploma" runat="server" CssClass="rdBtn" OnSelectedIndexChanged="rblDiploma_SelectedIndexChanged1">
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="tab-pane fade" id="degree" role="tabpanel" aria-labelledby="degree-tab">
                                        <p class="card-description">The following list the available for <strong>Bachelor Programme </strong>(Choose one only)</p>
                                        <asp:RadioButtonList ID="rblDegree" runat="server" CssClass="rdBtn" OnSelectedIndexChanged="rblDegree_SelectedIndexChanged1">
                                        </asp:RadioButtonList>
                                    </div>
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
                        <p class="card-description"><strong>Step 1:</strong> Select the session joined available</p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblSession" runat="server">Select programme session</asp:Label><span style="color: red;">*</span>
                                </div>
                            <div class="col-md-9">
                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control"></asp:DropDownList>
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
                        <p class="card-description"><strong>Step 3:</strong> Upload supporting documents</p>
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
                                    <asp:FileUpload ID="UploadNRIC" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="Label2" runat="server">Upload your result slip</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:FileUpload ID="UploadResultSlip" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="Label1" runat="server">Upload medical report (optional)</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:FileUpload ID="UploadMedical" runat="server" />
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12 float-right text-right">
                                    <%--<asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" />--%>
                                    <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" Style="margin-left: 10px; height: 38px;" />
                                    <%-- <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn mr20 pdForm" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this Course ?" TargetControlID="btnUpdate" />
                                    <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn mr20 pdForm" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this Course ?" TargetControlID="btnDelete" />--%>
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
