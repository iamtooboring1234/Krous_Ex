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
    public partial class ExaminationDashboard : System.Web.UI.Page
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
                sqlQuery = "SELECT * FROM ExamTimetable et LEFT JOIN Course C ON et.CourseGUID = C.CourseGUID LEFT JOIN ExamPreparation ep ON et.ExamTimetableGUID = ep.ExamTimetableGUID ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand getCommand = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = getCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    lblNoExam.Visible = false;

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
                        strExam += "<h4 class=\"mb-0\"><i class=\"fas fa-edit mr-2\"></i>Assessment Title 1</h4>";
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

                        if (!String.IsNullOrEmpty(dt.Rows[i]["QuestionPaper"].ToString()))
                        {
                            strExam += "<p><i class=\"fas fa-check-circle mr-2\" style=\"color:#00d25b\"></i>Question Paper Uploaded</p>";
                        } else
                        {
                            strExam += "<p><i class=\"fas fa-times-circle mr-2\" style=\"color:#fc424a\"></i>Question Paper Is Not Uploaded Yet</p>";
                        }

                        if (!String.IsNullOrEmpty(dt.Rows[i]["AnswerSheet"].ToString()))
                        {
                            strExam += "<p><i class=\"fas fa-check-circle mr-2\" style=\"color:#00d25b\"></i>Answer Sheet Provided</p>";
                        }
                        else
                        {
                            strExam += "<p><i class=\"fas fa-times-circle mr-2\" style=\"color:#fc424a\"></i>Answer Sheet Is Not Provided Yet</p>";
                        }

                        strExam += "</div>";
                        strExam += "<hr />";

                        strExam += "<div class=\"row float-right\">";
                        if (DateTime.Now <= DateTime.Parse(dt.Rows[i]["ExamEndDateTime"].ToString()).AddDays(1))
                        {
                            if (DateTime.Now <= DateTime.Parse(dt.Rows[i]["ExamStartDateTime"].ToString()).AddDays(-1))
                            {
                                strExam += "<a href=\"ExaminationPrePreparationEntry?ExaminationPreparationGUID=" + dt.Rows[i]["ExaminationPreparationGUID"] + "\" class=\"btn btn-primary mr-2\">Manage</a>";
                            }
                            
                            if (DateTime.Now >= DateTime.Parse(dt.Rows[i]["ExamStartDateTime"].ToString()).AddMinutes(-30)) //change to datetime.now
                            {
                                strExam += "<a href=\"#\" class=\"btn btn-primary\">Mark Attendance</a>";
                            }
                        } else
                        {
                            strExam += "<a href=\"#\" class=\"btn btn-primary\">View Submission</a>";
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
                    }

                    litExamination.Text = strExam;
                }
                else
                {
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