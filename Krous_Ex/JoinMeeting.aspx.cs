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
    public partial class JoinMeeting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetMeetingListing();

        }

        protected void GetMeetingListing()
        {
            try
            {
                string meeting = Request.QueryString["MeetingLinkGUID"];

                if (clsLogin.GetLoginUserType() == "Student")
                {
                    txtRole.Text = "0";
                } else 
                {
                    txtRole.Text = "1";
                }

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                string strSelect = "SELECT * FROM MeetingLink WHERE MeetingLinkGUID = @MeetingLinkGUID ";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@MeetingLinkGUID", meeting);
                SqlDataReader reader = cmdSelect.ExecuteReader();

                dt.Load(reader);
                con.Close();


                if (dt.Rows.Count != 0)
                {
                    txtDate.Text = DateTime.Now.ToString();
                    txtMeetingID.Text = dt.Rows[0][2].ToString();
                    txtMeetingPass.Text = dt.Rows[0][3].ToString();
                    txtTopic.Text = dt.Rows[0][1].ToString();
                    txtName.Text = clsLogin.GetLoginUserName();
                }


            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (clsLogin.GetLoginUserType() == "Staff")
            {
                Response.Redirect("ExaminationDashboard");
            } else
            {
                Response.Redirect("StudentExaminationDashboard");
            }
        }
    }
}