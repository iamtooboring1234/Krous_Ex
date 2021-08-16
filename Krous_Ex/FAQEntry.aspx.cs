using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Left the login part take the staff name

namespace Krous_Ex
{
    public partial class FAQEntry : System.Web.UI.Page
    {

        private Guid FAQGUID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true) {

                loadFAQCategory();

                if (!String.IsNullOrEmpty(Request.QueryString["FAQGUID"]))
                {
                    FAQGUID = Guid.Parse(Request.QueryString["FAQGUID"]);
                    loadFAQ();
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                    btnBack.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    btnBack.Visible = false;
                }
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

        private void loadFAQ()
        {
            try 
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand getCommand = new SqlCommand("SELECT * FROM FAQ WHERE FAQGUID = @FAQGUID", con);
                getCommand.Parameters.AddWithValue("@FAQGUID", FAQGUID);
                SqlDataReader reader = getCommand.ExecuteReader();

                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0) {
                    txtFAQTitle.Text = dtFAQ.Rows[0]["FAQTitle"].ToString();
                    txtFAQDesc.Text = dtFAQ.Rows[0]["FAQDescription"].ToString();
                    ddlCategory.SelectedValue = dtFAQ.Rows[0]["FAQCategory"].ToString();
                    ddlFAQStatus.SelectedValue = dtFAQ.Rows[0]["FAQStatus"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, "Error");
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

        private bool updateFAQ()
        {
            FAQGUID = Guid.Parse(Request.QueryString["FAQGUID"]);

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

                SqlCommand updateCommand = new SqlCommand("UPDATE FAQ SET FAQTitle = @FAQTitle, FAQDescription = @FAQDescription, FAQCategory = @FAQCategory, FAQStatus = @FAQStatus, LastUpdatedBy = @LastUpdatedBy, LastUpdatedDate = @LastUpdatedDate WHERE FAQGUID = @FAQGUID;", con);

                updateCommand.Parameters.AddWithValue("@FAQGUID", FAQGUID);
                updateCommand.Parameters.AddWithValue("@FAQTitle", txtFAQTitle.Text);
                updateCommand.Parameters.AddWithValue("@FAQDescription", txtFAQDesc.Text);
                updateCommand.Parameters.AddWithValue("@FAQCategory", FAQCategory);
                updateCommand.Parameters.AddWithValue("@FAQStatus", ddlFAQStatus.SelectedValue);
                updateCommand.Parameters.AddWithValue("@LastUpdatedBy", "Admin");
                updateCommand.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                updateCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        private bool deleteFAQ()
        {
            FAQGUID = Guid.Parse(Request.QueryString["FAQGUID"]);

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

                SqlCommand deleteCommand = new SqlCommand("DELETE FROM FAQ WHERE FAQGUID = @FAQGUID;", con);

                deleteCommand.Parameters.AddWithValue("@FAQGUID", FAQGUID);

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validateFAQ())
            {
                if (insertFAQ())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "ShowPopup();", true);
                    Response.Redirect("FAQEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FAQListings");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["FAQGUID"]))
            {
                Response.Redirect("FAQEntry?FAQGUID=" + Request.QueryString["FAQGUID"]);
            } else
            {
                Response.Redirect("FAQEntry");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (validateFAQ())
            {
                if (updateFAQ())
                {
                    clsFunction.DisplayAJAXMessage(this, "FAQ updated.");
                    Response.Redirect("FAQListings");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update. Failed to update.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteFAQ())
            {
                clsFunction.DisplayAJAXMessage(this, "FAQ deleted.");
                Response.Redirect("FAQListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to delete. No such record.");
            }
        }

        private bool validateFAQ()
        {
            if (txtFAQTitle.Text == "")
            {
                return false;
            }

            if (rdNew.Checked == true)
            {
                if (txtNewCategory.Text == "")
                {
                    return false;
                }
            }

            if (rdExisting.Checked == true)
            {
                if (ddlCategory.SelectedValue == "")
                {
                    return false;
                }
            }

            if (txtFAQDesc.Text == "")
            {
                return false;
            }

            return true;
        }

        protected void rdExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (rdExisting.Checked == true)
            {
                txtNewCategory.Text = "";
                txtNewCategory.Enabled = false;
                ddlCategory.Enabled = true;
            }
        }

        protected void rdNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNew.Checked == true)
            {
                ddlCategory.SelectedValue = "";
                ddlCategory.Enabled = false;
                txtNewCategory.Enabled = true;
            }
        }



        protected void txtFAQTitle_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}