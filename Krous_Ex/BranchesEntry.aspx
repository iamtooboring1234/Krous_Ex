<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="BranchesEntry.aspx.cs" Inherits="Krous_Ex.BranchesEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/layouts.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblBranchEntry" runat="server">Branch Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert new branch </p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblBranchName" runat="server">Branch Name</asp:Label><span style="color: red;"> *</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" placeholder="Example : KUALA LUMPUR MAIN CAMPUS"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblBranchAddress" runat="server">Branch Address</asp:Label><span style="color: red;"> *</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtBranchAddress" CssClass="form-control" Style="resize: none" TextMode="multiline" Columns="60" Rows="6" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblBranchEmail" runat="server">Branch Email</asp:Label><span style="color: red;"> *</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtBranchEmail" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label1" runat="server">Branch Phone No.</asp:Label><span style="color: red;"> *</span>
                                        </div>
                                        <div class="col-md-8">
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtBranchPhone"
                                                Mask="+(99)9999999999" MessageValidatorTip="true" ErrorTooltipEnabled="True"></ajaxToolkit:MaskedEditExtender>
                                            <asp:TextBox ID="txtBranchPhone" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-success pdForm" OnClick="btnUpdate_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this Branch ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn btn-danger pdForm" OnClick="btnDelete_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this Branch ?" TargetControlID="btnDelete" />
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/formpickers.js"></script>
    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
