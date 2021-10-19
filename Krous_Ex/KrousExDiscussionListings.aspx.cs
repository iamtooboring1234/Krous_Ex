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
    public partial class KrousExDiscussionListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ForumGUID"]))
            {
                loadGV();

                var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (myCookie != null)
                {
                    panelLogin.Visible = false;
                    panelPost.Visible = true;
                }
            }
            else
            {
                Response.Redirect("KrousExForumListings.aspx");
            }
            

        }

        private void loadGV()
        {
            try
            {
                string sqlQuery;
                string strTable = "";
                string strLink = "";

                sqlQuery = " SELECT t1.ForumCategory, t1.DiscGUID, t1.DiscTopic, t1.DiscIsPinned, t1.TotalReply, t1.DiscCreatedBy, t1.CreatedDate, t2.LastReplyBy, t2.LastReplyDate ";
                sqlQuery += "FROM ";
                sqlQuery += "(SELECT F.ForumCategory, D.DiscGUID, D.DiscTopic, D.DiscIsPinned, COUNT(R.ReplyGUID) as TotalReply, D.DiscCreatedBy, Convert(varchar, D.DiscCreatedDate, 120) as CreatedDate ";
                sqlQuery += "FROM Forum F LEFT JOIN Discussion D ON F.ForumGUID = D.ForumGUID LEFT OUTER JOIN Replies R ";
                sqlQuery += "On D.DiscGUID = R.DiscGUID ";
                sqlQuery += "WHERE F.ForumGUID = @ForumGUID ";
                sqlQuery += "GROUP BY D.DiscGUID, D.DiscTopic, D.DiscCreatedBy, D.DiscCreatedDate, D.DiscIsPinned, F.ForumCategory) t1 ";
                sqlQuery += "LEFT JOIN ";
                sqlQuery += "(SELECT D.DiscGUID, R.Reply_By as LastReplyBy, convert(varchar, max(R.Reply_Date), 120) as LastReplyDate ";
                sqlQuery += "FROM Discussion D, Replies R ";
                sqlQuery += "WHERE D.DiscGUID = R.DiscGUID ";
                sqlQuery += "GROUP BY D.DiscGUID, R.Reply_By) t2 ";
                sqlQuery += "ON t1.DiscGUID = t2.DiscGUID ";
                sqlQuery += "ORDER BY t1.DiscIsPinned desc ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@ForumGUID", Request.QueryString["ForumGUID"]);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtDisc = new DataTable();
                dtDisc.Load(reader);
                con.Close();

                if (dtDisc.Rows.Count != 0)
                {
                    for (int i = 0; i < dtDisc.Rows.Count; i++)
                    {
                        if(dtDisc.Rows[i]["DiscIsPinned"].ToString() == "Yes") { strTable += "<tr style=\"text-align:left\" class=\"pinned-thread\">"; } else { strTable += "<tr style=\"text-align:left\">"; }
                        strTable += "<td class=\"text-center\" align=\"center\" style=\"width:120px;\">";
                        strTable += "<a href=\"KrousExViewDiscussion.aspx?ForumGUID=" + dtDisc.Rows[i]["DiscGUID"] + "\">View</a>";
                        strTable += "</td>";
                        if (dtDisc.Rows[i]["DiscIsPinned"].ToString() == "Yes") { strTable += "<td><p><a href=\"KrousExViewDiscussion.aspx?ForumGUID=" + dtDisc.Rows[i]["DiscGUID"] + "\">General Discussion<i class=\"fas fa-thumbtack pl-2\" style=\"color:yellow\"></i></a></p>Discuss School Related</td>"; } else { strTable += "<td><p><a href=\"DiscussionEntry.aspx?ForumGUID=" + dtDisc.Rows[i]["DiscGUID"] + "\">General Discussion</a></p>Discuss School Related</td>"; }
                        strTable += "<td style=\"width:20px;text-align:center\">" + dtDisc.Rows[i]["TotalReply"] + "</td>";
                        strTable += "<td style=\"width:20px\">by " + dtDisc.Rows[i]["DiscCreatedBy"] + "<br/>" + dtDisc.Rows[i]["CreatedDate"] + "</td>";
                        if (String.IsNullOrEmpty(dtDisc.Rows[i]["LastReplyDate"].ToString()))
                        {
                            strTable += "<td style=\"width:20px\">n/a</td>";
                        }
                        else
                        {
                            strTable += "<td style=\"width:20px\">" + dtDisc.Rows[i]["LastReplyBy"] + "<br/>" +  dtDisc.Rows[i]["LastReplyDate"] + "</td>";
                        }
                        strTable += "</tr>";
                        strLink = dtDisc.Rows[i]["ForumCategory"].ToString();
                    }

                    Literal1.Text = strTable;
                    Literal2.Text = "> <a href=\"KrousExForumListings.aspx\" class=\"active-forum-link\">" + strLink + "</a>";

                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}