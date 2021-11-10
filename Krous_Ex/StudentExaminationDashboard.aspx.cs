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
    public partial class StudentExaminationDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadExamination();
            }
        }
        private void loadExamination()
        {
            try
            {
                string sqlQuery;
                string strExam = "";
                int count = 0;

                sqlQuery = "SELECT* FROM ExamTimetable et ";
                sqlQuery += "LEFT JOIN Course C ON et.CourseGUID = C.CourseGUID ";
                sqlQuery += "LEFT JOIN ExamPreparation ep ON et.ExamTimetableGUID = ep.ExamTimetableGUID ";
                sqlQuery += "LEFT JOIN CurrentSessionSemester css ON css.SessionGUID = et.SessionGUID ";
                sqlQuery += "INNER JOIN ProgrammeCourse pc ON css.SemesterGUID = pc.SemesterGUID AND et.CourseGUID = pc.CourseGUID ";
                sqlQuery += "WHERE css.StudentGUID = @StudentGUID ";
                sqlQuery += "ORDER BY CourseName, ExamStartDateTime ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand getCommand = new SqlCommand(sqlQuery, con);

                getCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());

                SqlDataReader reader = getCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    lblNoExam.Visible = false;

                    DataTable dtExamTimeTable = new DataTable();

                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                    {
                        con.Open();
                        getCommand = new SqlCommand("SELECT * FROM AcademicCalender WHERE GETDATE() BETWEEN SemesterStartDate AND SemesterEndDate ", con);
                        reader = getCommand.ExecuteReader();
                        dtExamTimeTable.Load(reader);
                        con.Close();
                    }

                    DateTime examStartDate = DateTime.Parse(dtExamTimeTable.Rows[0]["SemesterEndDate"].ToString()).AddDays(-(int.Parse(dtExamTimeTable.Rows[0]["SemesterBreakDuration"].ToString()) + int.Parse(dtExamTimeTable.Rows[0]["SemesterExaminationDuration"].ToString()) + 1));
                    DateTime examEndDate = DateTime.Parse(dtExamTimeTable.Rows[0]["SemesterEndDate"].ToString()).AddDays(-(int.Parse(dtExamTimeTable.Rows[0]["SemesterBreakDuration"].ToString())));
                    examStartDate = examStartDate.AddHours(9);

                    DateTime test = DateTime.Parse("21-dec-2021 8:30:00");
                    if (test >= examStartDate.AddMinutes(-30))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (count % 3 == 0)
                            {
                                strExam = "<div class=\"row\">";
                                strExam += "<div class=\"col-md-12\">";
                            }

                            strExam += "<div class=\"col-sm-4 grid-margin stretch-card float-left\" style=\"margin-top: 30px; \">";
                            strExam += "<div class=\"card\">";
                            strExam += "<div class=\"card-body\">";
                            strExam += "<div class=\"row\">";
                            strExam += "<div class=\"col-sm-12 col-xl-12 my-auto\">";
                            strExam += "<div class=\"d-flex d-sm-block align-items-center\">";
                            strExam += "<h4 class=\"mb-0\"><i class=\"fas fa-edit mr-2\"></i>" + dt.Rows[i]["CourseName"] + "</h4>";
                            strExam += "</div>";
                            strExam += "<hr />";
                            strExam += "<h6 class=\"text-muted font-weight-normal\">";
                            strExam += "<p class=\"mb-1\" style=\"color: white\">Exam Start Time : </p>";
                            strExam += "Created Date";
                            strExam += "</h6>";
                            strExam += "<h6 class=\"text-muted font-weight-normal\">";
                            strExam += "<p class=\"mb-1\" style=\"color: white\">Exam End Time : </p>";
                            strExam += "Created Date";
                            strExam += "</h6>";
                            strExam += "<hr />";
                            strExam += "<div class=\"d-flex d-sm-block align-items-center\">";

                            if (!string.IsNullOrEmpty(dt.Rows[i]["QuestionPaper"].ToString()))
                            {
                                strExam += "<p><i class=\"fas fa-check-circle mr-2\" style=\"color:#00d25b\"></i>Question Paper Uploaded</p>";
                            }
                            else
                            {
                                strExam += "<p><i class=\"fas fa-times-circle mr-2\" style=\"color:#fc424a\"></i>Question Paper Is Not Uploaded Yet</p>";
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[i]["AnswerSheet"].ToString()))
                            {
                                strExam += "<p><i class=\"fas fa-check-circle mr-2\" style=\"color:#00d25b\"></i>Answer Sheet Provided</p>";
                            }
                            else
                            {
                                strExam += "<p><i class=\"fas fa-times-circle mr-2\" style=\"color:#fc424a\"></i>Answer Sheet Is Not Provided Yet</p>";
                            }

                            strExam += "</div>";
                            strExam += "<hr />";

                            DateTime test2 = DateTime.Parse("21/12/2021 10:00:00");

                            strExam += "<div class=\"row float-right\">";
                            if (test2 <= DateTime.Parse(dt.Rows[i]["ExamEndDateTime"].ToString()).AddHours(1))
                            {
                                if (test2 >= DateTime.Parse(dt.Rows[i]["ExamStartDateTime"].ToString()).AddMinutes(-10))
                                {
                                    strExam += "<a href=\"StudentExaminationSubmission?ExamTimetableGUID=" + dt.Rows[i]["ExamTimetableGUID"] + "\" class=\"btn btn-primary\">Join</a>";
                                }
                            }
                            else
                            {
                                strExam += "<a href=\"StudentExaminationSubmission?ExamTimetableGUID=" + dt.Rows[i]["ExamTimetableGUID"] + "\" class=\"btn btn-primary\">View Submission</a>";
                            }

                            strExam += "</div>";
                            strExam += "</div>";
                            strExam += "</div>";
                            strExam += "</div>";
                            strExam += "</div>";
                            strExam += "</div>";

                            count++;

                            if (count % 3 == 0)
                            {
                                strExam += "</div>";
                                strExam += "</div>";
                            }

                            litExamination.Text = strExam;

                        }
                    }

                }
                else
                {
                    lblNoExam.Text = "You're not available to see the examination submission yet.";
                    lblNoExam.Visible = true;
                }
            
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }
}