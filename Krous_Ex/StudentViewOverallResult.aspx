<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentViewOverallResult.aspx.cs" Inherits="Krous_Ex.StudentViewOverallResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <style type="text/css">
        table.OverallResultTable {
            table-layout: fixed;
        }

        table.OverallResultTable td, table.OverallResultTable th {
            white-space: normal !important;
            word-wrap: break-word;
        }

        table.OverallResultTable tr:hover td {
            color: black;
            background-color: white;
        }

        @media only screen and (max-width: 480px) {
            .hidden-480 {
                display: none !important;
            }
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
                            <asp:Label ID="lblSemesterResult" runat="server">My Overall Result</asp:Label>
                        </h3>
                    </div>
                </div>

                <hr />

                <asp:Panel ID="Panel1" runat="server">
                    <div class="table-responsive">

                        <asp:Literal ID="litSemesterResult" runat="server"></asp:Literal>

                    </div>
                </asp:Panel>
                <div class="row align-items-center">
                    <div class="col-md-12 text-right mt-3">
                        <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
