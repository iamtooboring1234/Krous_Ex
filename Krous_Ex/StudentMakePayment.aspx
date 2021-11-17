<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentMakePayment.aspx.cs" Inherits="Krous_Ex.StudentMakePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://www.paypal.com/sdk/js?client-id=AUR-pzxfxl4dk2hZgmY6EAik0tl9Wxd1GHBAO9_NhC3wbExE2tx-K3kKs9MqoJ2jtkhy5RnjzuQr3aiE&components=buttons&currency=MYR"></script>


    <script>
        //let total = document.getElementById("head_amount");
        paypal.Buttons({

            // Set up the transaction
            createOrder: function (data, actions) {
                // This function sets up the details of the transaction, including the amount and line item details.
                return actions.order.create({
                    purchase_units: [{

                        amount: {
                            value:'<%=lblTotalPrice.Text%>', currency: 'MYR'

                        }
                    }]
                });
            },

            // Finalize the transaction
            onApprove: function (data, actions) {
                return actions.order.capture().then(function (details) {
                    // Successful capture! For demo purposes:
                    alert("Your payment has been made successfully!");

                    var button = document.getElementById("<%=hiddenBtn.ClientID%>");
                    setTimeout(function () {
                        button.click();
                        // Something you want delayed.

                    }, 1000);
                });
            },

            //onCancel: function (data) {
            //    // Show a cancel page, or return to cart
            //    window.history.go(-1)
            //}
        }).render('#paypal-button-container');
    </script>


    <script type="text/javascript" src="https://github.com/niklasvh/html2canvas/releases/download/v1.0.0-alpha.8/html2canvas.min.js"></script>
    <script type="text/javascript">
        function ConvertToImage(btnPrintPayment) {
            html2canvas($("#divPaymentDetails")[0]).then(function (canvas) {
                var label = document.getElementById("<%=lblPaymentReferenceNo.ClientID %>").innerText;
                var base64 = canvas.toDataURL();
                $("[id*=hfImagePayment]").val(base64);
                __doPostBack(btnPrintPayment.name, label);
            });
            return false;
        }
    </script>



    <style>
        table, th {
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div id="divPaymentDetails">
                <div class="card-body">
                    <div style="padding: 20px;">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class=" pdForm">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblReference" runat="server" Style="float: right; font-size: 15px;">Reference No.</asp:Label><br />
                                            <asp:Label ID="lblPaymentReferenceNo" runat="server" Style="float: right; font-size: 13px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class=" pdForm">
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: center; margin-top: 14px;">
                                            <asp:Label ID="lblTitle" runat="server" Style="font-size: 26px;">Krous Ex Learning Education</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class=" pdForm">
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: center; margin-top: 12px;">
                                            <asp:Label ID="lblStudentBill" runat="server" Style="font-size: 20px;">STUDENT BILL</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <hr style="border: 1px solid #0066CC;" />
                                <div class="row" style="padding-left: 84px;">
                                    <div class="col-lg-12" style="font-family: system-ui;">
                                        <div class="form-group pdForm">
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblName" runat="server">Name</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblContact" runat="server">Contact No.</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-5">
                                                    <asp:Label ID="lblContactNumber" runat="server"></asp:Label>
                                                </div>

                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblIC" runat="server">IC Number</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblICNumber" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblAcademic" runat="server">Academic Year</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-5">
                                                    <asp:Label ID="lblAcaYear" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblYS" runat="server">Semester</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblYearSem" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblProgrammeName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end of form-group pdform-->
                                    </div>
                                </div>

                                <!--display course-->
                                <asp:Literal ID="litPayment" runat="server"></asp:Literal>
                                <div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-8 ml-5">
                                            <div class="d-flex" style="margin-left: 67px;">
                                                <span class="font-weight-bold"><strong>
                                                    <asp:Label ID="lblTotal" runat="server">Total Amount:</asp:Label></strong></span>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="d-flex" style="margin-left: 68px;">
                                                <span class="font-weight-bold"><strong>
                                                    <asp:Label ID="lblTotalPrice" runat="server"></asp:Label></strong></span>
                                                <%--<input name="hdnAmount" type="hidden" id="amount" value="" runat="server" clientidmode="static" />     --%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr style="border: 1px solid #0066CC;" />
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:Literal ID="litDate" runat="server"></asp:Literal>
                                        <%--<asp:Label ID="lblOverdue" runat="server"></asp:Label>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--end of divPaymentDetails-->

            <div class="card-body">
                <div class="row" style="padding: 0px 30px 0px 30px">
                    <div class="col-md-3">
                        <asp:HiddenField ID="hfImagePayment" runat="server" />
                        <asp:Button ID="btnPrintPayment" type="button" CssClass="btn btn-outline-secondary btn-fw" Style="height: 31px;" runat="server" Text="Download bill as image" OnClientClick="return ConvertToImage(this)" OnClick="btnPrintPayment_Click" />
                    </div>
                    <div class="col-md-9">
                        <asp:Label ID="Label1" runat="server" Style="padding-left: 403px; font-size: 19px;">Checkout :</asp:Label>
                    </div>
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-9">
                        <!--paypal button-->
                        <div>
                            <div id="paypal-button-container" style="float: right; width: 50%;"></div>
                        </div>
                    </div>
                </div>
            </div>

            <!--hidden field-->
            <div style="display: none">
                <asp:Button ID="hiddenBtn" runat="server" OnClick="hiddenBtn_Click" OnClientClick="javascript:update();" />
            </div>
        </div>
    </div>
</asp:Content>
