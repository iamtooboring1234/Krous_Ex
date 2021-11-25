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
    public partial class KrousExEditComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["NullReply"] != null)
                {
                    if (Session["NullReply"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showReplyPostSuccessfully(); ", true);
                        Session["NullReply"] = null;
                    }
                }

                var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (myCookie != null)
                {
                    if (Request.QueryString["ReplyGUID"] != null)
                    {
                        loadReply();
                    }
                    else
                    {
                        Response.Redirect("KrousExForumListings");
                    }

                }
                else
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

                SqlCommand getCommand = new SqlCommand("SELECT D.DiscTopic, R.ReplyContent FROM Discussion D, Replies R WHERE D.DiscGUID = R.DiscGUID AND R.ReplyGUID = ReplyGUID ", con);
                getCommand.Parameters.AddWithValue("@ReplyGUID", Request.QueryString["ReplyGUID"]);
                SqlDataReader reader = getCommand.ExecuteReader();

                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0)
                {
                    txtDiscTopic.Text = dtFAQ.Rows[0]["DiscTopic"].ToString();
                    txtReplyContent.Text = dtFAQ.Rows[0]["ReplyContent"].ToString();
                }

                con.Close();
            }
            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error occured. Please contact KrousEx for support.");
            }
        }


        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReplyContent.Text))
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                    con.Open();

                    SqlCommand updateCommand = new SqlCommand("UPDATE Replies SET ReplyContent = @ReplyContent, ReplyDate = @ReplyDate WHERE ReplyGUID = @ReplyGUID ", con);

                    updateCommand.Parameters.AddWithValue("@ReplyGUID", Request.QueryString["ReplyGUID"]);
                    updateCommand.Parameters.AddWithValue("@ReplyContent", txtReplyContent.Text);
                    updateCommand.Parameters.AddWithValue("@ReplyDate", DateTime.Now);

                    updateCommand.ExecuteNonQuery();

                    con.Close();

                }
                catch (Exception ex)
                {
                    clsFunction.DisplayAJAXMessage(this, ex.Message);
                }

                if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"].ToString()))
                {
                    Session["DeleteReply"] = "Yes";
                    Response.Redirect("KrousExViewDiscussion?DiscGUID=" + Request.QueryString["DiscGUID"]);
                }
                else
                {
                    Response.Redirect("KrousExForumListings");
                }
            } else
            {
                clsFunction.DisplayAJAXMessage(this, "Reply cannot be null.");
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