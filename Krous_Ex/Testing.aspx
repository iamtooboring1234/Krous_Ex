<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Student_Site.Master" AutoEventWireup="true" CodeBehind="Testing.aspx.cs" Inherits="Krous_Ex.Testing" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/fontawesome.min.css" />
    <link href="Assests/main/css/Contact.css" rel="stylesheet" />

    <script>
        // Jquery Dependency

        $("input[data-type='currency']").on({
            keyup: function () {
                formatCurrency($(this));
            },
            blur: function () {
                formatCurrency($(this), "blur");
            }
        });


        function formatNumber(n) {
            // format number 1000000 to 1,234,567
            return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }


        function formatCurrency(input, blur) {
            // appends $ to value, validates decimal side
            // and puts cursor back in right position.

            // get input value
            var input_val = input.val
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">

</asp:Content  >