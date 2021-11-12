<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentExaminationSubmission.aspx.cs" Inherits="Krous_Ex.StudentExaminationSubmission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblExaminationSubmission" runat="server">Examination Submission</asp:Label>
                        </h3>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class=" pdForm">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblCourseTitle" runat="server" Style="font-size: 30px;"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblQuestionPaper" runat="server">Question Paper </asp:Label>
                                </div>
                                <div class="col-md-8 col-form-label">
                                    <asp:HyperLink ID="hlQuestionPaper" Target="_blank" runat="server"></asp:HyperLink>
                                    <asp:LinkButton ID="lbQuestionPaper" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                    <asp:Label ID="lblNoQuestionPaper" runat="server"></asp:Label>
                                    <%--<asp:Button ID="btnDownloadIC" runat="server" Text="download" />--%>
                                    <%--<asp:TextBox ID="txtIc" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>--%>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblAnswerSheet" runat="server">Answer Sheet </asp:Label>
                                </div>
                                <div class="col-md-8 col-form-label">
                                    <asp:HyperLink ID="hlAnswerSheet" Target="_blank" runat="server"></asp:HyperLink>
                                    <asp:LinkButton ID="lbAnswerSheet" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                    <%--<asp:Button ID="btnDownloadIC" runat="server" Text="download" />--%>
                                    <%--<asp:TextBox ID="txtIc" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12 mt-4">
        <div class="card">
            <div class="card-body">
                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-9">
                            <asp:Label ID="lblYourWork" runat="server" Style="font-size: 23px;">Your Submission</asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblStatus" runat="server" CssClass="float-right text-warning" Text="Assigned"></asp:Label>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="form-group pdForm" style="font-family: system-ui; font-size: 18px;">
                        <div class="row">

                            <div class="col-md-2 col-form-label">
                                <asp:Label ID="Label1" runat="server">Upload your file here </asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="form-group pdForm" style="font-family: system-ui; font-size: 18px;">
                        <div class="row">
                            <div class="col-md-2 col-form-label">
                                <asp:Label ID="Label3" runat="server">Last uploaded file </asp:Label>
                            </div>
                            <div class="col-md-8 col-form-label">
                                <asp:HyperLink ID="hlPreviousFile" Target="_blank" runat="server"></asp:HyperLink>
                                <asp:LinkButton ID="lbPreviousFile" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel3" runat="server">
                    <div class="form-group pdForm" style="border: 1px solid white">
                        <div class="row">
                            <div class="col-md-8">
                                <asp:HyperLink ID="hlSubmitFile" Target="_blank" runat="server"></asp:HyperLink>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <ajaxToolkit:AsyncFileUpload runat="server" ID="FileUploadAnswer" Style="font-size: 16px;" Width="100%" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-8">
                            <p style="color: red">Be careful on resubmitting the file, you may be turned in late.**</p>
                        </div>
                    </div>
                </div>

                <div class="form-group pdForm">
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <asp:Button Text="Submit" ID="btnSubmit" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSubmit_Click" />
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

    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
