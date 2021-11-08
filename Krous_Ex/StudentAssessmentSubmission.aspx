<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentAssessmentSubmission.aspx.cs" Inherits="Krous_Ex.StudentAssessmentSubmission" %>

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
                            <asp:Label ID="lblCourseIncEntry" runat="server">Assessment Submission</asp:Label>
                        </h3>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <center>
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Image ID="assessmentIcon" runat="server" ImageUrl="~/Assests/main/img/assessment.png" Height="60px" Width="60px" />
                                    <asp:Label ID="lblAssessmentTitle" runat="server" style="font-size:23px;"></asp:Label>
                                </div>
                            </div>
                            </center>
                        </div>

                        <!--search-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                </div>
                                <div class="col-md-10">
                                </div>
                            </div>
                        </div>


                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgCategory" runat="server">Programme Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgCategory" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
