using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class KrousExDeleteComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (myCookie != null)
                {
                    if (Request.QueryString["ReplyGUID"] != null)
                    {
                        loadReply();
                    } else
                    {
                        Response.Redirect("KrousExForumListings");
                    }
                    
                } else
                {
                    Response.Redirect("Homepage");
                }
            }
        }

        private void loadReply()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand getCommand = new SqlCommand("SELECT D.DiscTopic, R.Reply_Content FROM Discussion D, Replies R WHERE D.DiscGUID = R.DiscGUID AND R.ReplyGUID = ReplyGUID ", con);
                getCommand.Parameters.AddWithValue("@ReplyGUID", Request.QueryString["ReplyGUID"]);
                SqlDataReader reader = getCommand.ExecuteReader();

                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0)
                {
                    txtDiscTopic.Text = dtFAQ.Rows[0]["DiscTopic"].ToString();
                    txtReplyContent.Text = dtFAQ.Rows[0]["Reply_Content"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, "Error");
            }
        }


        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand deleteCommand = new SqlCommand("DELETE FROM Replies WHERE ReplyGUID = @ReplyGUID ", con);

                deleteCommand.Parameters.AddWithValue("@ReplyGUID", Request.QueryString["ReplyGUID"]);

                deleteCommand.ExecuteNonQuery();

                con.Close();

                if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"].ToString()))
                {
                    Response.Redirect("KrousExViewDiscussion?DiscGUID=" + Request.QueryString["DiscGUID"]);
                }

            }
            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to delete.");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"].ToString()))
            {
                Response.Redirect("KrousExViewDiscussion?DiscGUID=" + Request.QueryString["DiscGUID"]);
            }
        }
    }
}