<%@ Page Title="" Language="C#" MasterPageFile="~/Staff_Site.Master" AutoEventWireup="true" CodeBehind="FAQListings.aspx.cs" Inherits="Krous_Ex.FAQListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
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
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblFAQTitle" runat="server">FAQ Title</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFAQTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblFAQCategory" runat="server">Category</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 control-label">
                                    <asp:Label ID="lblFAQStatus" runat="server">FAQ status</asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlFAQStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button text="Search" id="btnSearch" runat="server" Width="18%" CssClass="btn btn-success mr20"/>
                                    <asp:Button text="Add new FAQ" id="btnAdd" runat="server" Width="18%" CssClass="btn btn-success mr20" OnClick="btnAdd_Click"/>
                                </div>
                            </div>
                        </div>
                        <hr />
                    </div>
                </div>
            </div>     
        </div>
    </div>

        <div class="col-lg-12">
        <div class="card">
            <div class="card-body" style="padding-top:0">
                <div class="row">
    <div class="panel-body ">
        <div class="table-responsive">
            <div class="gv-section gv-staff text-center table-responsive table-wrapper-scroll-y">
                <asp:GridView ID="gvFAQ" runat="server" Width="100%" CssClass="table" AutoGenerateColumns="False" DataKeyNames="FAQGUID"  CellPadding="10" CellSpacing="2">
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
                    <HeaderStyle BackColor="#393939" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#222" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No FAQ Record Found !"></asp:Label>
            </div>
        </div>
    </div>
               </div>
        </div>
    </div>
 </div>

</asp:Content>