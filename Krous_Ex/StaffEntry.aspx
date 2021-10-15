<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StaffEntry.aspx.cs" Inherits="Krous_Ex.StaffEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>

    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

</asp:Content>
