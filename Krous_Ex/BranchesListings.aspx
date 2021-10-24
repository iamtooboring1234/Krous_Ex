<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="BranchesListings.aspx.cs" Inherits="Krous_Ex.BranchesListings" %>

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
                            <asp:Label ID="lblBranchList" runat="server" Font-Size="large">Branch Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Branch Available</p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <!-- name-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblBranchName" runat="server">Branch Name</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                </div>
                            </div>
                        </div>
                        <!--address-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblBranchAddr" runat="server">Branch Address</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtBranchAddress" CssClass="form-control" Style="resize: none" TextMode="multiline" Columns="60" Rows="6" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!--email-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblBranchEmail" runat="server">Branch Email</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtBranchEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!--tel-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblBranchTel" runat="server">Branch Contact No.</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtBranchTel"
                                        Mask="+(99)9999999999" MessageValidatorTip="true" InputDirection="LeftToRight" ErrorTooltipEnabled="True"></ajaxToolkit:MaskedEditExtender>
                                    <asp:TextBox ID="txtBranchTel" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:GridView ID="gvBranches" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="BranchesGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("BranchesGUID", "~/BranchesEntry.aspx?BranchesGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BranchesGUID" HeaderText="BranchesGUID" ReadOnly="true" SortExpression="BranchesGUID" Visible="false" />
                                <asp:BoundField DataField="BranchesName" HeaderText="Branch Name" SortExpression="BranchesName" />
                                <asp:BoundField DataField="BranchesAddress" HeaderText="Branches Address" SortExpression="BranchesAddress" />
                                <asp:BoundField DataField="BranchesEmail" HeaderText="Branches Email" SortExpression="BranchesEmail" />
                                <asp:BoundField DataField="BranchesTel" HeaderText="Branches Tel." SortExpression="BranchesTel" />
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="" HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Branch Record Found !"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/data-table.js"></script>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $("[id*=gvBranches]").prepend($("<thead></thead>").html($("[id*=gvBranches]").find("tr:first"))).DataTable({
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
