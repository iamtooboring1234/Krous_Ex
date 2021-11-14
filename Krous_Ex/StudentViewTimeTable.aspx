<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentViewTimeTable.aspx.cs" Inherits="Krous_Ex.StudentViewTimeTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />
    <style type="text/css">
        table.tableTimetable tr:hover td {
            color: black;
            background-color: white;
        }
    </style>

    <script>
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    </script>

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
                <hr />
                <asp:Panel ID="Panel1" runat="server">
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group pdForm">
                                <div class="row justify-content-center">
                                    <div class="col-md-12 col-form-label">
                                        <asp:Label ID="lblSemester" runat="server"> </asp:Label><span style="color: red;">*</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group pdForm">
                                <div class="row justify-content-center">
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <table class="table table-hover table-bordered tableTimetable">
                            <thead>
                                <tr>
                                    <td>Date/Time</td>
                                    <td colspan="2" width="18" align="center" valign="middle">08:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">09:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">10:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">11:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">12:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">01:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">02:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">03:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">04:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">05:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">06:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">07:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">08:00</td>
                                    <td colspan="2" width="18" align="center" valign="middle">09:00</td>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="litTest" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>
