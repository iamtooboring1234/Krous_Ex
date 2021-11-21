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
    public partial class StudentViewAssessment : System.Web.UI.Page
    {
        Guid userGuid;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGuid = Guid.Parse(clsLogin.GetLoginUserGUID());

            loadAssessment();
        }
        private void loadAssessment()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                string strAssessment = "";
                int count = 0;
                string sqlQuery;
                // '1f9dc3a7-c11c-4737-91f4-fa4848fa4f14' 202201
                //'f8a8fc26-c7ba-498c-b195-4b80cf7d0f54' y1s1
                //'28630E63-31C2-4418-9297-5D389CC62F0F' rsd

                //get student current session and semester
                SqlCommand getCssCmd = new SqlCommand("SELECT SessionGUID, SemesterGUID FROM CurrentSessionSemester WHERE StudentGUID = @StudentGUID ", con);
                getCssCmd.Parameters.AddWithValue("@StudentGUID", userGuid);
                SqlDataReader dtrStudCss = getCssCmd.ExecuteReader();
                DataTable dtStudCss = new DataTable();
                dtStudCss.Load(dtrStudCss);
                Guid sessionGUID = Guid.Parse(dtStudCss.Rows[0]["SessionGUID"].ToString());
                Guid semesterGUID = Guid.Parse(dtStudCss.Rows[0]["SemesterGUID"].ToString());

                //get the student programme registered
                SqlCommand getStudProgRegCmd = new SqlCommand("SELECT ProgrammeGUID FROM Student_Programme_Register WHERE StudentGUID = @StudentGUID ", con);
                getStudProgRegCmd.Parameters.AddWithValue("@StudentGUID", userGuid);
                SqlDataReader dtrStudProg = getStudProgRegCmd.ExecuteReader();
                DataTable dtStudProg = new DataTable();
                dtStudProg.Load(dtrStudProg);
                Guid programmeGUID = Guid.Parse(dtStudProg.Rows[0]["ProgrammeGUID"].ToString());
  
                //load assessment
                sqlQuery = "SELECT a.AssessmentGUID, s.StaffFullName, a.AssessmentTitle, CONVERT(VARCHAR, a.CreatedDate, 100) as CreatedDate, CONVERT(VARCHAR, a.DueDate, 100) as DueDate, c.CourseAbbrv, c.CourseName ";
                sqlQuery += "FROM Assessment a ";
                sqlQuery += "LEFT JOIN Course c ON a.CourseGUID = c.CourseGUID ";
                sqlQuery += "LEFT JOIN Staff s ON a.StaffGUID = s.StaffGUID ";
                sqlQuery += "LEFT JOIN CurrentSessionSemester css ON A.SessionGUID = css.SessionGUID AND A.SemesterGUID = css.SemesterGUID ";
                sqlQuery += "INNER JOIN GroupStudentList gsl ON css.StudentGUID = gsl.StudentGUID ";
                sqlQuery += "INNER JOIN Student_Programme_Register spr ON css.StudentGUID = spr.StudentGUID ";
                sqlQuery += "WHERE a.SessionGUID = @SessionGUID AND ";
                sqlQuery += "a.SemesterGUID = @SemesterGUID AND ";
                sqlQuery += "a.ProgrammeGUID = @ProgrammeGUID AND ";
                sqlQuery += "spr.ProgrammeGUID = @ProgrammeGUID AND ";
                sqlQuery += "css.StudentGUID = @StudentGUID ";

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@SessionGUID", sessionGUID);
                GetCommand.Parameters.AddWithValue("@SemesterGUID", semesterGUID);
                GetCommand.Parameters.AddWithValue("@ProgrammeGUID", programmeGUID);
                GetCommand.Parameters.AddWithValue("@StudentGUID", userGuid);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtAssessment = new DataTable();
                dtAssessment.Load(reader);


                con.Close();

                if (dtAssessment.Rows.Count != 0)
                {
                    for (int i = 0; i < dtAssessment.Rows.Count; i++)
                    {
                        if (count % 3 == 0)
                        {
                            strAssessment += "<div class=\"row\">";
                            strAssessment += "<div class=\"col-md-12\">";
                        }

                        strAssessment += "<div class=\"col-sm-4 grid-margin stretch-card float-left\" style=\"margin-top: 30px;\">";
                        strAssessment += "<div class=\"card\">";
                        strAssessment += "<div class=\"card-body\">";
                        strAssessment += "<h5>" + dtAssessment.Rows[i]["CourseName"].ToString() + " (" + dtAssessment.Rows[i]["CourseAbbrv"].ToString() + ")" + "</h5>";
                        strAssessment += "<h5>" + dtAssessment.Rows[i]["StaffFullName"].ToString() + "</h5>";
                        strAssessment += "<hr />";
                        strAssessment += "<div class=\"row\">";
                        strAssessment += "<div class=\"col-sm-12 col-xl-12 my-auto\">";
                        strAssessment += "<div class=\"d-flex d-sm-block align-items-center\">";
                        strAssessment += "<h4 class=\"mb-0\"><i class=\"fas fa-tasks\" style=\"margin-right:6px;\"></i>" + dtAssessment.Rows[i]["AssessmentTitle"].ToString() + "</h4>";
                        strAssessment += "</div>";
                        strAssessment += "<hr />";
                        strAssessment += "<h6 class=\"text-muted font-weight-normal\">Created  : <strong>" + dtAssessment.Rows[i]["CreatedDate"].ToString() + "</strong></h6>";
                        strAssessment += "<h6 class=\"text-muted font-weight-normal\">Due Date : <strong>" + dtAssessment.Rows[i]["DueDate"].ToString() + "</strong></h6>";
                        strAssessment += "<hr />";
                        strAssessment += "<a href=\"StudentAssessmentSubmission.aspx?AssessmentGUID=" + dtAssessment.Rows[i]["AssessmentGUID"] + "\" class=\"btn btn-outline-warning btn-fw float-right\"style=\"width:30%\">View</a>";
                        strAssessment += "</div>";
                        strAssessment += "</div>";
                        strAssessment += "</div>";
                        strAssessment += "</div>";
                        strAssessment += "</div>";

                        count++;

                        if (count % 3 == 0)
                        {
                            strAssessment += "</div>";
                            strAssessment += "</div>";
                        }
                        
                    }
                    litAssessment.Text = strAssessment;
                }
                else
                {
                    lblNoData.Visible = true;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }   
        }
    }
}