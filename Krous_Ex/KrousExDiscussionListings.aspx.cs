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

                HyperLink1.NavigateUrl = "StudentLogin.aspx?FromURL=KrousExDiscussionListings&ForumGUID=" + Request.QueryString["ForumGUID"].ToString() ;
                

                var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (IsPostBack != true)
                {
                    if (myCookie != null)
                    {
                        panelLogin.Visible = false;
                        panelPost.Visible = true;
                    }
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

                sqlQuery = "SELECT t1.ForumCategory, t1.DiscGUID, t1.DiscTopic, t1.DiscIsPinned, t1.DiscIsLocked, t1.TotalReply, t1.DiscCreatedBy, t1.CreatedDate, t2.LastReplyBy, CONVERT(varchar, t2.Reply_Date, 120) as LastReplyDate ";
                sqlQuery += "FROM ";
                sqlQuery += "(SELECT F.ForumCategory, D.DiscGUID, D.DiscTopic, D.DiscIsPinned, D.DiscIsLocked, COUNT(R.ReplyGUID) as TotalReply, D.DiscCreatedBy, Convert(varchar, D.DiscCreatedDate, 120) as CreatedDate ";
                sqlQuery += "FROM Forum F LEFT JOIN Discussion D ON F.ForumGUID = D.ForumGUID LEFT OUTER JOIN Replies R ";
                sqlQuery += "On D.DiscGUID = R.DiscGUID ";
                sqlQuery += "WHERE F.ForumGUID = @ForumGUID AND D.DiscStatus = 'Active' ";
                sqlQuery += "GROUP BY D.DiscGUID, D.DiscTopic, D.DiscCreatedBy, D.DiscCreatedDate, D.DiscIsPinned, F.ForumCategory, D.DiscIsLocked) t1 ";
                sqlQuery += "LEFT JOIN ";
                sqlQuery += "(SELECT D.DiscGUID, R1.Reply_By as LastReplyBy, R1.Reply_Date ";
                sqlQuery += "FROM Discussion D LEFT JOIN Replies R1 ON ";
                sqlQuery += "D.DiscGUID = R1.DiscGUID ";
                sqlQuery += "LEFT JOIN(SELECT DiscGUID, MAX(Reply_Date) as LatestReplyDate FROM Replies GROUP BY DiscGUID) R2 on ";
                sqlQuery += " R1.DiscGUID = R2.DiscGUID ";
                sqlQuery += "WHERE D.DiscGUID = R1.DiscGUID AND R1.Reply_Date = LatestReplyDate) t2 ";
                sqlQuery += " ON t1.DiscGUID = t2.DiscGUID ";
                sqlQuery += " WHERE t1.DiscGUID IS NOT NULL ";
                sqlQuery += " ORDER BY t1.DiscIsPinned desc ";

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
                    panelDiscList.Visible = true;
                    panelNoRecord.Visible = false;
                    for (int i = 0; i < dtDisc.Rows.Count; i++)
                    {
                        if(dtDisc.Rows[i]["DiscIsPinned"].ToString() == "Yes") { strTable += "<tr style=\"text-align:left\" class=\"pinned-thread\">"; } else { strTable += "<tr style=\"text-align:left\">"; }
                        strTable += "<td class=\"text-center\" align=\"center\" style=\"width:120px;\">";
                        strTable += "<a href=\"KrousExViewDiscussion.aspx?DiscGUID=" + dtDisc.Rows[i]["DiscGUID"] + "\">View</a>";
                        strTable += "</td>";
                        if (dtDisc.Rows[i]["DiscIsPinned"].ToString() == "Yes") { strTable += "<td><a href=\"KrousExViewDiscussion.aspx?DiscGUID=" + dtDisc.Rows[i]["DiscGUID"] + "\">" + dtDisc.Rows[i]["DiscTopic"] + "<i class=\"fas fa-thumbtack pl-2\" style=\"color:yellow\"></i></a></td>"; } else { strTable += "<td><a href=\"DiscussionEntry.aspx?ForumGUID=" + dtDisc.Rows[i]["DiscGUID"] + "\">" + dtDisc.Rows[i]["DiscTopic"] + "</a></td>"; }
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
                }
                else
                {
                    panelDiscList.Visible = false;
                    panelNoRecord.Visible = true;
                }

                Literal1.Text = strTable;
                if (strLink != "")
                {
                    Literal2.Text = "> <a href=\"KrousExForumListings.aspx\" class=\"active-forum-link\">" + strLink + "</a>";
                } else
                {
                    Literal2.Text = "> <a href=\"KrousExForumListings.aspx\" class=\"active-forum-link\">" + Request.QueryString["ForumCategory"] + "</a>";
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}