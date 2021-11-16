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
                sqlQuery = "SELECT * FROM ExamTimetable et LEFT JOIN Course C ON et.CourseGUID = C.CourseGUID LEFT JOIN ExamPreparation ep ON et.ExamTimetableGUID = ep.ExamTimetableGUID LEFT JOIN MeetingLink ml ON et.MeetingLinkGUID = ml.MeetingLinkGUID ";

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
                            strExam += "<div class=\"row\">";
                            strExam += "<div class=\"col-md-12\">";
                        }

                        strExam += "<div class=\"col-sm-4 grid-margin stretch-card float-left\" style=\"margin-top: 30px; \">";
                        strExam += "<div class=\"card\">";
                        strExam += "<div class=\"card-body\">";
                        strExam += "<div class=\"row\">";
                        strExam += "<div class=\"col-sm-12 col-xl-12 my-auto\">";
                        strExam += "<div class=\"d-flex d-sm-block align-items-center\">";
                        strExam += "<h4 class=\"mb-0\"><i class=\"fas fa-edit mr-2\"></i>"+ dt.Rows[i]["CourseName"] + "</h4>";
                        strExam += "</div>";
                        strExam += "<hr />";
                        strExam += "<table style=\"font-size: 0.875rem;\">";
                        strExam += "<tr>";
                        strExam += "<td class=\"mb-1 text-muted\">Exam Start Time </td> <td>:</td> </tr> <tr colspan=\"2\"> <td>" + dt.Rows[i]["ExamStartDateTime"] + "</td> </tr>";
                        strExam += "<tr>";
                        strExam += "<td class=\"mb-1 text-muted\">Exam End Time </td> <td>:</td> </tr> <tr colspan=\"2\"> <td>" + dt.Rows[i]["ExamEndDateTime"] + "</td> </tr>";
                        if (!string.IsNullOrEmpty(dt.Rows[i]["MeetingLinkGUID"].ToString()))
                        {
                            strExam += "<tr>";
                            strExam += "<td class=\"mb-1 text-muted\">Meeting Link </td> <td>:</td> </tr> <tr colspan=\"2\"> <td>" + "<a href=\"Testing3?MeetingLinkGUID=" + dt.Rows[i]["MeetingLinkGUID"] + "\" class=\"btn btn-primary\">Join</a></td>";
                            strExam += "</tr>";
                        }
                        strExam += "</table>";
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