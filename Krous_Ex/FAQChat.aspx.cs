using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class FAQChat : System.Web.UI.Page
    {
        private string StaffGUID = "";
        private string ChatGUID = "";
        private string SaveFolderPath = ConfigurationManager.AppSettings.Get("FAQChatSavePath");
        private string UploadChatFolderPath = ConfigurationManager.AppSettings.Get("FAQChatUploadPath");

        protected void Page_PreInit(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                if (clsLogin.GetLoginUserType() == "Staff")
                {
                    MasterPageFile = "~/StaffMaster.Master";
                }
            } else
            {
                Response.Redirect("Homepage");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (String.IsNullOrEmpty(Request.QueryString["ChatGUID"]))
                {
                    clsFunction.DisplayAJAXMessage(this, "Page Not Available");
                    Response.Redirect("Homepage");
                }

                ChatGUID = Request.QueryString["ChatGUID"].ToString();

                hdCurrentUserGUID.Value = clsLogin.GetLoginUserGUID();
                hdUserType.Value = clsLogin.GetLoginUserType();
                hdChatGUID.Value = ChatGUID;

                if (clsLogin.GetLoginUserType() == "Staff")
                {
                    hdUserType.Value = "Staff";
                    StaffGUID = clsLogin.GetLoginUserGUID();
                }

                if (Session["NewChat"] == null)
                {
                    LoadMessage();
                } else
                {
                    hdNewChat.Value = Session["NewChat"].ToString();
                    hdCheckDate.Value = "";
                    Session["NewChat"] = "";
                }
            }
        }

        protected void LoadMessage()
        {
            try
            {
                string strSQLcommand;
                strSQLcommand = "SELECT M.*,C.ChatStatus,S.StaffGUID, SS.StudentUsername FROM [Message] M  ";
                strSQLcommand += "LEFT JOIN chat C ON M.ChatGUID = C.ChatGUID ";
                strSQLcommand += "LEFT JOIN Student SS ON C.StudentGUID = SS.StudentGUID ";
                strSQLcommand += "LEFT JOIN Staff S ON C.StaffGUID = S.StaffGUID ";
                strSQLcommand += "WHERE M.ChatGUID = '" + ChatGUID + "' ";
                strSQLcommand += "ORDER BY M.SendDate";
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                var GetCommand = new SqlCommand(strSQLcommand, con);
                SqlDataReader reader = GetCommand.ExecuteReader();
                var dtMessage = new DataTable();
                string strOldMessage = "";
                string pdfLinkColor = "";
                var SplitFileName = new string[0];

                dtMessage.Load(reader);
                con.Close();
                if (dtMessage.Rows.Count != 0)
                {
                    if (StaffGUID != "")
                    {
                        if (dtMessage.Rows[0]["ChatStatus"].ToString() == "In Progress" & dtMessage.Rows[0]["StaffGUID"].ToString() != StaffGUID)
                        {
                            clsFunction.DisplayAJAXMessage(this, "This chat already handle by other staff");
                            Response.Redirect("StaffChatList.aspx");
                        }
                        else if (dtMessage.Rows[0]["ChatStatus"].ToString() == "Pending")
                        {
                            updateChat();
                        }
                    }

                    if (dtMessage.Rows[0]["ChatStatus"].ToString() != "Ended")
                    {
                        DateTime convertDate;
                        DateTime checkDate = default;
                        bool checkYesterday = true;
                        bool checkToday = true;
                        for (int i = 0, loopTo = dtMessage.Rows.Count - 1; i <= loopTo; i++)
                        {
                            convertDate = DateTime.Parse(dtMessage.Rows[i]["SendDate"].ToString());
                            if (convertDate.Date == DateTime.Now.Date & checkToday == true)
                            {
                                strOldMessage += "<div class=\"row col-lg-12 justify-content-center m-0\"><hr class=\"col-md-4\" style=\"border: 1px solid white;margin:auto;\">";
                                strOldMessage += "<div class=\"media media-meta-day\">Today</div><hr class=\"col-md-4\" style=\"border: 1px solid white;margin:auto;\">";
                                strOldMessage += "</div>";
                                checkToday = false;
                                checkDate = convertDate;
                            }
                            else if (convertDate.Date == DateTime.Now.AddDays(-1).Date & checkYesterday == true)
                            {
                                strOldMessage += "<div class=\"row col-lg-12 justify-content-center m-0\"><hr class=\"col-md-4\" style=\"border: 1px solid white;margin:auto;\">";
                                strOldMessage += "<div class=\"media media-meta-day\">Yesterday</div><hr class=\"col-md-4\" style=\"border: 1px solid white;margin:auto;\">";
                                strOldMessage += "</div>";
                                checkYesterday = false;
                                checkDate = convertDate;
                            }
                            else if (convertDate.Date != checkDate.Date)
                            {
                                strOldMessage += "<div class=\"row col-lg-12 justify-content-center m-0\"><hr class=\"col-md-4\" style=\"border: 1px solid white;margin:auto;\">";
                                strOldMessage += "<div class=\"media media-meta-day\">" + convertDate.ToString("MMMM d, yyyy") + "</div><hr class=\"col-md-4\" style=\"border: 1px solid white;margin:auto;\">";
                                strOldMessage += "</div>";
                                checkDate = convertDate;
                            }

                            hdCheckDate.Value = convertDate.ToString("MM-dd-yyyy");
                            if (dtMessage.Rows[i]["UserType"].ToString() == hdUserType.Value)
                            {
                                strOldMessage += "<div class=\"row col-lg-12 justify-content-end m-0\">";
                                strOldMessage += "<div class=\"media media-chat media-chat-reverse mediaPadding\">";
                                pdfLinkColor = "pdfLinkWhite";
                            }
                            else
                            {
                                strOldMessage += "<div class=\"row col-lg-12 m-0\">";
                                strOldMessage += "<div class=\"media media-chat mediaPadding\">";
                                pdfLinkColor = "pdfLinkBlack";
                            }

                            strOldMessage += "<div class=\"media-body\">";
                            if (dtMessage.Rows[i]["MessageType"].ToString() == "Text")
                            {
                                strOldMessage += "<p>" + dtMessage.Rows[i]["MessageDetail"].ToString() + "</p>";
                            }
                            else if (dtMessage.Rows[i]["MessageType"].ToString() == "Image")
                            {
                                strOldMessage += "<p>";
                                strOldMessage += "<img src=\"" + UploadChatFolderPath + dtMessage.Rows[i]["MessageDetail"].ToString() + "\" Style=\"cursor: pointer\" alt=\"Send Image\" width=\"250px\" height=\"200px\" onclick=\"enlargeImage(this.src)\">";
                                strOldMessage += "</p>";
                            }
                            else
                            {
                                SplitFileName = dtMessage.Rows[i]["MessageDetail"].ToString().Split('_');
                                strOldMessage += "<p>";
                                strOldMessage += "<a class=\"" + pdfLinkColor + "\" target=\"_blank\" href=\"" + UploadChatFolderPath + dtMessage.Rows[i]["MessageDetail"].ToString() + "\">" + SplitFileName[0] + ".pdf</a>";
                                strOldMessage += "</p>";
                            }

                            strOldMessage += "<p class=\"meta\"><time>" + DateTime.Parse(dtMessage.Rows[i]["SendDate"].ToString()).ToString("hh:mm: tt") + "</time></p>";
                            strOldMessage += "</div>";
                            strOldMessage += "</div>";
                            strOldMessage += "</div>";
                        }

                        litMessage.Text = strOldMessage;
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Chat already ended");
                        Response.Redirect("FAQChatView.aspx?ChatGUID=" + ChatGUID);
                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "No message found");
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void updateChat()
        {
            try
            {
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                var UpdateCommand = new SqlCommand("UPDATE Chat SET ChatStatus = 'In Progress', StaffGUID = @StaffGUID WHERE ChatGUID = @ChatGUID", con);
                UpdateCommand.Parameters.AddWithValue("@StaffGUID", Guid.Parse(StaffGUID));
                UpdateCommand.Parameters.AddWithValue("@ChatGUID", Guid.Parse(ChatGUID));
                UpdateCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void FileUploadComplete(object sender, EventArgs e)
        {
            string ChatImageName = Path.GetFileNameWithoutExtension(AsyncFileUploadChat.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(AsyncFileUploadChat.FileName);

            AsyncFileUploadChat.SaveAs(Server.MapPath(SaveFolderPath) + ChatImageName);

            Response.Cookies["ImagePath"].Value = UploadChatFolderPath;
            Response.Cookies["ImagePath"].Expires = DateTime.Now.AddMinutes(10d);
            Response.Cookies["ChatImageName"].Value = ChatImageName;
            Response.Cookies["ChatImageName"].Expires = DateTime.Now.AddMinutes(10d);
        }
    }
}