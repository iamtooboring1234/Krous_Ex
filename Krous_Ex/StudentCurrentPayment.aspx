<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentCurrentPayment.aspx.cs" Inherits="Krous_Ex.StudentCurrentPayment" %>

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
                        <h3>Current Payment</h3>
                        <p class="card-description"><span style="color: red;">*</span>Please note that you should pay the bill(s) before the <strong style="color: red">DUE DATE</strong> is approaching.</p>
                    </div>
                </div>
                <hr />
                <!--table-->
                <div class="gv-section text-center">
                    <asp:GridView ID="gvCurrentPayment" runat="server" Width="100%" CssClass="table table-bordered tableCurrentPayment" AutoGenerateColumns="False" DataKeyNames="PaymentGUID" CellPadding="10" CellSpacing="2" Border="0">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="text-center" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlView" runat="server" Text="View" Target="_blank" Style="margin-right: 10px;" />
                                    <asp:LinkButton ID="lbDownload" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="PaymentGUID" HeaderText="RegisterGUID" ReadOnly="true" SortExpression="RegisterGUID" Visible="false" />
                            <asp:BoundField DataField="PaymentNo" HeaderText="Reference No." ReadOnly="true" SortExpression="PaymentNo" />
                            <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" ReadOnly="true" SortExpression="PaymentStatus" Visible="false" />
                            <asp:BoundField DataField="TotalAmount" HeaderText="Amount" SortExpression="TotalAmount" ReadOnly="True" />
                            <asp:BoundField DataField="DateIssued" HeaderText="Date Issued" ReadOnly="true" SortExpression="DateIssued" />
                            <asp:BoundField DataField="DateOverdue" HeaderText="Due Date" SortExpression="DateOverdue" ReadOnly="True" />
                              <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btnMakePayment" CssClass="btn btn-success btn-fw" style="height:31px;" runat="server" Text="Make Payment" OnClick="btnMakePayment_Click"/>
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
            $(".tableCurrentPayment").prepend($("<thead></thead>").html($(".tableCurrentPayment").find("tr:first"))).DataTable({
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
