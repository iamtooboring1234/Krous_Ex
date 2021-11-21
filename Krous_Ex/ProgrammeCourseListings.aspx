<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ProgrammeCourseListings.aspx.cs" Inherits="Krous_Ex.ProgrammeCourseListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <script src="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body pb-0">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFacultyList" runat="server" Font-Size="large">Programme Course Listings</asp:Label>
                        </h3>
                        <p class="card-description">List of Course Registered Under Programme </p>
                    </div>
                </div>
                <hr />
            </div>
        </div>
    </div>

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
                        <asp:GridView ID="gvProgCourse" runat="server" Width="100%" CssClass="table table-bordered tableProgCourse" AutoGenerateColumns="False" DataKeyNames="ProgrammeGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ProgrammeGUID", "~/ProgrammeCourseListings.aspx?ProgrammeGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProgrammeGUID" HeaderText="ProgrammeGUID" ReadOnly="true" SortExpression="ProgrammeGUID" Visible="false" />
                                <asp:BoundField DataField="ProgrammeName" HeaderText="Programme Name" SortExpression="ProgrammeName" />
                                <asp:BoundField DataField="TotalCourseRegistered" HeaderText="Total Course Registered" SortExpression="Semester" />
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

    <hr />

    <asp:Panel ID="panelProgCourseDetails" runat="server" Visible="false">
        <div class="col-lg-12 stretch-card">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <p class="card-description"><strong>Step 2:</strong> Select to edit the courses registered of each semester. </p>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <div class="gv-section text-center">
                            <asp:GridView ID="gvProgCourseDetails" runat="server" Width="100%" CssClass="table table-bordered tableProgCourseDetails" AutoGenerateColumns="False" DataKeyNames="ProgrammeGUID, SemesterGUID, ProgrammeCategory" CellPadding="10" CellSpacing="2" Border="0">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("~/ProgrammeCourseEntry.aspx?ProgrammeGUID={0}&SemesterGUID={1}&ProgrammeCategory={2}", Eval("ProgrammeGUID"), Eval("SemesterGUID"), Eval("ProgrammeCategory")) %>' Text="View" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProgrammeCourseGUID" HeaderText="ProgrammeCourseGUID" ReadOnly="true" SortExpression="ProgrammeCourseGUID" Visible="false" />
                                    <asp:BoundField DataField="SemesterGUID" HeaderText="SemesterGUID" ReadOnly="true" SortExpression="SemesterGUID" Visible="false" />
                                    <asp:BoundField DataField="SemesterYear" HeaderText="Year" SortExpression="ProgrammeName" />
                                    <asp:BoundField DataField="SemesterSem" HeaderText="Semester" SortExpression="Semester" />
                                    <asp:BoundField DataField="TotalCoursePerSemester" HeaderText="Courses Registered Per Semester" SortExpression="IntakeSession" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="" HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:Label ID="lblNoData1" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Faculty Record Found !"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $(".tableProgCourse").prepend($("<thead></thead>").html($(".tableProgCourse").find("tr:first"))).DataTable({
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

        $(document).ready(function () {
            $(".tableProgCourseDetails").prepend($("<thead></thead>").html($(".tableProgCourseDetails").find("tr:first"))).DataTable({
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
