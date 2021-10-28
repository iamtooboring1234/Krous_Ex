using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffCourseInCharge : System.Web.UI.Page
    {
        Guid userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (userGUID != null)
                {
                 
                }

            }
        }

        protected bool insertCourseInc()
        {
            Guid CourseInChargeGUID = Guid.NewGuid();

            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand insertCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

              
                insertCmd = new SqlCommand("INSERT INTO Course_In_Charge VALUES (@CourseInChargeGUID, @CourseGUID, @StaffGUID)", con);
                insertCmd.Parameters.AddWithValue("@CourseInChargeGUID", CourseInChargeGUID);
                //insertCmd.Parameters.AddWithValue("@CourseGUID", );
                //insertCmd.Parameters.AddWithValue("@StaffGUID", );
                insertCmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }


        //each teacher can in charge max 3 diff courses
        //need check the max number 
    }
}