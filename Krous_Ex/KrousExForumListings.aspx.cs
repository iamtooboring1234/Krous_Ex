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
    public partial class KrousExForumListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGV();

            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                panelLogin.Visible = false;
                panelPost.Visible = true;
            }

        }

        private void loadGV()
        {
            try
            {
                string sqlQuery;
                string strTable = "";
                string lastCategory = "";

                sqlQuery = "SELECT t1.ForumGUID, t1.ForumTopic, t1.ForumCategory, t1.ForumDesc, t1.TotalDisc, t1.TotalReply, t2.DiscCreatedBy as LatestCreatedBy, max(t2.DiscCreatedDate) as LastUpdated ";
                sqlQuery += "FROM ";
                sqlQuery += "(SELECT F.ForumGUID, F.ForumTopic, F.ForumCategory, F.ForumDesc, COUNT(DISTINCT(D.DiscGUID)) AS TotalDisc, COUNT(R.ReplyGUID) AS TotalReply ";
                sqlQuery += "FROM Forum F LEFT OUTER JOIN Discussion D on F.ForumGUID = D.ForumGUID LEFT JOIN Replies R ON D.DiscGUID = R.DiscGUID ";
                sqlQuery += " WHERE F.ForumStatus = 'Active' ";
                sqlQuery += "  GROUP BY F.ForumGUID, F.ForumTopic, F.ForumCategory, F.ForumDesc) t1 ";
                sqlQuery += "  LEFT JOIN ";
                sqlQuery += " (SELECT F.ForumGUID, D1.DiscGUID, D1.DiscCreatedBy, D1.DiscCreatedDate ";
                sqlQuery += " FROM Forum F LEFT JOIN Discussion D1 On F.ForumGUID = D1.ForumGUID ";
                sqlQuery += " LEFT JOIN(SELECT DiscGUID, Max(DiscCreatedDate) as LatestCreatedDate FROM Discussion Group by DiscGUID) D2 on D1.DiscGUID = D2.DiscGUID ";
                sqlQuery += "WHERE D1.DiscCreatedDate = LatestCreatedDate) t2 ";
                sqlQuery += " ON t1.ForumGUID = t2.ForumGUID ";
                sqlQuery += " GROUP BY t1.ForumGUID, t1.ForumTopic, t1.ForumCategory, t1.ForumDesc, t1.TotalDisc, t1.TotalReply, t2.DiscCreatedBy ";
                sqlQuery += " ORDER BY t1.ForumCategory ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtForum = new DataTable();
                dtForum.Load(reader);
                con.Close();

                if (dtForum.Rows.Count != 0)
                {

                    for (int i = 0; i < dtForum.Rows.Count; i++)
                    {
                        
                        
                        if (lastCategory != dtForum.Rows[i]["ForumCategory"].ToString())
                        {
                            strTable += "<tr style=\"border-bottom: 2px solid white\">";
                            strTable += "<td colspan=\"5\" style=\"text-align: left;color:white\" class=\"p-2\">" + dtForum.Rows[i]["ForumCategory"];
                        } else
                        {
                            strTable += "<tr>";
                            strTable += "<td hidden colspan=\"5\"></td>";
                        }
                        strTable += "</td>";
                        strTable += "</tr>";
                        strTable += "<tr style=\"text-align:left\">";
                        strTable += "<td class=\"text-center\" align=\"center\" style=\"width:120px;\">";
                        strTable += "<a href=\"KrousExDiscussionListings.aspx?ForumGUID=" + dtForum.Rows[i]["ForumGUID"] + "&ForumCategory=" + dtForum.Rows[i]["ForumCategory"] + "\">View</a>";
                        strTable += "</td>";
                        strTable += "<td><p><a href=\"KrousExDiscussionListings.aspx?ForumGUID=" + dtForum.Rows[i]["ForumGUID"] + "&ForumCategory=" + dtForum.Rows[i]["ForumCategory"] + "\">" + dtForum.Rows[i]["ForumTopic"] + "</a></p>" + dtForum.Rows[i]["ForumDesc"] + "</td>";
                        strTable += "<td style=\"width:20px;text-align:center\">" + dtForum.Rows[i]["TotalDisc"] + "</td>";
                        strTable += "<td style=\"width:20px;text-align:center\">" + dtForum.Rows[i]["TotalReply"] + "</td>";
                        if (!String.IsNullOrEmpty(dtForum.Rows[i]["LatestCreatedBy"].ToString()))
                        {
                            strTable += "<td style=\"width:20px\">" + dtForum.Rows[i]["LatestCreatedBy"] + "<br />" + dtForum.Rows[i]["LastUpdated"] + "</td>";
                        } else
                        {
                            strTable += "<td style=\"width:20px\">n/a</td>";
                        }
                        strTable += "</tr>";

                        lastCategory = dtForum.Rows[i]["ForumCategory"].ToString();
                    }

                    Literal1.Text = strTable;
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