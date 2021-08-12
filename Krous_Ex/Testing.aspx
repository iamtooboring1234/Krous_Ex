<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Student_Site.Master" AutoEventWireup="true" CodeBehind="Testing.aspx.cs" Inherits="Krous_Ex.Testing" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/testing.css" rel="stylesheet"/>
     <script src="_scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="_scripts/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <SCRIPT type="text/javascript">
        $(function () {
            $("#txtDate").datepicker();
        });

	</SCRIPT>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">

    
 
    <div>
    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
    </div>    
  

</asp:Content  >