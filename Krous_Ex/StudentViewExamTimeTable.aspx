<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentViewExamTimeTable.aspx.cs" Inherits="Krous_Ex.StudentViewExamTimeTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFAQEntry" runat="server">My Examination Timetable</asp:Label>
                        </h3>
                    </div>
                </div>
                <hr />
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Label ID="lblNotYet" runat="server" CssClass="text-danger" Visible="false" Text=""></asp:Label>
                    <asp:Literal ID="litExamTime" runat="server"></asp:Literal>
                </asp:Panel>
            </div>
        </div>
        <%--                                <div class="row align-items-center">
                <div class="col-md-12">
                    <asp:Button ID="btnExport" runat="server" Text="Button" OnClick="btnExport_Click" />
                </div>
            </div>--%>
    </div>

</asp:Content>
<%--<div class="row">
    <div class="col-md-8 grid-margin stretch-card">
        <div class="d-flex flex-row">
            <div class="mr-3 text-center ml-3">
                <div class="p-4 border border-primary">
                    <p class="m-0">18</p>
                    <p class="m-0">Jan</p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 align-self-center">
                    <div class="d-flex flex-column">
                    <h6>BACS2103 SOFTWARE QUALITY ASSURANCE AND TESTING</h6>
                    <p class="m-0 text-info">9:00 AM to 12:00 PM</p></div>
                </div>
            </div>
        </div>
    </div>
</div>--%>