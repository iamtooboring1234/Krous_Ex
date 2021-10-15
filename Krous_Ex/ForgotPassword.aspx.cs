using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        String userType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["userType"] == "Student")
            {
                userType = "Student";
            }
            else
            {
                userType = "Staff";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            

            try
            {
                Session["email"] = txtEmailAddress.Text;
                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                if(userType == "Student")
                {
                    cmd = new SqlCommand("SELECT * FROM Student WHERE Email = @email", con);
                    cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                    
                }else if( userType == "Staff")
                {
                    cmd = new SqlCommand("SELECT * FROM Staff WHERE Email = @email", con);
                    cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                }

                SqlDataReader dtrStudent = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrStudent);

                if (dt.Rows.Count == 0)
                {
                    clsFunction.DisplayAJAXMessage(this, "Please a valid email address");
                    txtEmailAddress.Text = string.Empty;
                    txtEmailAddress.Focus();
                    return;
                }
                else
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

        private void SendEmail()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();

            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("krousExnoreply@gmail.com", "krousex2021");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("krousExnoreply@gmail.com");

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();


                sb.Append("Hi, <br/> The given link below is to allow you to reset your password. <br/> Click Here : <br/>");
                sb.Append("");
                
            
                https://localhost:44375/ResetPassword.aspx
                sb.Append("<a href=http://localhost:57355/codesoluation/resetlink.aspx?username=" + GetUserEmail(txtemail.Text));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

    }
}