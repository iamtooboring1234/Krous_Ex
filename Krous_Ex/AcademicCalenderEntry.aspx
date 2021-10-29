<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true"  MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="AcademicCalenderEntry.aspx.cs" Inherits="Krous_Ex.AcademicCalenderEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/general.css" rel="stylesheet" />
    <link href="Assests/main/css/bootstrap-datepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="Assests/main/js/bootstrap-datepicker.min.js"></script>

        <style>
        .form-check table.form-check-input tbody tr td label  {
            padding: 0 0.625rem;
            font-size: 0.875rem;
            line-height: 1.75;
            color: #6c7293;
            font-weight: bold;
        }

        .form-check table.form-check-input tbody tr td {
            padding-left: 50px;
        }

        .form-check table.form-check-input tbody tr td input {
            width: 1em;
            height: 1em;
            margin-top: 0.25em;
            vertical-align: top;
            background-color: #fff;
            background-repeat: no-repeat;
            background-position: center;
            background-size: contain;
            border: 1px solid rgba(0, 0, 0, 0.25);
        }

        .form-check-checkbox table tbody tr td label, .form-check-all label {
            padding-left: 0.625rem;
            font-size: 0.875rem;
            line-height: 1.5;
            color: #6c7293;
        }
    </style>

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
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label4" runat="server">Who to notify?</asp:Label>
                                        </div>
                                        <div class="col-md-8 justify-content-center d-flex form-check p-0 m-0">
                                            <asp:RadioButtonList ID="radSemesterDuration" runat="server" CssClass="form-check-input mt-0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radSemesterDuration_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="1">Long Semester</asp:ListItem>
                                                <asp:ListItem Value="2">Short Semester</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label3" runat="server">Semester Name</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtSemesterName" runat="server" CssClass="form-control" placeholder="June 2020 Intake"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblType" runat="server">Type of programmes</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlCalenderType" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="DipUnderPost">Diploma, Undergraduate & Postgraduate</asp:ListItem>
                                                <asp:ListItem Value="Foundation">Foundation</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label2" runat="server">Session </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                               <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSemesterDate" runat="server">Semester Date</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                            <div class="col-md-4">
                                                <span class="input-group-addon input-group-append border-left" >
                                                    <asp:TextBox ID="txtSemesterStartDate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSemesterStartDate_TextChanged"></asp:TextBox>  
                                                    <span class="mdi mdi-calendar input-group-text"></span>
                                                </span>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtSemesterStartDate" EnableViewState="false" Format="dd/MM/yyyy" > </ajaxToolkit:CalendarExtender>  
                                            </div>
                                            <div class="input-group-addon col-form-label mx-4">to</div>
                                            <div class="col-md-4">
                                                <span class="input-group-addon input-group-append border-left">
                                                    <asp:TextBox ID="txtSemesterEndDate" runat="server" CssClass="form-control" OnTextChanged="txtSemesterEndDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <span class="mdi mdi-calendar input-group-text"></span>
                                                </span>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtSemesterEndDate" EnableViewState="false" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>  
                                            </div>
                                                </div>
                                        </div>
                                   </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label8" runat="server">Semester Duration</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtSemesterDay" runat="server" AutoPostBack="true" OnTextChanged="txtSemesterDay_TextChanged" CssClass="form-control" Text="139"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="Label1" runat="server">Day(s) </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label5" runat="server">Semester Study Duration</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtSemesterStudyDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="Label9" runat="server">Day(s) </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label6" runat="server">Semester Examination Duration</asp:Label><span style="color: red;">*</span>
                                        </div>
                                          <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtSemesterExamDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="Label10" runat="server">Day(s) </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label7" runat="server">Semester Break Duration</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtSemesterBreakDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="Label11" runat="server">Day(s) </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <hr /> 
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
<%--                                        <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" OnClick="btnCancel_Click" />
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
                </asp:UpdatePanel>
            </div>
        </div>
    </div>    

    <script src="Assests/main/js/formpickers.js"></script>
    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
