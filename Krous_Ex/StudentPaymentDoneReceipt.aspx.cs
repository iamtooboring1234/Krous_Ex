using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StudentPaymentDoneReceipt : System.Web.UI.Page
    {
        Guid userGUID;
        Guid PaymentGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (IsPostBack != true)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["PaymentGUID"]))
                {
                    PaymentGUID = Guid.Parse(Request.QueryString["PaymentGUID"]);
                    loadReceiptDetails();
                }
            }
        }

        private void loadReceiptDetails()   
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string sqlQuery;

                sqlQuery = "SELECT r.ReceiptNo, r.DateIssued, p.PaymentNo, p.TotalAmount, p.TotalPaid, p.PaymentDate, s.StudentFullName ";
                sqlQuery += "FROM Receipt r ";
                sqlQuery += "LEFT JOIN Payment p ON r.PaymentGUID = p.PaymentGUID ";
                sqlQuery += "LEFT JOIN Student s ON p.StudentGUID = s.StudentGUID ";
                sqlQuery += "WHERE p.PaymentGUID = @PaymentGUID";

                SqlCommand receiptCmd = new SqlCommand(sqlQuery, con);
                receiptCmd.Parameters.AddWithValue("@PaymentGUID", PaymentGUID);
                SqlDataReader dtr = receiptCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtr);

                if(dt.Rows.Count != 0)
                {
                    lblReceiptNo.Text = dt.Rows[0]["ReceiptNo"].ToString();
                    lblPaymentNo.Text = dt.Rows[0]["PaymentNo"].ToString();
                    lblStudFullName.Text = dt.Rows[0]["StudentFullName"].ToString().ToUpper();
                    lblBillAmt.Text = "RM " + dt.Rows[0]["TotalAmount"].ToString();
                    lblAmountPaid.Text = "RM " + dt.Rows[0]["TotalPaid"].ToString();
                    lblPaidOn.Text = dt.Rows[0]["PaymentDate"].ToString();
                    lblReceiptDate.Text = dt.Rows[0]["DateIssued"].ToString();
                }



                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            //string base64 = Request.Form[hfPaymentReceipt.UniqueID].Split(',')[1];
            //string receiptNo = "Receipt" + Request["__EVENTARGUMENT"];
            //byte[] bytes = Convert.FromBase64String(base64);

            //string ReceiptImgName = paymentNo + ".png";

            //string folderName = "~/Uploads/Receipts/" + ReceiptNo + "/";
            //string ReceiptImgSavePath = Server.MapPath(folderName);
            //string ReceiptFullSavePath = ReceiptImgSavePath + ReceiptImgName;

            //MemoryStream ms = new MemoryStream(bytes);
            //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            //img.Save(ReceiptFullSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            ////download
            //Response.Clear();
            //Response.ContentType = "image/png";
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + receiptNo + ".png");
            //Response.Buffer = true;
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.BinaryWrite(bytes);
            //Response.End();
        }


    }
}