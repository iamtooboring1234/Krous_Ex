<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExDiscussionListings.aspx.cs" Inherits="Krous_Ex.KrousExDiscussionListings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>
    <link href="Assests/main/css/table.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container-fluid page-body-wrapper">
        <div class="main-panel">
            <div class="content-wrapper">
                <div class="row">
                    <div class="col-12 grid-margin stretch-card ">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title"><asp:HyperLink ID="HyperLink3" runat="server" href="KrousExForumListings" CssClass="forum-link">Forums</asp:HyperLink> <asp:Literal ID="Literal2" runat="server"></asp:Literal> </h4>
                                <asp:Panel ID="panelLogin" runat="server" Visible="true">
                                    <div class="row">
                                    <div class="col-md-10">
                                        <p class="card-description">
                                            <asp:HyperLink ID="HyperLink1" runat="server">Login</asp:HyperLink> to post new forum
                                        </p>
                                    </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="panelPost" runat="server" Visible="false">
                                    <div class="row">
                                    <div class="col-md-10">
                                        <p class="card-description">
                                            <asp:HyperLink ID="HyperLink2" runat="server" href="KrousExDiscussionEntry">Post</asp:HyperLink> a new forum
                                        </p>
                                    </div>
                                    </div>
                                </asp:Panel>
                                
                                <div class="table-responsive">
                                <div class="gv-section text-center">
                                <asp:Panel ID="panelDiscList" runat="server" Visible="false">
                                <table class="table table-bordered" cellspacing="2" cellpadding="10" rules="all" border="0" id="gvDisc" style="width:100%;">
                                <thead>
                                <tr class="header-style" align="left" style="background-color:#191C24;font-weight:bold;">
                                <th scope="col">&nbsp;</th><th scope="col">Discussion Topic <th scope="col">Replies</th><th scope="col">Created</th><th scope="col">Last Reply</th>
                                </tr>
                                </thead>
                                <tbody>
                                  
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>

                                </tbody>
                                </table>
                                </asp:Panel>
                                <asp:Panel ID="panelNoRecord" runat="server" Visible="true">
                                    <h4 class="card-title">No Record Found!</h4>
                                </asp:Panel>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/data-table.js"></script>


</asp:Content>
