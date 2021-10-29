<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StaffCourseIncEntry.aspx.cs" Inherits="Krous_Ex.StaffCourseIncEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .checkBox {
            padding-left: 20px;
        }

            .checkBox label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

                .checkBox label::before {
                    content: "";
                    display: inline-block;
                    position: absolute;
                    width: 17px;
                    height: 17px;
                    left: 0;
                    margin-left: -20px;
                    border: 1px solid #cccccc;
                    border-radius: 3px;
                    background-color: #fff;
                    -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                }

                .checkBox label::after {
                    display: inline-block;
                    position: absolute;
                    width: 16px;
                    height: 16px;
                    left: 0;
                    top: 0;
                    margin-left: -20px;
                    padding-left: 3px;
                    padding-top: 1px;
                    font-size: 11px;
                    color: #555555;
                }

            .checkBox input[type="checkbox"] {
                opacity: 0;
                z-index: 1;
            }

                .checkBox input[type="checkbox"]:checked + label::after {
                    font-family: "FontAwesome";
                    content: "\f00c";
                }

        .checkBox-primary input[type="checkbox"]:checked + label::before {
            background-color: #337ab7;
            border-color: #337ab7;
        }

        .checkBox-primary input[type="checkbox"]:checked + label::after {
            color: #fff;
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
                            <asp:Label ID="lblCourseIncEntry" runat="server">Course In-charge Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert the course in-charge by respective staff</p>
                    </div>
                </div>
                <hr />
                <%--<asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblStaffName" runat="server">Staff Full Name</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!--search-->
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                </div>
                                <div class="col-md-10">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="18%" CssClass="btn btn-primary p-2" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" Width="18%" CssClass="btn btn-dark p-2" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvStaffSearch" runat="server" Width="100%" CssClass="table table-bordered tableForum" AutoGenerateColumns="False" DataKeyNames="StaffGUID" CellPadding="10" CellSpacing="2" Border="0">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" onclick="Check_Click(this)" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                                <HeaderStyle CssClass="text-center" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StaffGUID" HeaderText="StaffGUID" ReadOnly="true" SortExpression="StaffGUID" Visible="false" />
                                            <asp:BoundField DataField="StaffFullName" HeaderText="Staff Full Name" SortExpression="StaffFullName" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle BackColor="" HorizontalAlign="Center" />
                                    </asp:GridView>
                                    <asp:Label ID="Label1" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No FAQ Record Found !"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgCategory" runat="server">Programme Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProgCategory_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>



    <asp:Panel ID="Panel1" runat="server">
        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="col-lg-12 mt-3">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Literal ID="litStep2" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="panel-body ">
                                <div class="table-responsive">
                                    <div class="gv-section gv-staff text-center">
                                        <asp:GridView ID="gvCourse" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-bordered" Border="0"
                                            DataKeyNames="CourseGUID" CellPadding="10" CellSpacing="2" OnSelectedIndexChanged="OnSelectedIndexChanged1">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton Text="Add" ID="lnkAdd" runat="server" CommandName="Select" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CourseGUID" HeaderText="CourseGUID" SortExpression="CourseGUID" ItemStyle-CssClass="d-none" ReadOnly="true" HeaderStyle-CssClass="d-none" />
                                                <asp:BoundField DataField="CourseAbbrv" HeaderText="Course Code" SortExpression="CourseAbbrv" />
                                                <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
                                                <asp:BoundField DataField="CreditHour" HeaderText="Credit Hour" SortExpression="CreditHour" />
                                                <asp:BoundField DataField="ProgrammeCategory" HeaderText="ProgrammeCategory" SortExpression="ProgrammeCategory" ItemStyle-CssClass="d-none" ReadOnly="true" HeaderStyle-CssClass="d-none" />
                                                <asp:BoundField DataField="ProgrammeGUID" HeaderText="ProgrammeGUID" SortExpression="ProgrammeGUID" ItemStyle-CssClass="d-none" ReadOnly="true" HeaderStyle-CssClass="d-none" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Record Found !"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 mt-3">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Literal ID="litStep3" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="panel-body ">
                                <div class="table-responsive">
                                    <div class="gv-section gv-staff text-center">
                                        <asp:GridView ID="gvSelectedCourse" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="CourseGUID"
                                            Border="0" CellPadding="10" CellSpacing="2" OnSelectedIndexChanged="OnSelectedIndexChanged2">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton Text="Remove" ID="lnkRemove" runat="server" CommandName="Select" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CourseGUID" HeaderText="CourseGUID" SortExpression="CourseGUID" ItemStyle-CssClass="d-none" ReadOnly="true" HeaderStyle-CssClass="d-none" />
                                                <asp:BoundField DataField="CourseAbbrv" HeaderText="Course Code" SortExpression="CourseAbbrv" />
                                                <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
                                                <asp:BoundField DataField="CreditHour" HeaderText="Credit Hour" SortExpression="CreditHour" />
                                                <asp:BoundField DataField="ProgrammeCategory" HeaderText="ProgrammeCategory" SortExpression="ProgrammeCategory" ItemStyle-CssClass="d-none" ReadOnly="true" HeaderStyle-CssClass="d-none" />
                                                <asp:BoundField DataField="ProgrammeGUID" HeaderText="ProgrammeGUID" SortExpression="ProgrammeGUID" ItemStyle-CssClass="d-none" ReadOnly="true" HeaderStyle-CssClass="d-none" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:Literal ID="litExist" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group pdForm">
                                        <div class="row">
                                            <div class="col-md-12 mt-5 text-right">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary p-2" Width="18%" OnClick="btnSubmit_Click" Visible="false" />
                                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to SUBMIT this ?" TargetControlID="btnSubmit" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger p-2" Width="18%" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"></asp:AsyncPostBackTrigger>--%>
                <asp:AsyncPostBackTrigger ControlID="gvCourse"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="gvSelectedCourse"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript">

        function CheckGridView() {
            document.cookie = "GridViewRefresh=Yes;";
        }
        window.onbeforeunload = CheckGridView;

    </script>
</asp:Content>
