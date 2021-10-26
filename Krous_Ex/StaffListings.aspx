<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StaffListings.aspx.cs" Inherits="Krous_Ex.StaffListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <%--<link href="Assests/main/css/layouts.css" rel="stylesheet" />--%>
    <link href="Assests/main/css/table.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

     <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblStaffList" runat="server" Font-Size="large">Staff Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Staff Available</p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblStaffUsername" runat="server">Staff Username</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblStaffFullName" runat="server">Staff Full Name</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblStaffRole" runat="server">Staff Role</asp:Label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlStaffRole" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                         <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblStaffPosition" runat="server">Staff Position</asp:Label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlPosition" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblStaffSpecialization" runat="server">Staff Specialization</asp:Label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlSpecialization" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label1" runat="server">Staff Course In-charge</asp:Label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlCourseInc" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
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
                        <asp:GridView ID="gvStaff" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="StaffGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("StaffGUID", "~/StaffEntry.aspx?StaffGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="StaffGUID" HeaderText="StaffGUID" ReadOnly="true" SortExpression="StaffGUID" Visible="false" />
                                <asp:BoundField DataField="StaffUsername" HeaderText="Username" SortExpression="StaffUsername" />
                                <asp:BoundField DataField="StaffFullName" HeaderText="Full Name" SortExpression="StaffFullName" />
                                <asp:BoundField DataField="StaffRole" HeaderText="Staff Role" SortExpression="StaffRole" />
                                <asp:BoundField DataField="StaffPositiion" HeaderText="Staff Position" SortExpression="StaffPositiion" />
                                <asp:BoundField DataField="Specialization" HeaderText="Specialization" SortExpression="Specialization" />
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
            $("[id*=gvStaff]").prepend($("<thead></thead>").html($("[id*=gvStaff]").find("tr:first"))).DataTable({
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

    <%--<div class="col-lg-12">
        <div class="card">
            <div class="card-body" style="padding-top:0">
                <div class="row">
                    <div class="table-responsive table-wrapper-scroll-y">
                    <table class="table">
                    <thead>
                    <tr>
                    <th scope="col">#</th>
                    <th scope="col">Username</th>
                    <th scope="col">Full Name</th>
                    <th scope="col">Roles</th>
                    <th scope="col">Phone Number</th>
                    <th scope="col">Email</th>
                    <th scope="col">NRIC</th>
                    <th scope="col">Specialization</th>
                    <th scope="col">Branches</th>
                    <th scope="col">Faculty</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                    <th scope="row">1</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">2</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">3</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">4</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">5</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">6</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    </tbody>
                    </table>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

</asp:Content>
