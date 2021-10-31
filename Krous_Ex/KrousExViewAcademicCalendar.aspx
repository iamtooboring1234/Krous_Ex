<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExViewAcademicCalendar.aspx.cs" Inherits="Krous_Ex.KrousExViewAcademicCalendar" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />
    <link href="Assests/main/css/modern-horizontal/style.css" rel="stylesheet" />

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
                                        <asp:Panel ID="Panel1" runat="server">
                                        <div class="table-responsive">
                                            <asp:Literal ID="litTable" runat="server"></asp:Literal>
                                        </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnExport" runat="server" Text="Button" OnClick="btnExport_Click" />
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
