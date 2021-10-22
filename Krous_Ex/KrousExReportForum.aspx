<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExReportForum.aspx.cs" Inherits="Krous_Ex.KrousExReportForum" %>
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
                                <h4 class="card-title">Report Forum Abuse</h4>
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
                                                    <asp:Label ID="lblReplyContent" runat="server">Reply Content</asp:Label><span style="color: red;">*</span>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtReplyContent" TextMode="MultiLine" Rows="20" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group pdForm">
                                            <div class="row justify-content-center">
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="Label1" runat="server">Select your reason</asp:Label><span style="color: red;">*</span>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlReason" CssClass="form-control" runat="server" OnTextChanged="ddlReason_TextChanged" AutoPostBack="true">
                                                        <asp:ListItem Selected="True">Advertising</asp:ListItem>
                                                        <asp:ListItem>Attacking/Insulting</asp:ListItem>
                                                        <asp:ListItem>Trolling</asp:ListItem>
                                                        <asp:ListItem>Derogatory/Offensive</asp:ListItem>
                                                        <asp:ListItem>Violates copyright/trademark</asp:ListItem>
                                                        <asp:ListItem>Sexually explicit/Vulgar</asp:ListItem>
                                                        <asp:ListItem>Other</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Panel ID="panelOtherReason" runat="server" Visible="false">
                                            <div class="form-group pdForm">
                                                <div class="row justify-content-center">
                                                    <div class="col-md-2 col-form-label">
                                                        
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtReason" TextMode="MultiLine" Rows="20" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

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
    </div>
</asp:Content>
