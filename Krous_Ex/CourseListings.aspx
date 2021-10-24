<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="CourseListings.aspx.cs" Inherits="Krous_Ex.CourseListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <link href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblCourseList" runat="server" Font-Size="large">Course Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Course Available</p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <!--name-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblCourseName" runat="server">Course Name</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <!--code, category-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblCourseAbbrv" runat="server">Course Abbreviation</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlCourseAbbrv" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblCourseCategory" runat="server">Course Category</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCourseCategory" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="Main Course">Main Course</asp:ListItem>
                                        <asp:ListItem Value="Elective Course">Elective Course</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <!--credit hour-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblCreditHour" runat="server">Credit Hour(s)</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCreditHour" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button Text="Search" ID="btnSearch" runat="server" Width="18%" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    <asp:Button Text="Add New" ID="btnAdd" runat="server" Width="18%" CssClass="btn btn-secondary" style="margin-left:12px; padding:10px 0;" OnClick="btnAdd_Click"/>
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
                        <asp:GridView ID="gvCourse" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="CourseGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("CourseGUID", "~/CourseEntry.aspx?CourseGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CourseGUID" HeaderText="CourseGUID" ReadOnly="true" SortExpression="CourseGUID" Visible="false" />
                                <asp:BoundField DataField="CourseName" HeaderText="Name" SortExpression="CourseName" />
                                <asp:BoundField DataField="CourseAbbrv" HeaderText="Abbreviation" SortExpression="CourseAbbrv" />
                                <asp:BoundField DataField="CreditHour" HeaderText="Credit Hour(s)" SortExpression="CreditHour" />
                                <asp:BoundField DataField="Category" HeaderText="Course Category" SortExpression="Category" />
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="" HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Course Record Found !"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/data-table.js"></script>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $("[id*=gvCourse]").prepend($("<thead></thead>").html($("[id*=gvCourse]").find("tr:first"))).DataTable({
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
