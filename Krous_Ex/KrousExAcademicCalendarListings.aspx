<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExAcademicCalendarListings.aspx.cs" Inherits="Krous_Ex.KrousExAcademicCalendarListings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



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
                                        <ul class="nav nav-tabs justify-content-center nav-fill" id="myTab" role="tablist">
                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                        </ul>
                                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
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
