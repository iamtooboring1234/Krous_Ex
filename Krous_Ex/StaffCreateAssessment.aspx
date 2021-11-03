﻿<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StaffCreateAssessment.aspx.cs" Inherits="Krous_Ex.StaffCreateAssessment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script src="Assests/main/js/bootstrap-datepicker.min.js"></script>
    <link href="Assests/main/css/bootstrap-datepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
   
    <link href="Assests/main/css/general.css" rel="stylesheet" />

    <style>
        .ajax__fileupload_fileItemInfo div.removeButton {
            width: 80px;
        }

        .ajax__fileupload_uploadbutton {
            width: 85px;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblCourseEntry" runat="server">Create New Assessment</asp:Label>
                        </h3>
                        <p class="card-description">Create & assign new assessments for student</p>
                        <hr />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12 mt-3">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblTitle" runat="server">Assessment Title</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtAssTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblAssDesc" runat="server">Assessment Description</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtDesc" TextMode="MultiLine" runat="server" CssClass="form-control" Style="height: 120px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblDueDate" runat="server">Due Date</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <div class='input-group date' id='datetimepicker8'>
                                                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="fa fa-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                            <script type="text/javascript">
                                                $(function () {
                                                    $('#txtDueDate').datetimepicker({
                                                        icons: {
                                                            time: "fa fa-clock-o",
                                                            date: "fa fa-calendar",
                                                            up: "fa fa-arrow-up",
                                                            down: "fa fa-arrow-down"
                                                        }
                                                    });
                                                });
                                            </script>

                                            <%--<span class="input-group-addon input-group-append border-left">
                                                <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDueDate_TextChanged"></asp:TextBox>
                                                <span class="mdi mdi-calendar input-group-text"></span>
                                            </span>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtDueDate" EnableViewState="false" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>--%>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="form-group pdForm">
                                <div class="row">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblFileUpload" runat="server">File(s) Upload</asp:Label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:FileUpload ID="UploadMaterials" AllowMultiple="true" runat="server" />
                                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Mode="Auto" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                        <%--<ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload2" AllowedFileTypes="jpeg,jpg,png,gif,pdf,zip,rar,ZIP,RAR,mp4,mp3,doc,docx,pdf" MaximumNumberOfFiles="10" Mode="Auto" runat="server" OnUploadComplete="AjaxFileUpload2_UploadComplete" />--%>
                                        <%--<ajaxToolkit:AsyncFileUpload OnClientUploadComplete="uploadComplete" runat="server" ID="AsyncFileUploadChat" OnUploadedComplete="FileUploadComplete" Style="display: none" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--assign to which group of student-->
        <div class="col-lg-12 mt-3">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <p class="card-description">Select the session and group to assign the assessments to</p>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group pdForm">
                                <div class="row">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblSession" runat="server">Session selection</asp:Label><span style="color: red;">*</span>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group pdForm">
                                <div class="row">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblGroupList" runat="server">Group selection</asp:Label><span style="color: red;">*</span>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlGroups" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-group pdForm">
                                <div class="row">
                                    <div class="col-md-12 float-right text-right">
                                        <asp:Button Text="Create" ID="btnCreate" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnCreate_Click" />
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to create this assessment?" TargetControlID="btnCreate" />
                                        <%--<asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm"/>--%>
                                        <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" Style="height: 37px;" />
                                        <%-- <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnUpdate_Click" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this FAQ ?" TargetControlID="btnUpdate" />
                                    <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnDelete_Click" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this FAQ ?" TargetControlID="btnDelete" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <script src="Assests/main/js/formpickers.js"></script>
        <script src="Assests/main/js/toastDemo.js"></script>
</asp:Content>