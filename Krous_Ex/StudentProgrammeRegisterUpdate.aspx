<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StudentProgrammeRegisterUpdate.aspx.cs" Inherits="Krous_Ex.StudentProgrammeRegisterUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <style>
        .linkButton
        {
            padding: 5px 10px;
            text-decoration: none;
            border: solid 1px black;
            background-color: #404040;
            border-radius: 8px;
        }
        .linkButton:hover
        {
            border: solid 1px Black;
            background-color: #E8E8E8;
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
                            <asp:Label ID="lblProgrammEntry" runat="server">Student Register Listing Details</asp:Label>
                        </h3>
                        <p class="card-description">Form to make modification of the student register status and view or download to</p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3 col-form-label">
                                            <asp:Label ID="lblStudName" runat="server">Student Name</asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtStudName" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3 col-form-label">
                                            <asp:Label ID="lblStudNRIC" runat="server">Student NRIC</asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtStudNRIC" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3 col-form-label">
                                            <asp:Label ID="lblProgName" runat="server">Programme Registered</asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtProgName" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3 col-form-label">
                                            <asp:Label ID="lblStatus" runat="server">Registeration Status</asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlRegisterStatus" CssClass="form-control">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                                <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblRegisterStatus" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3 col-form-label">
                                            <asp:Label ID="lblIcFile" runat="server">Student NRIC File</asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:HyperLink ID="hlIcFile" Target="_blank" runat="server"></asp:HyperLink>
                                            <asp:LinkButton ID="lbIc" runat="server" CssClass="linkButton" ><i class="fas fa-download"></i></asp:LinkButton>
                                            <%--<asp:Button ID="btnDownloadIC" runat="server" Text="download" />--%>
                                            <%--<asp:TextBox ID="txtIc" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3 col-form-label">
                                            <asp:Label ID="lblResultFile" runat="server">Student Result File</asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:HyperLink ID="hlResultFile" Target="_blank" runat="server"></asp:HyperLink>
                                            <asp:LinkButton ID="lbResult" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                            <%--<asp:Button ID="btnDownloadResult" runat="server" Text="download" />--%>
                                            <%--<asp:TextBox ID="txtResult" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3 col-form-label">
                                            <asp:Label ID="lblMedicalFile" runat="server">Student Medical File</asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:HyperLink ID="hlMedicalFile" Target="_blank" runat="server"></asp:HyperLink>
                                            <asp:LinkButton ID="lbMedical" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                            <asp:Label ID="lblNoMedicalFile" runat="server" Text="" Visible="false"></asp:Label>
                                            <%--<asp:Button ID="btnDownloadMedical" runat="server" Text="download" />--%>
                                            <%--<asp:TextBox ID="txtMedical" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                </div>

                                <hr />

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-3 col-form-label">
                                            <asp:Label ID="Label1" runat="server">Student Medical File</asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <hr />

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                            <asp:Button Text="Approve?" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-success pdForm" OnClick="btnUpdate_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this FAQ ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Reject?" ID="btnDelete" runat="server" Width="18%" CssClass="btn btn-danger pdForm" /> 
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this FAQ ?" TargetControlID="btnDelete" /> 
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" />

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




</asp:Content>
