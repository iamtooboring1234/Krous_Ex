<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExaminationPrePreparationEntry.aspx.cs" Inherits="Krous_Ex.ExaminationPrePreparationEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFAQList" runat="server" Font-Size="large">Examination Preparation</asp:Label>
                        </h3>
                        <p class="card-description">To pre-prepare the examination material </p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblSession" runat="server">Current Session </asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtSession" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hdSession" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblExamination" runat="server">Examination </asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlExamination" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamination_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblExamTime" runat="server">Examination Time </asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtExamStartTime" CssClass="timepickerstart form-control" runat="server" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="input-group-addon col-form-label mx-4">to</div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtExamEndTime" CssClass="timepickerend form-control" runat="server" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label3" runat="server">Question Paper Upload </asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <ajaxToolkit:AsyncFileUpload ID="FileUploadQuestion" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label2" runat="server">Answer Sheet Upload </asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <ajaxToolkit:AsyncFileUpload ID="FileUploadAnswerSheet" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12 float-right text-right">
                                    <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to upload this question paper and answer sheet?" TargetControlID="btnSave" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
