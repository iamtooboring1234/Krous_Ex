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
    public partial class CourseTimetableEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                
                if (Session["InsertCourseTimeTable"] != null)
                {
                    if (Session["InsertCourseTimeTable"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                        Session["InsertCourseTimeTable"] = null;
                    }
                }

                loadProgrammeCategory();
                loadSession();
                loadGroup();
                loadStaff();
                loadSemester();
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
            if (!string.IsNullOrEmpty(ddlProgrammeCategory.SelectedValue))
            {
                ddlProgramme.Enabled = true;
                loadProgramme(ddlProgrammeCategory.SelectedValue);
            }
            else
            {
                ddlProgramme.Enabled = false;
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

        private void loadSession()
        {
            try
            {
                ddlSession.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlSession.Items.Add(oList);

                string sqlQuery = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                sqlQuery = "SELECT * FROM Session ORDER BY SessionYear, SessionMonth ";
                
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dt.Rows[i]["SessionYear"].ToString() + dt.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                        oList.Value = dt.Rows[i]["SessionGUID"].ToString();
                        ddlSession.Items.Add(oList);
                    }

                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadCourse()
        {
            try
            {
                ddlCourse.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlCourse.Items.Add(oList);

                string sqlQuery = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                sqlQuery = "SELECT * FROM Course ORDER BY CourseName";

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dt.Rows[i]["CourseName"].ToString() + " (" + dt.Rows[i]["CourseAbbrv"].ToString().PadLeft(2, '0') + ")";
                        oList.Value = dt.Rows[i]["CourseGUID"].ToString();
                        ddlCourse.Items.Add(oList);
                    }
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadStaff()
        {
            try
            {
                ddlStaff.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlStaff.Items.Add(oList);

                string sqlQuery = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                sqlQuery = "SELECT * FROM Staff WHERE StaffRole = 'Academic Staff' ORDER BY StaffFullName";

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dt.Rows[i]["StaffFullName"].ToString();
                        oList.Value = dt.Rows[i]["StaffGUID"].ToString();
                        ddlStaff.Items.Add(oList);
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

        protected void radClassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radClassType.SelectedValue == "Main")
            {
                panelMain.Visible = true;
                panelReplacement.Visible = false;
            } else
            {
                panelMain.Visible = false;
                panelReplacement.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(isStaffInChargeCourse())
            {
                if (isGroupSessionSemesterHasStudent())
                {
                    if (InsertCourseTimeTable())
                    {
                        //duplicate check and crashed time check
                        Session["InsertCourseTimeTable"] = "Yes";
                        Response.Redirect("CourseTimetableEntry");
                    } else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Failed to insert! Please try again.");
                    }
                } else
                {
                    clsFunction.DisplayAJAXMessage(this, "Selected session, semester, programme or group may not exist any students. Please try again.");
                }
            } else
            {
                clsFunction.DisplayAJAXMessage(this, "The selected staff is not in charged of this course. Please head to Course In Charge to manage it.");
            }
        }

        private bool InsertCourseTimeTable()
        {
            try
            {
                DataTable dtSemester;

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    string sqlQuery = "SELECT ProgrammeCourseGUID FROM CurrentSessionSemester css ";
                    sqlQuery += "LEFT JOIN Student S ON css.StudentGUID = S.StudentGUID ";
                    sqlQuery += "LEFT JOIN ProgrammeCourse pc ON Css.SemesterGUID = pc.SemesterGUID ";
                    sqlQuery += "LEFT JOIN Session ss ON S.SessionGUID = ss.SessionGUID ";
                    sqlQuery += "WHERE pc.ProgrammeGUID = @ProgrammeGUID ";
                    sqlQuery += "AND CourseGUID = @CourseGUID ";
                    sqlQuery += "AND pc.SemesterGUID = @SemesterGUID ";
                    sqlQuery += "AND css.SessionGUID = @SessionGUID ";
                    sqlQuery += "AND pc.SessionMonth = ss.SessionMonth ";
                    sqlQuery += "GROUP BY ProgrammeCourseGUID ";

                    con.Open();
                    SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                    GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                    GetCommand.Parameters.AddWithValue("@CourseGUID", ddlCourse.SelectedValue);
                    GetCommand.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                    GetCommand.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);

                    SqlDataReader reader = GetCommand.ExecuteReader();

                    dtSemester = new DataTable();
                    dtSemester.Load(reader);
                    con.Close();
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    con.Open();

                    SqlCommand insertCommand = new SqlCommand("INSERT INTO TimetableCourse VALUES(newID(), @ProgrammeCourseGUID, @SessionGUID, @GroupGUID, @ClassStartTime, @ClassEndTime, @DaysOfWeek, @ClassType, @ClassCategory) ", con);
                    
                    insertCommand.Parameters.AddWithValue("@ProgrammeCourseGUID", dtSemester.Rows[0]["ProgrammeCourseGUID"]);
                    insertCommand.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                    insertCommand.Parameters.AddWithValue("@GroupGUID", ddlGroup.SelectedValue);
                    insertCommand.Parameters.AddWithValue("@ClassStartTime", txtClassStartTime.Text);
                    insertCommand.Parameters.AddWithValue("@ClassEndTime", txtClassEndTime.Text);
                    insertCommand.Parameters.AddWithValue("@DaysOfWeek", ddlWeekDay.SelectedValue);
                    insertCommand.Parameters.AddWithValue("@ClassType", radClassType.SelectedValue);
                    insertCommand.Parameters.AddWithValue("@ClassCategory", radClassCategory.SelectedValue);

                    insertCommand.ExecuteNonQuery();

                    con.Close();
                    return true;
                }

            } catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }

        private bool isGroupSessionSemesterHasStudent()
        {
            try
            {
                string sqlQuery = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                sqlQuery = "SELECT* FROM CurrentSessionSemester css ";
                sqlQuery += "LEFT JOIN GroupStudentList gsl ON Css.StudentGUID = gsl.StudentGUID ";
                sqlQuery += "LEFT JOIN Student S ON gsl.StudentGUID = S.StudentGUID ";
                sqlQuery += "LEFT JOIN Student_Programme_Register spr ON S.StudentGUID = spr.StudentGUID ";
                sqlQuery += "WHERE css.SessionGUID = @SessionGUID ";
                sqlQuery += "AND SemesterGUID = @SemesterGUID ";
                sqlQuery += "AND GroupGUID = @GroupGUID ";
                sqlQuery += "AND ProgrammeGUID = @ProgrammeGUID ";

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                GetCommand.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                GetCommand.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                GetCommand.Parameters.AddWithValue("@GroupGUID", ddlGroup.SelectedValue);
                GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
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

        private bool isStaffInChargeCourse()
        {
            try
            {
                string sqlQuery = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                sqlQuery = "SELECT * FROM Course_In_Charge WHERE StaffGUID = @StaffGUID ";

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                GetCommand.Parameters.AddWithValue("@StaffGUID", ddlStaff.SelectedValue);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    return true;
                } else
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

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlCourse.Items.Clear();

                if (!string.IsNullOrEmpty(ddlProgramme.SelectedValue) && !string.IsNullOrEmpty(ddlSession.SelectedValue) && !string.IsNullOrEmpty(ddlSemester.SelectedValue) && !string.IsNullOrEmpty(ddlGroup.SelectedValue))
                {
                    ListItem oList = new ListItem();

                    oList = new ListItem();
                    oList.Text = "";
                    oList.Value = "";
                    ddlCourse.Items.Add(oList);

                    string sqlQuery = "";
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                    con.Open();

                    sqlQuery = "SELECT * FROM ProgrammeCourse pc LEFT JOIN Course C ON Pc.CourseGUID = C.CourseGUID WHERE ProgrammeGUID = @ProgrammeGUID AND SemesterGUID = @SemesterGUID ";

                    SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                    GetCommand.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                    GetCommand.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);

                    SqlDataReader reader = GetCommand.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    con.Close();

                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["CourseName"].ToString().ToUpper().Equals("INDUSTRIAL TRAINING"))
                            {

                            }
                            else
                            {
                                oList = new ListItem();
                                oList.Text = dt.Rows[i]["CourseName"].ToString() + " (" + dt.Rows[i]["CourseAbbrv"].ToString().PadLeft(2, '0') + ") : " + dt.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                                oList.Value = dt.Rows[i]["CourseGUID"].ToString();
                                ddlCourse.Items.Add(oList);
                            }
                        }
                    }

                } else
                {

                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void txtReplacementClassStartTime_TextChanged(object sender, EventArgs e)
        {
        }

        protected void txtReplacementClassEndTime_TextChanged(object sender, EventArgs e)
        {

        }
    }
}