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
    public partial class ProgrammeCourseListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadProgCourse();

                if (!string.IsNullOrEmpty(Request.QueryString["ProgrammeGUID"]))
                {
                    panelProgCourseDetails.Visible = true;
                    loadProgCourseDetails();
                }
            }

            if (Session["AddedProgrammeCourse"] != null)
            {
                if (Session["AddedProgrammeCourse"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "Programme course added successfully !");
                    Session["AddedProgrammeCourse"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Error! Programme course added  unsuccessfully .");
                    Session["AddedProgrammeCourse"] = null;
                }
            }

        }

        private void loadProgCourse()
        {
            try
            {
                string loadQuery;
                loadQuery = "SELECT P.ProgrammeGUID, P.ProgrammeName, Count(Pc.CourseGUID) AS 'TotalCourseRegistered' FROM Programme P ";
                loadQuery += "INNER JOIN ProgrammeCourse Pc ON P.ProgrammeGUID = Pc.ProgrammeGUID ";
                loadQuery += "GROUP BY P.ProgrammeGUID, P.ProgrammeName ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);

                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvProgCourse.DataSource = dt;
                    gvProgCourse.DataBind();
                    gvProgCourse.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvProgCourse.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void loadProgCourseDetails()
        {
            try
            {
                string loadQuery;

                loadQuery = "SELECT P.ProgrammeGUID, P.ProgrammeCategory, S.SemesterYear, S.SemesterSem, S.SemesterGUID, Count(Pc.CourseGUID) AS 'TotalCoursePerSemester' FROM Programme P ";
                loadQuery += "INNER JOIN ProgrammeCourse Pc ON P.ProgrammeGUID = Pc.ProgrammeGUID ";
                loadQuery += "LEFT JOIN Semester S ON Pc.SemesterGUID = S.SemesterGUID ";
                loadQuery += "WHERE P.ProgrammeGUID = @ProgrammeGUID ";
                loadQuery += "GROUP BY P.ProgrammeGUID, S.SemesterYear, S.SemesterSem, S.SemesterGUID, P.ProgrammeCategory ";
                loadQuery += "ORDER BY S.SemesterYear, S.SemesterSem ";


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);

                loadGVCmd.Parameters.AddWithValue("@ProgrammeGUID", Request.QueryString["ProgrammeGUID"]);

                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvProgCourseDetails.DataSource = dt;
                    gvProgCourseDetails.DataBind();
                    gvProgCourseDetails.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvProgCourseDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }
}