<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="StaffCreateAssessment.aspx.cs" Inherits="Krous_Ex.StaffCreateAssessment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.full.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.min.css" rel="stylesheet" />

    <link href="Assests/main/css/general.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            $('.duedate').datetimepicker({
                theme: 'dark',
                format: 'd/m/Y H:i',
                defaultDate: new Date(),
            });
        });
    </script>

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
                                                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control duedate" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="fa fa-calendar" style="padding: 10px;"></span>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group pdForm">
                                <div class="row">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblFileUpload" runat="server">File Upload</asp:Label>
                                    </div>
                                    <div class="col-md-9">
                                        <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncUploadMaterial" UploaderStyle="Traditional" />
                                    </div>
                                </div>
                            </div>
                        </div>
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
                        <p class="card-description">Select the following to assign the assessments to specific group of students.</p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <!--group-->
                                 <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblGroupList" runat="server">Group selection</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlGroups" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGroups_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!--session month-->
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblSessionGroupList" runat="server">Session Month </asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSessionMonth" runat="server" CssClass="form-control" AutoPostBack="true" Enabled ="false" OnSelectedIndexChanged="ddlSessionMonth_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <!--session-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblCurrentSession" runat="server">Session Selection</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCurrentSession" runat="server" CssClass="form-control" AutoPostBack="true" Enabled ="false" OnSelectedIndexChanged="ddlCurrentSession_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <!--semester-->
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblSemesterList" runat="server">Semester Selection</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AutoPostBack="true" Enabled ="false" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                                <!--programme category-->
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgrammeCategory" runat="server">Programme Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlProgrammCategory" runat="server" CssClass="form-control" AutoPostBack="true" Enabled ="false" OnSelectedIndexChanged="ddlProgrammCategory_SelectedIndexChanged" ></asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        
                        <!--programme name-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgrammeList" runat="server">Programme Selection</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" AutoPostBack="true" Enabled ="false" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <!--course-->
                         <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblCourse" runat="server">Course Selection</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" Enabled ="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12 float-right text-right">
                                    <asp:Button Text="Create" ID="btnCreate" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnCreate_Click" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to create this assessment?" TargetControlID="btnCreate" />
                                    <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
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
