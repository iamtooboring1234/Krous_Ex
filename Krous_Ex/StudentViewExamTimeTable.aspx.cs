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
    public partial class StudentViewExamTimeTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                if (IsPostBack != true)
                {
                    loadExamtime();
                }

            }
            else
            {
                Response.Redirect("StudentLogin");
            }
        }

        private void loadExamtime()
        {
            try
            {

                string sqlQuery;
                string strTable = "";

                sqlQuery = "SELECT * ";
                sqlQuery += "FROM ProgrammeCourse pc, CurrentSessionSemester css, Semester s, Session ss, student st, Course C, ExamTimetable ex ";
                sqlQuery += "WHERE css.SemesterGUID = pc.SemesterGUID ";
                sqlQuery += "AND css.SemesterGUID = s.SemesterGUID ";
                sqlQuery += "AND css.SessionGUID = ss.SessionGUID ";
                sqlQuery += "AND css.StudentGUID = st.StudentGUID ";
                sqlQuery += "AND pc.CourseGUID = c.CourseGUID ";
                sqlQuery += "AND css.StudentGUID = @StudentGUID ";
                sqlQuery += "AND c.CourseGUID = ex.CourseGUID ";
                sqlQuery += "AND pc.SessionMonth = (SELECT S.SessionMonth FROM Session S, Student St WHERE S.SessionGUID = St.SessionGUID AND St.StudentGUID = @StudentGUID) ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                GetCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    //DateTime now = DateTime.Now;
                    DateTime now = DateTime.Parse("19-dec-2021");
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                    con.Open();

                    GetCommand = new SqlCommand("SELECT* FROM AcademicCalender A WHERE GETDATE() BETWEEN SemesterStartDate AND SemesterEndDate ", con);

                    reader = GetCommand.ExecuteReader();
                    DataTable dtDate = new DataTable();
                    dtDate.Load(reader);
                    con.Close();

                    DateTime examStartDate = DateTime.Parse(dtDate.Rows[0]["SemesterEndDate"].ToString()).AddDays(-(int.Parse(dtDate.Rows[0]["SemesterBreakDuration"].ToString()) + int.Parse(dtDate.Rows[0]["SemesterExaminationDuration"].ToString()) + 1));
                    DateTime examEndDate = DateTime.Parse(dtDate.Rows[0]["SemesterEndDate"].ToString()).AddDays(-(int.Parse(dtDate.Rows[0]["SemesterBreakDuration"].ToString())));

                    if (now >= examStartDate.AddDays(-14))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            strTable = "<div class=\"row\">";
                            strTable += "<div class=\"col-md-8 grid-margin stretch-card\">";
                            strTable += "<div class=\"d-flex flex-row\">";
                            strTable += "<div class=\"mr-3 text-center ml-3\">";
                            strTable += "<div class=\"p-4 border border-primary\">";
                            strTable += "<p class=\"m-0\">" + DateTime.Parse(dt.Rows[i]["ExamStartDateTime"].ToString()).ToString("dd") + "</p>";
                            strTable += "<p class=\"m-0\">" + DateTime.Parse(dt.Rows[i]["ExamStartDateTime"].ToString()).ToString("MMM") + "</p>";
                            strTable += "</div>";
                            strTable += "</div>";
                            strTable += "<div class=\"row\">";
                            strTable += "<div class=\"col-md-12 align-self-center\">";
                            strTable += "<div class=\"d-flex flex-column\">";
                            strTable += "<h6>" + dt.Rows[i]["CourseAbbrv"] + " " + dt.Rows[i]["CourseName"] + "</h6>";
                            strTable += "<p class=\"m-0 text-info\">" + DateTime.Parse(dt.Rows[i]["ExamStartDateTime"].ToString()).ToString("hh:mm tt") + " to " + DateTime.Parse(dt.Rows[i]["ExamEndDateTime"].ToString()).ToString("hh:mm tt") + "</p></div>";
                            strTable += "</div>";
                            strTable += "</div>";
                            strTable += "</div>";
                            strTable += "</div>";
                            strTable += "</div>";
                        }

                    }
                    else
                    {
                        lblNotYet.Visible = true;
                        lblNotYet.Text = "You're not allowed to view the exam timetable yet.";
                    }

                    litExamTime.Text = strTable;

                }
                else
                {
                    lblNotYet.Visible = true;
                    lblNotYet.Text = "This semester may or may not have final examination. Please wait for further notice.";
                }

            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}