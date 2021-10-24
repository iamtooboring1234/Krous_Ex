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
    public partial class StudentFAQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFAQ_Click(object sender, EventArgs e)
        {
            Response.Redirect("KrousExFAQ");
        }

        protected void btnChat_Click(object sender, EventArgs e)
        {
            try
            {
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                var GetCommand = new SqlCommand("SELECT * FROM Chat WHERE StudentGUID = @StudentGUID AND ChatStatus = 'Pending' OR ChatStatus = 'In Progress'", con);
                GetCommand.Parameters.AddWithValue("@StudentGUID", Guid.Parse(clsLogin.GetLoginUserGUID()));
                SqlDataReader reader = GetCommand.ExecuteReader();
                var dtChat = new DataTable();
                dtChat.Load(reader);
                con.Close();
                if (dtChat.Rows.Count == 0)
                {
                    var ChatGUID = Guid.NewGuid();
                    Session["NewChat"] = "True";
                    Response.Redirect("FAQChat.aspx?ChatGUID=" + ChatGUID.ToString());
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "You already have a chat is pending to reply or already in progress");
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}