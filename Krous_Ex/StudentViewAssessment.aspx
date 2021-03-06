<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentViewAssessment.aspx.cs" Inherits="Krous_Ex.StudentViewAssessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblViewAssessment" runat="server">All Assessments</asp:Label>
                        </h3>
                        <p class="card-description"></p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Literal ID="litAssessment" runat="server"></asp:Literal>

    <div class="col-lg-12 stretch-card">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <div class="gv-section text-center">
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="You have no assessment yet!"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<div class="row">
        <div class="col-md-12">
            <div class="col-sm-4 grid-margin stretch-card float-left" style="margin-top: 30px;">
                <div class="card">
                    <div class="card-body">
                        <h5></h5>
                        <h5>Krysty Woon</h5>
                        <hr />
                        <div class="row">
                            <div class="col-sm-12 col-xl-12 my-auto">
                                <div class="d-flex d-sm-block align-items-center">
                                    <h4 class="mb-0"><i class="fas fa-tasks" style="margin-right: 6px;"></i>Assessment Title 1</h4>
                                </div>
                                <hr />
                                <h6 class="text-muted font-weight-normal">
                                    <p>Created : </p>
                                </h6>
                                <h6 class="text-muted font-weight-normal">
                                    <p>Due Date : </p>
                                </h6>
                                <hr />
                                <div class="row">
                                    <div class="col-sm-12 col-xl-12 my-auto">
                                        <h6 class="text-muted font-weight-normal float-right">Submitted</h6>
                                    </div>
                                </div>

                                <a href="#" class="btn btn-primary float-right" style="width: 35%">View</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

</asp:Content>
