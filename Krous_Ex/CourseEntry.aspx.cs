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
            
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, "Error");
            }

        }

        private bool insertCourse()
        {
            //string Category;

            ////string Username = clsLogin.GetLoginUserName;

            //if (rdMain.Checked == true)
            //{
            //    Category = "Main Course";
            //}
            //else 
            //{
            //    Category = "Elective Course";
            //}


            //try
            //{
            //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            //    con.Open();

            //    SqlCommand insertCourCmd = new SqlCommand("INSERT INTO COURSE VALUES(@CourseID,@CourseName,@CourseDesc,@CreditHour,@Category,@CourseFee)", con);

            //    insertCourCmd.Parameters.AddWithValue("@CourseID", txtCourseID.Text);
            //    insertCourCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
            //    insertCourCmd.Parameters.AddWithValue("@CourseDesc", txtCourseDesc.Text);
            //    insertCourCmd.Parameters.AddWithValue("@CreditHour", ddlCreditHour.SelectedValue);
            //    insertCourCmd.Parameters.AddWithValue("@Category", Category);
            //    //insertCourCmd.Parameters.AddWithValue("@CourseFee", );

            //    insertCourCmd.ExecuteNonQuery();

            //    con.Close();

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex);
            //    return false;
            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CourseListings");
        }
    }
}