<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentCurrentPayment.aspx.cs" Inherits="Krous_Ex.StudentCurrentPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

     <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>Payment History
                            <%--<asp:Label ID="lblPaymentHistory" runat="server" Font-Size="large">Payment History</asp:Label>--%>
                        </h3>
                    </div>
                </div>
                <hr />
            </div>
        </div>
    </div>

    <!--table-->
    <div class="col-lg-12 stretch-card">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <div class="gv-section text-center">
                        <asp:GridView ID="gvCurrentPayment" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="RegisterGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                 <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" runat="server" Text="View" Target="_blank" Style="margin-right: 10px;" />
                                            <asp:LinkButton ID="lbDownload" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                <asp:BoundField DataField="PaymentGUID" HeaderText="RegisterGUID" ReadOnly="true" SortExpression="RegisterGUID" Visible="false" />
                                <asp:BoundField DataField="PaymentNo" HeaderText="Reference No." ReadOnly="true" SortExpression="PaymentNo" />                                
                                <asp:BoundField DataField="TotalAmount" HeaderText="Amount" SortExpression="TotalAmount" ReadOnly="True" />
                                <asp:BoundField DataField="PaymentDate" HeaderText="Paid On" ReadOnly="true" SortExpression="PaymentDate" Visible="false" />
                                <asp:BoundField DataField="PaymentStatus" HeaderText="Status" SortExpression="PaymentStatus" ReadOnly="True" />
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
    </div>

    <script src="Assests/main/js/data-table.js"></script>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $("[id*=gvCurrentPayment]").prepend($("<thead></thead>").html($("[id*=gvCurrentPayment]").find("tr:first"))).DataTable({
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
