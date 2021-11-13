<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentAssessmentSubmission.aspx.cs" Inherits="Krous_Ex.StudentAssessmentSubmission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/vendors/dropify/dropify.min.css" rel="stylesheet" />

    <style>
        .linkButton {
            padding: 5px 10px;
            text-decoration: none;
            border: solid 1px black;
            background-color: #404040;
            border-radius: 8px;
        }

            .linkButton:hover {
                border: solid 1px Black;
                background-color: #E8E8E8;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class=" pdForm">
                            <div class="row" style="margin-left: 20px;">
                                <div>
                                    <asp:Image ID="assessmentIcon" runat="server" ImageUrl="~/Assests/main/img/assessment.png" Height="57px" Width="60px" />
                                </div>
                                <div class="col-md-8" style="padding-top: 8px;">
                                    <asp:Label ID="lblAssessmentTitle" runat="server" Style="font-size: 30px;"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <hr style="border: 1px solid #0066CC;" />
                        <div class="row">
                            <div class="col-lg-12" style="font-family: system-ui;">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblStaffName" runat="server" Style="font-size: 18px;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-9">
                                            <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblDueDate" runat="server" CssClass="float-right"></asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="form-group pdForm">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblAssessmentDesc" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group pdForm">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblAssessmentFile" runat="server"><i class="fa fa-file" style="margin-right:8px;"></i>Attachment</asp:Label>
                                            </div>
                                            <div class="col-sm-1">:</div>
                                            <div class="col-md-9">
                                                <asp:HyperLink ID="hlAssessmentFile" Target="_blank" runat="server"></asp:HyperLink>
                                                <asp:LinkButton ID="lbAssFileDownload" runat="server" CssClass="linkButton" Style="margin-left: 5px;"><i class="fas fa-download"></i></asp:LinkButton>
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

    <!--submission-->
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="form-group pdForm">
                    <div class="row" style="margin-left: 20px;">
                        <div>
                            <asp:Image ID="ImgSubmit" runat="server" ImageUrl="~/Assests/main/img/submitIcon.png" Height="50px" Width="53px" />
                        </div>
                        <div class="col-md-9" style="padding-top: 8px;">
                            <asp:Label ID="lblYourWork" runat="server" Style="font-size: 23px;">Your Submission</asp:Label>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-6">
                            <asp:Label ID="lblSubmitDate" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblStatus" runat="server" CssClass="float-right"><strong></strong></asp:Label>
                        </div>
                    </div>
                </div>
                <hr style="border: 1px solid #478778;" />
                <div class="form-group pdForm" style="font-family: system-ui; font-size: 18px;">
                    <div class="row">
                        <div class="col-md-12 col-form-label">
                            <asp:Label ID="lblUpload" runat="server">Upload your file here</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-8">
                            <asp:HyperLink ID="hlSubmitFile" Target="_blank" runat="server" Visible="false"></asp:HyperLink>
                            <asp:LinkButton ID="lbDownloadFile" runat="server" CssClass="linkButton" Visible="false" Style="margin-left: 5px;"><i class="fas fa-download"></i></asp:LinkButton>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                                <ContentTemplate>
                                    <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Style="font-size: 16px;" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-12 float-right text-right">
                            <asp:Button Text="Submit" ID="btnSubmit" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSubmit_Click" />
                            <asp:Button Text="Submit" ID="btnResubmit" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" Visible="false" OnClick="btnResubmit_Click" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to turn in your submission?" TargetControlID="btnSubmit" />
                            <asp:Button Text="Unsubmit" ID="btnUnSubmit" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" Visible="false" OnClick="btnUnSubmit_Click" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to unsubmit your submission?" TargetControlID="btnUnSubmit" />
                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%--   <script src="Assests/main/js/dropify.js"></script>
    <script src="Assests/main/vendors/dropify/dropify.min.js"></script>
    <script src="Assests/main/vendors/js/vendor.bundle.base.js"></script>--%>
</asp:Content>
