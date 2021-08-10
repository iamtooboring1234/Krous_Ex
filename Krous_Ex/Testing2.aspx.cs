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
    public partial class Testing2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string constr = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM FAQ", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            gvCustomers.DataSource = dt;
                            gvCustomers.DataBind();
                        }
                    }
                }
            }
        }
    }
}