<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ForumReportListings.aspx.cs" Inherits="Krous_Ex.ForumReportListings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
                            <asp:Label ID="lblFAQList" runat="server" Font-Size="large">Forum Report</asp:Label>
                        </h3>
                        <p class="card-description">To approve/reject the report application </p>
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
                                    <asp:Label ID="lblFAQCategory" runat="server">Report Reason</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblFAQStatus" runat="server">Report status</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlFAQStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                                        <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
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
                        <asp:GridView ID="gvForumReport" runat="server" Width="100%" CssClass="table table-bordered tableForum" AutoGenerateColumns="False" DataKeyNames="ForumReportGUID"  CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true"
                                            onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true"
                                            onclick="Check_Click(this)" />
                                        
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="text-center" HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ForumReportGUID" HeaderText="ForumReportGUID" ReadOnly="true" SortExpression="ForumReportGUID" Visible="false" />
                                <asp:BoundField DataField="ReplyBy" HeaderText="Reply By" SortExpression="ReplyBy" />
                                <asp:BoundField DataField="ReplyContent" HeaderText="Reply Content" SortExpression="ReplyContent" />
                                <asp:BoundField DataField="ReportReason" HeaderText="Report Reason" SortExpression="ReportReason" />
                                <asp:BoundField DataField="ReportBy" HeaderText="Report By" SortExpression="ReportBy" />
                                <asp:BoundField DataField="ReportStatus" HeaderText="Status" SortExpression="ReportStatus" />
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="" HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No FAQ Record Found !"></asp:Label>
                    </div>
                </div>
                <asp:Panel ID="panelButton" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-md-12 mt-5 text-right">
                            <p class="card-description">
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-primary p-2" Width="18%" OnClick="btnApprove_Click" />
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to PIN this discussion ?" TargetControlID="btnApprove" />
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger p-2" Width="18%" OnClick="btnReject_Click" />
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to PIN this discussion ?" TargetControlID="btnReject" />
                            </p>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/data-table.js"></script>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $(".tableForum").prepend($("<thead></thead>").html($(".tableForum").find("tr:first"))).DataTable({
                "searching": false,
                "pageLength": 10,
                "order": [[1, 'asc']],
                "lengthMenu": [[1, 5, 10, 25, 50, -1], [1, 5, 10, 25, 50, "All"]],
                columnDefs: [{
                    orderable: false,
                    className: 'select-checkbox',
                    targets: 0,
                    responsive: true,
                    sPaginationType: 'full_numbers'
                }],
                select: {
                    style: 'os',
                    selector: 'td:first-child'
                },
            });
        });
    </script>

</asp:Content>
