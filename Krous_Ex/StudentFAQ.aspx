<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentFAQ.aspx.cs" Inherits="Krous_Ex.StudentFAQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblStudentFAQ" runat="server">Help</asp:Label>
                        </h3>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 text-center">
                        <i style="color:#ffd815;" class="fas fa-question-circle fa-4x mb-2"></i>
                        <h2 class="text-center text-uppercase mb-2" style="color: #00AB9F">FAQs</h2>
                        <h3 class="text-center desc mb-3">Hava a question? Check our FAQs first!</h3>
                        <asp:Button ID="btnFAQ" runat="server" CssClass="btn btn-primary pl-5 pr-5 pt-2 pb-2" Text="Enter FAQs page" OnClick="btnFAQ_Click"/>
                    </div>
                    <div class="col-md-6 text-center">
                        <i style="color:#ffd815;" class="fas fa-comments fa-4x mb-2"></i>
                        <h2 class="text-center text-uppercase mb-2" style="color: #00AB9F">FAQs</h2>
                        <h3 class="text-center desc mb-3">Can't find answer? Contact us now!</h3>
                        <asp:Button ID="btnChat" runat="server" CssClass="btn btn-primary pl-5 pr-5 pt-2 pb-2" Text="Create a chat" OnClick="btnChat_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
