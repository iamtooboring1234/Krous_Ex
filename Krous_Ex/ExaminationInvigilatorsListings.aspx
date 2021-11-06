<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExaminationInvigilatorsListings.aspx.cs" Inherits="Krous_Ex.ExaminationInvigilatorsListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.full.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.min.css" rel="stylesheet" />

    <link href="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.js"></script>

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <script>
        function pageLoad() {
            bind();
        };

        function bind() {
            $('.startDateTime').bind('keydown', function (e) {
                if (e.which == 13)
                    e.stopImmediatePropagation();
            }).datetimepicker({
                defaultTime: '09:00',
                closeOnDateSelect: true,
                timepicker: true,
                theme: 'dark',
                format: 'd/m/Y h:i',
                defaultDate: new Date(),
            });
            $('.endDateTime').bind('keydown', function (e) {
                if (e.which == 13)
                    e.stopImmediatePropagation();
            }).datetimepicker({
                defaultTime: '09:00',
                closeOnDateSelect: true,
                timepicker: true,
                theme: 'dark',
                format: 'd/m/Y h:i',
                defaultDate: new Date(),
                onShow: function (ct) {
                    this.setOptions({
                        minDate: jQuery('.startDateTime').val() ? jQuery('.startDateTime').val() : false
                    })
                },
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
                            <asp:Label ID="lblFAQList" runat="server" Font-Size="large">Examination Invigilators Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Invigilators In-Charge </p>
                    </div>
                </div>
                <hr />
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
                                    <asp:Label ID="lblStaffName" runat="server">Staff Name </asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtStaffName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblCourse" runat="server">Course </asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblExamStartEndDateTime" runat="server">Exam Date</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <span class="input-group-addon input-group-append border-left">
                                                <asp:TextBox ID="txtExamStartDateTime" runat="server" CssClass="form-control startDateTime"></asp:TextBox>
                                                <span class="mdi mdi-calendar input-group-text"></span>
                                            </span>
                                        </div>
                                        <div class="input-group-addon col-form-label mx-4">to</div>
                                        <div class="col-md-4">
                                            <span class="input-group-addon input-group-append border-left">
                                                <asp:TextBox ID="txtExamEndDateTime" runat="server" CssClass="form-control endDateTime"></asp:TextBox>
                                                <span class="mdi mdi-calendar input-group-text"></span>
                                            </span>
                                        </div>
                                    </div>
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

                        <div class="col-lg-12 stretch-card">
                            <div class="card">
                                <div class="table-responsive">
                                    <div class="gv-section text-center">
                                        <asp:GridView ID="gvExamInvi" runat="server" Width="100%" CssClass="table table-bordered tableExamInvi" AutoGenerateColumns="False" DataKeyNames="ExamTimetableGUID" CellPadding="10" CellSpacing="2" Border="0">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ExamTimetableGUID", "~/ExaminationInvigilatorsEntry.aspx?ExamTimetableGUID={0}") %>' Text="View" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ExamTimetableGUID" HeaderText="ExamTimetableGUID" ReadOnly="true" SortExpression="ExamTimetableGUID" Visible="false" />
                                                <asp:BoundField DataField="CourseAbbrv" HeaderText="Course Code" SortExpression="FAQTitle" />
                                                <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="FAQCategory" />
                                                <asp:BoundField DataField="ExamStartDateTime" HeaderText="Start Datetime" SortExpression="FAQStatus" />
                                                <asp:BoundField DataField="ExamEndDateTime" HeaderText="End Datetime" SortExpression="FAQStatus" />
                                                <asp:BoundField DataField="TotalInvi" HeaderText="No. of Invigilators" SortExpression="FAQStatus" />
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
                </div>
            </div>
        </div>
    </div>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $(".tableExamInvi").prepend($("<thead></thead>").html($(".tableExamInvi").find("tr:first"))).DataTable({
                "searching": false,
                "pageLength": 10,
                "order": [[1, 'asc']],
                "lengthMenu": [[1, 5, 10, 25, 50, -1], [1, 5, 10, 25, 50, "All"]],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }]
            });
        });
    </script>


</asp:Content>
