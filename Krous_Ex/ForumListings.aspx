<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ForumListings.aspx.cs" Inherits="Krous_Ex.ForumListings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <link href="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.js"></script>

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
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblForumTopic" runat="server">Forum Topic</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtForumTopic" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblForumCategory" runat="server">Category</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblForumStatus" runat="server">Forum Status</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlForumStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button text="Search" id="btnSearch" runat="server" Width="20%" CssClass="btn btn-primary p-2" OnClick="btnSearch_Click"/>
                                </div>
                            </div>
                        </div>

                        <hr />

                        <div class="col-lg-12 stretch-card">
                            <div class="card">
                                <div class="table-responsive">
                                    <div class="gv-section text-center">
                                        <asp:GridView ID="gvForumMng" runat="server" Width="100%" CssClass="table table-bordered tableForum" AutoGenerateColumns="False" DataKeyNames="ForumGUID, ForumType"  CellPadding="10" CellSpacing="2" Border="0">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("~/ForumEntry.aspx?ForumGUID={0}&ForumType={1}", Eval("ForumGUID"), Eval("ForumType")) %>' Text="View" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ForumGUID" HeaderText="ForumGUID" ReadOnly="true" SortExpression="ForumGUID" Visible="false" />
                                                <asp:BoundField DataField="ForumTopic" HeaderText="Topic" SortExpression="ForumTopic" />
                                                <asp:BoundField DataField="ForumDesc" HeaderText="Description" SortExpression="ForumDesc" />
                                                <asp:BoundField DataField="ForumCategory" HeaderText="Category" SortExpression="ForumCategory" />
                                                <asp:BoundField DataField="ForumStatus" HeaderText="Status" SortExpression="ForumStatus" />
                                                <asp:BoundField DataField="ForumType" HeaderText="Type" SortExpression="ForumType" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Forum Record Found !"></asp:Label>
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
            $(".tableForum").prepend($("<thead></thead>").html($(".tableForum").find("tr:first"))).DataTable({
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