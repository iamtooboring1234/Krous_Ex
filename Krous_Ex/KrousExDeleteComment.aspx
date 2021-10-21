<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExDeleteComment.aspx.cs" Inherits="Krous_Ex.KrousExDeleteComment" %>
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
                                <h4 class="card-title">Are you sure to delete this message?</h4>
                                <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="lblDiscTopic" runat="server">Discussion Topic</asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtDiscTopic" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="lblReplyContent" runat="server">Your Reply</asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtReplyContent" TextMode="MultiLine" Rows="20" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row justify-content-center">
                                    <div class="col-md-10 text-right">
                                        <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-primary" Width="18%" OnClick="btnYes_Click"/>
                                        <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-danger" Width="18%" OnClick="btnNo_Click"/>
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
