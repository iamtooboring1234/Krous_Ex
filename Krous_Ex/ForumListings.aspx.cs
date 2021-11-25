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
    public partial class ForumListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["UpdateForum"] != null)
                {
                    if (Session["UpdateForum"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUpdateSuccessToast(); ", true);
                        Session["UpdateForum"] = null;
                    }
                }

                if (Session["DeleteForum"] != null)
                {
                    if (Session["DeleteForum"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showDeleteSuccessToast(); ", true);
                        Session["DeleteForum"] = null;
                    }
                }

                loadFAQCategory();
                loadGV();

            }
        }

        private void loadGV()
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT * FROM Forum ";
                sqlQuery += "WHERE CASE WHEN @ForumTopic = '' THEN @ForumTopic ELSE ForumTopic END LIKE '%'+@ForumTopic+'%' AND ";
                sqlQuery += "CASE WHEN @ForumCategory = '' then @ForumCategory ELSE ForumCategory END = @ForumCategory AND ";
                sqlQuery += "ForumStatus = @ForumStatus AND ForumType = @ForumType ";
                sqlQuery += "ORDER BY ForumCreatedDate, ForumCategory";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@ForumTopic", txtForumTopic.Text);
                GetCommand.Parameters.AddWithValue("@ForumCategory", ddlCategory.SelectedValue);
                GetCommand.Parameters.AddWithValue("@ForumStatus", "Active");
                GetCommand.Parameters.AddWithValue("@ForumType", "Public");
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtForum = new DataTable();
                dtForum.Load(reader);
                con.Close();

                if (dtForum.Rows.Count != 0)
                {
                    gvForumMng.DataSource = dtForum;
                    gvForumMng.DataBind();
                    gvForumMng.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvForumMng.Visible = false;
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadFAQCategory()
        {
            try
            {
                ddlCategory.Items.Clear();

                ListItem oList = new ListItem();

                if (Request.QueryString["FAQGUID"] == null)
                {
                    oList = new ListItem();
                    oList.Text = "";
                    oList.Value = "";
                    ddlCategory.Items.Add(oList);
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ForumCategory FROM Forum GROUP BY ForumCategory ORDER BY ForumCategory", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                for (int i = 0; i <= dtFAQ.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtFAQ.Rows[i]["ForumCategory"].ToString();
                    oList.Value = dtFAQ.Rows[i]["ForumCategory"].ToString();
                    ddlCategory.Items.Add(oList);
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, "Error");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("FAQEntry");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery;

                if (ddlForumStatus.SelectedValue != "All")
                {
                    sqlQuery = "SELECT * FROM Forum ";
                    sqlQuery += "WHERE CASE WHEN @ForumTopic = '' THEN @ForumTopic ELSE ForumTopic END LIKE '%'+@ForumTopic+'%' AND ";
                    sqlQuery += "CASE WHEN @ForumCategory = '' then @ForumCategory ELSE ForumCategory END = @ForumCategory AND ";
                    sqlQuery += "ForumStatus = @ForumStatus AND ForumType = @ForumType ";
                    sqlQuery += "ORDER BY ForumCreatedDate, ForumCategory";
                }
                else
                {
                    sqlQuery = "SELECT * FROM Forum ";
                    sqlQuery += "WHERE CASE WHEN @ForumTopic = '' THEN @ForumTopic ELSE ForumTopic END LIKE '%'+@ForumTopic+'%' AND ";
                    sqlQuery += "CASE WHEN @ForumCategory = '' then @ForumCategory ELSE ForumCategory END = @ForumCategory ";
                    sqlQuery += "ORDER BY ForumCreatedDate, ForumCategory";
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@ForumTopic", txtForumTopic.Text);
                GetCommand.Parameters.AddWithValue("@ForumCategory", ddlCategory.SelectedValue);
                GetCommand.Parameters.AddWithValue("@ForumStatus", ddlForumStatus.SelectedValue);
                GetCommand.Parameters.AddWithValue("@ForumType", "Public");
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtForum = new DataTable();
                dtForum.Load(reader);
                con.Close();

                if (dtForum.Rows.Count != 0)
                {
                    gvForumMng.DataSource = dtForum;
                    gvForumMng.DataBind();
                    gvForumMng.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvForumMng.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}