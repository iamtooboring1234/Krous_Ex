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
    public partial class ProgrammeCourseEntry : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AddedProgrammeCourse"] != null)
            {
                if (Session["AddedProgrammeCourse"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "Programme course added successfully !");
                    Session["AddedProgrammeCourse"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Error! Programme course added  unsuccessfully .");
                    Session["AddedProgrammeCourse"] = null;
                }
            }

            if (IsPostBack != true)
            {
                gvSelectedCourse.DataSource = null;
                gvSelectedCourse.DataBind();
                loadProgrammeCategory();
                loadProgramme("");
                loadSemester();
                litStep2.Text = "<p class=\"card-description\"><strong>Please select all dropdown list from step 1.</strong></p>";
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
                litStep3.Text = "<p class=\"card-description\"><strong>Please complete all the other step first.</strong></p>";
            }
        }

        private void loadProgrammeCategory()
        {
            try
            {
                ddlProgrammCategory.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgrammCategory.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ProgrammeCategory FROM Programme GROUP BY ProgrammeCategory ORDER BY ProgrammeCategory", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProgCat = new DataTable();
                dtProgCat.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProgCat.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                    oList.Value = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                    ddlProgrammCategory.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading programme category.");
            }
        }

        private void loadProgramme(string programmeCategory)
        {
            try
            {
                ddlProgramme.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgramme.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ProgrammeGUID, ProgrammeAbbrv, ProgrammeName FROM Programme WHERE ProgrammeCategory = @ProgrammeCategory ORDER BY ProgrammeAbbrv", con);
                
                GetCommand.Parameters.AddWithValue("@ProgrammeCategory", programmeCategory);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProg = new DataTable();
                dtProg.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProg.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProg.Rows[i]["ProgrammeName"].ToString() + " (" + dtProg.Rows[i]["ProgrammeAbbrv"].ToString() + ")";
                    oList.Value = dtProg.Rows[i]["ProgrammeGUID"].ToString();
                    ddlProgramme.Items.Add(oList);
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }


        private void loadSemester()
        {
            try
            {
                ddlSemester.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlSemester.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Semester ORDER BY SemesterYear, SemesterSem ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSems = new DataTable();
                dtSems.Load(reader);
                con.Close();

                for (int i = 0; i <= dtSems.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = "Year " + dtSems.Rows[i]["SemesterYear"].ToString() + " Sem " + dtSems.Rows[i]["SemesterSem"].ToString();
                    oList.Value = dtSems.Rows[i]["SemesterGUID"].ToString();
                    ddlSemester.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading semester.");
            }
        }

        private void loadGV()
        {
            try
            {
                string sqlQuery = "SELECT CourseGUID, CourseAbbrv, CourseName, CreditHour FROM COURSE ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

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
                clsFunction.DisplayAJAXMessage(this, ex.Message);
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
                    }

                    DT.Rows.Add(CourseGUID, CourseAbbrv, CourseName, CreditHour);
                    Session["dtProgCourse"] = DT;
                    gvSelectedCourse.DataSource = DT;
                    gvSelectedCourse.DataBind();

                    var dt = (DataTable)ViewState["CourseTable"];
                    foreach (GridViewRow gvRow in gvCourse.Rows)
                    {
                        if (gvRow.Cells[1].Text == row.Cells[1].Text)
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
                        litStep3.Text = "<p class=\"card-description\"><strong>Step 3: </strong>Confirm the course selected before proceed.</p>";
                    }

                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "This citizen has be selected !");
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

                dt.Rows.Add(dr);

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

        protected void ddlProgrammCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlProgrammCategory.SelectedValue != "")
            {
                loadProgramme(ddlProgrammCategory.SelectedValue);
                ddlSemester.SelectedIndex = 0;
                ddlProgramme.Enabled = true;
                ddlSemester.Enabled = true;
                gvCourse.DataSource = null;
                gvCourse.DataBind();
                litStep2.Text = "<p class=\"card-description\"><strong>Please select all dropdown list from step 1.</strong></p>";
            }
            else
            {
                gvCourse.DataSource = null;
                gvCourse.DataBind();
                ddlProgramme.Enabled = false;
                ddlSemester.Enabled = false;
                litStep2.Text = "<p class=\"card-description\"><strong>Please select all dropdown list from step 1.</strong></p>";
            }
        }

        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemester.SelectedValue != "" && ddlProgramme.SelectedValue != "" && ddlProgrammCategory.SelectedValue != "")
            {
                litStep2.Text = "<p class=\"card-description\"><strong>Step 2:</strong> Select course</p>";
                if (!checkDuplicateSemsCourse())
                {
                    loadGV();
                } else
                {
                    litStep2.Text = "<p class=\"card-description\"><strong>Please change semester.</strong></p>";
                    clsFunction.DisplayAJAXMessage(this, "Have existing record. Please go listings to manage it.");
                    gvCourse.DataSource = null;
                    gvCourse.DataBind();
                }
            }
            else
            {
                litStep2.Text = "<p class=\"card-description\"><strong>Please select all dropdown list from step 1.</strong></p>";
                gvCourse.DataSource = null;
                gvCourse.DataBind();
            }
        }

        protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgramme.SelectedValue != "" && ddlSemester.SelectedValue != "" && ddlProgrammCategory.SelectedValue != "")
            {
                litStep2.Text = "<p class=\"card-description\"><strong>Step 2:</strong> Select course</p>";
                if (!checkDuplicateSemsCourse())
                {
                    loadGV();
                }
                else
                {
                    litStep2.Text = "<p class=\"card-description\"><strong>Please change semester.</strong></p>";
                    clsFunction.DisplayAJAXMessage(this, "Have existing record. Please go listings to manage it.");
                    gvCourse.DataSource = null;
                    gvCourse.DataBind();
                }
            }
            else
            {
                litStep2.Text = "<p class=\"card-description\"><strong>Please select all dropdown list from step 1.</strong></p>";
                gvCourse.DataSource = null;
                gvCourse.DataBind();
            }
        }

        private bool checkDuplicateSemsCourse()
        {
            string sqlQuery = "SELECT * FROM ProgrammeCourse ";
            sqlQuery += "WHERE CASE WHEN @ProgrammeGUID = '' THEN @ProgrammeGUID ELSE ProgrammeGUID END = @ProgrammeGUID AND ";
            sqlQuery += "CASE WHEN @SemesterGUID = '' then @SemesterGUID ELSE SemesterGUID END = @SemesterGUID ";

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                GetCommand.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtCourse = new DataTable();
                dtCourse.Load(reader);
                con.Close();

                if (dtCourse.Rows.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }

        }

        private bool AddProgrammeCourse()
        {
            string sqlQuery;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                sqlQuery = "INSERT INTO ProgrammeCourse (ProgrammeCourseGUID, CourseGUID, SemesterGUID, ProgrammeGUID) VALUES ";

                int countRow = 1;

                foreach (GridViewRow row in gvSelectedCourse.Rows)
                {
                    sqlQuery += "(NEWID(), '" + row.Cells[1].Text + "', @SemesterGUID, @ProgrammeGUID)";
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

                InsertCommand.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);

                InsertCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsExistInProgCourse())
            {
                if (AddProgrammeCourse())
                {
                    Session["AddedProgrammeCourse"] = "Yes";
                    Response.Redirect("ProgrammeCourseEntry.aspx");
                }
            } 
        }

        private bool IsExistInProgCourse()
        {
            string sqlQuery = "SELECT C.CourseName, CourseAbbrv FROM ProgrammeCourse P, Course C ";
            sqlQuery += "WHERE P.CourseGUID = C.CourseGUID AND P.CourseGUID = @CourseGUID ";
            DataTable dtCourse = new DataTable();
            string message = "";

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                foreach (GridViewRow row in gvSelectedCourse.Rows)
                {
                    GetCommand.Parameters.AddWithValue("@CourseGUID", Guid.Parse(row.Cells[1].Text));
                    SqlDataReader reader = GetCommand.ExecuteReader();
                    dtCourse.Load(reader);
                }

                con.Close();

                if (dtCourse.Rows.Count != 0)
                {
                    message += "<br /><div class=\"card-description\" style=\"text-align:left\"><p>Currently, some of the selected course are existed in another semester : </p>";
                    for (int i = 0; i <= dtCourse.Rows.Count - 1; i++)
                    {
                        message += "<p>- " + dtCourse.Rows[i]["CourseName"] + " (" + dtCourse.Rows[i]["CourseAbbrv"] + ") </p>";
                    }
                    message += "<p>Please <strong style=\"color:red\">REMOVE</strong> it before proceed.</p></div>";

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
    }
}