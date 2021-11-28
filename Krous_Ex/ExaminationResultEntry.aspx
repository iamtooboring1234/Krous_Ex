<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ExaminationResultEntry.aspx.cs" Inherits="Krous_Ex.ExaminationResultEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <link href="Assests/main/vendors/select2/select2.min.css" rel="stylesheet" />
    <script src="Assests/main/vendors/select2/select2.min.js"></script>

    <style>
        .form-check table.form-check-input tbody tr td label {
            padding: 0 0.625rem;
            font-size: 0.875rem;
            line-height: 1.75;
            color: #6c7293;
            font-weight: bold;
        }

        .form-check table.form-check-input tbody tr td {
            border: 0;
            padding-left: 50px;
        }

        .form-check table.form-check-input {
            margin-left: 0 !important;
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
            $('.ddl-student').select2({
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
                            <asp:Label ID="lblFAQList" runat="server" Font-Size="large">Examination Result Entry</asp:Label>
                        </h3>
                        <p class="card-description"></p>
                    </div>
                </div>

                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label3" runat="server">Session Category</asp:Label>
                                </div>
                                <div class="col-md-8 form-check p-0 m-0">
                                    <div class="row justify-content-center">
                                        <div class="col-md-8">
                                            <asp:RadioButtonList ID="radSession" runat="server" CssClass="form-check-input mt-0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radSession_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="1">Current Session</asp:ListItem>
                                                <asp:ListItem Value="2">Existing Session</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />

                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="panelCurrent" runat="server" Visible="true">
                            <div class="form-group pdForm">
                                <div class="row justify-content-center">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblSession" runat="server">Current Session </asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtSession" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="hdSession" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="panelExisting" runat="server" Visible="false">
                            <div class="form-group pdForm">
                                <div class="row justify-content-center">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblExistingSession" runat="server">Session </asp:Label><span style="color: red">*</span>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlExistingSession" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExistingSession_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label1" runat="server">Programme Category</asp:Label><span style="color: red">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgrammeCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProgrammeCategory_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgramme" runat="server">Programme </asp:Label><span style="color: red">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="panelSemester" runat="server" Visible="true">
                            <div class="form-group pdForm">
                                <div class="row justify-content-center">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblSemester" runat="server">Semester </asp:Label><span style="color: red">*</span>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblGroup" runat="server">Group </asp:Label><span style="color: red">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button Text="Search" ID="btnSearch" runat="server" Width="20%" CssClass="btn btn-primary p-2" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>


                        <hr />

                        <asp:Literal ID="litInfo" runat="server">Complete the above required information to search student.</asp:Literal>

                        <asp:Panel ID="panelCourseMark" runat="server" Visible="false">
                            <div class="form-group pdForm">
                                <div class="row justify-content-center">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblStudent" runat="server">Student Name </asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlStudent" runat="server" CssClass="form-control ddl-student" OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <hr />

                            <div class="row justify-content-center">
                                <div class="col-lg-10 stretch-card ">
                                    <div class="card">
                                        <div class="table-responsive">
                                            <div class="gv-section text-center">
                                                <asp:GridView ID="gvMark" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="CourseGUID" CellPadding="10" CellSpacing="2" Border="0" OnRowDataBound="gvMark_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="CourseGUID" HeaderText="CourseGUID" ReadOnly="true" SortExpression="CourseGUID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
                                                        <asp:BoundField DataField="CourseAbbrv" HeaderText="Course Abbreviation" SortExpression="CourseAbbrv" />
                                                        <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
                                                        <asp:TemplateField HeaderText="Mark" SortExpression="Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMark" TextMode="Number" runat="server" onkeydown="return (event.keyCode!=13);" onKeyPress="if(this.value.length==3) return false;"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="" HorizontalAlign="Center" />
                                                </asp:GridView>
                                                <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No FAQ Record Found !"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />

                            <div class="form-group pdForm">
                                <div class="row">
                                    <div class="col-md-12 float-right text-right">
                                        <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary p-2" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="Assests/main/js/toastDemo.js"></script>
</asp:Content>
