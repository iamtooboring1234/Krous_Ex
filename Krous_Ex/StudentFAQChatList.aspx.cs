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
    public partial class StudentFAQChatList : System.Web.UI.Page
    {
        private string chatStatus = "";
        private Guid StudentGUID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadGV("False");
            }
        }

        protected void rblChatStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblChatStatus.SelectedValue == "1")
            {
                chatStatus = "";
                loadGV("False");
            } else
            {
                chatStatus = "Ended";
                loadGV("True");
            }
        }

        private void loadGV(string checkEnd)
        {
            try
            {
                StudentGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
                string sqlQuery;

                sqlQuery = "SELECT C.ChatGUID, st.StudentFullName AS [SentBy], C.CreatedDate, C.ChatStatus, ";
                sqlQuery += "CASE WHEN s.StaffFullName IS NULL THEN '-' ELSE S.StaffFullName END AS [RepliedBy], S.StaffGUID, ";
                sqlQuery += "CASE WHEN C.EndDate IS NULL THEN '-' ELSE FORMAT(C.EndDate, 'dd-MM-yyyy hh:mm:tt') END AS [EndDate] ";
                sqlQuery += "FROM CHAT C LEFT JOIN Student st ON C.StudentGUID = st.StudentGUID ";
                sqlQuery += "LEFT JOIN Staff s ON C.StaffGUID = S.StaffGUID ";

                if (checkEnd == "False")
                {
                    sqlQuery += "WHERE C.ChatStatus = 'Pending' OR C.ChatStatus = 'In Progress' AND ";
                }
                else
                {
                    sqlQuery += "WHERE C.ChatStatus = 'Ended' AND ";
                }

                sqlQuery += "st.StudentGUID = @StudentGUID ";
                sqlQuery += "ORDER BY C.CreatedDate";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@StudentGUID", StudentGUID);
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