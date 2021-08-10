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
    public partial class FAQListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {

                loadFAQCategory();
                loadGV();

            }
        }

        private void loadGV()
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT * FROM FAQ ";
                sqlQuery += "WHERE CASE WHEN @FAQTitle = '' THEN @FAQTitle ELSE FAQTitle END LIKE '%'+@FAQTitle+'%' AND ";
                sqlQuery += "CASE WHEN @FAQCategory = '' then @FAQCategory ELSE FAQCategory END = @FAQCategory AND ";
                sqlQuery += "FAQStatus = @FAQStatus ";
                sqlQuery += "ORDER BY CreatedDate, FAQCategory";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@FAQTitle", txtFAQTitle.Text);
                GetCommand.Parameters.AddWithValue("@FAQCategory", ddlCategory.SelectedValue);
                GetCommand.Parameters.AddWithValue("@FAQStatus", ddlFAQStatus.SelectedValue);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0)
                {
                    gvFAQ.DataSource = dtFAQ;
                    gvFAQ.DataBind();
                    gvFAQ.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvFAQ.Visible = false;
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
                SqlCommand GetCommand = new SqlCommand("SELECT FAQCategory FROM FAQ GROUP BY FAQCategory ORDER BY FAQCategory", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                for (int i = 0; i <= dtFAQ.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtFAQ.Rows[i]["FAQCategory"].ToString();
                    oList.Value = dtFAQ.Rows[i]["FAQCategory"].ToString();
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

                sqlQuery = "SELECT * FROM FAQ ";
                sqlQuery += "WHERE CASE WHEN @FAQTitle = '' THEN @FAQTitle ELSE FAQTitle END LIKE '%'+@FAQTitle+'%' AND ";
                sqlQuery += "CASE WHEN @FAQCategory = '' then @FAQCategory ELSE FAQCategory END = @FAQCategory AND ";
                sqlQuery += "FAQStatus = @FAQStatus ";
                sqlQuery += "ORDER BY CreatedDate, FAQCategory";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@FAQTitle", txtFAQTitle.Text);
                GetCommand.Parameters.AddWithValue("@FAQCategory", ddlCategory.SelectedValue);
                GetCommand.Parameters.AddWithValue("@FAQStatus", ddlFAQStatus.SelectedValue);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0)
                {
                    gvFAQ.DataSource = dtFAQ;
                    gvFAQ.DataBind();
                    gvFAQ.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvFAQ.Visible = false;
                }
            } 
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}