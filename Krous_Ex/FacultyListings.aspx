<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="FacultyListings.aspx.cs" Inherits="Krous_Ex.FacultyListings" %>

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
                            <asp:Label ID="lblFacultyList" runat="server" Font-Size="large">Faculty Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Faculty Available</p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <!-- name-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblFacultyName" runat="server">Faculty Name</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFacultyName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!--abbrv-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblFacultyAbbrv" runat="server">Faculty Abbreviation</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlFacultyAbbrv" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtFacultyAbbrv" runat="server" CssClass="form-control"></asp:TextBox>--%>
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
                        <asp:GridView ID="gvFaculty" runat="server" Width="100%" CssClass="table table-bordered tableFaculty" AutoGenerateColumns="False" DataKeyNames="FacultyGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("FacultyGUID", "~/FacultyEntry.aspx?FacultyGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="FacultyGUID" HeaderText="FacultyGUID" ReadOnly="true" SortExpression="FacultyGUID" Visible="false" />
                                <asp:BoundField DataField="FacultyName" HeaderText="Faculty Name" SortExpression="FacultyName" />
                                <asp:BoundField DataField="FacultyAbbrv" HeaderText="Faculty Abbreviation" SortExpression="FacultyAbbrv" />
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
            $(".tableFaculty").prepend($("<thead></thead>").html($(".tableFaculty").find("tr:first"))).DataTable({
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
