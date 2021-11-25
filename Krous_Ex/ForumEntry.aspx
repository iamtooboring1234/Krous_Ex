<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ForumEntry.aspx.cs" Inherits="Krous_Ex.ForumEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    <script>
    function ShowPopup()
    {
        $('#succeedModal').modal('show');
    }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <div class="modal fade" id="succeedModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Successfully</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                FAQ created. 
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <asp:Panel ID="panelPublic" runat="server" Visible="true">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFAQEntry" runat="server">Public Forum Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert Public Forum</p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblForumTopic" runat="server">Forum Topic</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtForumTopic" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblForumDesc" runat="server">Forum Description</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtForumDesc" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblForumCategory" runat="server">Category</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton runat="server" ID="rdExisting" Text="Use existing category" CssClass="rdBtn" Checked="true" GroupName="Category" AutoPostBack="true" OnCheckedChanged="rdExisting_CheckedChanged" />
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton runat="server" ID="rdNew" Text="Add new category" CssClass="rdBtn" GroupName="Category" AutoPostBack="true" OnCheckedChanged="rdNew_CheckedChanged" />
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtNewCategory" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblForumStatus" runat="server">Forum status</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="col-md-4 p-0">
                                                <asp:DropDownList ID="ddlForumStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdExisting" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="rdNew" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
                </asp:Panel>
                <asp:Panel ID="panelPrivate" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="Label1" runat="server">Private Forum Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert Private Forum</p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label2" runat="server">Forum Topic</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label3" runat="server">Forum Description</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required" controltovalidate="txtFAQTitle" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label4" runat="server">Category</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton runat="server" ID="RadioButton1" Text="Use existing category" CssClass="rdBtn" Checked="true" GroupName="Category" AutoPostBack="true" OnCheckedChanged="rdExisting_CheckedChanged" />
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton runat="server" ID="RadioButton2" Text="Add new category" CssClass="rdBtn" GroupName="Category" AutoPostBack="true" OnCheckedChanged="rdNew_CheckedChanged" />
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label5" runat="server">Forum status</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                                <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <hr />
                            </div>
                        </div>

                                                        

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdExisting" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="rdNew" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
                </asp:Panel>

                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-12 float-right text-right">
                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-primary p-2" OnClick="btnUpdate_Click" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this Forum ?" TargetControlID="btnUpdate" />
                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary p-2" OnClick="btnSave_Click" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn btn-danger p-2" OnClick="btnDelete_Click" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this Forum ?" TargetControlID="btnDelete" />
                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn btn-dark p-2" OnClick="btnBack_Click" />
                            <asp:Button Text="Reset" ID="btnReset" runat="server" Width="18%" CssClass="btn btn-dark p-2" OnClick="btnReset_Click" />

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
