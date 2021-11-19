<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ProgrammeCourseEntry.aspx.cs" Inherits="Krous_Ex.ProgrammeCourseEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <link href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="Label8" runat="server">Programme Course</asp:Label>
                        </h3>
                        <p class="card-description m-0">Form to insert programme course </p>
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
                        <p class="card-description"><strong>Step 1:</strong> Select Programme & Semester </p>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label3" runat="server">Programme Category </asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgrammCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProgrammCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label1" runat="server">Programme Name </asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label2" runat="server">Semester </asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label4" runat="server">Session </asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSessionMonth" runat="server" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlSessionMonth_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger p-2" Width="18%" Visible="false" OnClick="btnCancel_Click"/>
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
