<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExViewDiscussion.aspx.cs" Inherits="Krous_Ex.KrousExViewDiscussion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>
    <link href="Assests/main/css/table.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/plug-ins/1.10.19/sorting/absolute.js"></script>
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
                                            <asp:HyperLink ID="HyperLink1" runat="server" href="StudentLogin.aspx?FromURL=KrousExDiscussionListings">Login</asp:HyperLink> to post new forum 
                                        </p>
                                    </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="panelPost" runat="server" Visible="false">
                                    <div class="row">
                                    <div class="col-md-10">
                                        <p class="card-description">
                                            <asp:HyperLink ID="HyperLink2" runat="server" href="DiscussionEntry">Post</asp:HyperLink> a new forum / <a href="#postReplyContainer">Post</a> a reply
                                        </p>
                                    </div>
                                    </div>
                                </asp:Panel>

                                <div class="table-responsive">
                                <div class="gv-section text-center">
                                <div>
                                <table class="table table-bordered" cellspacing="2" cellpadding="10" rules="all" border="0" id="gvReply" style="width:100%;">
                                <thead>
                                <tr class="header-style" align="left" style="background-color:#191C24;font-weight:bold;border:none">
                                <th scope="col" width="150px" style="border:none"></th> <th scope="col" style="border:none"></th>
                                </tr>
                                <asp:Literal ID="tableHead" runat="server"></asp:Literal>
                                </thead>
                                <tbody>

                                    <asp:Literal ID="tableBody" runat="server"></asp:Literal>

                                </tbody>
                                </table>
                                </div>
                                </div>
                                </div>

                                <asp:Panel ID="panelDelete" runat="server" Visible="true">
                                    <div class="row">
                                    <div class="col-md-12 mt-5 text-right">
                                        <p class="card-description">
                                            <asp:HyperLink ID="HyperLink4" runat="server" href="DiscussionEntry">Post</asp:HyperLink> a new forum / <a href="#postReplyContainer">Post</a> a reply
                                        </p>
                                    </div>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>


                    </div>
                </div>
                <asp:Panel ID="panelPostReply" runat="server" Visible="true">
                    <div id="postReplyContainer" class="row">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-title">Post a reply</div>
                                    <div class="card-description">
                                        Your name:
                                        <asp:Literal ID="Literal3" runat="server"></asp:Literal>

                                        Comment: <span style="color:red"> *</span>
                                        <p>
                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="10" Width="100%" MaxLength="999"></asp:TextBox>
                                        </p>
                                    </div>
                                    <div class="col-md-12 text-right pr-0">
                                        <asp:Button ID="btnPostReply" runat="server" Text="Post" CssClass="btn btn-primary" Width="18%" OnClick="btnPostReply_Click"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/data-table.js"></script>

    <script>
    $(document).ready(function () {

        function updateHeader(data) {

            var table = $('#example').DataTable();
            table.row.add(data).draw();

            $('.rowData', table.table().header()).each(function (i) {

                $(this).html(data[i]);

            });
        }

        var table = $('#example').DataTable({
            "sorting": false
        });

    });
    </script>

</asp:Content>
