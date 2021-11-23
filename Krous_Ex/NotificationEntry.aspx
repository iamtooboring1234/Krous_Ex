<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="NotificationEntry.aspx.cs" Inherits="Krous_Ex.NotificationEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .form-check table.form-check-input tbody tr td label  {
            padding: 0 0.625rem;
            font-size: 0.875rem;
            line-height: 1.75;
            color: #6c7293;
            font-weight: bold;
        }

        .form-check table.form-check-input tbody tr td {
            padding-left: 50px;
        }

        .form-check table.form-check-input {
            margin-left: 0 !important;
        }

        .form-check table.form-check-input tbody tr td input {
            width: 1em;
            height: 1em;
            margin-top: 0.25em;
            vertical-align: top;
            background-color: #fff;
            background-repeat: no-repeat;
            background-position: center;
            background-size: contain;
            border: 1px solid rgba(0, 0, 0, 0.25);
        }

        .form-check-checkbox table tbody tr td label, .form-check-all label {
            padding-left: 0.625rem;
            font-size: 0.875rem;
            line-height: 1.5;
            color: #6c7293;
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
                            <asp:Label ID="Label8" runat="server">Notification Entry</asp:Label>
                        </h3>
                        <p class="card-description m-0">Form to send notification </p>
                        <hr />
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="Label3" runat="server">Who to notify?</asp:Label>
                                        </div>
                                        <div class="col-md-8 form-check p-0 m-0">
                                            <div class="row justify-content-center">
                                                <div class="col-md-8">
                                            <asp:RadioButtonList ID="radNotificationType" runat="server" CssClass="form-check-input mt-0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radNotificationType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="1">All</asp:ListItem>
                                                <asp:ListItem Value="2">Staff</asp:ListItem>
                                                <asp:ListItem Value="3">Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <asp:Panel ID="panelStaff" runat="server" Visible="true">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label9" runat="server">Branches </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8 form-check-checkbox">
                                                <asp:CheckBox ID="cbBranchAll" runat="server" OnCheckedChanged="cbBranchAll_CheckedChanged" AutoPostBack="true" Text="All" CssClass="form-check-all"/>
                                                <asp:CheckBoxList ID="cbBranch" runat="server" OnSelectedIndexChanged="cbBranch_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label10" runat="server">Faculty </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8 form-check-checkbox">
                                                <asp:CheckBox ID="cbFacultyAll" runat="server" OnCheckedChanged="cbFacultyAll_CheckedChanged" AutoPostBack="true" Text="All" CssClass="form-check-all"/> 
                                                <asp:CheckBoxList ID="cbFaculty" runat="server" OnSelectedIndexChanged="cbFaculty_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label11" runat="server">Staff Role </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8 form-check-checkbox">
                                                <asp:DropDownList ID="ddlStaffRole" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="Academic Staff">Academic Staff</asp:ListItem>
                                                    <asp:ListItem Value="Non-Academic Staff">Non-Academic Staff</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                <%--<div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label11" runat="server">Programme </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8 form-check-checkbox">
                                                <asp:Panel ID="panelProgramme" runat="server" Visible="false">
                                                <asp:CheckBox ID="cbProgrammeAll" runat="server" AutoPostBack="true" Text="All" CssClass="form-check-all"/> 
                                                <asp:CheckBoxList ID="cbProgramme" runat="server" AutoPostBack="true"></asp:CheckBoxList>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="panelStudent" runat="server" Visible="true">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label4" runat="server">Branches </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8 form-check-checkbox">
                                                <asp:CheckBox ID="cbStudentBranchAll" runat="server" OnCheckedChanged="cbStudentBranchAll_CheckedChanged" AutoPostBack="true" Text="All" CssClass="form-check-all"/>
                                                <asp:CheckBoxList ID="cbStudentBranch" runat="server" OnSelectedIndexChanged="cbStudentBranch_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label5" runat="server">Faculty </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8 form-check-checkbox">
                                                <asp:CheckBox ID="cbStudentFacultyAll" runat="server" OnCheckedChanged="cbStudentFacultyAll_CheckedChanged" AutoPostBack="true" Text="All" CssClass="form-check-all"/> 
                                                <asp:CheckBoxList ID="cbStudentFaculty" runat="server" OnSelectedIndexChanged="cbStudentFaculty_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label6" runat="server">Session </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8 form-check-checkbox">
                                                <asp:CheckBox ID="cbStudentSessionAll" runat="server" OnCheckedChanged="cbStudentSessionAll_CheckedChanged" AutoPostBack="true" Text="All" CssClass="form-check-all"/> 
                                                <asp:CheckBoxList ID="cbStudentSession" runat="server" OnSelectedIndexChanged="cbStudentSession_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="panelNotification" runat="server" Visible="true">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label1" runat="server">Notification Subject </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtNotificationSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label2" runat="server">Notification Content </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtNotificationContent" TextMode="MultiLine" Rows="15" Width="100% " runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <hr />
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12 float-right text-right">
                                    <asp:Button Text="Send" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click"/>
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to send this notification ?" TargetControlID="btnSave" />
                                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" Style="margin-left: 10px; height: 38px;" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>

                        <asp:Label ID="testcmd" runat="server"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
