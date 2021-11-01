<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentFAQChatList.aspx.cs" Inherits="Krous_Ex.StudentFAQChatList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />

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
                            <asp:Label ID="lblChatList" runat="server">Your Chat List(s)</asp:Label>
                        </h3>
                        <p class="card-description">To view the on-going and history of a chat </p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body ">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row justify-content-center">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblChatStatus" runat="server">Chat Status:</asp:Label>
                                        </div>
                                        <div class="col-md-8 form-check p-0 m-0">
                                            <div class="row justify-content-center">
                                                <div class="col-md-8">
                                                    <asp:RadioButtonList ID="rblChatStatus" runat="server" CssClass="form-check-input mt-0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblChatStatus_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="true">Current Chat</asp:ListItem>
                                                        <asp:ListItem Value="2">Chat History</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <hr />

                            <div class="col-lg-12 stretch-card">
                                <div class="card">
                                    <div class="table-responsive">
                                        <div class="gv-section gv-citizen text-center">
                                            <asp:GridView ID="gvChat" runat="server" CssClass="table table-bordered tableChat" Width="100%" AutoGenerateColumns="False" DataKeyNames="ChatGUID" BorderStyle="none" CellPadding="10" CellSpacing="2">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ChatGUID", "~/FAQChat.aspx?ChatGUID={0}") %>' Text="Enter Chat"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField  ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ChatGUID", "~/FAQChatView.aspx?ChatGUID={0}") %>' Text="View Message" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ChatGUID" HeaderText="ChatGUID" ReadOnly="true" SortExpression="ChatGUID" Visible="false" />
                                                    <asp:BoundField DataField="StaffGUID" HeaderText="StaffGUID" ReadOnly="true" SortExpression="StaffGUID" Visible="false" />
                                                    <asp:BoundField DataField="SendBy" HeaderText="Send By" SortExpression="SendBy" Visible="false" />
                                                    <asp:BoundField DataField="ChatStatus" HeaderText="Chat Status" SortExpression="ChatStatus" />
                                                    <asp:BoundField DataField="RepliedBy" HeaderText="Replied By" SortExpression="RepliedBy" />
                                                    <asp:BoundField DataField="CreatedDate" HeaderText="Sent Date" SortExpression="CreatedDate" DataFormatString="{0:dd-MM-yyyy hh:mm tt}" />
                                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="End Date" DataFormatString="{0:dd-MM-yyyy hh:mm tt}" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="" HorizontalAlign="Center" />
                                            </asp:GridView>
                                            <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Chat Record Found !"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdCheckChatStatus" runat="server" />

    <script>
        function pageLoad() {
            bindGrid();
        };

        var $ = jQuery.noConflict();

        function bindGrid() {
            $(".tableChat").prepend($("<thead></thead>").html($(".tableChat").find("tr:first"))).DataTable({
                "searching": false,
                "pageLength": 10,
                "order": [[4, 'asc']],
                "lengthMenu": [[1, 5, 10, 25, 50, -1], [1, 5, 10, 25, 50, "All"]],
                columnDefs: [{
                    'targets': [0, 1], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }]
            });
        };

    </script>

</asp:Content>
