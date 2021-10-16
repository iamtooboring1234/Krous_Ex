using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class CourseEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadCourseCategory();
        }

        private void loadCourseCategory()
        {
            try
            {
                //ddlCreditHour.Items.Clear();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                //con.Open();
                //SqlCommand selectCmd = new SqlCommand("SELECT CreditHour FROM Course Group By CreditHour Order By CreditHour", con);
                //SqlDataReader reader = selectCmd.ExecuteReader();

                //DataTable dtFAQ = new DataTable();
                //dtFAQ.Load(reader);
                //con.Close();

            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, "Error");
            }

        }

        private bool insertCourse()
        {
            string Category;

            //string Username = clsLogin.GetLoginUserName;

            if (rdMain.Checked == true)
            {
                Category = "Main Course";
            }
            else
            {
                Category = "Elective Course";
            }

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand insertCourCmd = new SqlCommand("INSERT INTO COURSE VALUES(@CourseID,@CourseName,@CourseDesc,@CreditHour,@Category,@CourseFee)", con);

                insertCourCmd.Parameters.AddWithValue("@CourseID", txtCourseID.Text);
                insertCourCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                insertCourCmd.Parameters.AddWithValue("@CourseDesc", txtCourseDesc.Text);
                insertCourCmd.Parameters.AddWithValue("@CreditHour", ddlCreditHour.SelectedValue);
                insertCourCmd.Parameters.AddWithValue("@Category", Category);
                insertCourCmd.Parameters.AddWithValue("@CourseFee", txtCourseFee.Text);

                insertCourCmd.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        private bool validateCourse()
        {
            string Category;

            if (txtCourseID.Text == "")
            {
                return false;
            }

            if (txtCourseName.Text == "")
            {
                return false;
            }

            if (txtCourseDesc.Text == "")
            {
                return false;
            }

            if (txtCourseFee.Text == "")
            {
                return false;
            }

            if (rdMain.Checked == true)
            {
                Category = "Main Course";
            }
            else
            {
                Category = "Elective Course";
            }

            return true;
        }

        protected void btnSave_CLick(object sender, EventArgs e)
        {
            if (validateCourse())
            {
                if (insertCourse())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "ShowPopup();", true);
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to insert details. Failed to create.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CourseListings");
        }
    }
}