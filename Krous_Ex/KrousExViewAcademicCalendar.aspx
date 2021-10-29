<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExViewAcademicCalendar.aspx.cs" Inherits="Krous_Ex.KrousExViewAcademicCalendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <style>

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container-fluid page-body-wrapper">
        <div class="main-panel">
            <div class="content-wrapper">
                <div class="row">
                    <div class="col-12 grid-margin stretch-card ">
                        <div class="card">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col-md-12">
                                        <h4 class="card-title">Academic Calendar</h4>
                                        <div class="table-responsive">
                                            <asp:Literal ID="litTest" runat="server"></asp:Literal><asp:Table ID="Table1" runat="server"></asp:Table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
