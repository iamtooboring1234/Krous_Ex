<%@ Page Title="" Language="C#" MasterPageFile="~/Staff_Site.Master" AutoEventWireup="true" CodeBehind="FAQEntry.aspx.cs" Inherits="Krous_Ex.FAQEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
        <div class="row">
            <div class="col-md-12">
                <h3>
                    <asp:Label ID="lblFAQEntry" runat="server">FAQ Entry</asp:Label>
                </h3>
                <p class="card-description">Form to insert Frequently Asked Question (FAQ) </p>
            </div>
        </div>
        <hr />
        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblFAQTitle" runat="server">FAQ Title</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtFAQTitle" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblFAQCategory" runat="server">Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:RadioButton runat="server" ID="rdExisting" Text="Use existing category" CssClass="rdBtn" Checked="true" GroupName="Category" AutoPostBack="true" />
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" ></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <asp:RadioButton runat="server" ID="rdNew" Text="Add new category" CssClass="rdBtn" GroupName="Category" AutoPostBack="true" />
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtNewCategory" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblFAQStatus" runat="server">FAQ status</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlFAQStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblFAQDesc" runat="server">FAQ Description</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtFAQDesc" CssClass="form-control" Style="resize: none" TextMode="multiline" Columns="60" Rows="6" runat="server" />
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12 d-flex justify-content-center text-right">
                                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" />
                                    <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" OnClick="btnSave_Click" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                    <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update these details ?" TargetControlID="btnUpdate" />
                                    <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this FAQ ?" TargetControlID="btnDelete" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdExisting" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="rdNew" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
            </div>
        </div>


</asp:Content>
