<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExaminationReleaseResult.aspx.cs" Inherits="Krous_Ex.ExaminationReleaseResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblExaminationReleaseResult" runat="server">Release Examination Result</asp:Label>
                        </h3>
                        <p class="card-description"><span class="text-warning mr-2"><i class="fas fa-exclamation-circle"></i></span><span class="text-danger">WARNING: </span>
                            Releasing the result will update all student to next the semester and session. Please <span class="text-danger">ENSURE</span> that all examination result is successfully entered.
                        </p>
                    </div>
                </div>
                <hr />
                <asp:Button Text="Release" ID="btnRelease" runat="server" Width="18%" CssClass="btn btn-danger pr-4 pl-4 pt-2 pb-2" OnClick="btnRelease_Click" />
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to release the result ?" TargetControlID="btnRelease" />

            </div>
        </div>
    </div>

     <script src="Assests/main/js/toastDemo.js"></script>

</asp:Content>
