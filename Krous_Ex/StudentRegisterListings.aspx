<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StudentRegisterListings.aspx.cs" Inherits="Krous_Ex.StudentRegisterListings" %>

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
                            <asp:Label ID="lblCourseList" runat="server" Font-Size="large">Student Registration Listings</asp:Label>
                        </h3> 
                        <p class="card-description">List of the student that have register a programme. </p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <!--name-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblStudName" runat="server">Student Name</asp:Label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtStudName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <!--student ic-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblNRIC" runat="server">Student NRIC</asp:Label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtNRIC" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button Text="Search" ID="btnSearch" runat="server" Width="18%" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    <%--<asp:Button Text="Add New" ID="btnAdd" runat="server" Width="18%" CssClass="btn btn-secondary" Style="margin-left: 12px; padding: 10px 0;" OnClick="btnAdd_Click" />--%>
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
                        <asp:GridView ID="gvCourse" runat="server" Width="100%" CssClass="table table-bordered tableCoruse" AutoGenerateColumns="False" DataKeyNames="RegisterGUID" CellPadding="10" CellSpacing="2" Border="0" OnRowDataBound="gvCourse_RowDataBound">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegisterGUID" runat="server" Text='<%# Eval("RegisterGUID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="StudentGUID" HeaderText="Name" ReadOnly="true" SortExpression="StudentGUID" Visible="false" />
                                <asp:BoundField DataField="ProgrammeGUID" HeaderText="ProgrammeGUID" ReadOnly="true" SortExpression="ProgrammeGUID" Visible="false" />
                                <asp:BoundField DataField="StudentFullName" HeaderText="Student Name" SortExpression="StudentFullName" ReadOnly="True" />
                                <asp:BoundField DataField="NRIC" HeaderText="Student NRIC" SortExpression="NRIC" ReadOnly="True" />
                                <asp:BoundField DataField="ProgrammeName" HeaderText="Programme Name" SortExpression="ProgrammeName" ReadOnly="True" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                            <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                            <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="" HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="StudentRecord Not Found !"></asp:Label>
                    </div>
                </div>
                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lblUpdate" runat="server" Visible="false" Text="0"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-success mr20" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script src="Assests/main/js/data-table.js"></script>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $(".tableCoruse").prepend($("<thead></thead>").html($(".tableCoruse").find("tr:first"))).DataTable({
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

    <script src="Assests/main/js/toastDemo.js"></script>
</asp:Content>
