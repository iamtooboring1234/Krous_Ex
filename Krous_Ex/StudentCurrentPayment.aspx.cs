using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

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
    }
}