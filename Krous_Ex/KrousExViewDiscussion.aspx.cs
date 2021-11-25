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
            if (IsPostBack != true)
            {
                if (Session["ReportForum"] != null)
                {
                    if(Session["ReportForum"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showReportCommentsSuccesfully(); ", true);
                        Session["ReportForum"] = null;
                    }
                }

                if (Session["DeleteReply"] != null)
                {
                    if (Session["DeleteReply"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showDeleteCommentsSuccesfully(); ", true);
                        Session["DeleteReply"] = null;
                    }
                }

                if (Session["EditReply"] != null)
                {
                    if (Session["EditReply"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUpdateCommentsSuccesfully(); ", true);
                        Session["EditReply"] = null;
                    }
                }


                if (Session["PostReply"] != null)
                {
                    if (Session["PostReply"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showReplyPostSuccessfully(); ", true);
                        Session["PostReply"] = null;
                    }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"]))
                {
                    loadGV();
                    HyperLink1.DataBind();

                    var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                    if (myCookie != null)
                    {
                        panelLogin.Visible = false;
                        panelPost.Visible = true;
                        Literal3.Text = "<p style=\"color:white\">" + clsLogin.GetLoginUserName() + "</p>";
                    }
                }
                else
                {
                    Response.Redirect("KrousExForumListings.aspx", true);
                }
            }
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

                sqlQuery = "SELECT F.ForumGUID, F.ForumTopic,F.ForumCategory, D.DiscGUID, D.DiscContent, D.DiscIsPinned, D.DiscIsLocked, D.DiscCreatedBy as CreatedBy, Convert(varchar, D.DiscCreatedDate, 120) as CreatedDate, R.ReplyGUID, R.ReplyContent as RepliedContent, R.ReplyBy as RepliedBy, Convert(varchar, R.ReplyDate, 120) as RepliedDate ";
                sqlQuery += " FROM Forum F LEFT OUTER JOIN Discussion D ON F.ForumGUID = D.ForumGUID LEFT OUTER JOIN Replies R ON D.DiscGUID = R.DiscGUID ";
                sqlQuery += "WHERE D.DiscGUID = @DiscGUID ORDER BY R.Replydate ";

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
                    var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                    if(dtReply.Rows[0]["DiscIsPinned"].ToString() == "Yes")
                    {
                        if (myCookie != null)
                        {
                            if (clsLogin.GetLoginUserType() == "Staff")
                            {
                                btnPin.Visible = false;
                                btnUnpin.Visible = true;
                            } else
                            {
                                btnPin.Visible = false;
                                btnUnpin.Visible = false;
                            }
                        }
                    } else
                    {
                        if (myCookie != null)
                        {
                            if (clsLogin.GetLoginUserType() == "Staff")
                            {
                                btnPin.Visible = true;
                                btnUnpin.Visible = false;
                            }
                            else
                            {
                                btnPin.Visible = false;
                                btnUnpin.Visible = false;
                            }
                        }
                    }

                    if (dtReply.Rows[0]["DiscIsLocked"].ToString() == "Yes") {
                            
                        if (myCookie != null)
                        {
                            panelPostReply.Visible = false;
                            if (clsLogin.GetLoginUserType() == "Staff")
                            {
                                btnLock.Visible = false;
                                btnUnlock.Visible = true;
                                panelManage.Visible = true;
                                panelPostReply.Visible = true;
                            } else
                            {
                                panelManage.Visible = false;
                                panelPostReply.Visible = false;
                            }
                        }
                        else
                        {
                            panelPostReply.Visible = false;
                        }

                        strTableHead += "<tr class=\"disc-head-content\">";
                        strTableHead += "<td hidden></td>";
                        strTableHead += "<td style = \"padding: 0.5rem 0.25rem 0.5rem 20px!important; background:#2f3545;\" colspan = \"2\">" + dtReply.Rows[0]["CreatedDate"] + "<span class=\"float-right\" style=\"padding-right:10px\">" + dtReply.Rows.Count + " replies <i style=\"color:yellow\" class=\"fas fa-lock\"></i></span></td>";
                        strTableHead += "</tr>";
                    }
                    else
                    {
                        if (myCookie != null)
                        {
                            panelPostReply.Visible = false;
                            if (clsLogin.GetLoginUserType() == "Staff")
                            {
                                btnLock.Visible = true;
                                btnUnlock.Visible = false;
                                panelManage.Visible = true;
                                panelPostReply.Visible = true;
                            }
                            else
                            {
                                panelPostReply.Visible = true;
                                panelManage.Visible = false;
                            }
                        }

                        strTableHead += "<tr class=\"disc-head-content\">";
                        strTableHead += "<td hidden></td>";
                        strTableHead += "<td style = \"padding: 0.5rem 0.25rem 0.5rem 20px!important; background:#2f3545;\" colspan = \"2\">" + dtReply.Rows[0]["CreatedDate"] + "<span class=\"float-right\" style=\"padding-right:10px\">" + dtReply.Rows.Count + " replies </span></td>";
                        strTableHead += "</tr>";
                    }

                    if (myCookie != null) {
                        if (clsLogin.GetLoginUserType() == "Student")
                        {
                            if (dtReply.Rows[0]["CreatedBy"].ToString() == clsLogin.GetLoginUserName())
                            {
                                panelManage.Visible = true;
                                btnLock.Visible = false;
                                btnUnlock.Visible = false;
                                btnDelete.Visible = true;
                            }
                        }
                    }


                    con.Open();
                    GetCommand = new SqlCommand("SELECT * FROM STUDENT WHERE StudentUsername = @StudentUserName ", con);
                    GetCommand.Parameters.AddWithValue("@StudentUserName", dtReply.Rows[0]["CreatedBy"].ToString());
                    reader = GetCommand.ExecuteReader();
                    DataTable dtStudentCreator = new DataTable();
                    dtStudentCreator.Load(reader);

                    GetCommand = new SqlCommand("SELECT * FROM Staff WHERE StaffUsername = @StaffUsername ", con);
                    GetCommand.Parameters.AddWithValue("@StaffUsername", dtReply.Rows[0]["CreatedBy"].ToString());
                    reader = GetCommand.ExecuteReader();
                    DataTable dtStaffCreator = new DataTable();
                    dtStaffCreator.Load(reader);
                    con.Close();

                    if (dtStudentCreator.Rows.Count != 0)
                    {
                        strTableHead += "<tr class=\"disc-body-content\">";
                        strTableHead += "<td style=\"text-align:center\">";
                        strTableHead += "<img src=\"" + ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dtStudentCreator.Rows[0]["ProfileImage"].ToString() + "\"/>";
                        strTableHead += "<p class=\"pt-3\">" + dtReply.Rows[0]["CreatedBy"] + " </p> ";
                        strTableHead += "</td>";
                        strTableHead += "<td style=\"padding: 10px 0.25rem 0.25rem 20px !important; vertical-align:top\">" + dtReply.Rows[0]["DiscContent"];
                        strTableHead += "</td>";
                        strTableHead += "</tr>";
                    } else
                    {
                        strTableHead += "<tr class=\"disc-body-content\">";
                        strTableHead += "<td style=\"text-align:center\">";
                        strTableHead += "<img src=\"" + ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dtStaffCreator.Rows[0]["ProfileImage"].ToString() + "\"/>";
                        strTableHead += "<p class=\"pt-3\">" + dtReply.Rows[0]["CreatedBy"] + " </p> ";
                        strTableHead += "</td>";
                        strTableHead += "<td style=\"padding: 10px 0.25rem 0.25rem 20px !important; vertical-align:top\">" + dtReply.Rows[0]["DiscContent"];
                        strTableHead += "</td>";
                        strTableHead += "</tr>";
                    }

                    
                    for (int i = 0; i < dtReply.Rows.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(dtReply.Rows[i]["RepliedContent"].ToString()))
                        {
                            con.Open();
                            GetCommand = new SqlCommand("SELECT * FROM STUDENT WHERE StudentUsername = @StudentUserName ", con);
                            GetCommand.Parameters.AddWithValue("@StudentUserName", dtReply.Rows[i]["RepliedBy"].ToString());
                            reader = GetCommand.ExecuteReader();
                            DataTable dtStudent = new DataTable();
                            dtStudent.Load(reader);

                            GetCommand = new SqlCommand("SELECT * FROM Staff WHERE StaffUsername = @StaffUsername ", con);
                            GetCommand.Parameters.AddWithValue("@StaffUsername", dtReply.Rows[i]["RepliedBy"].ToString());
                            reader = GetCommand.ExecuteReader();
                            DataTable dtStaff = new DataTable();
                            dtStaff.Load(reader);
                            con.Close();

                            strTableBody += "<tr>";
                            strTableBody += "<td hidden></td>";
                            strTableBody += "<td style = \"padding: 0.5rem 0.25rem 0.5rem 20px!important; background:#2f3545;\" colspan = \"2\">" + dtReply.Rows[i]["RepliedDate"] + "<span class=\"float-right\" style=\"padding-right:10px\">#" + y + "</span></td>";
                            strTableBody += "</tr>";
                            strTableBody += "<tr>";
                            strTableBody += "<td style=\"text-align:center\">";

                            if (dtStudent.Rows.Count != 0)
                            {
                                strTableBody += "<img src=\"" + ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dtStudent.Rows[0]["ProfileImage"].ToString() + "\"/>";
                            } else
                            {
                                strTableBody += "<img src=\"" + ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dtStaff.Rows[0]["ProfileImage"].ToString() + "\"/>";
                            }

                            if (myCookie != null)
                            {
                                if (clsLogin.GetLoginUserType() == "Staff")
                                {
                                    strTableBody += "<p class=\"pt-3\">" + dtReply.Rows[i]["RepliedBy"] + "</p><p><a style=\"color:red\" href=\"KrousExDeleteComment?ReplyGUID=" + dtReply.Rows[i]["ReplyGUID"] + "&DiscGUID=" + dtReply.Rows[i]["DiscGUID"] + "\"><i class=\"fas fa-trash-alt\"></i></a></p>";
                                }
                                else
                                {
                                    if (dtReply.Rows[i]["RepliedBy"].ToString() == clsLogin.GetLoginUserName())
                                    {
                                        strTableBody += "<p class=\"pt-3\">";

                                        if (dtReply.Rows[0]["DiscIsLocked"].ToString() != "Yes")
                                        {
                                            strTableBody += dtReply.Rows[i]["RepliedBy"] + "</p><p><a Class=\"mr-3\" href=\"KrousExEditComments?ReplyGUID=" + dtReply.Rows[i]["ReplyGUID"] + "&DiscGUID=" + dtReply.Rows[i]["DiscGUID"] + "\"><i style=\"color: #00d25b;\" class=\"fas fa-edit\"></i></a>";
                                        }
                                        strTableBody += "<a href=\"KrousExDeleteComment?ReplyGUID=" + dtReply.Rows[i]["ReplyGUID"] + "&DiscGUID=" + dtReply.Rows[i]["DiscGUID"] + "\"><i style=\"color: red;\" class=\"fas fa-trash-alt\"></i></a></p>";
                                    }
                                    else
                                    {
                                        strTableBody += "<p class=\"pt-3\">" + dtReply.Rows[i]["RepliedBy"] + "</p><p><a href=\"KrousExReportForum?ReplyGUID=" + dtReply.Rows[i]["ReplyGUID"] + "&DiscGUID=" + dtReply.Rows[i]["DiscGUID"] + "\"><i style=\"background: red;border-radius: 2px;padding: 5px 10px;color: white;\" class=\"fas fa-exclamation\"></i></a></p>";
                                    } 
                                }
                            } else
                            {
                                strTableBody += "<p class=\"pt-3\">" + dtReply.Rows[i]["RepliedBy"] + "</p>";
                            }
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
            if (isReplyMessageEmpty())
            {
                if (insertReply())
                {
                    Session["PostReply"] = "Yes";
                    Response.Redirect("KrousExViewDiscussion.aspx?DiscGUID=" + Request.QueryString["DiscGUID"]);
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
                }
            } else
            {
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showWarningToast(); ", true);
             }
        }

        private bool isReplyMessageEmpty()
        {
            if (!string.IsNullOrEmpty(txtComment.Text))
            {
                return true;
            } else
            {
                return false;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteForum())
            {
                Session["DeleteDiscussion"] = "Yes";
                Response.Redirect("KrousExForumListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to delete. No such record.");
            }
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            if (updateDiscussion("lock"))
            {
                Session["DiscussionLocked"] = "Yes";
                Response.Redirect("KrousExForumListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to lock. Please contact KrousEx support.");
            }
        }

        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            if (updateDiscussion("unlock"))
            {
                Session["DiscussionUnlocked"] = "Yes";
                Response.Redirect("KrousExForumListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to unlock. Please contact KrousEx support.");
            }
        }

        protected void btnPin_Click(object sender, EventArgs e)
        {
            if (updateDiscussion("pin"))
            {
                Session["DiscussionPinned"] = "Yes";
                Response.Redirect("KrousExForumListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to pin. Please contact KrousEx support.");
            }
        }

        protected void btnUnpin_Click(object sender, EventArgs e)
        {
            if (updateDiscussion("unpin"))
            {
                Session["DiscussionUnpinned"] = "Yes";
                Response.Redirect("KrousExForumListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to unpin. Please contact KrousEx support.");
            }
        }

        private bool updateDiscussion(String button)
        {
            Guid DiscGUID = Guid.Parse(Request.QueryString["DiscGUID"]);

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand deleteCommand = null;

                if (button == "lock") { deleteCommand = new SqlCommand("UPDATE Discussion SET DiscIsLocked = 'Yes' WHERE DiscGUID = @DiscGUID ", con); }
                else if (button == "unlock") { deleteCommand = new SqlCommand("UPDATE Discussion SET DiscIsLocked = 'No' WHERE DiscGUID = @DiscGUID ", con); }
                else if (button == "pin") { deleteCommand = new SqlCommand("UPDATE Discussion SET DiscIsPinned = 'Yes' WHERE DiscGUID = @DiscGUID ", con); }
                else if (button == "unpin") { deleteCommand = new SqlCommand("UPDATE Discussion SET DiscIsPinned = 'No' WHERE DiscGUID = @DiscGUID ", con);  }

                deleteCommand.Parameters.AddWithValue("@DiscGUID", DiscGUID);

                deleteCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        private bool deleteForum()
        {
            Guid DiscGUID = Guid.Parse(Request.QueryString["DiscGUID"]);

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand deleteCommand = new SqlCommand("UPDATE Discussion SET DiscStatus = 'Inactive' WHERE DiscGUID = @DiscGUID ", con);

                deleteCommand.Parameters.AddWithValue("@DiscGUID", DiscGUID);

                deleteCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
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

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO REPLIES VALUES(@ReplyGUID,@DiscGUID,@ReplyContent,@ReplyDate,@ReplyBy)", con);

                InsertCommand.Parameters.AddWithValue("@ReplyGUID", ReplyGUID);
                InsertCommand.Parameters.AddWithValue("@DiscGUID", Request.QueryString["DiscGUID"]);
                InsertCommand.Parameters.AddWithValue("@ReplyContent", txtComment.Text);
                InsertCommand.Parameters.AddWithValue("@ReplyDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@ReplyBy", clsLogin.GetLoginUserName());

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