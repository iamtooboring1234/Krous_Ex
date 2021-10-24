<%@ Page Title="" Language="C#" MasterPageFile="~/AllUserSite.Master" AutoEventWireup="true" CodeBehind="KrousExContactUs.aspx.cs" Inherits="Krous_Ex.KrousExContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container-fluid page-body-wrapper">
        <div class="main-panel">
            <div class="content-wrapper">
                <div class="row">
                    <div class="col-12 grid-margin stretch-card ">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Are you sure to delete this message?</h4>
                                <div class="panel-body">
                                    <div class="form-horizontal">
                                        <div class="form-group pdForm">
                                            <div class="row justify-content-center">
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="lblName" runat="server">Your Name</asp:Label><span style="color: red;">*</span>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required *" ControlToValidate="txtName" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group pdForm">
                                            <div class="row justify-content-center">
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="lblEmailAddress" runat="server">Your Email</asp:Label><span style="color: red;">*</span>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required *" ControlToValidate="txtEmailAddress" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group pdForm">
                                            <div class="row justify-content-center">
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="lblEmailSubject" runat="server">Subject</asp:Label><span style="color: red;">*</span>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtEmailSubject" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required *" ControlToValidate="txtEmailSubject" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group pdForm">
                                            <div class="row justify-content-center">
                                                <div class="col-md-2 col-form-label">
                                                    <asp:Label ID="lblEmailContent" runat="server">Content</asp:Label><span style="color: red;">*</span>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtEmailContent" TextMode="MultiLine" Rows="20" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required *" ControlToValidate="txtEmailContent" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row justify-content-center">
                                        <div class="col-md-10 text-right">
                                            <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary" Width="18%" OnClick="btnSend_Click" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-dark" Width="18%" OnClick="btnClear_Click" />
                                         </div>
                                    </div>
                                </div>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
