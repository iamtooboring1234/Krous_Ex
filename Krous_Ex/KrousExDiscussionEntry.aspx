<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExDiscussionEntry.aspx.cs" Inherits="Krous_Ex.KrousExDiscussionEntry" %>

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
                                    <h4 class="card-title p-0">Create Forums Topic</h4>
                                        <div class="form-group pdForm">
                                            <div class="row">
                                                <div class="col-md-12 col-form-label">
                                                    <asp:Label ID="lblDiscTopic" runat="server">Discussion Topic</asp:Label><span style="color: red;"> *</span>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtDiscTopic" Width="100%" MaxLength="100" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group pdForm">
                                            <div class="row">
                                                <div class="col-md-12 col-form-label">
                                                    <asp:Label ID="lblDiscDesc" runat="server">Discussion Description</asp:Label><span style="color: red;"> *</span>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtDiscDesc" Width="100%" MaxLength="100" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group pdForm">
                                            <div class="row">
                                                <div class="col-md-12 col-form-label">
                                                    <asp:Label ID="lblForumCategory" runat="server">Forum Category</asp:Label><span style="color: red;"> *</span>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group pdForm">
                                            <div class="row">
                                                <div class="col-md-12 col-form-label">
                                                    <asp:Label ID="Label1" runat="server">Discussion </asp:Label><span style="color: red;"> *</span>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtDiscContent" TextMode="MultiLine" Rows="15" Width="100%" MaxLength="999" runat="server" CssClass="form-control"></asp:TextBox>                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group pdForm">
                                            <div class="row">
                                                <div class="col-md-12 text-right">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary pl-5 pr-5 pt-2 pb-2" OnClick="btnSubmit_Click"/>
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
    </div>

    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
