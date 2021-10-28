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
    public partial class Testing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadFaculty();
        }


        private void loadFaculty()
        {
            DropDownList1.Items.Clear();

            try
            {
                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("select * from session WHERE DATEADD(Day, 14, GETDATE()) < convert(varchar, concat(sessionYear, '-', SessionMonth, '-', DAY(getdate())), 22) order by SessionYear, SessionMonth; ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

  
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dt.Rows[i]["SessionYear"].ToString() + dt.Rows[i]["SessionMonth"].ToString();
                    oList.Value = dt.Rows[i]["SessionGUID"].ToString();
                    DropDownList1.Items.Add(oList);
                }

            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading faculty.");
            }
        }

    }
}