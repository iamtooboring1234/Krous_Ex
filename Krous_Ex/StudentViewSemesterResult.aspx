<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentViewSemesterResult.aspx.cs" Inherits="Krous_Ex.StudentViewSemesterResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <style type="text/css">
        table.SemesterResultTable {
            table-layout: fixed;
        }

        table.SemesterResultTable td {
            white-space: normal !important;
            word-wrap: break-word;
        }

        table.SemesterResultTable tr:hover td {
            color: black;
            background-color: white;
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
                            <asp:Label ID="lblSemesterResult" runat="server">My Semester Result</asp:Label>
                        </h3>
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
                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <hr />

                <asp:Panel ID="Panel1" runat="server">
                    <div class="table-responsive">
                        <table class="table table-bordered table-hover SemesterResultTable">
                            <asp:Literal ID="litSemesterResult" runat="server"></asp:Literal>
                        </table>
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
