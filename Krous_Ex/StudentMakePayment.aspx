<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentMakePayment.aspx.cs" Inherits="Krous_Ex.StudentMakePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://www.paypal.com/sdk/js?client-id=AcFUzJGf-ncsAe35gb2ygkQ95BCP-_z31R1nXQk_3bxELTWx6qGky1GyKVQSPmjYGB_nzMTWITTtXdIE&components=buttons&currency=MYR&locale=en_MY"></script>
    <script>

        let total = document.getElementById("head_lblTotalAmount")?.innerText.replace("RM", "");

        paypal.Buttons({
            style: {
                layout: 'vertical',
                color: 'blue',
                shape: 'rect',
                label: 'paypal'
            },
            createOrder: function (data, actions) {
                // This function sets up the details of the transaction, including the amount and line item details.
                return actions.order.create({
                    purchase_units: [{
                        amount: {
                            value: '0.01'
                        }
                    }]
                });
            },
            onApprove: function (data, actions) {
                // This function captures the funds from the transaction.
                return actions.order.capture().then(function (details) {
                    // This function shows a transaction success message to your buyer.
                    alert("Your payment has been made successfully!");

                    //var button = document.getElementById('MainContent_hiddenBtn');
                    //setTimeout(function () {
                    //    button.click();

                    //    // Something you want delayed.

                    //}, 1000);
                });
            },
            onCancel: function (data) {
                // Show a cancel page, or return to cart
                window.history.go(-1)
            }

        }).render('#paypal-button-container');
    </script>

    <script type="text/javascript">
        function ConvertToImage(btnExport) {
            html2canvas($("#divPaymentDetails")[0]).then(function (canvas) {
               <%-- var label = document.getElementById("<%=lblOrderNo.ClientID %>").innerText;--%>
                var base64 = canvas.toDataURL();
                $("[id*=hfImageData]").val(base64);
                __doPostBack(btnExport.name, label);
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
                                        <div class="col-md-12" style="text-align: center; margin-top: 14px;">
                                            <asp:Label ID="lblAssessmentTitle" runat="server" Style="font-size: 26px;">Krous Ex Learning Education</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class=" pdForm">
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: center; margin-top: 12px;">
                                            <asp:Label ID="Label1" runat="server" Style="font-size: 20px;">STUDENT BILL</asp:Label>
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
                                                    <asp:Label ID="lblStudentName" runat="server">WOON CUI YEN</asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblContact" runat="server">Contact No.</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-5">
                                                    <asp:Label ID="lblContactNumber" runat="server">01126085647</asp:Label>
                                                </div>

                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblIC" runat="server">IC Number</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblICNumber" runat="server">001225-14-0368</asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblAcademic" runat="server">Academic Year</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-5">
                                                    <asp:Label ID="lblAcaYear" runat="server">2021/22</asp:Label>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblYS" runat="server">Semester</asp:Label>
                                                </div>
                                                <p>:</p>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblYearSem" runat="server">Year 3 Semester 2</asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblProgrammeName" runat="server">BACHELOR OF INFORMATION TECHNOLOGY (HONOURS) IN SOFTWARE SYSTEMS DEVELOPMENT</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end of form-group pdform-->
                                    </div>
                                </div>

                                <!--display course-->
                                <asp:Literal ID="litPayment" runat="server"></asp:Literal>
                                <div style="margin-top: 20px;">
                                    <div style="padding: 0px 50px 0px 50px">
                                        <table class="table table-clear">
                                            <tbody>
                                                <tr>
                                                    <th colspan="2" style="width: 80%; font-size: 15px;">Tuition Fee</th>
                                                    <th style="font-size: 15px;">Amount(RM)</th>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%">1</td>
                                                    <td>gfgf</td>
                                                    <td>50.00</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%">2</td>
                                                    <td>gfgf</td>
                                                    <td>50.00</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%">3</td>
                                                    <td>gfgf</td>
                                                    <td>50.00</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%">4</td>
                                                    <td>gfgf</td>
                                                    <td>50.00</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%">5</td>
                                                    <td>gfgf</td>
                                                    <td>50.00</td>
                                                </tr>


                                                <tr>
                                                    <td></td>
                                                    <th style="font-size: 17px;">Total Amount :</th>
                                                    <th style="font-size: 17px;"><span style="color:limegreen">250.00</span></th>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <hr style="border: 1px solid #0066CC;" />
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:Label ID="Label2" runat="server">PLEASE MAKE SURE YOU PAY THIS BILL BY <strong style="color:red">10-12-2021</strong></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--end of divPaymentDetails-->
            </div>

            <div class="card-body">
                <div class="row" style="padding:0px 30px 0px 30px">
                    <div class="col-md-5">
                        <asp:Button ID="btnPrintPayment" type="button" CssClass="btn btn-inverse-primary btn-fw" style="height:31px; margin-top:12px;" runat="server" Text="Download bill as image" /> 
                    </div>
                    <div class="col-md-7">
                        <!--paypal button-->
                        <div>
                            <div id="paypal-button-container" class="col-md-4 mt-3" style="float: right;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
