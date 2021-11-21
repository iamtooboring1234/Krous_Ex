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
    public partial class CourseListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["deleteCourse"] != null)
            {
                if (Session["deleteCourse"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "Course has been deleted successfully !");
                    Session["deleteCourse"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Error! Course deleted unsuccessfully.");
                    Session["deleteCourse"] = null;
                }
            }

            if (IsPostBack != true)
            {
                loadCourseAbbrv();
                loadCourseGV();
                lblNoData.Visible = false;
            }
        }

        protected void loadCourseAbbrv()
        {
            try
            {
                ddlCourseAbbrv.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT CourseGUID, CourseAbbrv FROM Course GROUP BY CourseGUID, CourseAbbrv ORDER BY CourseAbbrv", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlCourseAbbrv.DataSource = ds;
                ddlCourseAbbrv.DataTextField = "CourseAbbrv";
                ddlCourseAbbrv.DataValueField = "CourseAbbrv"; 
                ddlCourseAbbrv.DataBind();
                ddlCourseAbbrv.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadCourseGV()
        {
            try
            {
                string loadQuery;
                loadQuery = "SELECT * FROM Course ";
                loadQuery += "WHERE CASE WHEN @CourseAbbrv = '' THEN @CourseAbbrv ELSE CourseAbbrv END = @CourseAbbrv AND "; //ddl
                loadQuery += "CASE WHEN @CourseName = '' THEN @CourseName ELSE CourseName END LIKE '%'+@CourseName+'%' AND "; //text
                loadQuery += "CASE WHEN @CreditHour = '' THEN @CreditHour ELSE CreditHour END LIKE '%'+@CreditHour+'%' AND "; //text
                loadQuery += "Category = @Category "; //all ddl
                loadQuery += "ORDER BY CourseName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);
                loadGVCmd.Parameters.AddWithValue("@CourseAbbrv", ddlCourseAbbrv.SelectedValue);
                loadGVCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                loadGVCmd.Parameters.AddWithValue("@CreditHour", txtCreditHour.Text);
                loadGVCmd.Parameters.AddWithValue("@Category", ddlCourseCategory.SelectedValue);

                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                    gvCourse.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvCourse.Visible = false;
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

                if (ddlCourseCategory.SelectedValue != "All")
                {
                    searchQuery = "SELECT * FROM Course ";
                    searchQuery += "WHERE CASE WHEN @CourseAbbrv = '' THEN @CourseAbbrv ELSE CourseAbbrv END = @CourseAbbrv AND "; //ddl
                    searchQuery += "CASE WHEN @CourseName = '' THEN @CourseName ELSE CourseName END LIKE '%'+@CourseName+'%' AND "; //text
                    searchQuery += "CASE WHEN @CreditHour = '' THEN @CreditHour ELSE CreditHour END LIKE '%'+@CreditHour+'%' AND "; //text
                    searchQuery += "Category = @Category "; //all ddl
                    searchQuery += "ORDER BY CourseName";
                }
                else
                {
                    searchQuery = "SELECT * FROM Course ";
                    searchQuery += "WHERE CASE WHEN @CourseAbbrv = '' THEN @CourseAbbrv ELSE CourseAbbrv END = @CourseAbbrv AND "; //ddl
                    searchQuery += "CASE WHEN @CourseName = '' THEN @CourseName ELSE CourseName END LIKE '%'+@CourseName+'%' AND "; //text
                    searchQuery += "CASE WHEN @CreditHour = 0 THEN @CreditHour ELSE CreditHour END LIKE '%'+@CreditHour+'%' "; //text
                    searchQuery += "ORDER BY CourseName";
                }
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand searchCmd = new SqlCommand(searchQuery, con);

                searchCmd.Parameters.AddWithValue("@CourseAbbrv", ddlCourseAbbrv.SelectedValue);
                searchCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                searchCmd.Parameters.AddWithValue("@CreditHour", txtCreditHour.Text);
                searchCmd.Parameters.AddWithValue("@Category", ddlCourseCategory.SelectedValue);
                SqlDataReader dtSearch = searchCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtSearch);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                    gvCourse.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvCourse.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CourseEntry");
        }
    }
}