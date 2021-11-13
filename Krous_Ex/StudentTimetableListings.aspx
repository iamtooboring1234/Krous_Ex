<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentTimetableListings.aspx.cs" Inherits="Krous_Ex.StudentTimetableListings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <script src="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12 stretch-card">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <p class="card-description"><strong>Step 1:</strong> Select programme to view total courses registered of each semester. </p>
                    </div>
                </div>
                <div class="table-responsive">
                    <div class="gv-section text-center">
                        <asp:GridView ID="gvTimetable" runat="server" Width="100%" CssClass="table table-bordered tableTimetable" AutoGenerateColumns="False" DataKeyNames="AcademicCalenderGUID, SessionGUID" CellPadding="10" CellSpacing="2" Border="0" OnRowDataBound="gvTimetable_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlViewTimetable" runat="server" NavigateUrl='<%# String.Format("~/StudentViewTimeTable.aspx?AcademicCalenderGUID={0}&SessionGUID={1}", Eval("AcademicCalenderGUID"), Eval("SessionGUID")) %>' Text="View Timetable" CssClass="btn btn-success pr-4 pl-4 pt-2 pb-2" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AcademicCalenderGUID" HeaderText="AcademicCalenderGUID" ReadOnly="true" SortExpression="AcademicCalenderGUID" Visible="false" />
                                <asp:BoundField DataField="SessionGUID" HeaderText="SessionGUID" ReadOnly="true" SortExpression="SessionGUID" Visible="false" />
                                <asp:BoundField DataField="SemesterEndDate" HeaderText="SemesterEndDate" ReadOnly="true" SortExpression="SemesterEndDate" Visible="false" />
                                <asp:BoundField DataField="SessionYearMonth" HeaderText="Session" SortExpression="Session" />
                                <asp:BoundField DataField="DurationDate" HeaderText="Duration Date" SortExpression="DurationDate" />
                                <asp:BoundField DataField="TotalWeek" HeaderText="Total Week" SortExpression="TotalWeek" />
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
            $(".tableTimetable").prepend($("<thead></thead>").html($(".tableTimetable").find("tr:first"))).DataTable({
                "searching": false,
                "pageLength": 10,
                "order": [[1, 'desc']],
                "lengthMenu": [[1, 5, 10, 25, 50, -1], [1, 5, 10, 25, 50, "All"]],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }]
            });
        });
    </script>

</asp:Content>
