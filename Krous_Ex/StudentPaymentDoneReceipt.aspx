<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentPaymentDoneReceipt.aspx.cs" Inherits="Krous_Ex.StudentPaymentDoneReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        #box-shadow {
            border: 1px solid;
            padding: 10px;
            box-shadow: 6px 8px #C27616;
        }
    </style>

     <script type="text/javascript" src="https://github.com/niklasvh/html2canvas/releases/download/v1.0.0-alpha.8/html2canvas.min.js"></script>
    <script type="text/javascript">
        function ConvertToImage(btnPrintReceipt) {
            html2canvas($("#paymentReceipt")[0]).then(function (canvas) {
                var label = document.getElementById("<%=lblReceiptNo.ClientID %>").innerText;
                var base64 = canvas.toDataURL();
                $("[id*=hfPaymentReceipt]").val(base64);
                __doPostBack(btnPrintReceipt.name, label);
            });
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div id="paymentReceipt">
                <div class="card-body">
                    <div style="padding: 20px;">
                        <div class="panel-body">
                            <div class="form-horizontal">
                              
                                <div class=" pdForm">
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: center; margin-top: 29px;">
                                            <asp:Label ID="lblTitle" runat="server" Style="font-size: 26px;">Krous Ex Learning Education</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class=" pdForm">
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: center; margin-top: 12px;">
                                            <asp:Label ID="lblStudentBill" runat="server" Style="font-size: 20px;"><strong>RECEIPT</strong></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm" style="margin-top: 20px;">
                                    <div class="row">
                                        <div class="col-lg-12" style="font-family: system-ui;">
                                            <div id="box-shadow" style="width: 80%; margin: auto">
                                                <div style="padding: 14px 20px 0px 20px">
                                                    <div class="form-group pdForm">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="lblReceipt" runat="server">Receipt Number</asp:Label>
                                                            </div>
                                                            <div class="col-sm-1">:</div>
                                                            <div class="col-md-5">
                                                                <asp:Label ID="lblReceiptNo" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group pdForm">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="Label1" runat="server">Payment Reference No.</asp:Label>
                                                            </div>
                                                            <div class="col-sm-1">:</div>
                                                            <div class="col-md-5">
                                                                <asp:Label ID="lblPaymentNo" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group pdForm">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="Label3" runat="server">Full Name</asp:Label>
                                                            </div>
                                                            <div class="col-sm-1">:</div>
                                                            <div class="col-md-5">
                                                                <asp:Label ID="lblStudFullName" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group pdForm">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="Label5" runat="server">Bill Amount</asp:Label>
                                                            </div>
                                                            <div class="col-sm-1">:</div>
                                                            <div class="col-md-5">
                                                                <asp:Label ID="lblBillAmt" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group pdForm">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="Label7" runat="server">Amount Paid</asp:Label>
                                                            </div>
                                                            <div class="col-sm-1">:</div>
                                                            <div class="col-md-5">
                                                                <asp:Label ID="lblAmountPaid" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group pdForm">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="Label9" runat="server">Paid On</asp:Label>
                                                            </div>
                                                            <div class="col-sm-1">:</div>
                                                            <div class="col-md-5">
                                                                <asp:Label ID="lblPaidOn" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group pdForm">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="Label11" runat="server">Receipt generated</asp:Label>
                                                            </div>
                                                            <div class="col-sm-1">:</div>
                                                            <div class="col-md-5">
                                                                <asp:Label ID="lblReceiptDate" runat="server"></asp:Label>
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
                </div>
            </div>
            <!--end of paymentReceipt-->

            <div class="card-body">
                <div class="row" style="float: right">
                    <div class="col-md-3">
                        <asp:HiddenField ID="hfPaymentReceipt" runat="server" />
                        <asp:Button ID="btnPrintReceipt" type="button" CssClass="btn btn-outline-secondary btn-fw" Style="height: 31px; margin-top: 12px;" runat="server" Text="Download receipt as image" OnClientClick="return ConvertToImage(this)" OnClick="btnPrintReceipt_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
