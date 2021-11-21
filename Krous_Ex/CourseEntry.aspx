<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="CourseEntry.aspx.cs" Inherits="Krous_Ex.CourseEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblCourseEntry" runat="server">Course Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert new course details</p>
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
                                            <asp:Label ID="lblCourseName" runat="server">Course Name</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblCourseAbbrv" runat="server">Course Abbreviation</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCourseAbbrv" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" placeholder="Example:BACS3183"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblCourseDesc" runat="server">Course Description</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCourseDesc" CssClass="form-control" Style="resize: none" TextMode="multiline" Columns="40" Rows="6" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblCourseCategory" runat="server">Course Category</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RadioButtonList ID="rbCourseCategory" runat="server" RepeatDirection="Horizontal" CssClass="rdBtn">
                                                <asp:ListItem Selected="True" Value="Main Course">Main Course</asp:ListItem>
                                                <asp:ListItem Value="Elective Course">Elective Course</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblCreditHour" runat="server">Credit Hours</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtCreditHour" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
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
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this Course ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn btn-danger pdForm" OnClick="btnDelete_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this Course ?" TargetControlID="btnDelete" />
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

    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
