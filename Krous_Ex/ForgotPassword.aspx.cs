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
                //Session["email"] = txtEmailAddress.Text;

                if(txtEmailAddress.Text == "")
                {
                    clsFunction.DisplayAJAXMessage(this, "Please enter a valid email address.");
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

        private bool SendEmail()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();
            bool sentBoolean = false;
            Guid ResetPasswordGUID = Guid.NewGuid();

            try
            {
                if (Request.QueryString["UserType"] == "Student")
                {
                    userType = "Student";
                }
                else
                {
                    userType = "Staff";
                }

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
                  
                cmd = new SqlCommand("SELECT * FROM " + userType + " WHERE Email = @email", con);
                cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                SqlDataReader dtrSelect = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrSelect);
                con.Close();

                //if got record then insert into Reset password table
                if (dt.Rows.Count != 0)
                {
                    con.Open();
                    string userGUID = dt.Rows[0]["" + userType + "GUID"].ToString();
                    String username = dt.Rows[0][1].ToString();

                    string LinkToken = GenerateCode().ToString();
                    String email = txtEmailAddress.Text.ToString();

                    SqlCommand linkCmd = new SqlCommand("SELECT * FROM ResetPassword WHERE " + userType + "GUID = @userGUID AND Status = 'Pending'", con);
                    linkCmd.Parameters.AddWithValue("@userGUID", Guid.Parse(userGUID));
                    SqlDataReader dtrLink = linkCmd.ExecuteReader();
                    DataTable dtLink = new DataTable();
                    dtLink.Load(dtrLink);

                    if (dtLink.Rows.Count == 0)
                    {
                        SqlCommand insertCmd = new SqlCommand();
                        insertCmd = new SqlCommand("INSERT INTO ResetPassword VALUES (@ResetPasswordGUID, @StudentGUID, @StaffGUID, @Status, @LinkToken, @CreatedTime, @ExpiredTime)", con);
                        insertCmd.Parameters.AddWithValue("@ResetPasswordGUID", Guid.NewGuid());

                        if (userType == "Student")
                        {
                            insertCmd.Parameters.AddWithValue("@StudentGUID", Guid.Parse(userGUID));
                            insertCmd.Parameters.AddWithValue("@StaffGUID", DBNull.Value);
                        }
                        else
                        {
                            insertCmd.Parameters.AddWithValue("@StudentGUID", DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@StaffGUID", Guid.Parse(userGUID));
                        }
                        insertCmd.Parameters.AddWithValue("@Status", "Pending");
                        insertCmd.Parameters.AddWithValue("@LinkToken", LinkToken);
                        insertCmd.Parameters.AddWithValue("@CreatedTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        insertCmd.Parameters.AddWithValue("@ExpiredTime", DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss")); //time now + 15mins
                        insertCmd.ExecuteNonQuery();
                        insertCmd.Dispose();
                    }
                    else
                    {
                        DateTime expiredDate = DateTime.Parse(dtLink.Rows[0]["ExpiredTime"].ToString());

                        if (DateTime.Now >= expiredDate) //if the user did not reset and already later than the expired date, it will update new 
                        {
                            SqlCommand updateCmd = new SqlCommand();
                            updateCmd = new SqlCommand("UPDATE ResetPassword SET ExpiredTime = @ExpiredTime, CreatedTime = @CreatedTime, LinkToken = @LinkToken WHERE " + userType + "GUID = @userGUID AND Status = 'Pending'", con);
                            updateCmd.Parameters.AddWithValue("@userGUID", Guid.Parse(userGUID));
                            updateCmd.Parameters.AddWithValue("@LinkToken", LinkToken);
                            updateCmd.Parameters.AddWithValue("@CreatedTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            updateCmd.Parameters.AddWithValue("@ExpiredTime", DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss")); //time now + 15mins
                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            DisplayAlertMsg("The email has been sent to you email account. Please check before it expired in 15 minutes.");
                            return false;
                        }
                    }

                    con.Close();

                    String url = ConfigurationManager.AppSettings["ResetPasswordURL"].ToString() + userGUID + "&ResetPasswordGUID=" + ResetPasswordGUID.ToString() + "&UserType=" + userType + "&LinkToken=" + LinkToken;
                    String body = "<b>Hello " + username + "<b><br />You have requested to reset your password for your account. Use the URL link below to change it.<br /><br />URL link: <a href= '" + url + "'><b>Reset Your Password<b></a><br />If you didn't request this, please ignore this email.";
                    mail.To.Add(email);
                    mail.Subject = "Reset Password";
                    mail.IsBodyHtml = true;
                    mail.Body = body;

                    client.Send(mail);

                    DisplayAlertMsg("Success! If your valid email address exists in our database, you will receive a password recovery link at your email account.");
                    sentBoolean = true;

                    con.Close();
                    return sentBoolean;

                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
            return false;
        }

        private String GenerateCode()
        {
            Random r = new Random();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");

            return sixDigitNumber;
        }

        protected void DisplayAlertMsg(string msg)
        {
            string myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["UserType"] == "Student")
            {
                Response.Redirect("StudentLogin.aspx");
            }
            else
            {
                Response.Redirect("StaffLogin.aspx");
            }
        }

    }
}