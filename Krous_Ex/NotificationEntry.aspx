﻿<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="NotificationEntry.aspx.cs" Inherits="Krous_Ex.NotificationEntry" %>

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
                                        <div class="col-md-8 justify-content-center d-flex form-check p-0 m-0">
                                            <asp:RadioButtonList ID="radNotificationType" runat="server" CssClass="form-check-input" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radNotificationType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="1">All</asp:ListItem>
                                                <asp:ListItem Value="2">Staff</asp:ListItem>
                                                <asp:ListItem Value="3">Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <asp:Panel ID="panelAll" runat="server" Visible="true">
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
                                                <asp:Label ID="Label2" runat="server">Notification Subject </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtNotificationContent" TextMode="MultiLine" Rows="15" Width="100% " runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
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
                                                <asp:Label ID="Label11" runat="server">Programme </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label4" runat="server">Notification Subject </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label5" runat="server">Notification Subject </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="TextBox2" TextMode="MultiLine" Rows="15" Width="100% " runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="panelStudent" runat="server" Visible="true">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label6" runat="server">Notification Subject </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group pdForm">
                                        <div class="row justify-content-center">
                                            <div class="col-md-2 col-form-label">
                                                <asp:Label ID="Label7" runat="server">Notification Subject </asp:Label><span style="color: red;">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="TextBox4" TextMode="MultiLine" Rows="15" Width="100% " runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <hr />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
