<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="AcademicCalenderEntry.aspx.cs" Inherits="Krous_Ex.AcademicCalenderEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/general.css" rel="stylesheet" />
    <link href="Assests/main/css/bootstrap-datepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="Assests/main/js/bootstrap-datepicker.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h3>
                            <asp:Label ID="lblFAQEntry" runat="server">Semester Entry</asp:Label>
                        </h3>
                        <p class="card-description">Form to insert Semester Details </p>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblType" runat="server">Type of programmes</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlCalenderType" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="DipUnderPost">Diploma, Undergraduate & Postgraduate</asp:ListItem>
                                                <asp:ListItem Value="Foundation">Foundation</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblYear" runat="server">Year </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtYear" runat="server" AutoPostBack="true" OnTextChanged="txtYear_TextChanged"  CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSession" runat="server">Session </asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlSession" runat="server"  CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                               <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblSemesterDate" runat="server">First Semester Date</asp:Label><span style="color: red;">*</span>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="col-md-4 float-left" style="padding-left:0">
                                                <span class="input-group-addon input-group-append border-left" >
                                                    <asp:TextBox ID="txtFirstSemesterStartDate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>  
                                                    <span class="mdi mdi-calendar input-group-text"></span>
                                                </span>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtFirstSemesterStartDate" Format="dd/MM/yyyy" > </ajaxToolkit:CalendarExtender>  
                                            </div>
                                            <div class="input-group-addon col-form-label mx-4 float-left">to</div>
                                            <div class="col-md-4 float-left pl-0">
                                                <span class="input-group-addon input-group-append border-left" >
                                                    <asp:TextBox ID="txtFirstSemesterEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="mdi mdi-calendar input-group-text"></span>
                                                </span>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" CssClass="black" PopupButtonID="imgPopup" runat="server" TargetControlID="txtFirstSemesterEndDate" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>  
                                            </div>
                                            <div class="col-md-1 float-left pl-0">
                                                <asp:TextBox ID="txtFirstSemesterDays" runat="server" AutoPostBack="true" OnTextChanged="txtFirstSemesterDays_TextChanged" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1 float-left col-form-label">
                                                <asp:Label ID="Label1" runat="server">Days </asp:Label>
                                            </div>
                                        </div>

                                   </div>
                                </div>

                                

                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
                                            <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-primary mr20 pdForm" OnClick="btnSave_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
<%--                                        <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-dark mr20 pdForm" OnClick="btnCancel_Click" />
                                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnUpdate_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update this FAQ ?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnDelete_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this FAQ ?" TargetControlID="btnDelete" />--%>
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

    <script src="Assests/main/js/formpickers.js"></script>


</asp:Content>
