<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StaffAssessmentListings.aspx.cs" Inherits="Krous_Ex.StaffAssessmentListings" %>

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
                            <asp:Label ID="lblProgList" runat="server" Font-Size="large">Staff Assessment Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Assessment Created by Staff</p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <!--name-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgName" runat="server">Created By</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtStaffName" runat="server" CssClass="form-control" placeholder="Staff Full Name" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <!--group no-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblGroupNo" runat="server">Group No</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlGroupNo" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button Text="Search" ID="btnSearch" runat="server" Width="18%" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                     <asp:Button Text="Create New" ID="Button1" runat="server" Width="18%" CssClass="btn btn-primary" OnClick="Button1_Click"/>
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
                        <asp:GridView ID="gvAssessment" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="AssessmentGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("AssessmentGUID", "~/StaffAssessmentDetails.aspx?AssessmentGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AssessmentGUID" HeaderText="AssessmentGUID" ReadOnly="true" SortExpression="AssessmentGUID" Visible="false" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate"/>
                                <asp:BoundField DataField="StaffFullName" HeaderText="Created By" SortExpression="StaffFullName" />
                                <asp:BoundField DataField="AssessmentTitle" HeaderText="Assessment Title" SortExpression="AssessmentTitle" />
                                <asp:BoundField DataField="DueDate" HeaderText="Assessment Due Date" SortExpression="DueDate" />
                                <asp:BoundField DataField="GroupNo" HeaderText="Group Assigned" SortExpression="GroupNo" />

                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="" HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Assessment Record Found !"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/data-table.js"></script>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $("[id*=gvAssessment]").prepend($("<thead></thead>").html($("[id*=gvAssessment]").find("tr:first"))).DataTable({
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
