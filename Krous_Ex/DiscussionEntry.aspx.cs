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
    public partial class DiscussionEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ForumGUID"]))
            {
                loadGV();
            } else
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

                sqlQuery = "SELECT t1.DiscGUID, t1.DiscTopic, t1.DiscIsPinned, t1.TotalReply, t1.Created, t2.LastReply ";
                sqlQuery += "FROM ";
                sqlQuery += "(SELECT D.DiscGUID, D.DiscTopic, D.DiscIsPinned, COUNT(R.ReplyGUID) as TotalReply, CONCAT(D.DiscCreatedBy, ', <br />', Convert(varchar, D.DiscCreatedDate, 120)) As Created ";
                sqlQuery += "FROM Forum F LEFT JOIN Discussion D ON F.ForumGUID = D.ForumGUID LEFT OUTER JOIN Replies R On D.DiscGUID = R.DiscGUID ";
                sqlQuery += "WHERE F.ForumGUID = @ForumGUID ";
                sqlQuery += "GROUP BY D.DiscGUID, D.DiscTopic, D.DiscCreatedBy, D.DiscCreatedDate, D.DiscIsPinned) t1 ";
                sqlQuery += "LEFT JOIN ";
                sqlQuery += "(SELECT TOP 1 D.DiscGUID, CONCAT(R.Reply_By, ', <br />', Convert(varchar, R.Reply_Date, 120)) As LastReply ";
                sqlQuery += "FROM Discussion D, Replies R ";
                sqlQuery += "WHERE D.DiscGUID = R.DiscGUID ";
                sqlQuery += "GROUP BY D.DiscGUID, R.Reply_By, R.Reply_Date ";
                sqlQuery += "ORDER BY R.Reply_Date Desc) t2 ";
                sqlQuery += "ON t1.DiscGUID = t2.DiscGUID ";

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
                        strTable += "<tr style=\"text-align:left\">";
                        strTable += "<td class=\"text-center\" align=\"center\" style=\"width:120px;\">";
                        strTable += "<a href=\"DiscussionEntry.aspx?ForumGUID=" + dtDisc.Rows[i]["DiscGUID"] + "\">View</a>";
                        strTable += "</td>";
                        strTable += "<td><p><a href=\"DiscussionEntry.aspx?ForumGUID=" + dtDisc.Rows[i]["DiscGUID"] + "\">General Discussion</a></p>Discuss School Related</td>";
                        strTable += "<td style=\"width:20px;text-align:center\">" + dtDisc.Rows[i]["TotalReply"] + "</td>";
                        strTable += "<td style=\"width:20px\">by " + dtDisc.Rows[i]["Created"] + "</td>";
                        if (String.IsNullOrEmpty(dtDisc.Rows[i]["LastReply"].ToString())) {
                            strTable += "<td style=\"width:20px\">n/a</td>";
                        }
                        else {
                            strTable += "<td style=\"width:20px\">" + dtDisc.Rows[i]["LastReply"] + "</td>";
                        }
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