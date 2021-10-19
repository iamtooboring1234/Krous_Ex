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

                sqlQuery = "SELECT t1.ForumGUID, t1.ForumTopic, t1.ForumCategory, t1.ForumDesc, t1.TotalDisc, t1.TotalReply, t2.LastUpdated ";
                sqlQuery += "FROM ";
                sqlQuery += "(SELECT F.ForumGUID, F.ForumTopic, F.ForumCategory, F.ForumDesc, COUNT(DISTINCT(D.DiscGUID)) AS TotalDisc, COUNT(R.ReplyGUID) AS TotalReply ";
                sqlQuery += "FROM Forum F, Discussion D, Replies R ";
                sqlQuery += "WHERE F.ForumGUID = D.ForumGUID AND D.DiscGUID = R.DiscGUID AND F.ForumStatus = 'Active' ";
                sqlQuery += "GROUP BY F.ForumGUID, F.ForumTopic, F.ForumCategory, F.ForumDesc) t1 ";
                sqlQuery += "LEFT JOIN ";
                sqlQuery += "(SELECT TOP 1 F.ForumGUID, CONCAT(Reply_By,', <br />',Convert(varchar, Reply_Date, 120)) As LastUpdated ";
                sqlQuery += "FROM Forum F, Discussion D, Replies R ";
                sqlQuery += "WHERE F.ForumGUID = D.ForumGUID AND D.DiscGUID = R.DiscGUID ";
                sqlQuery += "ORDER BY Reply_Date DESC ";
                sqlQuery += ") t2 ";
                sqlQuery += "ON t1.ForumGUID = t2.ForumGUID ";

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
                        strTable += "<tr style=\"border-bottom: 2px solid white\">";
                        strTable += "<td colspan=\"5\" style=\"text-align: left;color:white\" class=\"p-2\">" + dtForum.Rows[i]["ForumCategory"];
                        strTable += "</td>";
                        strTable += "</tr>";
                        strTable += "<tr style=\"text-align:left\">";
                        strTable += "<td class=\"text-center\" align=\"center\" style=\"width:120px;\">";
                        strTable += "<a href=\"KrousExDiscussionListings.aspx?ForumGUID=" + dtForum.Rows[i]["ForumGUID"] + "\">View</a>";
                        strTable += "</td>";
                        strTable += "<td><p><a href=\"KrousExDiscussionListings.aspx?ForumGUID=" + dtForum.Rows[i]["ForumGUID"] + "\">General Discussion</a></p>Discuss School Related</td>";
                        strTable += "<td style=\"width:20px;text-align:center\">" + dtForum.Rows[i]["TotalDisc"] + "</td>";
                        strTable += "<td style=\"width:20px;text-align:center\">" + dtForum.Rows[i]["TotalReply"] + "</td>";
                        strTable += "<td style=\"width:20px\">" + dtForum.Rows[i]["LastUpdated"] + "</td>";
                        strTable += "</tr>";
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