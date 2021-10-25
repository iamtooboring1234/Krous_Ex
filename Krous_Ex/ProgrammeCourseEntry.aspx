﻿<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ProgrammeCourseEntry.aspx.cs" Inherits="Krous_Ex.ProgrammeCourseEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <link href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="Label8" runat="server">Programme Course</asp:Label>
                        </h3>
                        <p class="card-description m-0">Form to insert programme course </p>
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
                        <p class="card-description"><strong>Step 1:</strong> Select Programme & Semester </p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label3" runat="server">Programme Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgrammCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProgrammCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label1" runat="server">Programme Name</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label2" runat="server">Semester</asp:Label><span style="color: red;">*</span>

                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
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
                        <p class="card-description"><strong>Step 2:</strong> Select course </p>
                    </div>
                </div>
                <%--<div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label5" runat="server">Programme Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProgrammCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label6" runat="server">Programme Name</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label7" runat="server">Semester</asp:Label><span style="color: red;">*</span>

                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>

</asp:Content>
