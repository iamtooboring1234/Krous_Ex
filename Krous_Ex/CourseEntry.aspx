<%@ Page Title="" Language="C#" MasterPageFile="~/Staff_Site.Master" AutoEventWireup="true" CodeBehind="~/CourseEntry.aspx.cs" Inherits="Krous_Ex.FAQEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />
    <link href="Assests/main/css/layouts.css" rel="stylesheet" />
    
    <script>

        $(document).ready(function () {
            $("input[data-type='currency']").on({
                keyup: function () {
                    formatCurrency($(this));
                },
                blur: function () {
                    formatCurrency($(this), "blur");
                }
            });
        });

        function formatNumber(n) {
            // format number 1000000 to 1,234,567
            return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
        }

        function formatCurrency(input, blur) {
            // appends RM to value, validates decimal side
            // and puts cursor back in right position.

            // get input value
            var input_val = input.val();

            // don't validate empty input
            if (input_val === "") { return; }

            // original length
            var original_len = input_val.length;

            // initial caret position 
            var caret_pos = input.prop("selectionStart");

            // check for decimal
            if (input_val.indexOf(".") >= 0) {

                // get position of first decimal
                // this prevents multiple decimals from
                // being entered
                var decimal_pos = input_val.indexOf(".");

                // split number by decimal point
                var left_side = input_val.substring(0, decimal_pos);
                var right_side = input_val.substring(decimal_pos);

                // add commas to left side of number
                left_side = formatNumber(left_side);

                // validate right side
                right_side = formatNumber(right_side);

                // On blur make sure 2 numbers after decimal
                if (blur === "blur") {
                    right_side += "00";
                }

                // Limit decimal to only 2 digits
                right_side = right_side.substring(0, 2);

                // join number by .
                input_val = "RM "  + left_side + "." + right_side;

            } else {
                // no decimal entered
                // add commas to number
                // remove all non-digits
                input_val = formatNumber(input_val);
                input_val = "RM " + input_val;

                // final formatting
                if (blur === "blur") {
                    input_val += ".00";
                }
            }
            input.val(input_val);
            var updated_len = input_val.length;
            caret_pos = updated_len - original_len + caret_pos;
            input[0].setSelectionRange(caret_pos, caret_pos);
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
        <div class="row">
            <div class="col-md-12">
                <h3>
                    <asp:Label ID="lblCourse" runat="server">Course Entry</asp:Label>
                </h3>
                <p class="card-description">Form to insert new course details</p>
            </div>
        </div>
        <hr />
        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblCourseID" runat="server">Course ID</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtCourseID" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" style="width:115px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblCourseName" runat="server">Course Name</asp:Label><span style="color: red;">*</span>
                                </div>
                                 <div class="col-md-10">
                                    <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" style="width:750px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblCourseDesc" runat="server">Course Description</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtCourseDesc" CssClass="form-control" Style="resize: none" TextMode="multiline" Columns="60" Rows="6" runat="server"/>
                                     <%--<textarea id="txtCourseDesc" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13);" cols="20" rows="2" 
                                               style="width:750px; height:135px; border-radius:5px; color:black; padding-left:12px;"></textarea>--%>
                                </div>
                                
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblCreditHour" runat="server">Credit Hours</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCreditHour" runat="server" CssClass="form-control" style="width:115px;">
                                        <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        <asp:ListItem Value="13">13</asp:ListItem>
                                        <asp:ListItem Value="14">14</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblCourseCategory" runat="server">Course Category</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:RadioButton runat="server" ID="rdMain" Text="Main Course" CssClass="rdBtn" Checked="true" GroupName="Category" AutoPostBack="true" />
                                </div>
                                <div class="col-md-3">
                                    <asp:RadioButton runat="server" ID="rbElective" Text="Elective Course" CssClass="rdBtn" Checked="true" GroupName="Category" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label for="currency-field" ID="lblCourseFee" runat="server">Estimated Course Fee</asp:Label><span style="color: red;">*</span>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtCourseFee" type="text" name="currency-field" runat="server" CssClass="form-control" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" value="" data-type="currency" placeholder="RM 1,000.00" style="width:326px;"></asp:TextBox>
                                     <%--<input type="text" name="currency-field" id="currency-field" class="form-control" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" value="" data-type="currency" placeholder="RM 1,000.00" style="width:326px;">--%>
                                </div>
                            </div>
                        </div>         

                        <hr />
                        <div class="form-group pdForm">
                            <div class="row">
                                <div class="col-md-12 d-flex justify-content-center text-right">
                                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" OnClick="btnCancel_Click"/>
                                    <asp:Button Text="Save" ID="btnSave" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" OnClick="btnSave_Click" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure to add these details ?" TargetControlID="btnSave" />
                                    <asp:Button Text="Update" ID="btnUpdate" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure to update these details ?" TargetControlID="btnUpdate" />
                                    <asp:Button Text="Delete" ID="btnDelete" runat="server" Width="18%" CssClass="btn btn-success mr20 pdForm" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Are you sure to delete this course ?" TargetControlID="btnDelete" />
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
