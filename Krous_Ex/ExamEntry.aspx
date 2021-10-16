<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExamEntry.aspx.cs" Inherits="Krous_Ex.ExamEntry" %>

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
                    <asp:Label ID="lblFAQEntry" runat="server">Exam Entry</asp:Label>
                </h3>
                <p class="card-description"></p>
            </div>
        </div>
        <hr />
        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblFAQTitle" runat="server">Exam Title</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFAQTitle" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label1" runat="server">Programme</asp:Label><span style="color: red;">*</span>                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" ></asp:DropDownList>
                                </div>
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label2" runat="server">Course</asp:Label><span style="color: red;">*</span>                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" ></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblFAQStatus" runat="server">Exam status</asp:Label><span style="color: red;">*</span>
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
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label3" runat="server">Start Time</asp:Label><span style="color: red;">*</span>                                
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtStartTime" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                                </div>
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label4" runat="server">End Time</asp:Label><span style="color: red;">*</span>                                
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtEndTime" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label5" runat="server">Exam Date</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblFAQDesc" runat="server">Exam Description</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFAQDesc" CssClass="form-control" Style="resize: none" TextMode="multiline" Columns="60" Rows="6" runat="server" />
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12 d-flex justify-content-center text-right">
                                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" />
                                    <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" />
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
        </asp:UpdatePanel>
    </div>
            </div>
        </div>

</asp:Content>
