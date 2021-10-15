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

        public object Conversion { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["UserType"] == "Student")
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
            try
            {
                if(txtEmailAddress.Text == "")
                {
                    clsFunction.DisplayAJAXMessage(this, "Please enter a valid email address to reset new password.");
                }
                else
                {
                    SendEmail();
                }
            }
            catch(Exception ex)
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
                  
                if (userType == "Student")
                {
                    cmd = new SqlCommand("SELECT StudGUID, StudUsername FROM " + userType + " WHERE Email = @email", con);
                    cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text);

                }
                else if (userType == "Staff")
                {
                    cmd = new SqlCommand("SELECT StaffGUID, StaffUsername FROM "+ userType +" WHERE Email = @email", con);
                    cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                }

                SqlDataReader dtrSelect = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrSelect);

                //if got record then insert into Reser password table
                if(dt.Rows.Count != 0)
                {
                    String userGUID = dt.Rows[0][0].ToString();
                    String username = dt.Rows[0][1].ToString();

                    string LinkToken = GenerateCode().ToString();
                    Guid ResetPasswordGUID = Guid.NewGuid();
                    String insertCmd = "";  


                    if(userType == "Student")
                    {
                        insertCmd = "INSERT INTO ResetPassword (ResetPasswordGUID, StudGUID, Status, LinkToken, CreatedTime, ExpiredTime) VALUES (@ResetPasswordGUID, @UserGUID, @Status, @LinkToken, @CreatedTime, @ExpiredTime)";
                    }
                    else /*if(userType == "Staff")*/
                    {
                        insertCmd = "INSERT INTO ResetPassword (ResetPasswordGUID, StaffGUID, Status, LinkToken, CreatedTime, ExpiredTime) VALUES (@ResetPasswordGUID, @UserGUID, @Status, @LinkToken, @CreatedTime, @ExpiredTime)";
                    }
                    SqlCommand cmdInsert = new SqlCommand(insertCmd, con);
                    cmdInsert.Parameters.AddWithValue("@Status", "Pending");
                    cmdInsert.Parameters.AddWithValue("@LinkToken", LinkToken);
                    cmdInsert.Parameters.AddWithValue("@CreatedTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmdInsert.Parameters.AddWithValue("@ExpiredTime", DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss"));
                    cmdInsert.ExecuteNonQuery();
                }



       


                sb.Append("Hi, <br/> The given link below is to allow you to reset your password. <br/> Click Here : <br/>");
                sb.Append("");
                

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private String GenerateCode()
        {
            Random r = new Random();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");

            return sixDigitNumber;
        }
    }
}