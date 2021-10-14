<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="SemesterEntry.aspx.cs" Inherits="Krous_Ex.SemesterEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/general.css" rel="stylesheet" />
    <link href="Assests/main/css/bootstrap-datepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="Assests/main/js/bootstrap-datepicker.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFAQEntry" runat="server">Semester Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert Semester Details </p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSemesterName" runat="server">Semester Name</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtSemesterName" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblFAQCategory" runat="server">Category</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton runat="server" ID="rdExisting" Text="Use existing category" CssClass="rdBtn" Checked="true" GroupName="Category" AutoPostBack="true" OnCheckedChanged="rdExisting_CheckedChanged"/>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="ddlSemesterCategory" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton runat="server" ID="rdNew" Text="Add new category" CssClass="rdBtn" GroupName="Category" AutoPostBack="true" OnCheckedChanged="rdNew_CheckedChanged" />
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtNewCategory" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                               <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSemesterDate" runat="server">Semester Date</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="col-md-4 float-left" style="padding-left:0">
                                                <span class="input-group-addon input-group-append border-left" >
                                                    <asp:TextBox ID="txtSemesterStartDate" runat="server" CssClass="form-control" OnTextChanged="txtSemesterStartDate_TextChanged" AutoPostBack="true"></asp:TextBox>  
                                                    <span class="mdi mdi-calendar input-group-text"></span>
                                                </span>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtSemesterStartDate" Format="dd/MM/yyyy" > </ajaxToolkit:CalendarExtender>  
                                            </div>
                                            <div class="input-group-addon float-left col-form-label mx-4">to</div>
                                            <div class="col-md-4 float-left">
                                                <span class="input-group-addon input-group-append border-left" >
                                                    <asp:TextBox ID="txtSemesterEndDate" runat="server" CssClass="form-control" OnTextChanged="txtSemesterEndDate_TextChanged"></asp:TextBox>
                                                    <span class="mdi mdi-calendar input-group-text"></span>
                                                </span>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtSemesterEndDate" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>  
                                            </div>
                                        </div>
                                   </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSemesterDuration" runat="server">Semester Duration</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtSemesterDuration" CssClass="form-control" type="number" min="0" max="10" step="1" Text="1" OnTextChanged="txtSemesterDuration_TextChanged" AutoPostBack="True"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                         <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblWeek" runat="server">Week</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblStudyDuration" runat="server">Study Week Duration</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtStudyDuration" CssClass="form-control" type="number" min="0" max="10" step="1" Text="1" AutoPostBack="True" OnTextChanged="txtStudyDuration_TextChanged"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                         <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label2" runat="server">Day(s)</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblExamDuration" runat="server">Examination Duration</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtExamDuration" CssClass="form-control" type="number" min="0" max="10" step="1" Text="1" AutoPostBack="True"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                         <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label4" runat="server">Day(s)</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSemesterBreakDuration" runat="server">Semester Break Duration</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtSemesterBreakDuration" CssClass="form-control" type="number" min="0" max="10" step="1" Text="1" AutoPostBack="True"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                         <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label6" runat="server">Day(s)</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
<%--                                            <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" OnClick="btnCancel_Click" />
                                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnUpdate_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this FAQ ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnDelete_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this FAQ ?" TargetControlID="btnDelete" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdExisting" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="rdNew" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    </Triggers>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>    

    <script src="Assests/main/js/formpickers.js"></script>


</asp:Content>
