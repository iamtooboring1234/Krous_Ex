<%@ Page Title="" Language="C#" MasterPageFile="~/StaffMaster.Master" AutoEventWireup="true" CodeBehind="ExaminationSubmissionListings.aspx.cs" Inherits="Krous_Ex.ExaminationSubmissionListings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/table.css" rel="stylesheet" />

    <script src="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.js"></script>
    <link href="Assests/main/vendors/JQuery.datatable/jquery.dataTables.min.css" rel="stylesheet" />

    <style>
        .linkButton {
            padding: 5px 10px;
            text-decoration: none;
            border: solid 1px black;
            background-color: #404040;
            border-radius: 8px;
        }

            .linkButton:hover {
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
                            <asp:Label ID="lblExaminationSubmission" runat="server">Examination Submission Listings</asp:Label>
                        </h3>
                    </div>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class=" pdForm">
                            <div class="row">

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12 stretch-card">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <div class="gv-section text-center">
                        <asp:GridView ID="gvSubmission" runat="server" Width="100%" CssClass="table table-bordered tableSubmission" AutoGenerateColumns="False" DataKeyNames="ExamSubmissionGUID" CellPadding="10" CellSpacing="2" Border="0" OnRowDataBound="gvSubmission_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlView" runat="server" Text="View" Target="_blank" />
                                        <asp:LinkButton ID="lbDownload" runat="server" CssClass="linkButton"><i class="fas fa-download"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ExamSubmissionGUID" HeaderText="ExamSubmissionGUID" ReadOnly="true" SortExpression="ExamSubmissionGUID" Visible="false" />
                                <asp:BoundField DataField="StudentFullName" HeaderText="Student Name" SortExpression="FacultyName" />
                                <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" SortExpression="FacultyName" />
                                <asp:BoundField DataField="SubmissionStatus" HeaderText="Submission Status" SortExpression="FacultyName" />
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#191c24" Font-Bold="True" HorizontalAlign="Left" CssClass="header-style" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="" HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblNoData" runat="server" Visible="false" Font-Size="Large" Font-Bold="true" Text="No Faculty Record Found !"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        var $ = jQuery.noConflict();

        $(document).ready(function () {
            $(".tableSubmission").prepend($("<thead></thead>").html($(".tableSubmission").find("tr:first"))).DataTable({
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

</asp:Content>
