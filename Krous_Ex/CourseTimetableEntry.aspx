<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="CourseTimetableEntry.aspx.cs" Inherits="Krous_Ex.CourseTimetableEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.full.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.min.css" rel="stylesheet" />

    <link href="Assests/main/vendors/select2/select2.min.css" rel="stylesheet" />
    <script src="Assests/main/vendors/select2/select2.min.js"></script>

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />

    <style>
        .select2-container .select2-selection--single {
            height: calc(2.25rem + 2px);
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered,
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: white;
            padding-left: 0;
            line-height: 1.65;
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
            padding: 0.4375rem 1rem;
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
            $('.startTime').bind('keydown', function (e) {
                if (e.which == 13)
                    e.stopImmediatePropagation();
            }).datetimepicker({
                defaultTime: '09:00',
                closeOnDateSelect: true,
                datepicker: false,
                timepicker: true,
                theme: 'dark',
                format: 'H:i',
                minTime: '9:00',
                maxTime: '21:30',
                step: 30,
                defaultDate: new Date(),
            });
            $('.endTime').bind('keydown', function (e) {
                if (e.which == 13)
                    e.stopImmediatePropagation();
            }).datetimepicker({
                defaultTime: '09:00',
                closeOnDateSelect: true,
                datepicker: false,
                timepicker: true,
                theme: 'dark',
                format: 'H:i',
                minTime: '9:00',
                maxTime: '21:30',
                step: 30,
                defaultDate: new Date(),
            });
            $('.startDate').bind('keydown', function (e) {
                if (e.which == 13)
                    e.stopImmediatePropagation();
            }).datetimepicker({
                defaultTime: '09:00',
                closeOnDateSelect: true,
                theme: 'dark',
                format: 'd/m/Y H:i',
                minTime: '9:00',
                maxTime: '21:30',
                step: 30,
                defaultDate: new Date(),
                onShow: function (ct) {
                    this.setOptions({
                        maxDate: jQuery('.endDate').val() ? jQuery('.endDate').val() : false
                    })
                },
            });
            $('.endDate').bind('keydown', function (e) {
                if (e.which == 13)
                    e.stopImmediatePropagation();
            }).datetimepicker({
                defaultTime: '09:00',
                closeOnDateSelect: true,
                theme: 'dark',
                format: 'd/m/Y H:i',
                minTime: '9:00',
                maxTime: '21:30',
                step: 30,
                defaultDate: new Date(), onShow: function (ct) {
                    this.setOptions({
                        minDate: jQuery('.startDate').val() ? jQuery('.startDate').val() : false,
                        maxDate: jQuery('.startDate').val() ? jQuery('.startDate').val() : false
                    })
                },
            });
            $('.ddl-course').select2({
                placeholder: "Select a course"
            });
            $('.ddl-staff').select2({
                placeholder: "Select a staff"
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
                            <asp:Label ID="lblFAQEntry" runat="server">Course Timetable Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert Course Schedule Details </p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                        <p class="card-description"><strong>Step 1:</strong> Select Programme, Session, Semester & Group Number </p>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblProgrammeCategory" runat="server">Programme Category </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlProgrammeCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProgrammeCategory_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblProgramme" runat="server">Programme </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSession" runat="server">Session </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSemester" runat="server">Semester </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblGroupNo" runat="server">Group No </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <p class="card-description"><strong>Step 2:</strong> Select Current Available Course </p>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblCourse" runat="server">Course </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control ddl-course"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblClassType" runat="server">Class Type </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <asp:RadioButtonList ID="radClassType" runat="server" CssClass="rdBtn" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radClassType_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="Main">Main</asp:ListItem>
                                                        <asp:ListItem Value="Replacement">Replacement</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblClassCategory" runat="server">Class Category </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <asp:RadioButtonList ID="radClassCategory" runat="server" CssClass="rdBtn" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radClassType_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="Lecture">Lecture</asp:ListItem>
                                                        <asp:ListItem Value="Tutorial">Tutorial</asp:ListItem>
                                                        <asp:ListItem Value="Practical">Practical</asp:ListItem>
                                                        <asp:ListItem Value="Blended">Blended (T/P)</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblStaffInCharge" runat="server">Staff-in-charge </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control ddl-staff"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="panelMain" runat="server">
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="lblWeekday" runat="server">Weekday </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlWeekDay" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="Monday">Monday</asp:ListItem>
                                                    <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                                    <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                                    <asp:ListItem Value="Thursday">Thursday</asp:ListItem>
                                                    <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                                    <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="lblClassTime" runat="server">Class Time</asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <span class="input-group-addon input-group-append border-left">
                                                            <asp:TextBox ID="txtClassStartTime" runat="server" CssClass="form-control startTime" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <span class="mdi mdi-calendar input-group-text"></span>
                                                        </span>
                                                    </div>
                                                    <div class="input-group-addon col-form-label mx-4">to</div>
                                                    <div class="col-md-4">
                                                        <span class="input-group-addon input-group-append border-left">
                                                            <asp:TextBox ID="txtClassEndTime" runat="server" CssClass="form-control endTime" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <span class="mdi mdi-calendar input-group-text"></span>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="panelReplacement" runat="server" Visible="false">
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label1" runat="server">Class Time</asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <span class="input-group-addon input-group-append border-left">
                                                            <asp:TextBox ID="txtReplacementClassStartTime" runat="server" CssClass="form-control startDate" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="txtReplacementClassStartTime_TextChanged"></asp:TextBox>
                                                            <span class="mdi mdi-calendar input-group-text"></span>
                                                        </span>
                                                    </div>
                                                    <div class="input-group-addon col-form-label mx-4">to</div>
                                                    <div class="col-md-4">
                                                        <span class="input-group-addon input-group-append border-left">
                                                            <asp:TextBox ID="txtReplacementClassEndTime" runat="server" CssClass="form-control endDate" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="txtReplacementClassEndTime_TextChanged"></asp:TextBox>
                                                            <span class="mdi mdi-calendar input-group-text"></span>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
                                            <%--<asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" OnClick="btnCancel_Click" />
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
