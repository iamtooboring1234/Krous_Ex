<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="StaffAssessmentDetails.aspx.cs" Inherits="Krous_Ex.StaffAssessmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.full.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datetimepicker/jquery.datetimepicker.min.css" rel="stylesheet" />

    <link href="Assests/main/css/general.css" rel="stylesheet" />

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <link href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>


    <script type="text/javascript">
        $(function () {
            $('.duedate').datetimepicker({
                theme: 'dark',
                format: 'd/m/Y H:i',
                defaultDate: new Date(),
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <!--Assessment Details-->
                    <div id="divDetails" runat="server" class="col-lg-12">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-10 col-form-label">
                                            <h3>
                                                <asp:Label ID="lblAssessmentTitle" runat="server" Text=""></asp:Label>
                                            </h3>
                                            <asp:TextBox ID="txtAssessmentTitle" runat="server" CssClass="form-control" Visible="false" placeholder="Assessment Title" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dropdown">
                                                <asp:LinkButton ID="lbMenu" CssClass="btn btn-outline-warning dropdown-toggle" role="button" type="button" data-toggle="dropdown" runat="server" Style="width: 160px; padding: 10px; margin-top: 5px;" Visible="false">Menu</asp:LinkButton>
                                                <div class="dropdown-menu">
                                                    <asp:LinkButton ID="lbModify" CssClass="dropdown-item" runat="server" OnClick="lbModify_Click">Update Assessment</asp:LinkButton>
                                                    <asp:LinkButton ID="lbDelete" CssClass="dropdown-item" runat="server" OnClick="lbDelete_Click">Delete Assessment</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-3 col-sm-6 grid-margin stretch-card">
                                        <div class="card" style="background-color: black;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-9">
                                                        <div class="d-flex align-items-center align-self-start">
                                                            <h5 class="mb-0">Created On</h5>
                                                        </div>
                                                        <asp:Label ID="lblCreatedDate" runat="server" CssClass="text-success ms-2 mb-0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-3 col-sm-6 grid-margin stretch-card">
                                        <div class="card" style="background-color: black;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-9">
                                                        <div class="d-flex align-items-center align-self-start">
                                                            <h5 class="mb-0">Due Date</h5>
                                                        </div>
                                                        <asp:Label ID="lblAssessmentDueDate" runat="server" CssClass="text-reddit ms-2 mb-0"></asp:Label>
                                                    </div>
                                                    <div id="dateTimePicker" runat="server">
                                                        <div class='input-group date' id='datetimepicker8' style="padding: 2px;">
                                                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control duedate" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="fa fa-calendar" style="padding: 10px;"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-3 col-sm-6 grid-margin stretch-card">
                                        <div class="card" style="background-color: black;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-9">
                                                        <div class="d-flex align-items-center align-self-start">
                                                            <h5 class="mb-0">Last Update</h5>
                                                        </div>
                                                        <asp:Label ID="lblLastUpdate" runat="server" CssClass="text-facebook ms-2 mb-0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-3 col-sm-6 grid-margin stretch-card">
                                        <div class="card" style="background-color: black;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-9">
                                                        <div class="d-flex align-items-center align-self-start">
                                                            <h5 class="mb-0">Created By</h5>
                                                        </div>
                                                        <asp:Label ID="lblCreatedBy" runat="server" CssClass="text-info ms-2 mb-0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblAssDesc" runat="server" Text="Assessment Description"></asp:Label>
                                        </div>
                                        <div class="col-md-9 col-form-label">
                                            <asp:Label ID="lblAssessmentDesc" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtAssessmentDesc" TextMode="MultiLine" runat="server" CssClass="form-control" Style="height: 120px;" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblAssessmentFile" runat="server">Assessment File</asp:Label>
                                        </div>
                                        <div class="col-md-8 col-form-label">
                                            <asp:HyperLink ID="hlFile" Target="_blank" runat="server"></asp:HyperLink>
                                            <asp:LinkButton ID="lbDownload" runat="server" CssClass="linkButton" Style="margin-left: 5px;"><i class="fas fa-download"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbRemove" runat="server" CssClass="linkButton" Style="margin-left: 5px;" OnClick="lbRemove_Click"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                                <ContentTemplate>
                                                    <ajaxToolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Mode="Auto" Visible="false" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-2 col-form-label">
                                            <asp:Label ID="lblAssigned" runat="server">Assigned to</asp:Label>
                                        </div>
                                        <div class="col-md-8 col-form-label">
                                            <asp:Label ID="lblGroupSession" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <hr />
                                <div class="form-group pdForm">
                                    <div class="row">
                                        <div class="col-md-12 float-right text-right">
                                            <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-success pdForm" OnClick="btnUpdate_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" ConfirmText="Are you sure to update this assessment details?" TargetControlID="btnUpdate" />
                                            <asp:Button Text="Back" ID="btnBack" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBack_Click" />
                                            <asp:Button Text="Back" ID="btnBackListing" runat="server" Width="18%" CssClass="btn mr20 pdForm" OnClick="btnBackListing_Click" />
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


    <!--subsmission table-->
    <div class="col-lg-12 mt-4">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4>Submission List</h4>
                    </div>
                    <div class="table-responsive">
                        <div class="gv-section text-center"> 
                            <asp:GridView ID="gvSubmissionList" runat="server" Width="100%" CssClass="table table-bordered tableSubmissionList" AutoGenerateColumns="False" DataKeyNames="SubmissionGUID" CellPadding="10" CellSpacing="2" Border="0" OnRowDataBound="gvSubmissionList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlView" runat="server" Text="View" Target="_blank" Style="margin-right: 10px;" />
                                            <asp:LinkButton ID="lbDownload" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SubmissionGUID" HeaderText="SubmissionGUID" ReadOnly="true" SortExpression="SubmissionGUID" Visible="false" />
                                    <asp:BoundField DataField="StudentFullName" HeaderText="Student Name" SortExpression="StudentFullName" />
                                    <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" SortExpression="SubmissionDate" />
                                    <asp:BoundField DataField="SubmissionStatus" HeaderText="Submission Status" SortExpression="SubmissionStatus" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="" HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Submission Record Found !"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {     
            $(".tableSubmissionList").prepend($("<thead></thead>").html($(".tableSubmissionList").find("tr:first"))).DataTable({
                "searching": false,
                "pageLength": 10,
                "order": [[1, 'asc']],
                "lengthMenu": [[1, 5, 10, 25, 50, -1], [1, 5, 10, 25, 50, "All"]],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }]
            });
        });
    </script>



    <script src="Assests/main/js/hoverable-collapse.js"></script>
    <script src="Assests/main/js/formpickers.js"></script>
</asp:Content>
