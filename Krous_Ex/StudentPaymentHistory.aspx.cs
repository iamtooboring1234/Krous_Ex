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
    public partial class StudentPaymentHistory : System.Web.UI.Page
    {
        Guid userGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (IsPostBack != true)
            {
                loadPaymentHistory();
            }
        }


        private void loadPaymentHistory()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string sqlQuery;

                sqlQuery = "SELECT p.PaymentGUID, p.PaymentNo , p.TotalAmount, p.TotalPaid, CONVERT(VARCHAR, p.PaymentDate, 100) as PaymentDate, p.PaymentStatus FROM Payment p LEFT JOIN Student s ON p.StudentGUID = s.StudentGUID WHERE s.StudentGUID = @StudentGUID";
                SqlCommand paymentCmd = new SqlCommand(sqlQuery, con);
                paymentCmd.Parameters.AddWithValue("StudentGUID", userGUID);
                SqlDataReader dtr = paymentCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtr);

                if (dt.Rows.Count != 0)
                {
                    if(dt.Rows[0]["PaymentStatus"].ToString() == "Paid")
                    {
                        gvPaymentHistory.DataSource = dt;
                        gvPaymentHistory.DataBind();
                        gvPaymentHistory.Visible = true;
                        lblNoData.Visible = false;
                    } 
                }
                else
                {
                    lblNoData.Visible = true;
                    gvPaymentHistory.Visible = false;
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