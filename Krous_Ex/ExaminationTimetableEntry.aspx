<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExaminationTimetableEntry.aspx.cs" Inherits="Krous_Ex.ExaminationTimetableEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/general.css" rel="stylesheet" />

    <script src="Assests/main/vendors/JQuery.timepicker/jquery.timepicker.min.js"></script>
    <link href="Assests/main/vendors/JQuery.timepicker/jquery.timepicker.min.css" rel="stylesheet" />

    <link href="Assests/main/vendors/select2/select2.min.css" rel="stylesheet" />
    <script src="Assests/main/vendors/select2/select2.min.js"></script>


    <style>
        .ui-timepicker-container .ui-timepicker-viewport {
            background: #2A3038;
        }

        .ui-timepicker-viewport li.ui-menu-item a {
            color: white;
        }

        .ui-timepicker-viewport li.ui-menu-item a:hover, #ui-active-item {
            color: black;
        }

        .ui-timepicker-viewport::-webkit-scrollbar {
            display: none;
        }

        .select2-container .select2-selection--single {
            height: calc(2.25rem + 2px);
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered,
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: white;
            padding-left: 0;
            line-height: 1;
        }

        .select2-container {
            flex: 0 0 100%;
            max-width: 100%;
            margin: 0;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow {
            top: 5px;
        }

        .select2-container--default .select2-selection--single {
            background-color: #2A3038;
            border: 1px solid #2c2e33;
        }

        .select2-container--default .select2-results__option--selected {
            background-color: #ddd;
            color: black;
        }

        .select2-container--default.select2-container--disabled .select2-selection--single {
            background-color: black;
            cursor: default;
        }

        .select2-container--default .select2-results__option[aria-selected=true] {
            background-color: darkgrey;
        }
    </style>


    <script>
        function pageLoad() {
            bind();
        };

        function bind() {
            $('.timepickerstart').timepicker({
                timeFormat: 'h:mm p',
                interval: 30,
                minTime: '9',
                maxTime: '6:00pm',

                dynamic: false,
                dropdown: true,
                scrollbar: true
            });
            $('.timepickerend').timepicker({
                timeFormat: 'h:mm p',
                interval: 30,
                minTime: '10',
                maxTime: '6:00pm',

                dynamic: false,
                dropdown: true,
                scrollbar: true
            });

            $('.ddl-course').select2({
                placeholder: "Select an option"
            });
        };

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblExaminationEntry" runat="server">Examination Timetable Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert new examination time </p>
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
                                            <asp:Label ID="lblSession" runat="server">Current Session </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtSession" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdSession" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblCourse" runat="server">Available Course </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control ddl-course"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblExamDate" runat="server">Examination Date</asp:Label><span style="color: red;"> *</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <span class="input-group-addon input-group-append border-left">
                                                        <asp:TextBox ID="txtExamDate" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <span class="mdi mdi-calendar input-group-text"></span>
                                                    </span>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtExamDate" EnableViewState="false" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                                    <asp:HiddenField ID="hdStartDate" runat="server" />
                                                    <asp:HiddenField ID="hdEndDate" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblExamTime" runat="server">Examination Time </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtStartTime" CssClass="timepickerstart form-control" runat="server" AutoCompleteType="Disabled" onkeydown="return false;"></asp:TextBox>
                                                </div>
                                                <div class="input-group-addon col-form-label mx-4">to</div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtEndTime" CssClass="timepickerend form-control" runat="server" AutoCompleteType="Disabled" onkeydown="return false;"></asp:TextBox>
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
                                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-success pdForm" OnClick="btnUpdate_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this FAQ ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn btn-danger pdForm" OnClick="btnDelete_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this FAQ ?" TargetControlID="btnDelete" />
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
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

    <asp:HiddenField ID="Host" runat="server" />
    <asp:HiddenField ID="Join" runat="server" />
    <asp:HiddenField ID="Code" runat="server" />

    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
