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
    public partial class FAQChatView : System.Web.UI.Page
    {
        private string SaveFolderPath = ConfigurationManager.AppSettings.Get("FAQChatSavePath");
        private string UploadChatFolderPath = ConfigurationManager.AppSettings.Get("FAQChatUploadPath");

        private void ChatView_PreInit(object sender, EventArgs e)
        {
            if (clsLogin.GetLoginUserType() == "Staff")
            {
                MasterPageFile = "~/StaffMaster.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Request.QueryString["ChatGUID"] is object)
                {
                    LoadMessage(Request.QueryString["ChatGUID"].ToString());
                }
            }
        }

        private void LoadMessage(string ChatGUID)
        {
            try
            {
                string strSQLcommand;
                strSQLcommand = "SELECT M.*,C.ChatStatus,S.StaffGUID FROM [Message] M  ";
                strSQLcommand += "LEFT JOIN chat C ON M.ChatGUID = C.ChatGUID ";
                strSQLcommand += "LEFT JOIN Staff S ON C.StaffGUID = S.StaffGUID ";
                strSQLcommand += "WHERE M.ChatGUID = '" + ChatGUID + "' ";
                strSQLcommand += "ORDER BY M.SendDate";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand(strSQLcommand, con);
                SqlDataReader reader = GetCommand.ExecuteReader();
                string userType;
                DataTable dtMessage = new DataTable();
                string strOldMessage = "";
                string pdfLinkColor = "";
                var SplitFileName = new string[0];

                dtMessage.Load(reader);
                con.Close();
                if (dtMessage.Rows.Count != 0)
                {
                    if (clsLogin.GetLoginUserType() == "Staff")
                    {
                        userType = "Staff";
                        if (dtMessage.Rows[0]["ChatStatus"].ToString() == "Pending")
                        {
                            hdCheckChatStatus.Value = "Pending";
                        }
                    }
                    else
                    {
                        userType = clsLogin.GetLoginUserType();
                    }

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

                        if (dtMessage.Rows[i]["UserType"].ToString() == userType)
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
                    if (dtMessage.Rows[0]["ChatStatus"].ToString() == "Ended")
                    {
                        EnterChatBtn.Visible = false;
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

        protected void EnterChatBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FAQChat.aspx?ChatGUID=" + Request.QueryString["ChatGUID"].ToString());
        }
    }
}