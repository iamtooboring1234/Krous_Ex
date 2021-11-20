<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExaminationInvigilatorsEntry.aspx.cs" Inherits="Krous_Ex.ExaminationInvigilatorsEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/general.css" rel="stylesheet" />

    <link href="Assests/main/vendors/select2/select2.min.css" rel="stylesheet" />
    <script src="Assests/main/vendors/select2/select2.min.js"></script>

    <script>
        function pageLoad() {
            bind();
        };

        function bind() {
            $('.ddlSelect2Staff').select2({
                placeholder: "Select an option"
            });
            $('.ddlSelect2Course').select2({
                placeholder: "Select an option"
            });
            $('.ddlSelect2Staff').on('change', function () {
                $('#<%=hdSelectedStaff.ClientID%>').val($(this).val());
            });
        };
    </script>

    <style>
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

        .select2-container--default .select2-selection--single, .select2-container--default .select2-selection--multiple {
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

        .select2-container--default .select2-selection--multiple .select2-selection__choice {
            font-size: inherit;
            line-height: inherit;
        }

        .select2-container--default .select2-selection--multiple .select2-selection__choice {
            border: 1px solid transparent;
        }

        .body_blSelectedStaff ul {
            margin: 0;
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
                            <asp:Label ID="lblExaminationEntry" runat="server">Examination Invigilators Entry</asp:Label>
                        </h3>
                        <p class="card-description">To entry invigilators for particular examination </p>
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
                                            <asp:Label ID="lblCourseExam" runat="server">Available Exam </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlCourseExam" runat="server" CssClass="form-control ddlSelect2Course" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:HiddenField ID="hdCourseExam" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSemesterDate" runat="server">Exam Date</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <span class="input-group-addon input-group-append border-left">
                                                        <asp:TextBox ID="txtExamStartDateTime" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <span class="mdi mdi-calendar input-group-text"></span>
                                                    </span>
                                                </div>
                                                <div class="input-group-addon col-form-label mx-4">to</div>
                                                <div class="col-md-4">
                                                    <span class="input-group-addon input-group-append border-left">
                                                        <asp:TextBox ID="txtExamEndDateTime" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <span class="mdi mdi-calendar input-group-text"></span>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="panelSelectedStaff" runat="server" Visible="false">
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="lblSelectedStaff" runat="server">Selected Staff </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:BulletedList ID="blSelectedStaff" runat="server"></asp:BulletedList>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblInvigilators" runat="server">Available Staff </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control ddlSelect2Staff" multiple="multiple"></asp:DropDownList>
                                            <asp:HiddenField ID="hdSelectedStaff" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this FAQ ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this FAQ ?" TargetControlID="btnDelete" />
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn" OnClick="btnBack_Click" />
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

    <script src="Assests/main/js/toastDemo.js"></script>


</asp:Content>
