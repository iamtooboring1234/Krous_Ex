<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentPaymentHistory.aspx.cs" Inherits="Krous_Ex.StudentPaymentHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/general.css" rel="stylesheet" />

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
                        <h3>Payment History</h3>
                    </div>
                </div>
                <hr />
                <!--table-->
                <div class="gv-section text-center">
                    <asp:GridView ID="gvPaymentHistory" runat="server" Width="100%" CssClass="table table-bordered tablePaymentHistory" AutoGenerateColumns="False" DataKeyNames="PaymentGUID" CellPadding="10" CellSpacing="2" Border="0">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PaymentGUID" HeaderText="RegisterGUID" ReadOnly="true" SortExpression="RegisterGUID" Visible="false" />
                            <asp:BoundField DataField="PaymentNo" HeaderText="Reference No." ReadOnly="true" SortExpression="PaymentNo" />
                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" SortExpression="TotalAmount" ReadOnly="True" />
                            <asp:BoundField DataField="TotalPaid" HeaderText="Amount Paid" SortExpression="TotalPaid" ReadOnly="True" />
                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" ReadOnly="true" SortExpression="PaymentDate" />
                            <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" ReadOnly="true" SortExpression="PaymentStatus" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlView" runat="server" Text="View" Target="_blank" Style="margin-right: 10px;" />
                                    <asp:LinkButton ID="lbDownload" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="" HorizontalAlign="Center" />
                    </asp:GridView>
                    <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Payment Records Found!"></asp:Label>
                </div>
            </div>
        </div>
    </div>




    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $(".tablePaymentHistory").prepend($("<thead></thead>").html($(".tablePaymentHistory").find("tr:first"))).DataTable({
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
