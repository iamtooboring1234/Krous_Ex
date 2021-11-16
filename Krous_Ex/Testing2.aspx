<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testing2.aspx.cs" Inherits="Krous_Ex.Testing2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <h2>Host URL</h2>
            <asp:Label ID="Host" runat="server" Text="Link"></asp:Label>
            <h2>Join URL</h2>
            <asp:Label ID="Join" runat="server" Text="Link"></asp:Label>
            <h2>Response Code</h2>
            <asp:Label ID="Code" runat="server" Text="Code"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Create" OnClick="Button1_Click" />
        </div>

        <div class="col-lg-12 stretch-card">
            <div class="card">
                <div class="table-responsive">
                    <div class="gv-section text-center">
                        <asp:GridView ID="gvFAQ" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="MeetingLinkGUID" CellPadding="10" CellSpacing="2" Border="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("MeetingLinkGUID", "~/JoinMeeting.aspx?MeetingLinkGUID={0}") %>' Text="View" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="MeetingLinkGUID" HeaderText="MeetingLinkGUID" ReadOnly="true" SortExpression="MeetingLinkGUID" Visible="false" />
                                <asp:BoundField DataField="RoomID" HeaderText="Title" SortExpression="FAQTitle" />
                                <asp:BoundField DataField="RoomPass" HeaderText="Category" SortExpression="FAQCategory" />
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

    </form>
</body>
</html>
