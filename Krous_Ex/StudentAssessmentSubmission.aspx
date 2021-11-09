<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentAssessmentSubmission.aspx.cs" Inherits="Krous_Ex.StudentAssessmentSubmission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/vendors/dropify/dropify.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <%-- <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblCourseIncEntry" runat="server">Assessment Submission</asp:Label>
                        </h3>
                    </div>
                </div>
                <hr />--%>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class=" pdForm">
                            <div class="row" style="margin-left: 20px;">
                                <div>
                                    <asp:Image ID="assessmentIcon" runat="server" ImageUrl="~/Assests/main/img/assessment.png" Height="57px" Width="60px" />
                                </div>
                                <div class="col-md-8" style="padding-top: 8px;">
                                    <asp:Label ID="lblAssessmentTitle" runat="server" Style="font-size: 30px;">Advanced Database Management Test</asp:Label>
                                </div>
                            </div>
                        </div>
                        <hr style="border: 1px solid #0066CC;" />
                        <div class="row">
                            <div class="col-lg-12" style="font-family: system-ui;">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblStaffName" runat="server" Style="font-size: 18px;">Lau Pin Jian</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-9">
                                            <asp:Label ID="lblCreatedDate" runat="server">Jul 22 11:30PM</asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblDueDate" runat="server" CssClass="float-right"><asp:Label runat="server" >Due</asp:Label> Jul 25 11:30 PM</asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="form-group pdForm">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblAssessmentDesc" runat="server">menggunakan Wu Yu (吴语) di Shanghai, sebahagian JiangSu dan ZheJiang, tetapi  sebahagian JiangSu dan sebahagian JiangSu dan ZheJiang, tetapi Wu ZheJiang, tetapi WuWu Yu sebenarnya berasal dari SuZhou. Selain itu, bahasa Kantonis merupakan dialek yang dikenali orang ramai di China. Walaupun hanya 5.6% penduduk yang menggunakan, tetapi amat s</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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

                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-9">
                            <asp:Label ID="lblYourWork" runat="server" Style="font-size: 23px;">Your Submission</asp:Label>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblStatus" runat="server" CssClass="float-right">Submitted</asp:Label>
                        </div>
                    </div>
                </div>
                <hr style="border: 1px solid #478778; margin-top: 19px;" />
                <div class="form-group pdForm" style="font-family: system-ui; font-size: 18px;">
                    <div class="row">
                        <div class="col-md-2 col-form-label">
                            <asp:Label ID="Label1" runat="server">Upload your file here </asp:Label>
                        </div>
                    </div>
                </div>
                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-8">
                            <asp:HyperLink ID="hlSubmitFile" Target="_blank" runat="server"></asp:HyperLink>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Style="font-size: 16px;" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>


               <%-- <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-12 float-right text-right">
                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" />
                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" />
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>



 <%--   <script src="Assests/main/js/dropify.js"></script>
    <script src="Assests/main/vendors/dropify/dropify.min.js"></script>
    <script src="Assests/main/vendors/js/vendor.bundle.base.js"></script>--%>
</asp:Content>
