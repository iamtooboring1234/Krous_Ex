<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExaminationDashboard.aspx.cs" Inherits="Krous_Ex.ExaminationDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row">
        <div class="col-lg-12 grid-margin" >
            <div class=" col-md-12">
                <div class="card">
                    <div class="card-body" >
                        <div class="row">
                            <div class="col-md-12">
                                <h3 style="margin-left:20px;">
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

    <div class="row"> 
        <div class="col-lg-12">
            <div class="col-md-3 grid-margin stretch-card">
                <div class="card col-md-12">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-9">
                                <p style="margin:0">Courses</p>
                                <div class="d-flex align-items-center align-self-start col-md-8">
                                    <h3 style="margin:10px 0">1500</h3>
                                </div>
                            </div>
                            <div class="col-md-3 text-center">
                                <p></p>
                                <img src="Assests/main/img/online-course.png" width="48" height="48"/>
                            </div>
                        </div>                       
                    </div>
                </div>
            </div>
            <div class="col-md-3 grid-margin stretch-card">
                <div class="card col-md-12">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-9">
                                <p style="margin:0">Students</p>
                                <div class="d-flex align-items-center align-self-start col-md-8">
                                    <h3 style="margin:10px 0">1500</h3>
                                </div>
                            </div>
                            <div class="col-md-3 text-center">
                                <p></p>
                                <img src="Assests/main/img/graduated.png" width="48" height="48"/>
                            </div>
                        </div>                       
                    </div>
                </div>
            </div>
            <div class="col-md-3 grid-margin stretch-card">
                <div class="card col-md-12">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-9">
                                <p style="margin:0">Exam Created</p>
                                <div class="d-flex align-items-center align-self-start col-md-8">
                                    <h3 style="margin:10px 0">1500</h3>
                                </div>
                            </div>
                            <div class="col-md-3 text-center">
                                <p></p>
                                <img src="Assests/main/img/presentation.png" width="48" height="48"/>
                            </div>
                        </div>                       
                    </div>
                </div>
            </div>
            <div class="col-md-3 grid-margin stretch-card">
                <div class="card col-md-12">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-9">
                                <p style="margin:0">Students Enroll in Exams</p>
                                <div class="d-flex align-items-center align-self-start col-md-8">
                                    <h3 style="margin:10px 0">1500</h3>
                                </div>
                            </div>
                            <div class="col-md-3 text-center">
                                <p></p>
                                <img src="Assests/main/img/contract.png" width="48" height="48"/>
                            </div>
                        </div>                       
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12 grid-margin">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body" style="margin:10px 0">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-8">
                                    <i class="fas fa-book" style="margin-right: 10px"></i>
                                    <asp:Label ID="Label1" runat="server" Font-Size="large">Jump Into Exam Creation</asp:Label>
                                </div>
                                <div class="col-md-4 text-center">
                                    <a href="#" class="submit-btn">Create your exam</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
