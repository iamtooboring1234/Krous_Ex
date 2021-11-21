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
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showExaminationUpdateSuccessToast(); ", true);
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
                        //txtSession.Text = "202105";
                        //hdSession.Value = "dbe19259-5228-47ee-8cde-86344f5d1f74";
                        txtSession.Text = dtSession.Rows[0]["SessionYear"].ToString() + dtSession.Rows[0]["SessionMonth"].ToString().PadLeft(2, '0');
                        hdSession.Value = dtSession.Rows[0]["SessionGUID"].ToString();
                    }
                    else 
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
            if (!string.IsNullOrEmpty(ddlProgrammeCategory.SelectedValue)) {
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
                        sqlQuery += "AND css.SemesterGUID = @SemesterGUID ";
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
                        string sqlQuery = "SELECT * FROM ExamResult ex LEFT JOIN Session S ON ex.SessionGUID = S.SessionGUID ";
                        sqlQuery += "LEFT JOIN Student St ON ex.StudentGUID = St.StudentGUID ";
                        sqlQuery += "LEFT JOIN GroupStudentList Gs ON St.StudentGUID = Gs.StudentGUID ";
                        sqlQuery += "LEFT JOIN Student_Programme_Register spr ON St.StudentGUID = spr.StudentGUID ";
                        sqlQuery += "LEFT JOIN CurrentSessionSemester css ON ex.StudentGUID = css.StudentGUID ";
                        sqlQuery += "WHERE ex.SessionGUID = @SessionGUID ";
                        sqlQuery += "AND ProgrammeGUID = @ProgrammeGUID ";
                        sqlQuery += "AND GroupGUID = @GroupGUID ";
                        sqlQuery += "ORDER BY St.StudentFullName ";

                        con.Open();
                        GetCommand = new SqlCommand(sqlQuery, con);

                        GetCommand.Parameters.AddWithValue("@SessionGUID", ddlExistingSession.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
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
                if (!string.IsNullOrEmpty(ddlStudent.SelectedValue))
                {
                    string sqlQuery;

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                    SqlDataReader reader;
                    con.Open();

                    if (radSession.SelectedValue == "1")
                    {

                        sqlQuery = "SELECT * FROM CurrentSessionSemester css ";
                        sqlQuery += "LEFT JOIN ProgrammeCourse pc ON css.SemesterGUID = pc.SemesterGUID ";
                        sqlQuery += "LEFT JOIN Course C ON pc.CourseGUID = C.CourseGUID ";
                        sqlQuery += "LEFT JOIN ExamResult er ON er.StudentGUID = css.StudentGUID AND css.SessionGUID = er.SessionGUID ";
                        sqlQuery += "LEFT JOIN ExamResultPerCourse ec ON C.CourseGUID = ec.CourseGUID AND er.ExamResultGUID = ec.ExamResultGUID ";
                        sqlQuery += "WHERE css.StudentGUID = @StudentGUID AND pc.ProgrammeGUID = @ProgrammeGUID AND ";
                        sqlQuery += "pc.SessionMonth = (SELECT SessionMonth FROM Student st, Session S WHERE st.SessionGUID = s.SessionGUID AND st.StudentGUID = @StudentGUID) ";

                        SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                        GetCommand.Parameters.AddWithValue("@StudentGUID", ddlStudent.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                        reader = GetCommand.ExecuteReader();

                    } else
                    {
                        sqlQuery = "SELECT * FROM ProgrammeCourse pc ";
                        sqlQuery += "LEFT JOIN Course C ON pc.CourseGUID = C.CourseGUID ";
                        sqlQuery += "LEFT JOIN ExamResultPerCourse ec ON C.CourseGUID = ec.CourseGUID ";
                        sqlQuery += "LEFT JOIN ExamResult er ON ec.ExamResultGUID = er.ExamResultGUID ";
                        sqlQuery += "WHERE er.StudentGUID = @StudentGUID AND er.SessionGUID = @SessionGUID AND pc.ProgrammeGUID = @ProgrammeGUID AND ";
                        sqlQuery += "pc.SessionMonth = (SELECT SessionMonth FROM Student st, Session S WHERE st.SessionGUID = s.SessionGUID AND st.StudentGUID = @StudentGUID) ";

                        SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                        GetCommand.Parameters.AddWithValue("@StudentGUID", ddlStudent.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@SessionGUID", ddlExistingSession.SelectedValue);
                        GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                        reader = GetCommand.ExecuteReader();
                    }

                    DataTable dtCourseMark = new DataTable();
                    dtCourseMark.Load(reader);

                    con.Close();

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

                    if (radSession.SelectedValue == "1")
                    {
                        getCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);
                    } else
                    {
                        getCommand.Parameters.AddWithValue("@SessionGUID", ddlExistingSession.SelectedValue);
                    }
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
                        if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtMark")).Text))
                        {
                            DataTable dtHasExistingCourse = new DataTable();

                            string CourseGUID = row.Cells[0].Text;
                            int intMark = int.Parse(((TextBox)row.FindControl("txtMark")).Text);
                            string strGrade = "";

                            if (intMark >= 80 && intMark <= 100)
                            {
                                strGrade = "A";
                            }
                            else if (intMark >= 75 && intMark <= 79)
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
                            }
                            else
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
                                        insertCommand.Parameters.AddWithValue("@Remarks", remarks);
                                    }
                                    else
                                    {
                                        insertCommand.Parameters.AddWithValue("@Remarks", DBNull.Value);
                                    }

                                    insertCommand.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            else
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
                }

                updateGPANCGPA();

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
            if(string.IsNullOrEmpty(ddlProgramme.SelectedValue))
            {
                return false;
            }

            if (string.IsNullOrEmpty(ddlProgrammeCategory.SelectedValue))
            {
                return false;
            }
            if (radSession.SelectedValue == "1")
            {
                if (string.IsNullOrEmpty(ddlSemester.SelectedValue))
                {
                    return false;
                }
            }

            if (string.IsNullOrEmpty(ddlGroup.SelectedValue))
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
            ddlStudent.Items.Clear();
            gvMark.DataSource = "";
            gvMark.DataBind();
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
                panelSemester.Visible = true;
            }
            else if (radSession.SelectedValue == "2")
            {
                loadSession();
                loadSemester();
                loadProgrammeCategory();
                loadGroup();
                panelCurrent.Visible = false;
                panelExisting.Visible = true;
                panelSemester.Visible = false;
            }
        }

        private bool updateGPANCGPA()
        {
            try
            {
                DataTable dtExamResult = new DataTable();
                DataTable dtSession = new DataTable();
                DataTable dtUpperSession = new DataTable();
                string sqlQuery;

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();

                SqlCommand GetCommand = new SqlCommand("SELECT S.SessionGUID, S.SessionMonth, S.SessionYear FROM AcademicCalender A, Session S WHERE S.SessionGUID = A.SessionGUID AND GetDate() BETWEEN A.SemesterStartDate AND A.SemesterEndDate ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                dtSession.Load(reader);

                string sessionGUID = ddlExistingSession.SelectedValue;

                sqlQuery = "WITH CTE AS( ";
                sqlQuery += "SELECT ROW_NUMBER() OVER(ORDER BY SessionYear, SessionMonth) as Row, er.SessionGUID FROM Session S LEFT JOIN ExamResult er ON S.SessionGUID = er.SessionGUID)";
                sqlQuery += "SELECT* FROM CTE WHERE Row = ((select Row from CTE where SessionGUID = @SessionGUID) +1) ";

                int x = 0;

                while (dtSession.Rows[0]["SessionGUID"].ToString() != sessionGUID)
                {
                    GetCommand = new SqlCommand("SELECT * FROM ExamResult er LEFT JOIN Session S ON er.SessionGUID = s.SessionGUID WHERE er.StudentGUID = @StudentGUID AND er.SessionGUID = @SessionGUID ORDER BY s.SessionYear, s.SessionMonth, er.StudentGUID  ", con);
                    GetCommand.Parameters.AddWithValue("@StudentGUID", ddlStudent.SelectedValue);
                    GetCommand.Parameters.AddWithValue("@SessionGUID", sessionGUID);

                    reader = GetCommand.ExecuteReader();
                    dtExamResult.Load(reader);

                    GetCommand = new SqlCommand(sqlQuery, con);

                    GetCommand.Parameters.AddWithValue("@SessionGUID", sessionGUID);

                    reader = GetCommand.ExecuteReader();
                    dtUpperSession.Load(reader);

                    sessionGUID = dtUpperSession.Rows[x]["SessionGUID"].ToString();

                   x++;
                }

                GetCommand = new SqlCommand("SELECT * FROM ExamResult er LEFT JOIN Session S ON er.SessionGUID = s.SessionGUID WHERE er.StudentGUID = @StudentGUID AND er.SessionGUID = @SessionGUID ORDER BY s.SessionYear, s.SessionMonth, er.StudentGUID  ", con);
                GetCommand.Parameters.AddWithValue("@StudentGUID", ddlStudent.SelectedValue);
                GetCommand.Parameters.AddWithValue("@SessionGUID", sessionGUID);

                reader = GetCommand.ExecuteReader();
                dtExamResult.Load(reader);

                con.Close();

                if (dtExamResult.Rows.Count != 0)
                {
                    DataTable dtCss = new DataTable();
                    double totalGradePoints = 0;
                    double totalCreditHour = 0;
                    double grandTotalGradePoints = 0;
                    double grandTotalCreditHour = 0;
                    double GPA = 0;
                    double CGPA = 0;
                    string lastStudentGUID = "";

                    DataTable dtExamResultPerCourse = new DataTable();

                    for (int i = 0; i < dtExamResult.Rows.Count; i++)
                    {
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                        {
                            dtExamResultPerCourse = new DataTable();

                            con.Open();
                            GetCommand = new SqlCommand("SELECT * FROM ExamResultPerCourse epc LEFT JOIN Course C ON epc.CourseGUID = C.CourseGUID WHERE ExamResultGUID = @ExamResultGUID ", con);

                            GetCommand.Parameters.AddWithValue("@ExamResultGUID", dtExamResult.Rows[i]["ExamResultGUID"].ToString());

                            reader = GetCommand.ExecuteReader();

                            dtExamResultPerCourse.Load(reader);
                            con.Close();

                            totalGradePoints = 0;
                            totalCreditHour = 0;
                            GPA = 0;
                            CGPA = 0;

                            for (int j = 0; j < dtExamResultPerCourse.Rows.Count; j++)
                            {
                                double gradePoints = 0;

                                if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "A")
                                {
                                    gradePoints = 4.0000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "A-")
                                {
                                    gradePoints = 3.7500;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "B+")
                                {
                                    gradePoints = 3.5000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "B")
                                {
                                    gradePoints = 3.0000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "B-")
                                {
                                    gradePoints = 2.7500;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "C+")
                                {
                                    gradePoints = 2.5000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "C")
                                {
                                    gradePoints = 2.0000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "F")
                                {
                                    gradePoints = 0.0000;
                                }

                                totalGradePoints += double.Parse(dtExamResultPerCourse.Rows[j]["CreditHour"].ToString()) * gradePoints;
                                totalCreditHour += double.Parse(dtExamResultPerCourse.Rows[j]["CreditHour"].ToString());
                            }

                            grandTotalGradePoints += totalGradePoints;
                            grandTotalCreditHour += totalCreditHour;
                        }

                        GPA = totalGradePoints / totalCreditHour;
                        CGPA = grandTotalGradePoints / grandTotalCreditHour;

                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                        {
                            con.Open();

                            SqlCommand updateCommand = new SqlCommand("UPDATE ExamResult SET GPA = @GPA, CGPA = @CGPA, ReleaseDate = @ReleaseDate WHERE ExamResultGUID = @ExamResultGUID ", con);

                            updateCommand.Parameters.AddWithValue("@ExamResultGUID", dtExamResult.Rows[i]["ExamResultGUID"]);
                            updateCommand.Parameters.AddWithValue("@GPA", String.Format("{0:0.0000}", GPA));
                            updateCommand.Parameters.AddWithValue("@CGPA", String.Format("{0:0.0000}", CGPA));
                            updateCommand.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);

                            updateCommand.ExecuteNonQuery();

                            if (lastStudentGUID != dtExamResult.Rows[i]["StudentGUID"].ToString())
                            {
                                GetCommand = new SqlCommand("SELECT * FROM CurrentSessionSemester css WHERE StudentGUID = @StudentGUID", con);
                                GetCommand.Parameters.AddWithValue("@StudentGUID", dtExamResult.Rows[i]["StudentGUID"]);
                                reader = GetCommand.ExecuteReader();
                                dtCss.Load(reader);
                            }
                            con.Close();

                            lastStudentGUID = dtExamResult.Rows[i]["StudentGUID"].ToString();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }
    }
}