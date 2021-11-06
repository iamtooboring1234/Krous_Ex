<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ExaminationResultEntry.aspx.cs" Inherits="Krous_Ex.ExaminationResultEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFAQList" runat="server" Font-Size="large">FAQs Listing</asp:Label>
                        </h3>
                        <p class="card-description">List of Frequently Asked Question (FAQ) </p>
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
                                    <asp:TextBox ID="txtSession" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hdSession" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="Label1" runat="server">Programme </asp:Label><span style="color: red">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgrammeCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProgrammeCategory_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblProgramme" runat="server">Programme </asp:Label><span style="color: red">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblSemester" runat="server">Semester </asp:Label><span style="color: red">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2 col-form-label">
                                    <asp:Label ID="lblGroup" runat="server">Group </asp:Label><span style="color: red">*</span>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row justify-content-center">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button Text="Search" ID="btnSearch" runat="server" Width="20%" CssClass="btn btn-primary p-2" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>

                        <hr />

                        <asp:Literal ID="litInfo" runat="server">Complete the above required information to search student.</asp:Literal>

                        <asp:Panel ID="panelCourseMark" runat="server" Visible="false">
                            <div class="form-group pdForm">
                                <div class="row justify-content-center">
                                    <div class="col-md-2 col-form-label">
                                        <asp:Label ID="lblStudent" runat="server">Student Name </asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlStudent" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <hr />

                            <div class="row justify-content-center">
                                <div class="col-lg-10 stretch-card ">
                                    <div class="card">
                                        <div class="table-responsive">
                                            <div class="gv-section text-center">
                                                <asp:GridView ID="gvMark" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="CourseGUID" CellPadding="10" CellSpacing="2" Border="0" OnRowDataBound="gvMark_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="CourseGUID" HeaderText="CourseGUID" ReadOnly="true" SortExpression="CourseGUID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"/>
                                                        <asp:BoundField DataField="CourseAbbrv" HeaderText="Course Abbreviation" SortExpression="CourseAbbrv" />
                                                        <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
                                                        <asp:TemplateField HeaderText="Mark" SortExpression="Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMark" TextMode="Number" runat="server" onkeydown="return (event.keyCode!=13);" onKeyPress="if(this.value.length==3) return false;"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="" HorizontalAlign="Center" />
                                                </asp:GridView>
                                                <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No FAQ Record Found !"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />

                            <div class="form-group pdForm">
                                <div class="row">
                                    <div class="col-md-12 float-right text-right">
                                        <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary p-2" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
