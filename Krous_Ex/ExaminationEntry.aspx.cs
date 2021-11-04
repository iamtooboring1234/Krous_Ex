using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class ExaminationEntry : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                if (IsPostBack != true)
                {
                    if (Session["InsertExam"] != null)
                    {
                        if (Session["InsertExam"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                            Session["InsertExam"] = null;
                        }
                    }

                    if (Session["UpdateExam"] != null)
                    {
                        if (Session["UpdateExam"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUpdateSuccessToast(); ", true);
                            Session["UpdateExam"] = null;
                        }
                    }

                    if (Session["DeleteExam"] != null)
                    {
                        if (Session["DeleteExam"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showDeleteSuccessToast(); ", true);
                            Session["DeleteExam"] = null;
                        }
                    }

                    loadSession();
                    loadCourse();

                    if (!String.IsNullOrEmpty(Request.QueryString["ExamTimeTableGUID"]))
                    {
                        loadExam();
                        ddlCourse.Enabled = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnBack.Visible = false;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;
                    }

                }
            } else
            {
                Response.Redirect("Homepage");
            }
        }

        private void loadExam()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCourseCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCourseCmd = new SqlCommand("SELECT * FROM ExamTimeTable E, Course C WHERE E.CourseGUID = E.CourseGUID AND ExamTimeTableGUID = @ExamTimeTableGUID", con);
                loadCourseCmd.Parameters.AddWithValue("@ExamTimeTableGUID", Request.QueryString["ExamTimeTableGUID"]);
                SqlDataReader reader = loadCourseCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                if (dt.Rows.Count != 0)
                {
                    ddlCourse.Text = dt.Rows[0]["CourseName"].ToString() + " (" + dt.Rows[0]["CourseAbbrv"].ToString() + ")";
                    txtExamDate.Text = DateTime.Parse(dt.Rows[0]["ExamStartDateTime"].ToString()).ToString("dd-MMM-yyyy");
                    txtStartTime.Text = DateTime.Parse(dt.Rows[0]["ExamStartDateTime"].ToString()).ToString("hh:mm tt");
                    txtEndTime.Text = DateTime.Parse(dt.Rows[0]["ExamEndDateTime"].ToString()).ToString("hh:mm tt");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void loadSession()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT S.SessionGUID, S.SessionMonth, S.SessionYear FROM AcademicCalender A, Session S WHERE S.SessionGUID = A.SessionGUID AND GetDate() BETWEEN A.SemesterStartDate AND A.SemesterEndDate;", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSession = new DataTable();
                dtSession.Load(reader);
                con.Close();

                if(dtSession.Rows.Count != 0)
                {
                    txtSession.Text = dtSession.Rows[0]["SessionYear"].ToString() + dtSession.Rows[0]["SessionMonth"].ToString().PadLeft(2, '0');
                    hdSession.Value = dtSession.Rows[0]["SessionGUID"].ToString();
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

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT C.CourseGUID, C.CourseName, C.CourseAbbrv FROM Course C, ProgrammeCourse Pc WHERE C.CourseGUID = Pc.CourseGUID GROUP BY C.CourseGUID, C.CourseName, C.CourseAbbrv ORDER BY C.CourseName ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtCourse = new DataTable();
                dtCourse.Load(reader);
                

                for (int i = 0; i <= dtCourse.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtCourse.Rows[i]["CourseName"].ToString() + " (" + dtCourse.Rows[i]["CourseAbbrv"].ToString() + ")";
                    oList.Value = dtCourse.Rows[i]["CourseGUID"].ToString();
                    ddlCourse.Items.Add(oList);
                }

                con.Close();
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExaminationListings");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (checkExistExam())
            {
                if (isBetweenExamDate())
                {
                    if (insertExam())
                    {
                        Session["InsertExam"] = "Yes";
                        Response.Redirect("ExaminationEntry");
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Error! Unable to insert.");
                    }
                } else
                {
                    clsFunction.DisplayAJAXMessage(this, "Exam date is not between " + hdStartDate.Value + " and " + hdEndDate.Value);
                }
            } else
            {
                clsFunction.DisplayAJAXMessage(this, "Selected Course: " + ddlCourse.SelectedItem.Text + " has existing record. Please go to listings to manage it." );
            }
        }

        private bool isBetweenExamDate()
        {
            DateTime startDate = DateTime.Parse(txtExamDate.Text);
            DateTime endDate = DateTime.Parse(txtExamDate.Text);

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand("SELECT * FROM AcademicCalender WHERE SessionGUID = @SessionGUID ", con);

                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtExamTimeTable = new DataTable();
                dtExamTimeTable.Load(reader);
                con.Close();

                if (dtExamTimeTable.Rows.Count != 0)
                {
                    DateTime examStartDate = DateTime.Parse(dtExamTimeTable.Rows[0]["SemesterEndDate"].ToString()).AddDays(-(int.Parse(dtExamTimeTable.Rows[0]["SemesterBreakDuration"].ToString()) + int.Parse(dtExamTimeTable.Rows[0]["SemesterExaminationDuration"].ToString()) + 1));
                    DateTime examEndDate = DateTime.Parse(dtExamTimeTable.Rows[0]["SemesterEndDate"].ToString()).AddDays(-(int.Parse(dtExamTimeTable.Rows[0]["SemesterBreakDuration"].ToString())));

                    hdEndDate.Value = examEndDate.ToString("dd-MMM-yyyy");
                    hdStartDate.Value = examStartDate.ToString("dd-MMM-yyyy");

                    if ((startDate >= examStartDate && startDate <= examEndDate) || (endDate >= examStartDate && endDate <= examEndDate))
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        private bool insertExam()
        {
            try
            {
                DateTime startDate = DateTime.Parse(txtExamDate.Text + " " + txtStartTime.Text);
                DateTime endDate = DateTime.Parse(txtExamDate.Text + " " + txtEndTime.Text);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO ExamTimetable VALUES(NEWID(),@SessionGUID,@CourseGUID,@ExamStartDateTime,@ExamEndDateTime)", con);

                InsertCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);
                InsertCommand.Parameters.AddWithValue("@CourseGUID", ddlCourse.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@ExamStartDateTime", startDate);
                InsertCommand.Parameters.AddWithValue("@ExamEndDateTime", endDate);

                InsertCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, "Input time is not in correct format.");
                return false;
            }
        }

        private bool checkExistExam()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamTimetable WHERE CourseGUID = @CourseGUID ", con);

                GetCommand.Parameters.AddWithValue("@CourseGUID", ddlCourse.SelectedValue);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtExamTimeTable = new DataTable();
                dtExamTimeTable.Load(reader);
                con.Close();

                if (dtExamTimeTable.Rows.Count != 0)
                {
                    return false;
                } else
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (updateExam())
            {
                Session["UpdateExam"] = "Yes";
                Response.Redirect("ExaminationEntry?ExamTimetableGUID=" + Request.QueryString["ExamTimetableGUID"]);
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Error! Unable to update.");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeleteExam())
            {
                Session["DeleteExam"] = "Yes";
                Response.Redirect("ExaminationEntry");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Error! Unable to delete.");
            }
        }

        private bool updateExam()
        {
            DateTime startDate = DateTime.Parse(txtExamDate.Text + " " + txtStartTime.Text);
            DateTime endDate = DateTime.Parse(txtExamDate.Text + " " + txtEndTime.Text);

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand updateCommand = new SqlCommand("UPDATE ExamTimetable SET ExamStartDateTime = @ExamStartDateTime, ExamEndDateTime = @ExamEndDateTime WHERE ExamTimetableGUID = @ExamTimetableGUID ", con);

                updateCommand.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);
                updateCommand.Parameters.AddWithValue("@ExamStartDateTime", startDate);
                updateCommand.Parameters.AddWithValue("@ExamEndDateTime", endDate);

                updateCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        private bool DeleteExam()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand deleteCommand = new SqlCommand("DELETE FROM ExamTimetable WHERE ExamTimetableGUID = @ExamTimetableGUID;", con);

                deleteCommand.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);

                deleteCommand.ExecuteNonQuery();

                con.Close();

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