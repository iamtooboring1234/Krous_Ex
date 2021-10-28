<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StaffCourseIncEntry.aspx.cs" Inherits="Krous_Ex.StaffCourseInCharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet" type="text/css" />

     <style type="text/css">
        .checkBox
        {
            padding-left: 20px;
        }
        .checkBox label
        {
            display: inline-block;
            vertical-align: middle;
            position: relative;
            padding-left: 5px;
        }
        .checkBox label::before
        {
            content: "";
            display: inline-block;
            position: absolute;
            width: 17px;
            height: 17px;
            left: 0;
            margin-left: -20px;
            border: 1px solid #cccccc;
            border-radius: 3px;
            background-color: #fff;
            -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
            -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
            transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
        }
        .checkBox label::after
        {
            display: inline-block;
            position: absolute;
            width: 16px;
            height: 16px;
            left: 0;
            top: 0;
            margin-left: -20px;
            padding-left: 3px;
            padding-top: 1px;
            font-size: 11px;
            color: #555555;
        }
        .checkBox input[type="checkbox"]
        {
            opacity: 0;
            z-index: 1;
        }
        .checkBox input[type="checkbox"]:checked + label::after
        {
            font-family: "FontAwesome";
            content: "\f00c";
        }
         
        .checkBox-primary input[type="checkbox"]:checked + label::before
        {
            background-color: #337ab7;
            border-color: #337ab7;
        }
        .checkBox-primary input[type="checkbox"]:checked + label::after
        {
            color: #fff;
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
                            <asp:Label ID="lblCourseIncEntry" runat="server">Course In-charge Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert the course in-charge by respective staff</p>
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
                                            <asp:Label ID="lblStaffName" runat="server">Staff Full Name</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblCourseList" runat="server">List of Courses</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-3" style="margin-top: 9px;">
                                            <p class="card-description">Foundation Courses</p>
                                            <hr />
                                            <asp:CheckBoxList ID="cbFoundation" runat="server" CssClass="checkBox">
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                        </div>
                                        <div class="col-md-3">
                                            <p class="card-description">Diploma Courses</p>
                                            <hr />
                                            <asp:CheckBoxList ID="cbDiploma" runat="server" CssClass="checkBox">
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                        </div>
                                        <div class="col-md-3">
                                            <p class="card-description">Bachelor Degree Courses</p>
                                            <hr />
                                            <asp:CheckBoxList ID="cbDegree" runat="server" CssClass="checkBox">
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                                <asp:ListItem>testing</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>


                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" />
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                            <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" Style="margin-left: 10px; height: 38px;" />
                                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn mr20 pdForm" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this Programme ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn mr20 pdForm" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this Programme ?" TargetControlID="btnDelete" />
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
