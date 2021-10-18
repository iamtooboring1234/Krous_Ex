<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="DiscussionEntry.aspx.cs" Inherits="Krous_Ex.DiscussionEntry" %>

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
                                
                                    <asp:Panel ID="panelLogin" runat="server" Visible="true">
                                        <div class="row">
                                        <div class="col-md-10">
                                            <h4 class="card-title">Forums</h4>
                                            <p class="card-description">
                                                <asp:HyperLink ID="HyperLink1" runat="server" href="#">Login</asp:HyperLink> to post new forum
                                            </p>
                                        </div>
                                        <div class="col-md-2 justify-content-center align-self-center">
                                            <asp:Button ID="Button1" runat="server" Text="Button" CssClass="btn btn-primary" />
                                         </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="panelPost" runat="server" Visible="false">
                                        <asp:HyperLink ID="HyperLink2" runat="server">Login</asp:HyperLink>
                                    </asp:Panel>
                                    
                                    <div class="table-responsive">
                                    <div class="gv-section text-center">
                                    <div>
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

    <script src="Assests/main/js/data-table.js"></script>

</asp:Content>
