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
    public partial class KrousExContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool SendEmail()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();
            bool sentBoolean = false;

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

                //if got record then insert into Reset password table

                String body = 
                    "Hello, <b>" + txtName.Text + "</b>" +
                    "<br />Thank you for contacting us Our support team will be contact you soon.<br /><br /><hr />" + 
                    "<br />Below are the email content that you requested for support.<br />" + txtEmailContent.Text + "<br /><br /><hr />" +
                    "<br />If you didn't request this, please ignore this email." + 
                    "<br />Thanks Regards" + 
                    "<br />KrousEx";

                mail.To.Add(txtEmailAddress.Text);
                mail.Subject = "Contact CS #" + GenerateCode() + "- " + txtEmailSubject.Text;
                mail.IsBodyHtml = true;
                mail.Body = body;

                client.Send(mail);

                sentBoolean = true;

                return sentBoolean;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (SendEmail())
            {
                clsFunction.DisplayAJAXMessage(this, "Support email is successfully sent.");
                txtEmailAddress.Text = "";
                txtEmailContent.Text = "";
                txtEmailSubject.Text = "";
                txtName.Text = "";
            } else
            {
                clsFunction.DisplayAJAXMessage(this, "Error! Please fill in the required details.");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtEmailAddress.Text = "";
            txtEmailContent.Text = "";
            txtEmailSubject.Text = "";
            txtName.Text = "";
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