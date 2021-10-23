<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ProgrammeListings.aspx.cs" Inherits="Krous_Ex.ProgrammeListings" %>

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
                            <asp:Label ID="lblFAQList" runat="server" Font-Size="large">Programme Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Programmes Available</p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <!--name-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblFAQTitle" runat="server">Programme Name</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtProgName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <!--code can load, category-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblProgCode" runat="server">Programme Abbreviation</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlProgCode" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgCategory" runat="server">Programme Category</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlProgCategory" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="Foundation">Foundation</asp:ListItem>
                                        <asp:ListItem Value="Diploma">Diploma</asp:ListItem>
                                        <asp:ListItem Value="Bachelor Degree">Bachelor Degree</asp:ListItem>
                                        <asp:ListItem Value="Master">Master</asp:ListItem>
                                        <asp:ListItem Value="Doctor of Philosophy">Doctor of Philosophy</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <!--faculty can load-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblProgFaculty" runat="server">Faculty In-charge</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlFacultyInChg" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <!--full or part time-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3 col-form-label">
                                    <asp:Label ID="lblProgFullorPart" runat="server">Full/Part Time</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlFullorPart" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                        <asp:ListItem Value="Full Time">Full Time</asp:ListItem>
                                        <asp:ListItem Value="Part Time">Part Time</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button Text="Search" ID="btnSearch" runat="server" Width="18%" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
                        <asp:GridView ID="gvProgramme" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="ProgrammeGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ProgrammeGUID", "~/ProgrammeEntry.aspx?ProgrammeGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProgrammeGUID" HeaderText="ProgrammeGUID" ReadOnly="true" SortExpression="ProgrammeGUID" Visible="false" />
                                <asp:BoundField DataField="ProgrammeName" HeaderText="Name" SortExpression="ProgrammeName" />
                                <asp:BoundField DataField="ProgrammeAbbrv" HeaderText="Abbreviation" SortExpression="ProgrammeAbbrv" />
                                <asp:BoundField DataField="ProgrammeCategory" HeaderText="Category" SortExpression="ProgrammeCategory" />
                                <asp:BoundField DataField="ProgrammeFullorPart" HeaderText="Full/Part Time" SortExpression="ProgrammeFullorPart" />
                                <asp:BoundField DataField="ProgrammeFaculty" HeaderText="Faculty" SortExpression="ProgrammeFaculty" />
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="" HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Programme Record Found !"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

      <script src="Assests/main/js/data-table.js"></script>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $("[id*=gvProgramme]").prepend($("<thead></thead>").html($("[id*=gvProgramme]").find("tr:first"))).DataTable({
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
