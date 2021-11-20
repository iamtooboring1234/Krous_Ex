<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExForumListings.aspx.cs" Inherits="Krous_Ex.KrousExForumListings" %>
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
                                    <h4 class="card-title">Forums</h4>
                                    <asp:Panel ID="panelLogin" runat="server" Visible="true">
                                        <p class="card-description">
                                            <asp:HyperLink ID="HyperLink1" runat="server" href="StudentLogin.aspx?FromURL=KrousExForumListings">Login</asp:HyperLink> to post new forum
                                        </p>
                                    </asp:Panel>
                                    <asp:Panel ID="panelPost" runat="server" Visible="false">
                                        <p class="card-description">
                                            <asp:HyperLink ID="HyperLink2" runat="server" href="KrousExDiscussionEntry">Post</asp:HyperLink> a new forum
                                        </p>
                                    </asp:Panel>
                                    </div>

<%--                                    <div class="col-lg-12 stretch-card">
                                        <div class="table-responsive">
                                            <div class="gv-section text-center">
                                                <asp:GridView ID="gvForum" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="ForumGUID"  CellPadding="10" CellSpacing="2" Border="0">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ForumGUID", "~/KrousExDiscussionEntry.aspx?ForumGUID={0}") %>' Text="View" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ForumGUID" HeaderText="ForumGUID" ReadOnly="true" SortExpression="ForumGUID" Visible="false" />
                                                        <asp:BoundField DataField="ForumTopic" HeaderText="Forum Topic" SortExpression="ForumTopic" />
                                                        <asp:BoundField DataField="ForumDesc" HeaderText="Category" SortExpression="ForumDesc" />
                                                        <asp:BoundField DataField="TotalDisc" HeaderText="Topics" SortExpression="TotalDisc" ItemStyle-Width="20px"/>
                                                        <asp:BoundField DataField="TotalReply" HeaderText="Replies" SortExpression="TotalReply" ItemStyle-Width="20px" />
                                                        <asp:BoundField DataField="LastUpdated" HeaderText="Last Updated" SortExpression="LastUpdated" ItemStyle-Width="20px" HtmlEncode="false"/>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="" HorizontalAlign="Center" />
                                                </asp:GridView>
                                                <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No FAQ Record Found !"></asp:Label>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="table-responsive">
                                    <div class="gv-section text-center">
                                    <div>
                                    <table class="table table-bordered" cellspacing="2" cellpadding="10" rules="all" border="0" id="gvForum" style="width:100%;">
                                        <thead>
                                            <tr class="header-style" align="left" style="background-color:#191C24;font-weight:bold;">
                                                <th scope="col">&nbsp;</th><th scope="col">Forum <th scope="col">Topics</th><th scope="col">Replies</th><th scope="col">Last Updated</th>
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
    </div>

     <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
