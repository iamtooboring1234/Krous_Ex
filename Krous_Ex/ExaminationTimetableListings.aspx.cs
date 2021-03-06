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
    public partial class ExaminationTimetableListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                txtExamDate.Text = DateTime.Now.ToString();
                loadGV();
            }
        }

        protected void loadGV()
        {
            try
            {
                string sqlQuery;
                sqlQuery = "SELECT * FROM ExamTimetable E, Course C WHERE E.CourseGUID = C.CourseGUID AND E.SessionGUID = (SELECT S.SessionGUID FROM AcademicCalender A, Session S WHERE S.SessionGUID = A.SessionGUID AND GetDate() BETWEEN A.SemesterStartDate AND A.SemesterEndDate) ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand getCommand = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = getCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvExamTime.DataSource = dt;
                    gvExamTime.DataBind();
                    gvExamTime.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvExamTime.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchQuery;

                searchQuery = "SELECT * FROM ExamTimetable E, Course C WHERE E.CourseGUID = C.CourseGUID AND E.SessionGUID = (SELECT S.SessionGUID FROM AcademicCalender A, Session S WHERE S.SessionGUID = A.SessionGUID AND GetDate() BETWEEN A.SemesterStartDate AND A.SemesterEndDate) ";
                searchQuery += " AND @SelectedDate BETWEEN E.ExamStartDateTime AND E.ExamEndDateTime ";
                searchQuery += " AND CASE WHEN @CourseName = '' THEN @CourseName ELSE CourseName END LIKE '%'+@CourseName+'%'";
               

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand searchCmd = new SqlCommand(searchQuery, con);

                searchCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);

                if (!string.IsNullOrEmpty(txtExamDate.Text))
                {
                    searchCmd.Parameters.AddWithValue("@SelectedDate", DateTime.Parse(txtExamDate.Text).ToString("yyyy-MMM-dd HH:mm:ss"));
                } else
                {
                    searchCmd.Parameters.AddWithValue("@SelectedDate", DBNull.Value);
                }

                SqlDataReader reader = searchCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvExamTime.DataSource = dt;
                    gvExamTime.DataBind();
                    gvExamTime.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvExamTime.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExaminationTimetableEntry");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            loadGV();
        }
    }
}