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
    public partial class KrousExViewDiscussion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"]))
            {
                loadGV();

                var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (myCookie != null)
                {
                    panelLogin.Visible = false;
                    panelPost.Visible = true;
                    panelPostReply.Visible = true;
                    Literal3.Text = "<p style=\"color:white\">" + clsLogin.GetLoginUserName() + "</p>";
                }
            }
            else
            {
                Response.Redirect("KrousExForumListings.aspx", true);
            }

            loadGV();
        }

        private void loadGV()       
        {
            try
            {
                string sqlQuery;
                string strTableHead = "";
                string strTableBody = "";
                string strLink = "";
                int y = 1;

                sqlQuery = "SELECT F.ForumGUID, F.ForumTopic,F.ForumCategory, D.DiscGUID, D.DiscContent, D.DiscCreatedBy as CreatedBy, Convert(varchar, D.DiscCreatedDate, 120) as CreatedDate, R.Reply_Content as RepliedContent, R.Reply_By as RepliedBy, Convert(varchar, R.Reply_Date, 120) as RepliedDate ";
                sqlQuery += " FROM Forum F LEFT OUTER JOIN Discussion D ON F.ForumGUID = D.ForumGUID LEFT OUTER JOIN Replies R ON D.DiscGUID = R.DiscGUID ";
                sqlQuery += "WHERE D.DiscGUID = @DiscGUID ORDER BY R.Reply_date ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@DiscGUID", Request.QueryString["DiscGUID"]);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtReply = new DataTable();
                dtReply.Load(reader);
                con.Close();

                if (dtReply.Rows.Count != 0)
                {

                    strTableHead += "<tr class=\"disc-head-content\">";
                    strTableHead += "<td hidden></td>";
                    strTableHead += "<td style = \"padding: 0.5rem 0.25rem 0.5rem 20px!important; background:#2f3545;\" colspan = \"2\">" + dtReply.Rows[0]["CreatedDate"] + "<span class=\"float-right\" style=\"padding-right:10px\">" + dtReply.Rows.Count + " replies</span></td>";
                    strTableHead += "</tr>";

                    strTableHead += "<tr class=\"disc-body-content\">";
                    strTableHead += "<td style=\"text-align:center\">";
                    strTableHead += "<img src=\"Assests/main/images/faces/face1.jpg\"/>";
                    strTableHead += "<p class=\"pt-3\">" + dtReply.Rows[0]["CreatedBy"] + " </p> ";
                    strTableHead += "</td>";
                    strTableHead += "<td style=\"padding: 10px 0.25rem 0.25rem 20px !important; vertical-align:top\">" + dtReply.Rows[0]["DiscContent"];
                    strTableHead += "</td>";
                    strTableHead += "</tr>";

                    
                    for (int i = 0; i < dtReply.Rows.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(dtReply.Rows[i]["RepliedContent"].ToString()))
                        {
                            strTableBody += "<tr>";
                            strTableBody += "<td hidden></td>";
                            strTableBody += "<td style = \"padding: 0.5rem 0.25rem 0.5rem 20px!important; background:#2f3545;\" colspan = \"2\">" + dtReply.Rows[i]["RepliedDate"] + "<span class=\"float-right\" style=\"padding-right:10px\">#" + y + "</span></td>";
                            strTableBody += "</tr>";
                            strTableBody += "<tr>";
                            strTableBody += "<td style=\"text-align:center\">";
                            strTableBody += "<img src=\"Assests/main/images/faces/face1.jpg\"/>";
                            strTableBody += "<p class=\"pt-3\">" + dtReply.Rows[i]["RepliedBy"] + " </p> ";
                            strTableBody += "</td>";
                            strTableBody += "<td style=\"padding: 10px 0.25rem 0.25rem 20px !important; vertical-align:top\">" + dtReply.Rows[i]["RepliedContent"];
                            strTableBody += "</td>";
                            strTableBody += "</tr>";
                        }

                        y++;
                    }

                    strLink = "> <a href =\"KrousExForumListings.aspx\" class=\"forum-link\">" + dtReply.Rows[0]["ForumCategory"] + "</a> > <a href =\"KrousExDiscussionListings.aspx?ForumGUID=" + dtReply.Rows[0]["ForumGUID"] + "\" class=\"active-forum-link\">" + dtReply.Rows[0]["ForumTopic"] + "</a>";

                    tableHead.Text = strTableHead;
                    tableBody.Text = strTableBody;
                    Literal2.Text = strLink;

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

        protected void btnPostReply_Click(object sender, EventArgs e)
        {
            if(insertReply())
            {
                Response.Redirect("KrousExViewDiscussion.aspx?DiscGUID=" + Request.QueryString["DiscGUID"]);
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
            }
        }

        private bool insertReply()
        {
            Guid ReplyGUID = Guid.NewGuid();

            string Username = clsLogin.GetLoginUserName();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO REPLIES VALUES(@ReplyGUID,@DiscGUID,@Reply_Content,@Reply_Date,@Reply_By)", con);

                InsertCommand.Parameters.AddWithValue("@ReplyGUID", ReplyGUID);
                InsertCommand.Parameters.AddWithValue("@DiscGUID", Request.QueryString["DiscGUID"]);
                InsertCommand.Parameters.AddWithValue("@Reply_Content", txtComment.Text);
                InsertCommand.Parameters.AddWithValue("@Reply_Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@Reply_By", clsLogin.GetLoginUserName());

                InsertCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }
    }
}