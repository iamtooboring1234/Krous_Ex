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
            if (!IsPostBack) { 
                loadForumCategory();
            }

        }

        private void loadForumCategory()
        {
            try
            {
                ListItem oList = new ListItem();

                //if (Request.QueryString["FAQGUID"] == null)
                //{
                //    oList = new ListItem();
                //    oList.Text = "";
                //    oList.Value = "";
                //    ddlCategory.Items.Add(oList);
                //}

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ForumGUID, ForumTopic FROM Forum GROUP BY ForumGUID, ForumTopic ORDER BY ForumTopic ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                for (int i = 0; i <= dtFAQ.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtFAQ.Rows[i]["ForumTopic"].ToString();
                    oList.Value = dtFAQ.Rows[i]["ForumGUID"].ToString();
                    ddlCategory.Items.Add(oList);
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (insertFAQ())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "ShowPopup();", true);
                Response.Redirect("KrousExDiscussionEntry");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
            }
        }

        private bool insertFAQ()
        {
            Guid DiscGUID = Guid.NewGuid();

            string Username = clsLogin.GetLoginUserName();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO Discussion VALUES(@DiscGUID,@ForumGUID,@DiscTopic,@DiscDesc,@DiscContent,@DiscStatus,@DiscIsPinned,@DiscIsLocked,@DiscCreatedBy,@DiscCreatedDate, @DiscLastUpdatedBy, @DiscLastUpdatedDate)", con);

                InsertCommand.Parameters.AddWithValue("@DiscGUID", DiscGUID);
                InsertCommand.Parameters.AddWithValue("@ForumGUID", Guid.Parse(ddlCategory.SelectedItem.Value));
                InsertCommand.Parameters.AddWithValue("@DiscTopic", txtDiscTopic.Text);
                InsertCommand.Parameters.AddWithValue("@DiscDesc", txtDiscDesc.Text);
                InsertCommand.Parameters.AddWithValue("@DiscContent", txtDiscContent.Text);
                InsertCommand.Parameters.AddWithValue("@DiscStatus", "Active");
                InsertCommand.Parameters.AddWithValue("@DiscIsPinned", "No");
                InsertCommand.Parameters.AddWithValue("@DiscIsLocked", "No");
                InsertCommand.Parameters.AddWithValue("@DiscCreatedBy", Username);
                InsertCommand.Parameters.AddWithValue("@DiscCreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@DiscLastUpdatedBy", Username);
                InsertCommand.Parameters.AddWithValue("@DiscLastUpdatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                InsertCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }
    }
}