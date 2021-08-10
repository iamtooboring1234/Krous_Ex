<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testing2.aspx.cs" Inherits="Krous_Ex.Testing2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.9/css/dataTables.bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Assests/main/css/bootstrap.min.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/dataTables.bootstrap.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=gvCustomers]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
    </form>
</body>
</html>