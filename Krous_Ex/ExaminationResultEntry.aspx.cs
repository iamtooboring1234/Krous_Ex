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
    public partial class ExaminationResultEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["InsertMark"] != null)
                {
                    if (Session["InsertMark"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUpdateSuccessToast(); ", true);
                        Session["InsertMark"] = null;
                    }
                }

                loadSession();
                loadSemester();
                loadProgrammeCategory();
                loadGroup();
            }
        }

        private void loadSession()
        {
            try
            {
                string sqlQuery = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                if (radSession.SelectedValue == "1") {
                    sqlQuery = "SELECT S.SessionGUID, S.SessionMonth, S.SessionYear FROM AcademicCalender A, Session S WHERE S.SessionGUID = A.SessionGUID AND GetDate() BETWEEN A.SemesterStartDate AND A.SemesterEndDate;";
                } else
                {
                    sqlQuery = "SELECT S.SessionGUID, S.SessionMonth, S.SessionYear FROM Session S INNER JOIN ExamResult er ON er.SessionGUID = S.SessionGUID GROUP BY S.SessionGUID, S.SessionMonth, S.SessionYear ORDER BY SessionYear, SessionMonth ";
                }

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSession = new DataTable();
                dtSession.Load(reader);
                con.Close();

                if (dtSession.Rows.Count != 0)
                {
                    if (radSession.SelectedValue == "1")
                    {
                        txtSession.Text = dtSession.Rows[0]["SessionYear"].ToString() + dtSession.Rows[0]["SessionMonth"].ToString().PadLeft(2, '0');
                        hdSession.Value = dtSession.Rows[0]["SessionGUID"].ToString();
                    } else 
                    {
                        ddlExistingSession.Items.Clear();

                        ListItem oList = new ListItem();

                        oList = new ListItem();
                        oList.Text = "";
                        oList.Value = "";
                        ddlExistingSession.Items.Add(oList);

                        for (int i = 0; i < dtSession.Rows.Count; i++)
                        {
                            oList = new ListItem();
                            oList.Text = dtSession.Rows[i]["SessionYear"].ToString() + dtSession.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                            oList.Value = dtSession.Rows[i]["SessionGUID"].ToString();
                            ddlExistingSession.Items.Add(oList);
                        } 
                    }
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
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Semester ORDER BY SemesterYear, SemesterSem", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSemester = new DataTable();
                dtSemester.Load(reader);
                con.Close();

                if (dtSemester.Rows.Count != 0)
                {
                    for (int i = 0; i < dtSemester.Rows.Count; i++)
                    {
                        oList = new ListItem();
                        oList.Text = "Year " + dtSemester.Rows[i]["SemesterYear"].ToString() + " Sem " + dtSemester.Rows[i]["SemesterSem"].ToString().PadLeft(2, '0');
                        oList.Value = dtSemester.Rows[i]["SemesterGUID"].ToString();
                        ddlSemester.Items.Add(oList);
                    }
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadProgrammeCategory()
        {
            try
            {
                ddlProgrammeCategory.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgrammeCategory.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ProgrammeCategory FROM Programme GROUP BY ProgrammeCategory ORDER BY ProgrammeCategory ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProgCat = new DataTable();
                dtProgCat.Load(reader);
                con.Close();

                if (dtProgCat.Rows.Count != 0)
                {
                    for (int i = 0; i < dtProgCat.Rows.Count; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                        oList.Value = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                        ddlProgrammeCategory.Items.Add(oList);
                    }
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void ddlProgrammeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlProgrammeCategory.SelectedValue)) {
                ddlProgramme.Enabled = true;
                loadProgramme(ddlProgrammeCategory.SelectedValue);
            } else {
                ddlProgramme.Enabled = false;
                gvMark.DataSource = "";
                gvMark.DataBind();
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
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Programme WHERE ProgrammeCategory = @ProgrammeCategory", con);

                GetCommand.Parameters.AddWithValue("@ProgrammeCategory", programmeCategory);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProgramme = new DataTable();
                dtProgramme.Load(reader);
                con.Close();

                if (dtProgramme.Rows.Count != 0)
                {
                    for (int i = 0; i < dtProgramme.Rows.Count; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dtProgramme.Rows[i]["ProgrammeName"].ToString() + " (" + dtProgramme.Rows[i]["ProgrammeAbbrv"].ToString() + ")";
                        oList.Value = dtProgramme.Rows[i]["ProgrammeGUID"].ToString();
                        ddlProgramme.Items.Add(oList);
                    }
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadGroup()
        {
            try
            {
                ddlGroup.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlGroup.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM [Group] ORDER BY GroupNo ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtGroup = new DataTable();
                dtGroup.Load(reader);
                con.Close();

                if (dtGroup.Rows.Count != 0)
                {
                    for (int i = 0; i < dtGroup.Rows.Count; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dtGroup.Rows[i]["GroupNo"].ToString();
                        oList.Value = dtGroup.Rows[i]["GroupGUID"].ToString();
                        ddlGroup.Items.Add(oList);
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gvMark.DataSource = "";
                gvMark.DataBind();

                if (isAllSelected())
                {
                    litInfo.Visible = false;
                    panelCourseMark.Visible = true;

                    ddlStudent.Items.Clear();

                    ListItem oList = new ListItem();

                    oList = new ListItem();
                    oList.Text = "";
                    oList.Value = "";
                    ddlStudent.Items.Add(oList);

                    SqlCommand GetCommand;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                    if (radSession.SelectedValue == "1")
                    {

                        string sqlQuery = "SELECT * FROM ";
                        sqlQuery += "CurrentSessionSemester css, Student S, GroupStudentList Gs, Student_Programme_Register spr ";
                        sqlQuery += "WHERE css.StudentGUID = S.StudentGUID AND S.StudentGUID = Gs.StudentGUID AND S.StudentGUID = spr.StudentGUID ";
                        sqlQuery += "AND css.SessionGUID = @SessionGUID ";
                        sqlQuery += "AND ProgrammeGUID = @ProgrammeGUID ";
                        sqlQuery += "AND SemesterGUID = @SemesterGUID ";
                        sqlQuery += "AND GroupGUID = @GroupGUID ";
                        sqlQuery += "ORDER BY S.StudentFullName";

                        con.Open();
                        GetCommand = new SqlCommand(sqlQuery, con);

                        GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);
                        GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@GroupGUID", ddlGroup.SelectedValue);
                    } else
                    {
                        string sqlQuery = "SELECT * FROM ";
                        sqlQuery += "CurrentSessionSemester css, Student S, GroupStudentList Gs, Student_Programme_Register spr ";
                        sqlQuery += "WHERE css.StudentGUID = S.StudentGUID AND S.StudentGUID = Gs.StudentGUID AND S.StudentGUID = spr.StudentGUID ";
                        sqlQuery += "AND css.SessionGUID = @SessionGUID ";
                        sqlQuery += "AND ProgrammeGUID = @ProgrammeGUID ";
                        sqlQuery += "AND SemesterGUID = @SemesterGUID ";
                        sqlQuery += "AND GroupGUID = @GroupGUID ";
                        sqlQuery += "ORDER BY S.StudentFullName";

                        con.Open();
                        GetCommand = new SqlCommand(sqlQuery, con);

                        GetCommand.Parameters.AddWithValue("@SessionGUID", ddlExistingSession.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@GroupGUID", ddlGroup.SelectedValue);
                    }

                    SqlDataReader reader = GetCommand.ExecuteReader();

                    DataTable dtStudent = new DataTable();
                    dtStudent.Load(reader);
                    con.Close();

                    if (dtStudent.Rows.Count != 0)
                    {
                        for (int i = 0; i < dtStudent.Rows.Count; i++)
                        {
                            oList = new ListItem();
                            oList.Text = dtStudent.Rows[i]["StudentFullName"].ToString();
                            oList.Value = dtStudent.Rows[i]["StudentGUID"].ToString();
                            ddlStudent.Items.Add(oList);
                        }
                    } else
                    {
                        litInfo.Visible = true;
                        panelCourseMark.Visible = false;
                        litInfo.Text = "This session semester group may not have student. Please search again.";
                    }
                } else
                {
                    clsFunction.DisplayAJAXMessage(this, "Please select all the required information.");
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT* FROM CurrentSessionSemester css ";
                sqlQuery += "LEFT JOIN ProgrammeCourse pc ON css.SemesterGUID = pc.SemesterGUID ";
                sqlQuery += "LEFT JOIN Course C ON pc.CourseGUID = C.CourseGUID ";
                sqlQuery += "LEFT JOIN ExamResult er ON er.StudentGUID = css.StudentGUID ";
                sqlQuery += "LEFT JOIN ExamResultPerCourse ec ON C.CourseGUID = ec.CourseGUID AND er.ExamResultGUID = ec.ExamResultGUID ";
                sqlQuery += "WHERE css.StudentGUID = @StudentGUID AND ";
                sqlQuery += "pc.SessionMonth = (SELECT SessionMonth FROM Student st, Session S WHERE st.SessionGUID = s.SessionGUID AND st.StudentGUID = @StudentGUID) ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@StudentGUID", ddlStudent.SelectedValue);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtCourseMark = new DataTable();
                dtCourseMark.Load(reader);
                con.Close();
                if (!String.IsNullOrEmpty(ddlStudent.SelectedValue))
                {
                    if (dtCourseMark.Rows.Count != 0)
                    {
                        gvMark.DataSource = dtCourseMark;
                        gvMark.DataBind();
                        gvMark.Visible = true;
                        lblNoData.Visible = false;
                    }
                    else
                    {
                        lblNoData.Visible = true;
                        gvMark.Visible = false;
                    }
                }
                else
                {
                    gvMark.DataSource = "";
                    gvMark.DataBind();
                }

            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void gvMark_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtMark = (TextBox)e.Row.FindControl("txtMark");

                    if (gvMark.DataSource != null)
                    {
                        txtMark.Text = DataBinder.Eval(e.Row.DataItem, "Mark").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (InsertMark())
            {
                Session["InsertMark"] = "Yes";
                Response.Redirect("ExaminationResultEntry");
            }
        }

        private bool InsertMark()
        {
            try
            {
                DataTable dtHasExistingExam = new DataTable();
                Guid ExamResultGUID = Guid.NewGuid();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    con.Open();
                    SqlCommand getCommand = new SqlCommand("SELECT * FROM ExamResult WHERE StudentGUID = @StudentGUID AND SessionGUID = @SessionGUID", con);
                    getCommand.Parameters.AddWithValue("@StudentGUID", ddlStudent.SelectedValue);
                    getCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);
                    SqlDataReader reader = getCommand.ExecuteReader();
                    dtHasExistingExam.Load(reader);
                    con.Close();
                }

                if (dtHasExistingExam.Rows.Count == 0)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                    {
                        con.Open();
                        SqlCommand insertCommand = new SqlCommand("INSERT INTO ExamResult VALUES(@ExamResultGUID, @SessionGUID, @StudentGUID, NULL, NULL, 'Unreleased', NULL) ", con);
                        insertCommand.Parameters.AddWithValue("@ExamResultGUID", ExamResultGUID);
                        insertCommand.Parameters.AddWithValue("@StudentGUID", ddlStudent.SelectedValue);
                        insertCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);
                        insertCommand.ExecuteNonQuery();
                        con.Close();
                    }
                } else
                {
                    ExamResultGUID = Guid.Parse(dtHasExistingExam.Rows[0]["ExamResultGUID"].ToString());
                }

                foreach (GridViewRow row in gvMark.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DataTable dtHasExistingCourse = new DataTable();

                        string CourseGUID = row.Cells[0].Text;
                        int intMark = int.Parse(((TextBox)row.FindControl("txtMark")).Text);
                        string strGrade = "";

                        if (intMark >= 80 && intMark <= 100)
                        {
                            strGrade = "A";
                        } else if (intMark >= 75  && intMark <= 79)
                        {
                            strGrade = "A-";
                        }
                        else if (intMark >= 70 && intMark <= 74)
                        {
                            strGrade = "B+";
                        }
                        else if (intMark >= 65 && intMark <= 69)
                        {
                            strGrade = "B";
                        }
                        else if (intMark >= 60 && intMark <= 64)
                        {
                            strGrade = "B-";
                        }
                        else if (intMark >= 55 && intMark <= 59)
                        {
                            strGrade = "C+";
                        }
                        else if (intMark >= 50 && intMark <= 54)
                        {
                            strGrade = "C";
                        } else
                        {
                            strGrade = "F";
                        }

                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand getCommand = new SqlCommand("SELECT * FROM ExamResultPerCourse WHERE ExamResultGUID = @ExamResultGUID AND CourseGUID = @CourseGUID ", con);
                            getCommand.Parameters.AddWithValue("@ExamResultGUID", ExamResultGUID);
                            getCommand.Parameters.AddWithValue("@CourseGUID", CourseGUID);
                            SqlDataReader reader = getCommand.ExecuteReader();
                            dtHasExistingCourse.Load(reader);
                            con.Close();
                        }

                        if (dtHasExistingCourse.Rows.Count == 0)
                        {
                            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                            {
                                string remarks = null;
                                con.Open();
                                SqlCommand insertCommand = new SqlCommand("INSERT INTO ExamResultPerCourse VALUES(newID(), @ExamResultGUID, @CourseGUID, @Mark, @Grade, @Remarks) ", con);
                                insertCommand.Parameters.AddWithValue("@ExamResultGUID", ExamResultGUID);
                                insertCommand.Parameters.AddWithValue("@CourseGUID", CourseGUID);
                                insertCommand.Parameters.AddWithValue("@Mark", intMark);
                                insertCommand.Parameters.AddWithValue("@Grade", strGrade);

                                if (intMark <= 49)
                                {
                                    remarks = "You have failed this exam. You're required to retake this examination.";
                                }

                                insertCommand.Parameters.AddWithValue("@Remarks", remarks);

                                insertCommand.ExecuteNonQuery();
                                con.Close();
                            }
                        } else
                        {
                            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                            {
                                con.Open();
                                SqlCommand updateCommand = new SqlCommand("Update ExamResultPerCourse SET Mark = @Mark, Grade = @Grade, Remarks = @Remarks WHERE ExamResultGUID = @ExamResultGUID AND CourseGUID = @CourseGUID ", con);
                                updateCommand.Parameters.AddWithValue("@ExamResultGUID", ExamResultGUID);
                                updateCommand.Parameters.AddWithValue("@CourseGUID", CourseGUID);
                                updateCommand.Parameters.AddWithValue("@Mark", intMark);
                                updateCommand.Parameters.AddWithValue("@Grade", strGrade);

                                if (intMark >= 50)
                                {
                                    updateCommand.Parameters.AddWithValue("@Remarks", DBNull.Value);
                                }

                                updateCommand.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }

        private bool isAllSelected()
        {
            if(String.IsNullOrEmpty(ddlProgramme.SelectedValue))
            {
                return false;
            }

            if (String.IsNullOrEmpty(ddlProgrammeCategory.SelectedValue))
            {
                return false;
            }

            if (String.IsNullOrEmpty(ddlSemester.SelectedValue))
            {
                return false;
            }

            if (String.IsNullOrEmpty(ddlGroup.SelectedValue))
            {
                return false;
            }

            return true;
        }

        protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStudent.Items.Clear();
            gvMark.DataSource = "";
            gvMark.DataBind();
        }

        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStudent.Items.Clear();
            gvMark.DataSource = "";
            gvMark.DataBind();
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStudent.Items.Clear();
            gvMark.DataSource = "";
            gvMark.DataBind();
        }

        protected void ddlExistingSession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void radSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProgrammeCategory.Items.Clear();
            ddlProgramme.Items.Clear();
            ddlSemester.Items.Clear();
            ddlGroup.Items.Clear();
            ddlExistingSession.Items.Clear();

            ddlProgramme.Enabled = false;

            gvMark.DataSource = "";
            gvMark.DataBind();

            if (radSession.SelectedValue == "1")
            {
                loadSession();
                loadSemester();
                loadProgrammeCategory();
                loadGroup();
                panelCurrent.Visible = true;
                panelExisting.Visible = false;
            }
            else if (radSession.SelectedValue == "2")
            {
                loadSession();
                loadSemester();
                loadProgrammeCategory();
                loadGroup();
                panelCurrent.Visible = false;
                panelExisting.Visible = true;
            }
        }
    }
}