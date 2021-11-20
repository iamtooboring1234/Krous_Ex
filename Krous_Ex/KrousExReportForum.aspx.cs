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
    public partial class KrousExReportForum : System.Web.UI.Page
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

                SqlCommand getCommand = new SqlCommand("SELECT D.DiscTopic, R.ReplyContent, R.ReplyBy FROM Discussion D, Replies R WHERE D.DiscGUID = R.DiscGUID AND R.ReplyGUID = ReplyGUID ", con);
                getCommand.Parameters.AddWithValue("@ReplyGUID", Request.QueryString["ReplyGUID"]);
                SqlDataReader reader = getCommand.ExecuteReader();

                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0)
                {
                    txtDiscTopic.Text = dtFAQ.Rows[0]["DiscTopic"].ToString();
                    txtReplyContent.Text = dtFAQ.Rows[0]["ReplyContent"].ToString();
                    txtReplyBy.Text = dtFAQ.Rows[0]["ReplyBy"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Guid ForumReportGUID = Guid.NewGuid();

            string Username = clsLogin.GetLoginUserName();
            string Reason;

            if(ddlReason.SelectedValue == "Other")
            {
                Reason = txtReason.Text;
            } else
            {
                Reason = ddlReason.SelectedValue;
            }

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO ForumReport VALUES(@ForumReportGUID,@DiscGUID,@ReplyGUID,@ReplyBy,@ReplyContent,@ReportReason,@ReportStatus,@ReportBy,@ReportDate)", con);

                InsertCommand.Parameters.AddWithValue("@ForumReportGUID", ForumReportGUID);
                InsertCommand.Parameters.AddWithValue("@ReplyGUID", Guid.Parse(Request.QueryString["ReplyGUID"]));
                InsertCommand.Parameters.AddWithValue("@DiscGUID", Guid.Parse(Request.QueryString["DiscGUID"]));
                InsertCommand.Parameters.AddWithValue("@ReplyBy", txtReplyBy.Text);
                InsertCommand.Parameters.AddWithValue("@ReplyContent", txtReplyContent.Text);
                InsertCommand.Parameters.AddWithValue("@ReportReason", Reason);
                InsertCommand.Parameters.AddWithValue("@ReportStatus", "In Progress");
                InsertCommand.Parameters.AddWithValue("@ReportBy", Username);
                InsertCommand.Parameters.AddWithValue("@ReportDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                InsertCommand.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }


            if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"].ToString()))
            {
                Session["ReportForum"] = "Yes";
                Response.Redirect("KrousExViewDiscussion?DiscGUID=" + Request.QueryString["DiscGUID"]);
            }
            else
            {
                Response.Redirect("KrousExForumListings");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"].ToString()))
            {
                Response.Redirect("KrousExViewDiscussion?DiscGUID=" + Request.QueryString["DiscGUID"]);
            }
        }

        protected void ddlReason_TextChanged(object sender, EventArgs e)
        {
            if(ddlReason.SelectedValue == "Other")
            {
                panelOtherReason.Visible = true;
            } else
            {
                panelOtherReason.Visible = false;
            }
        }
    }
}