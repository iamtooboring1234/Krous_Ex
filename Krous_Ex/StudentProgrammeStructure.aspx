<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentProgrammeStructure.aspx.cs" Inherits="Krous_Ex.StudentProgrammeStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <style>
        .table td {
            border-top: 0;
            padding: 0.25em 0;
            vertical-align: top;
        }

        .table tr.font-size-11 td {
            font-size: 11px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblProgrammeStructure" runat="server">My Programme Structure</asp:Label>
                        </h3>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="table-responsive">
                        <table class="table">
                            <asp:Literal ID="litTest" runat="server"></asp:Literal>
                        </table>
                    </div>
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
