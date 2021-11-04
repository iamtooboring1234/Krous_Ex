<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExaminationListings.aspx.cs" Inherits="Krous_Ex.ExaminationListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <script src="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.css" rel="stylesheet" />

    <script src="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.full.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.min.css" rel="stylesheet" />

    <link href="Assests/main/css/general.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            $('.examDate').datetimepicker({
                defaultTime: '09:00',
                defaultDate: new Date(),
                closeOnDateSelect: true,
                theme: 'dark',
                format: 'd/m/Y H:i',
                defaultDate: new Date(),
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFacultyList" runat="server" Font-Size="large">Faculty Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Faculty Available</p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblFacultyName" runat="server">Course Name</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
 
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblExamDate" runat="server">Examination Date</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <span class="input-group-addon input-group-append border-left">
                                                <asp:TextBox ID="txtExamDate" runat="server" CssClass="form-control examDate" AutoCompleteType="Disabled"></asp:TextBox>
                                                <span class="mdi mdi-calendar input-group-text"></span>
                                            </span>
<%--                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtExamDate" EnableViewState="false" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                            <asp:HiddenField ID="hdStartDate" runat="server" />
                                            <asp:HiddenField ID="hdEndDate" runat="server" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button Text="Search" ID="btnSearch" runat="server" Width="18%" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    <asp:Button Text="Add New" ID="btnAdd" runat="server" Width="18%" CssClass="btn btn-secondary" Style="margin-left: 12px; padding: 10px 0;" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--table-->
    <div class="col-lg-12 stretch-card">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <div class="gv-section text-center">
                        <asp:GridView ID="gvExam" runat="server" Width="100%" CssClass="table table-bordered tableExam" AutoGenerateColumns="False" DataKeyNames="ExamTimeTableGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ExamTimeTableGUID", "~/ExaminationEntry.aspx?ExamTimeTableGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ExamTimeTableGUID" HeaderText="ExamTimeTableGUID" ReadOnly="true" SortExpression="ExamTimeTableGUID" Visible="false" />
                                <asp:BoundField DataField="CourseAbbrv" HeaderText="Course Code" SortExpression="FacultyName" />
                                <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="FacultyName" />
                                <asp:BoundField DataField="ExamStartDateTime" HeaderText="Start Date Time" SortExpression="ExamStartDateTime" />
                                <asp:BoundField DataField="ExamEndDateTime" HeaderText="End Date Time" SortExpression="ExamEndDateTime" />
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="" HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Faculty Record Found !"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $(".tableExam").prepend($("<thead></thead>").html($(".tableExam").find("tr:first"))).DataTable({
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
