<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StaffListings.aspx.cs" Inherits="Krous_Ex.StaffListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    <link href="Assests/main/css/table.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFAQList" runat="server" Font-Size="large">Staff Listings</asp:Label>
                        </h3>
                        <p class="card-description"></p>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblStaffUsername" runat="server">Staff Username :</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblStaffFullName" runat="server">Staff Full Name :</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblFAQCategory" runat="server">Staff Role :</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label1" runat="server">Branches :</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label2" runat="server">Faculty :</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="Label3" runat="server">Staff Specialization :</asp:Label>
                                </div>
                                <div class="col-md-8    ">
                                    <asp:DropDownList runat="server" ID="DropDownList3" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8 text-right">
                                    <asp:Button text="Search" id="btnSearch" runat="server" Width="18%" CssClass="btn btn-success mr20"/>
                                    <asp:Button text="Add new staff" id="btnAdd" runat="server" Width="18%" CssClass="btn btn-success mr20"/>
                                </div>
                            </div>
                        </div>

                        <hr />

        <%--            <div class="panel-body ">
                            <div class="table-responsive">
                                <div class="gv-section gv-staff text-center">
                                    <asp:GridView ID="gvFAQ" runat="server" Width="100%" CssClass="grdViewCss" AutoGenerateColumns="False" DataKeyNames="FAQGUID" BackColor="#CCCCCC" BorderStyle="none" CellPadding="10" CellSpacing="2" ForeColor="Black">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("FAQGUID", "~/FAQEntry.aspx?FAQGUID={0}") %>' Text="View" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FAQGUID" HeaderText="FAQGUID" ReadOnly="true" SortExpression="FAQGUID" Visible="false" />
                                            <asp:BoundField DataField="FAQTitle" HeaderText="Title" SortExpression="FAQTitle" />
                                            <asp:BoundField DataField="FAQCategory" HeaderText="Category" SortExpression="FAQCategory" />
                                            <asp:BoundField DataField="FAQStatus" HeaderText="Status" SortExpression="FAQStatus" />
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="#DC3545" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                    <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Chat Record Found !"></asp:Label>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body" style="padding-top:0">
                <div class="row">
                    <div class="table-responsive table-wrapper-scroll-y">
                    <table class="table">
                    <thead>
                    <tr>
                    <th scope="col">#</th>
                    <th scope="col">Username</th>
                    <th scope="col">Full Name</th>
                    <th scope="col">Roles</th>
                    <th scope="col">Phone Number</th>
                    <th scope="col">Email</th>
                    <th scope="col">NRIC</th>
                    <th scope="col">Specialization</th>
                    <th scope="col">Branches</th>
                    <th scope="col">Faculty</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                    <th scope="row">1</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">2</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">3</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">4</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">5</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    <tr>
                    <th scope="row">6</th>
                    <td>Abcd1234</td>
                    <td>Lau Pin Jian</td>
                    <td>Dean</td>
                    <td>012-3456789</td>
                    <td>laupj@gmail.com</td>
                    <td>000000-00-0000</td>
                    <td>Computing Science</td>
                    <td>KL Main Branch</td>
                    <td>FOCS</td>
                    </tr>
                    </tbody>
                    </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
