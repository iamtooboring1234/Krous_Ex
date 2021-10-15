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
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                cmd = new SqlCommand("SELECT * FROM Student WHERE Email = @email", con);
                cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                SqlDataReader dtrStudent = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrStudent);

                if(dt.Rows.Count != 0)
                {

                }
              

                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }
}