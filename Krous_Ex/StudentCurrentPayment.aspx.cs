using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Krous_Ex
{
    public partial class StudentCurrentPayment : System.Web.UI.Page
    {
        Guid userGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (IsPostBack != true)
            {
                //CheckStatusSendEmail();
                loadCurrentPaymentGV();
            }
        }

        private void loadCurrentPaymentGV()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string sqlQuery;

                sqlQuery = "SELECT p.PaymentGUID, p.PaymentNo , p.PaymentStatus, p.TotalAmount, CONVERT(VARCHAR, p.DateIssued, 100) as DateIssued, CONVERT(VARCHAR, p.DateOverdue, 100) as DateOverdue FROM Payment p LEFT JOIN Student s ON p.StudentGUID = s.StudentGUID WHERE s.StudentGUID = @StudentGUID";
                SqlCommand paymentCmd = new SqlCommand(sqlQuery, con);
                paymentCmd.Parameters.AddWithValue("StudentGUID", userGUID);
                SqlDataReader dtr = paymentCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtr);

                if(dt.Rows.Count != 0)
                {
                    if(dt.Rows[0]["PaymentStatus"].ToString() == "Pending")
                    {
                        gvCurrentPayment.DataSource = dt;
                        gvCurrentPayment.DataBind();
                        gvCurrentPayment.Visible = true;
                        lblNoData.Visible = false;
                    }
                    else
                    {
                        lblNoData.Visible = true;
                        gvCurrentPayment.Visible = false;
                    }

                }
                else
                {
                    lblNoData.Visible = true;
                    gvCurrentPayment.Visible = false;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnMakePayment_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            con.Open();

            string guidCmd;
            guidCmd = "SELECT p.PaymentGUID FROM Payment p LEFT JOIN Student s ON p.StudentGUID = s.StudentGUID WHERE s.StudentGUID = @StudentGUID";
            SqlCommand paymentCmd = new SqlCommand(guidCmd, con);
            paymentCmd.Parameters.AddWithValue("StudentGUID", userGUID);
            SqlDataReader dtr = paymentCmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dtr);

            Guid guid = Guid.Parse(dt.Rows[0]["PaymentGUID"].ToString());

            Response.Redirect("StudentMakePayment.aspx?PaymentGUID=" + guid);
            
        }


        //private bool CheckStatusSendEmail()
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
        //        con.Open();

        //        //get PaymentGUID
        //        string PaymentGuidCmd;
        //        PaymentGuidCmd = "SELECT p.PaymentGUID FROM Payment p LEFT JOIN Student s ON p.StudentGUID = s.StudentGUID WHERE s.StudentGUID = @StudentGUID";
        //        SqlCommand paymentCmd = new SqlCommand(PaymentGuidCmd, con);
        //        paymentCmd.Parameters.AddWithValue("StudentGUID", userGUID);
        //        SqlDataReader dtr = paymentCmd.ExecuteReader();
        //        DataTable dt = new DataTable();
        //        dt.Load(dtr);
        //        Guid guid = Guid.Parse(dt.Rows[0]["PaymentGUID"].ToString());

        //        //check if overdue
        //        string dateQuery;
        //        dateQuery = "SELECT p.PaymentGUID, p.DateOverdue FROM Payment p ";
        //        dateQuery += "LEFT JOIN Student s ON p.StudentGUID = s.StudentGUID ";
        //        dateQuery += " LEFT JOIN Student_Programme_Register spr ON spr.StudentGUID = s.StudentGUID ";
        //        dateQuery += "WHERE p.PaymentGUID = @PaymentGUID";
        //        SqlCommand dateCmd = new SqlCommand(dateQuery, con);
        //        dateCmd.Parameters.AddWithValue("PaymentGUID", guid);
        //        SqlDataReader dtrDate = dateCmd.ExecuteReader();
        //        DataTable dtDate = new DataTable();
        //        dtDate.Load(dtrDate);

        //        DateTime overdue = DateTime.Parse(dtDate.Rows[0]["DateOverdue"].ToString());
        //        DateTime datenow = DateTime.Now;

        //        if(datenow > overdue)
        //        {
        //            SmtpClient client = new SmtpClient();
        //            client.Port = 587;
        //            client.Host = "smtp.gmail.com";
        //            client.EnableSsl = true;
        //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new NetworkCredential("krousExnoreply@gmail.com", "krousex2021");

        //            MailMessage mail = new MailMessage();
        //            mail.From = new MailAddress("krousExnoreply@gmail.com");

        //            cmd = new SqlCommand("SELECT s.Email, s.StudentFullName, p.PaymentGUID FROM Payment p LEFT JOIN Student s ON p.StudentGUID = s.StudentGUID LEFT JOIN Student_Programme_Register spr ON spr.StudentGUID = s.StudentGUID WHERE p.PaymentGUID = @PaymentGUID ", con);
        //            cmd.Parameters.AddWithValue("@PaymentGUID", guid);
        //            SqlDataReader dtrSelect = cmd.ExecuteReader();
        //            DataTable dtEmail = new DataTable();
        //            dtEmail.Load(dtrSelect);
        //            con.Close();
        //            string fullname = dtEmail.Rows[0]["StudentFullName"].ToString();

        //            String body = "Hello " + fullname + ", <br />We are here to notify you, that your payment has been <span style=\"color:red\">OVERDUE</span>.<br />Please make sure you make the payment as soon as posible.<br /><br />Thank You.<br /><br />Best Regards,<br />Krous Ex";
        //            mail.To.Add(dt.Rows[0]["Email"].ToString());
        //            mail.Subject = "Payment Overdue";
        //            mail.IsBodyHtml = true;
        //            mail.Body = body;

        //            client.Send(mail);
        //        }
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        clsFunction.DisplayAJAXMessage(this, ex.Message);
        //        return false;
        //    }
        //}
    }


}