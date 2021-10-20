<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ProgrammeEntry.aspx.cs" Inherits="Krous_Ex.ProgrammeEntry" %>

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
                            <asp:Label ID="lblCourse" runat="server">Programme Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert new programme details</p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblProgName" runat="server">Programme Name</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtProgName" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is required!" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <!-- exp: RSD-->
                                            <asp:Label ID="lblProgAbbrv" runat="server">Programme Abbreviation</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtProgAbbrv" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is required!" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblProgDesc" runat="server">Programme Description</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtProgDesc" CssClass="form-control" Style="resize: none" TextMode="multiline" Columns="40" Rows="6" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="This field is required!" Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblProgCategory" runat="server">Programme Category</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlProgCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProgCategory_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="">Category</asp:ListItem>
                                                <asp:ListItem Value="Foundation">Foundation</asp:ListItem>
                                                <asp:ListItem Value="Diploma">Diploma</asp:ListItem>
                                                <asp:ListItem Value="Bachelor Degree">Bachelor Degree</asp:ListItem>
                                                <asp:ListItem Value="Master">Master</asp:ListItem>
                                                <asp:ListItem Value="Doctor of Philosophy">Doctor of Philosophy</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblProgDuration" runat="server">Programme Duration - Year(s)</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlProgDuration" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="">Duration</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <!--if they select master, only display-->
                                        <div class="col-md-4">
                                            <asp:RadioButtonList ID="rblFullorPart" runat="server" RepeatDirection="Horizontal" CssClass="rdBtn">
                                                <asp:ListItem>Full Time</asp:ListItem>
                                                <asp:ListItem>Part Time</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>

                                <!--after that need to include choosing the faculty-->

                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <%--<asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />--%>
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                            <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" Style="margin-left: 10px; height: 38px;" OnClick="btnCancel_Click" />
                                            <%--<asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnUpdate_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this FAQ ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnDelete_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this FAQ ?" TargetControlID="btnDelete" />--%>
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
