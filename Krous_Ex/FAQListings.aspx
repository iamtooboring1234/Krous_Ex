<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="FAQListings.aspx.cs" Inherits="Krous_Ex.FAQListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.9/css/dataTables.bootstrap.min.css" />

    <link href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>

<%--    <link href="https://nightly.datatables.net/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <script src="https://nightly.datatables.net/js/jquery.dataTables.js"></script>--%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFAQList" runat="server" Font-Size="large">FAQs Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Frequently Asked Question (FAQ) </p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblFAQTitle" runat="server">FAQ Title</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFAQTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblFAQCategory" runat="server">Category</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblFAQStatus" runat="server">FAQ status</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlFAQStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button text="Search" id="btnSearch" runat="server" Width="18%" CssClass="btn btn-primary" OnClick="btnSearch_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>     
        </div>
    </div>

    <div class="col-lg-12 stretch-card">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <div class="gv-section text-center">
                        <asp:GridView ID="gvFAQ" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="FAQGUID"  CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("FAQGUID", "~/FAQEntry.aspx?FAQGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="FAQGUID" HeaderText="FAQGUID" ReadOnly="true" SortExpression="FAQGUID" Visible="false" />
                                <asp:BoundField DataField="FAQTitle" HeaderText="Title" SortExpression="FAQTitle" />
                                <asp:BoundField DataField="FAQCategory" HeaderText="Category" SortExpression="FAQCategory" />
                                <asp:BoundField DataField="FAQStatus" HeaderText="Status" SortExpression="FAQStatus" />
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

    <script src="Assests/main/js/data-table.js"></script>

    <script>
        $(document).ready(function () {
            $.noConflict();
            $("[id*=gvFAQ]").prepend($("<thead></thead>").html($("[id*=gvFAQ]").find("tr:first"))).DataTable({
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