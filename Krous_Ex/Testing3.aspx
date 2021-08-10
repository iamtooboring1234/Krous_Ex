<%@ Page Title="" Language="C#" MasterPageFile="~/Staff_Site.Master" AutoEventWireup="true" CodeBehind="Testing3.aspx.cs" Inherits="Krous_Ex.Testing3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.9/css/dataTables.bootstrap.min.css" />
    <script type="text/javascript">
        $(function () {
            $('[id*=gvCustomers]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
        <div>
        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-striped"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="FAQGUID" HeaderText="FAQGUID" ReadOnly="true" SortExpression="FAQGUID" Visible="false" />
                <asp:BoundField DataField="FAQTitle" HeaderText="Title" SortExpression="FAQTitle" />
                <asp:BoundField DataField="FAQCategory" HeaderText="Category" SortExpression="FAQCategory" />
                <asp:BoundField DataField="FAQStatus" HeaderText="Status" SortExpression="FAQStatus" />
            </Columns>
        </asp:GridView>
    </div>



</asp:Content>
