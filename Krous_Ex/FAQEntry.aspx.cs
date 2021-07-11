using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class FAQEntry : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

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
                //clsFunction.DisplayAJAXMessage(this, "Error");
                Response.Write(ex);
            }
        }

        private bool insertFAQ()
        {
            Guid FAQGUID = Guid.NewGuid();

            string FAQCategory;

            //string Username = clsLogin.GetLoginUserName;

            if (rdExisting.Checked == true)
                FAQCategory = ddlCategory.SelectedValue;
            else
                FAQCategory = txtNewCategory.Text;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO FAQ VALUES(@FAQGUID,@FAQTitle,@FAQDescription,@FAQCategory,@FAQStatus,@createdBy,@CreatedDate,@LastUpdatedBy,@LastUpdatedDate)", con);

                InsertCommand.Parameters.AddWithValue("@FAQGUID", FAQGUID);
                InsertCommand.Parameters.AddWithValue("@FAQTitle", txtFAQTitle.Text);
                InsertCommand.Parameters.AddWithValue("@FAQDescription", txtFAQDesc.Text);
                InsertCommand.Parameters.AddWithValue("@FAQCategory", FAQCategory);
                InsertCommand.Parameters.AddWithValue("@FAQStatus", ddlFAQStatus.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@createdBy", "Admin");
                InsertCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@LastUpdatedBy", "Admin");
                InsertCommand.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (insertFAQ()) {
                Response.Write("Succesfully");
            } else
            {
                Response.Write("Unsuccesfully");
            }
        }
    }
}