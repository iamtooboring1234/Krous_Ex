using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StudentMakePayment : System.Web.UI.Page
    {
        Guid userGUID;
        Guid PaymentGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (Request.QueryString["PaymentGUID"] != null)
            {
                PaymentGUID = Guid.Parse(Request.QueryString["PaymentGUID"]);
            }
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["PaymentGUID"]))
                {
                    loadPaymentDetails();
                }
            }
        }

        private void loadPaymentDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string studentInfo;
                studentInfo = "SELECT s.StudentGUID, s.StudentFullName, s.PhoneNumber, s.NRIC, ss.SessionYear, sm.SemesterYear, sm.SemesterSem, p.ProgrammeName, CONVERT(VARCHAR, pm.DateOverdue, 100) as DateOverdue, pm.PaymentNo ";
                studentInfo += "FROM Student_Programme_Register spr ";
                studentInfo += "LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID ";
                studentInfo += "LEFT JOIN[Session] ss ON spr.SessionGUID = ss.SessionGUID ";
                studentInfo += "LEFT JOIN Semester sm ON spr.SemesterGUID = sm.SemesterGUID ";
                studentInfo += "LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID ";
                studentInfo += "LEFT JOIN Payment pm ON pm.StudentGUID = s.StudentGUID ";
                studentInfo += "WHERE s.StudentGUID = @StudentGUID AND pm.PaymentGUID = @PaymentGUID";

                SqlCommand getCommand = new SqlCommand(studentInfo, con);
                getCommand.Parameters.AddWithValue("@StudentGUID", userGUID);
                getCommand.Parameters.AddWithValue("@PaymentGUID", PaymentGUID);
                SqlDataReader dtrStudent = getCommand.ExecuteReader();
                DataTable dtStud = new DataTable();
                dtStud.Load(dtrStudent);

                string year = dtStud.Rows[0]["SessionYear"].ToString();
                string substr = year.Substring(2,2);
                int lastTwoYear = Convert.ToInt32(substr);
                int sum = lastTwoYear +1;
                string dateLiteral = "";

                if (dtStud.Rows.Count != 0)
                {
                    lblStudentName.Text = dtStud.Rows[0]["StudentFullName"].ToString().ToUpper(); //all upper case
                    lblContactNumber.Text = dtStud.Rows[0]["PhoneNumber"].ToString();
                    lblICNumber.Text = dtStud.Rows[0]["NRIC"].ToString();

                    lblAcaYear.Text = dtStud.Rows[0]["SessionYear"].ToString() + "/" + sum;
                    lblYearSem.Text = "Year " + dtStud.Rows[0]["SemesterYear"].ToString() + " Semester " + dtStud.Rows[0]["SemesterSem"].ToString();
                    lblProgrammeName.Text = dtStud.Rows[0]["ProgrammeName"].ToString().ToUpper();
                    lblPaymentReferenceNo.Text = dtStud.Rows[0]["PaymentNo"].ToString();
                    dateLiteral += "<asp:Label ID=\"lblOverdue\" runat=\"server\">PLEASE MAKE SURE YOU PAY THIS BILL BY <span style=\"color:red\">" + DateTime.Parse(dtStud.Rows[0]["DateOverdue"].ToString()).ToString("dd-MMM-yyyy") + "</span></asp:Label>";
                    litDate.Text = dateLiteral;
                    //lblOverdue.Text = "PLEASE MAKE SURE YOU PAY THIS BILL BY " + DateTime.Parse(dtStud.Rows[0]["DateOverdue"].ToString()).ToString("dd-MMM-yyyy");

                }

                //-------------------------------------------------------------------------------------------------------------------------------------------
                
                int fixedAmountPerCourse = 259;

                SqlCommand semesterGuid = new SqlCommand("SELECT spr.SemesterGUID, s.StudentGUID FROM Student_Programme_Register spr LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID WHERE s.StudentGUID = @StudentGUID", con);
                semesterGuid.Parameters.AddWithValue("@StudentGUID", userGUID);
                SqlDataReader dtrSem = semesterGuid.ExecuteReader();
                DataTable dtSem = new DataTable();
                dtSem.Load(dtrSem);

                Guid semesterGUID = Guid.Parse(dtSem.Rows[0]["SemesterGUID"].ToString());

                string paymentInfo;
                paymentInfo = "SELECT c.CreditHour, s.SemesterGUID, c.CourseGUID, c.CourseName FROM ProgrammeCourse pc ";
                paymentInfo += "LEFT JOIN Course c ON pc.CourseGUID = c.CourseGUID ";
                paymentInfo += "LEFT JOIN Programme p ON pc.ProgrammeGUID = p.ProgrammeGUID ";
                paymentInfo += "LEFT JOIN Student_Programme_Register spr ON p.ProgrammeGUID = spr.ProgrammeGUID ";
                paymentInfo += "LEFT JOIN Student st ON spr.StudentGUID = st.StudentGUID ";
                paymentInfo += "LEFT JOIN Semester s ON pc.SemesterGUID = s.SemesterGUID ";
                paymentInfo += "WHERE st.StudentGUID = @StudentGUID AND ";
                paymentInfo += "s.SemesterGUID = @SemesterGUID AND ";
                paymentInfo += "pc.SessionMonth = (SELECT s.SessionMonth FROM Session S LEFT JOIN Student st ON S.SessionGUID = st.SessionGUID WHERE StudentGUID = @StudentGUID)";

                SqlCommand getPayment = new SqlCommand(paymentInfo, con);
                getPayment.Parameters.AddWithValue("@StudentGUID", userGUID);
                getPayment.Parameters.AddWithValue("@SemesterGUID", semesterGUID);
                SqlDataReader dtrPayment = getPayment.ExecuteReader();
                DataTable dtPayment = new DataTable();
                dtPayment.Load(dtrPayment);

                //calculation
                int creditHour = int.Parse(dtPayment.Rows[0]["CreditHour"].ToString());
                decimal eachCourse = 0;
                decimal total = 0;

                string paymentTable = "";

                if (dtPayment.Rows.Count != 0)
                {
                    paymentTable += "<div style=\"margin-top: 20px;\">";
                    paymentTable += "<div style=\"padding: 0px 50px 0px 50px\">";
                    paymentTable += "<table class=\"table table-clear\">";
                    paymentTable += "<tbody>";
                    paymentTable += "<tr>";
                    paymentTable += "<th colspan=\"2\" style=\"width: 80%; font-size: 15px; \">Tuition Fee</th>";
                    paymentTable += "<th style=\"font-size: 15px;\" >Amount(RM)</ th >";
                    paymentTable += "</tr>";

                    int y = 1;

                    for (int i = 0; i < dtPayment.Rows.Count; i++)
                    {
                        eachCourse = creditHour * fixedAmountPerCourse;
                        total += eachCourse;
                        
                        paymentTable += "<tr>";
                        paymentTable += "<td style=\"width: 5%\">"+ y +". </td>";
                        paymentTable += "<td>" + dtPayment.Rows[i]["CourseName"].ToString() + "</td>";
                        paymentTable += "<td>" + Convert.ToDecimal(eachCourse).ToString("F2") + "</td>";
                        paymentTable += "</tr>";

                        y++;
                    }

                    lblTotalPrice.Text = Convert.ToDecimal(total).ToString("F2");
                    lblTotalPrice.ForeColor = System.Drawing.Color.LimeGreen;

                    //paymentTable += "<tr>";
                    //paymentTable += "<td></td>";
                    //paymentTable += "<th style=\"font-size: 17px;\">Total Amount :</th>";
                    //paymentTable += "<th style=\"font-size: 17px;\"><span style=\"color:limegreen\">" + Convert.ToDecimal(total).ToString("F2") + "</span></th>";
                    //paymentTable += "</tr>";
                    paymentTable += "</tbody>";
                    paymentTable += "</table>";
                    paymentTable += "</div>";
                    paymentTable += "</div>";
                }

                litPayment.Text = paymentTable;

                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnPrintPayment_Click(object sender, EventArgs e)
        {
            //id = Request.Form[hfImagePayment.UniqueID];
            try
            {
                string base64 = Request.Form[hfImagePayment.UniqueID].Split(',')[1];
                string paymentNo = "StudentBill_" + Request["__EVENTARGUMENT"];
                byte[] bytes = Convert.FromBase64String(base64);

                string ReceiptImgName = paymentNo + ".png";

                string folderName = "~/Uploads/Receipts/" + PaymentGUID + "/";
                string ReceiptImgSavePath = Server.MapPath(folderName);
                if (!(Directory.Exists(ReceiptImgSavePath)))
                {
                    Directory.CreateDirectory(ReceiptImgSavePath);
                }
                string ReceiptFullSavePath = ReceiptImgSavePath + ReceiptImgName;


                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    img.Save(ReceiptFullSavePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                Response.Clear();
                Response.ContentType = "image/png";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + paymentNo + ".png");
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }

            //SendEmail(ReceiptFullSavePath);   
        }

        protected void hiddenBtn_Click(object sender, EventArgs e)
        {
            //save the total paid, payment status, payment date
            //then insert a new receipt
            try
            {
                PaymentGUID = Guid.Parse(Request.QueryString["PaymentGUID"]);
                Guid receiptGUID = Guid.NewGuid();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                decimal total = Convert.ToDecimal(lblTotalPrice.Text.ToString());

                SqlCommand save = new SqlCommand("UPDATE Payment SET TotalPaid = @TotalPaid, PaymentStatus = @PaymentStatus, PaymentDate = @PaymentDate, PaymentMethod = @PaymentMethod WHERE PaymentGUID = @PaymentGUID", con);
                save.Parameters.AddWithValue("@PaymentGUID", PaymentGUID);
                save.Parameters.AddWithValue("@TotalPaid", lblTotalPrice.Text);
                save.Parameters.AddWithValue("@PaymentStatus", "Paid");
                save.Parameters.AddWithValue("@PaymentMethod", "PayPal");
                save.Parameters.AddWithValue("@PaymentDate", DateTime.Now.ToString());
                save.ExecuteNonQuery();

                //insert into receipt, createa new receipt for the payment
                string receiptNo = "R" + DateTime.Now.ToString("yyyyMMddHHmmss");
                SqlCommand insertReceipt = new SqlCommand("INSERT INTO Receipt (ReceiptGUID, ReceiptNo, PaymentGUID, DateIssued) VALUES (@ReceiptGUID, @ReceiptNo, @PaymentGUID, @DateIssued)", con);
                insertReceipt.Parameters.AddWithValue("@ReceiptGUID", receiptGUID);
                insertReceipt.Parameters.AddWithValue("@PaymentGUID", PaymentGUID);
                insertReceipt.Parameters.AddWithValue("@ReceiptNo", receiptNo);
                insertReceipt.Parameters.AddWithValue("@DateIssued", DateTime.Now.ToString());
                insertReceipt.ExecuteNonQuery();

                //clsFunction.DisplayAJAXMessage(this, "payment is successfully made");
                sendPaymentSuccess();
                Response.Redirect("StudentPaymentHistory");


                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        //send email after payment successful
        private bool sendPaymentSuccess()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            con.Open();

            PaymentGUID = Guid.Parse(Request.QueryString["PaymentGUID"]);

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("krousExnoreply@gmail.com", "krousex2021");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("krousExnoreply@gmail.com");

            cmd = new SqlCommand("SELECT s.Email, s.StudentFullName FROM Payment p LEFT JOIN Student s ON p.StudentGUID = s.StudentGUID WHERE p.PaymentGUID = @PaymentGUID", con);
            cmd.Parameters.AddWithValue("@PaymentGUID", PaymentGUID);
            SqlDataReader dtrSelect = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dtrSelect);
            con.Close();
            string fullname = dt.Rows[0]["StudentFullName"].ToString();

            String body = "Hello " + fullname + ", <br />We are here to notify you, that your payment has been made successfully. You can download the receipt at your payment history page.<br /><br />Thank You.<br /><br />Best Regards,<br />Krous Ex";
            mail.To.Add(dt.Rows[0]["Email"].ToString());
            mail.Subject = "Payment Success";
            mail.IsBodyHtml = true;
            mail.Body = body;

            client.Send(mail);

            return true;
        }
    }
}