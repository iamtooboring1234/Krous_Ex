using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffCourseIncEntry : System.Web.UI.Page
    {
        Guid userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AddedCourseInc"] != null)
            {
                if (Session["AddedCourseInc"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "Course In-charge by staff entry has been added successfully!");
                    Session["AddedCourseInc"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Course In-charge by staff entry added unsuccessfully. The staff may have already take in-charge of 5 courses at the moment.");
                    Session["AddedCourseInc"] = null;
                }
            }

            if (IsPostBack != true)
            {
                gvSelectedCourse.DataSource = null;
                gvSelectedCourse.DataBind();
                loadProgrammeCategory();
                litStep2.Text = "<p class=\"card-description\"><strong>Please enter staff and select the programme category first</strong></p>";
            }
            else
            {
                if (Request.Cookies["GridViewRefresh"].Value is object)
                {
                    if (Request.Cookies["GridViewRefresh"].Value == "Yes")
                    {
                        Session["dtProgCourse"] = null;
                        Response.Cookies["GridViewRefresh"].Value = null;
                    }
                }
            }

            if (Session["dtProgCourse"] == null)
            {
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
            }

            if (gvSelectedCourse.Rows.Count == 0)
            {
                litStep3.Text = "<p class=\"card-description\"><strong>Please complete all the above steps first.</strong></p>";
            }
        }

        protected void loadProgrammeCategory()
        {
            try
            {
                ddlProgCategory.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT ProgrammeCategory FROM Programme GROUP BY ProgrammeCategory ORDER BY ProgrammeCategory", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlProgCategory.DataSource = ds;
                ddlProgCategory.DataTextField = "ProgrammeCategory";
                ddlProgCategory.DataValueField = "ProgrammeCategory";
                ddlProgCategory.DataBind();
                ddlProgCategory.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void loadCourseGV()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string sqlQuery = "SELECT p.ProgrammeCategory, p.ProgrammeGUID, c.CourseGUID, c.CourseAbbrv, c.CourseName, c.CreditHour FROM Course c, ProgrammeCourse pc, Programme p WHERE pc.CourseGUID = c.CourseGUID AND pc.ProgrammeGUID = p.ProgrammeGUID AND p.ProgrammeCategory = @ProgrammeCategory GROUP BY p.ProgrammeCategory, p.ProgrammeGUID, c.CourseGUID, c.CourseAbbrv, c.CourseName, c.CreditHour ORDER BY c.CourseName"; //WHERE ddlProgCategory
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtCourse = new DataTable();
                dtCourse.Load(reader);
                con.Close();

                if (dtCourse.Rows.Count != 0)
                {
                    ViewState["CourseTable"] = dtCourse;
                    gvCourse.DataSource = dtCourse;
                    gvCourse.DataBind();
                    gvCourse.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvCourse.Visible = false;
                }
            }

            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void OnSelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                int checkSame = 0;
                GridViewRow row = gvCourse.SelectedRow;
                if (gvSelectedCourse.Rows.Count != 0)
                {
                    foreach (GridViewRow rowCheck in gvSelectedCourse.Rows)
                    {
                        if (rowCheck.Cells[2].Text == row.Cells[2].Text)
                        {
                            checkSame = 1;
                            break;
                        }
                    }
                }

                if (checkSame == 0)
                {
                    string CourseGUID = row.Cells[1].Text;
                    string CourseAbbrv = row.Cells[2].Text;
                    string CourseName = row.Cells[3].Text;
                    string CreditHour = row.Cells[4].Text;
                    string ProgrammeCategory = row.Cells[5].Text;
                    string ProgrammeGUID = row.Cells[6].Text;

                    var DT = new DataTable();
                    if (Session["dtProgCourse"] is object)
                    {
                        DT = Session["dtProgCourse"] as DataTable;
                    }
                    else
                    {
                        DT.Columns.Add("CourseGUID", typeof(string));
                        DT.Columns.Add("CourseAbbrv", typeof(string));
                        DT.Columns.Add("CourseName", typeof(string));
                        DT.Columns.Add("CreditHour", typeof(string));
                        DT.Columns.Add("ProgrammeCategory", typeof(string));
                        DT.Columns.Add("ProgrammeGUID", typeof(string));
                    }

                    DT.Rows.Add(CourseGUID, CourseAbbrv, CourseName, CreditHour, ProgrammeCategory, ProgrammeGUID);
                    Session["dtProgCourse"] = DT;
                    gvSelectedCourse.DataSource = DT;
                    gvSelectedCourse.DataBind();

                    var dt = (DataTable)ViewState["CourseTable"];
                    foreach (GridViewRow gvRow in gvCourse.Rows)
                    {
                        if (gvRow.Cells[2].Text == row.Cells[2].Text)
                        {
                            dt.Rows.RemoveAt(gvRow.RowIndex); 
                        }
                    }

                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();

                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;

                    if (gvSelectedCourse.Rows.Count != 0)
                    {
                        litStep3.Text = "<p class=\"card-description\"><strong>Confirm the course selected before proceed.</strong></p>";
                    }

                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "This course has be selected !");
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void OnSelectedIndexChanged2(object sender, EventArgs e)
        {

            try
            {
                var DT = new DataTable();
                DT = Session["dtProgCourse"] as DataTable;

                Guid CourseGUID = Guid.Parse(gvSelectedCourse.SelectedRow.Cells[1].Text);
                string CourseAbbrv = gvSelectedCourse.SelectedRow.Cells[2].Text;
                string CourseName = gvSelectedCourse.SelectedRow.Cells[3].Text;
                string CreditHour = gvSelectedCourse.SelectedRow.Cells[4].Text;
                string ProgrammeCategory = gvSelectedCourse.SelectedRow.Cells[5].Text;
                string ProgrammeGUID = gvSelectedCourse.SelectedRow.Cells[6].Text;

                DT.Rows.RemoveAt(gvSelectedCourse.SelectedRow.RowIndex);
                if (gvSelectedCourse.Rows.Count == 1)
                {
                    Session["dtProgCourse"] = null;
                }
                else
                {
                    Session["dtProgCourse"] = DT;
                }

                gvSelectedCourse.DataSource = DT;
                gvSelectedCourse.DataBind();

                var dt = (DataTable)ViewState["CourseTable"];

                DataRow dr = dt.NewRow();

                dr["CourseGUID"] = CourseGUID;
                dr["CourseAbbrv"] = CourseAbbrv;
                dr["CourseName"] = CourseName;
                dr["CreditHour"] = CreditHour;
                dr["ProgrammeCategory"] = ProgrammeCategory;
                dr["ProgrammeGUID"] = ProgrammeGUID;

                dt.Rows.Add(dr); //cong zhe bian tiao qu catch

                gvCourse.DataSource = dt;
                gvCourse.DataBind();

                if (gvSelectedCourse.Rows.Count != 0)
                {
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    litStep3.Text = "<p class=\"card-description\"><strong>Please select at least one course.</strong></p>";
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }


        protected void ddlProgCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlProgCategory.SelectedValue != "")
            {
                litStep2.Text = "<p class=\"card-description\"><strong>Select MAXIMUM 5 courses</strong></p>";
                loadCourseGV();
            }
            else
            {
                litStep2.Text = "<p class=\"card-description\"><strong>Please select the programme category before selecting the course</strong></p>";
                gvCourse.DataSource = null;
                gvCourse.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();

                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Staff WHERE CASE WHEN @StaffFullName = '' THEN @StaffFullName ELSE StaffFullName END LIKE '%'+@StaffFullName+'%' ORDER BY StaffFullName", con);
                GetCommand.Parameters.AddWithValue("@StaffFullName", txtFullname.Text);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvStaffSearch.DataSource = dt;
                    gvStaffSearch.DataBind();
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "No such records.");
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private bool CheckMax5Records()
        {
            try
            {
                foreach (GridViewRow row in gvStaffSearch.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                        if (chkRow.Checked)
                        {
                            Guid StaffGUID = Guid.Parse(gvStaffSearch.DataKeys[row.RowIndex].Value.ToString());

                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                            con.Open();

                            SqlCommand selectCmd = new SqlCommand("SELECT StaffGUID FROM Course_In_Charge WHERE StaffGUID = @StaffGUID", con);
                            selectCmd.Parameters.AddWithValue("@StaffGUID", StaffGUID);

                            SqlDataReader reader = selectCmd.ExecuteReader();
                            DataTable dtFound = new DataTable();
                            dtFound.Load(reader);
                            con.Close();

                            if (dtFound.Rows.Count == 5)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }

        protected bool addCourseInc()
        {
            string sqlQuery;
            try
            {
                foreach (GridViewRow row in gvStaffSearch.Rows) //Running all lines of grid
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                        if (chkRow.Checked)
                        {
                            Guid StaffGUID = Guid.Parse(gvStaffSearch.DataKeys[row.RowIndex].Value.ToString());
                      
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                            con.Open();

                            sqlQuery = "INSERT INTO Course_In_Charge (CourseInChargeGUID, CourseGUID, StaffGUID) VALUES ";

                            int countRow = 1;

                            foreach (GridViewRow rowSelected in gvSelectedCourse.Rows)
                            {
                                sqlQuery += "(NEWID(), '" + rowSelected.Cells[1].Text + "', @StaffGUID)";
                                if (countRow != gvSelectedCourse.Rows.Count)
                                {
                                    sqlQuery += ",";
                                }
                                else
                                {
                                    sqlQuery += ";";
                                }

                                countRow += 1;
                            }

                            Session["dtProgCourse"] = null;

                            SqlCommand InsertCommand = new SqlCommand(sqlQuery, con);
                            InsertCommand.Parameters.AddWithValue("@StaffGUID", StaffGUID);
                            InsertCommand.ExecuteNonQuery();
                            con.Close();
                            return true;     
                        }
                        else
                        {
                            clsFunction.DisplayAJAXMessage(this, "Please checked at least ONE checkbox.");
                        }                
                    }   
                }
                return false;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
        }

        private bool IsExistInCourseInCharge() 
        {
            string message = "";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string sqlQuery = "SELECT ci.CourseGUID, ci.StaffGUID, c.CourseName, c.CourseAbbrv FROM Course c, Course_In_Charge ci WHERE ci.CourseGUID = c.CourseGUID AND ci.StaffGUID = @StaffGUID AND ci.CourseGUID = @CourseGUID";
                DataTable dtCourse = new DataTable();
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                GetCommand.Parameters.Add("@CourseGUID", SqlDbType.NVarChar);

                foreach (GridViewRow row in gvStaffSearch.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                        if (chkRow.Checked)
                        {
                            Guid StaffGUID = Guid.Parse(gvStaffSearch.DataKeys[row.RowIndex].Value.ToString());
                            GetCommand.Parameters.AddWithValue("@StaffGUID", StaffGUID);

                            foreach (GridViewRow rowSelected in gvSelectedCourse.Rows)
                            {
                                GetCommand.Parameters["@CourseGUID"].Value = rowSelected.Cells[1].Text;
                                SqlDataReader reader = GetCommand.ExecuteReader();
                                dtCourse.Load(reader);
                            }
                        }
                    }
                }

                con.Close();

                if (dtCourse.Rows.Count != 0)
                {
                    message += "<br /><div class=\"card-description\" style=\"text-align:left\"><p>The course you've selected are already exist as shown below :</p>";
                    for (int i = 0; i <= dtCourse.Rows.Count - 1; i++)
                    {
                        message += "<p>- " + dtCourse.Rows[i]["CourseName"] + " (" + dtCourse.Rows[i]["CourseAbbrv"] + ") </p>";
                    }
                    message += "<p>Please <strong style=\"color:red\">REMOVE</strong> it to proceed.</p></div>";

                    litExist.Text = message;
                    litExist.Visible = true;

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsExistInCourseInCharge())
            {
                if (CheckMax5Records())
                {
                    if (addCourseInc())
                    {
                        Session["AddedCourseInc"] = "Yes";
                        Response.Redirect("StaffCourseIncEntry.aspx");
                    }
                }
                else
                {
                    Session["AddedCourseInc"] = "No";
                    Response.Redirect("StaffCourseIncEntry.aspx");
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            gvStaffSearch.DataSource = null;
            gvStaffSearch.DataBind();
            txtFullname.Text = string.Empty;
            txtFullname.Focus();
        }
    }
}