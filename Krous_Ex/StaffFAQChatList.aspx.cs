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
    public partial class StaffFAQChatList : System.Web.UI.Page
    {
        private Guid StaffGUID;
        private string chatStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                chatStatus = "Pending";
                hdCheckChatStatus.Value = "Pending";
                StaffGUID = Guid.Empty;
                loadGV();
            }
        }

        protected void rblChatStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblChatStatus.SelectedValue == "1")
            {
                chatStatus = "Pending";
                hdCheckChatStatus.Value = "Pending";
                StaffGUID = Guid.Empty;
                loadGV();
            }
            else if (rblChatStatus.SelectedValue == "2") 
            {
                chatStatus = "In Progress";
                hdCheckChatStatus.Value = "In Progress";
                StaffGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
                loadGV();
            } else
            {
                chatStatus = "Ended";
                hdCheckChatStatus.Value = "Ended";
                StaffGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
                loadGV();
            }
        }

        private void loadGV()
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT C.ChatGUID, st.StudentFullName AS [SentBy], C.CreatedDate, C.ChatStatus, ";
                sqlQuery += "CASE WHEN s.StaffFullName IS NULL THEN '-' ELSE S.StaffFullName END AS [RepliedBy], S.StaffGUID, ";
                sqlQuery += "CASE WHEN C.EndDate IS NULL THEN '-' ELSE FORMAT(C.EndDate, 'dd-MM-yyyy hh:mm:tt') END AS [EndDate] ";
                sqlQuery += "FROM CHAT C LEFT JOIN Student st ON C.StudentGUID = st.StudentGUID ";
                sqlQuery += "LEFT JOIN Staff s ON C.StaffGUID = S.StaffGUID ";
                sqlQuery += "WHERE C.ChatStatus = CASE WHEN @ChatStatus = '' THEN C.ChatStatus ELSE @ChatStatus END AND ";
                sqlQuery += "CASE WHEN @StaffGUID = '00000000-0000-0000-0000-000000000000' then @StaffGUID ELSE S.StaffGUID end = @StaffGUID ";
                sqlQuery += "ORDER BY C.CreatedDate";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@ChatStatus", chatStatus);
                GetCommand.Parameters.AddWithValue("@StaffGUID", StaffGUID);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtChat = new DataTable();
                dtChat.Load(reader);
                con.Close();
                if (dtChat.Rows.Count != 0)
                {
                    gvChat.DataSource = dtChat;
                    gvChat.DataBind();
                    gvChat.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvChat.Visible = false;
                }

                if (chatStatus == "Ended")
                {
                    gvChat.Columns[0].Visible = false;
                }
                else
                {
                    gvChat.Columns[0].Visible = true;
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}