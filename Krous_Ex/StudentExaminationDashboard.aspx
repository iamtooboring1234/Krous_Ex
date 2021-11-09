<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentExaminationDashboard.aspx.cs" Inherits="Krous_Ex.StudentExaminationDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    
    <div class="row">
        <div class="col-lg-12 grid-margin">
            <div class=" col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h3 style="margin-left: 20px;">
                                    <i class="fas fa-tachometer-alt"></i>
                                    <asp:Label ID="lblFAQList" runat="server" Font-Size="large">Examination Dashboard</asp:Label>
                                </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFacultyList" runat="server" Font-Size="large">Examination Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Examination </p>
                        <asp:Label ID="lblNoExam" runat="server" Text="Currently, there is no examinations are listed." Visible="false" CssClass="mt-3"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

<%--    <div class="row">
        <div class="col-md-12">
            <div class="col-sm-4 grid-margin stretch-card float-left" style="margin-top: 30px;">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-12 col-xl-12 my-auto">
                                <div class="d-flex d-sm-block align-items-center">
                                    <h4 class="mb-0"><i class="fas fa-edit mr-2"></i>Assessment Title 1</h4>
                                </div>
                                <hr />
                                <h6 class="text-muted font-weight-normal">
                                    <p class="mb-1" style="color: white">Exam Start Time : </p>
                                    Created Date
                                </h6>
                                <h6 class="text-muted font-weight-normal">
                                    <p class="mb-1" style="color: white">Exam End Time : </p>
                                    Created Date
                                </h6>
                                <hr />
                                <div class="d-flex d-sm-block align-items-center">
                                    <p><i class="fas fa-check-circle mr-2" style="color:#00d25b"></i>Question Paper Uploaded</p>
                                    <p><i class="fas fa-check-circle mr-2" style="color:#00d25b"></i>Answer Sheet Provided</p>
                                </div>
                                <hr />
                                <div class="row float-right">
                                    <a href="#" class="btn btn-primary mr-2">Manage</a>
                                    <a href="#" class="btn btn-primary">Mark Attendance</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <asp:Literal ID="litExamination" runat="server"></asp:Literal>

</asp:Content>
