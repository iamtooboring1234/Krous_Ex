<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentViewTimeTable.aspx.cs" Inherits="Krous_Ex.StudentViewTimeTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />
    <style type="text/css">

        table.tableTimetable tr:hover td {
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
                                        <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
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
                                    <td colspan="2" width="18" align="center" valign="middle">10:00</td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr align="center">
                                    <td>Mon
                                        <br>
                                        <span class="small">2021-11-01
                                            <div class="red"></div>
                                        </span></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td colspan="2">
                                        <div data-rel="tooltip" title="" data-original-title="SOFTWARE QUALITY ASSURANCE AND TESTING (Tutorial ) Bavani A/P Raja Pandian 
  (B100D, Block B)">
                                            <span class="small">BACS2103 (T) </span>
                                            <br>
                                            <span class="small">B100D</span><br>
                                            <span class="small">Bavani</span><br>
                                            <span style="font-size: 10px">12:00 PM - 1:00 PM</span>
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        <div data-rel="tooltip" title="" data-original-title="SOFTWARE QUALITY ASSURANCE AND TESTING (Practical ) Bavani A/P Raja Pandian 
  (B100D, Block B)">
                                            <span class="small">BACS2103 (P) </span>
                                            <br>
                                            <span class="small">B100D</span><br>
                                            <span class="small">Bavani</span><br>
                                            <span style="font-size: 10px">1:00 PM - 2:00 PM</span>
                                        </div>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr align="center">
                                    <td>Tue
                                        <br>
                                        <span class="small">2021-11-02
                                            <div class="red"></div>
                                        </span></td>
                                    <td></td>
                                    <td></td>
                                    <td colspan="4">
                                        <div data-rel="tooltip" title="" data-original-title="SOFTWARE QUALITY ASSURANCE AND TESTING (Lecture ) Lim Fung Ji 
  (K201, Block K)">
                                            <span class="small">BACS2103 (L) </span>
                                            <br>
                                            <span class="small">K201</span><br>
                                            <span class="small">Lim Fung Ji</span><br>
                                            <span style="font-size: 10px">9:00 AM - 11:00 AM</span>
                                        </div>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr align="center">
                                    <td>Wed
                                        <br>
                                        <span class="small">2021-11-03
                                            <div class="red"></div>
                                        </span></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td colspan="4">
                                        <div data-rel="tooltip" title="" data-original-title="HUBUNGAN ETNIK (Blended ) Najwa Binti Mazlan 
  (K205, Block K)">
                                            <span class="small">MPU-3113 (B) </span>
                                            <br>
                                            <span class="small">K205</span><br>
                                            <span class="small">Najwa Binti Mazlan</span><br>
                                            <span style="font-size: 10px">11:00 AM - 1:00 PM</span>
                                        </div>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td align="center">Thu<br>
                                        <span class="small">2021-11-04</span><div class="small red">Deepavali</div>
                                    </td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="center">Fri<br>
                                        <span class="small">2021-11-05</span><div class="small red"></div>
                                    </td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="center">Sat<br>
                                        <span class="small">2021-11-06</span><div class="small red"></div>
                                    </td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="center">Sun<br>
                                        <span class="small">2021-11-07</span><div class="small red"></div>
                                    </td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                    <td colspan="1" width="18" align="center" valign="middle"></td>
                                </tr>

                            </tbody>
                            <asp:Literal ID="litTest" runat="server"></asp:Literal>
                        </table>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>
