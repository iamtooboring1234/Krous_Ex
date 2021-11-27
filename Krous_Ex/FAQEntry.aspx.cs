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
    public partial class FAQEntry1 : System.Web.UI.Page
    {
        
        private Guid FAQGUID;
        private string strMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {

                if (IsPostBack != true)
                {

                    if (Session["InsertFAQ"] != null)
                    {
                        if (Session["InsertFAQ"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                            Session["InsertFAQ"] = null;
                        }
                    }

                    if (Session["UpdateFAQ"] != null)
                    {
                        if (Session["UpdateFAQ"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUpdateSuccessToast(); ", true);
                            Session["UpdateFAQ"] = null;
                        }
                    }

                    if (Session["DeleteFAQ"] != null)
                    {
                        if (Session["DeleteFAQ"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showDeleteSuccessToast(); ", true);
                            Session["DeleteFAQ"] = null;
                        }
                    }

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
            else
            {
                Response.Redirect("Homepage");
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
                clsFunction.DisplayAJAXMessage(this, ex.Message);
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

                if (dtFAQ.Rows.Count != 0)
                {
                    txtFAQTitle.Text = dtFAQ.Rows[0]["FAQTitle"].ToString();
                    txtFAQDesc.Text = dtFAQ.Rows[0]["FAQDescription"].ToString();
                    ddlCategory.SelectedValue = dtFAQ.Rows[0]["FAQCategory"].ToString();
                    ddlFAQStatus.SelectedValue = dtFAQ.Rows[0]["FAQStatus"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private bool insertFAQ()
        {
            Guid FAQGUID = Guid.NewGuid();

            string FAQCategory;

            string Username = clsLogin.GetLoginUserName();

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
                InsertCommand.Parameters.AddWithValue("@createdBy", Username);
                InsertCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@LastUpdatedBy", Username);
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

            string Username = clsLogin.GetLoginUserName();

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
                updateCommand.Parameters.AddWithValue("@LastUpdatedBy", Username);
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
            if (isEmptyField())
            {
                if (insertFAQ())
                {
                    Session["InsertFAQ"] = "Yes";
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["FAQGUID"]))
            {
                Response.Redirect("FAQEntry?FAQGUID=" + Request.QueryString["FAQGUID"]);
            }
            else
            {
                Response.Redirect("FAQEntry");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isEmptyField())
            {
                if (updateFAQ())
                {
                    Session["UpdateFAQ"] = "Yes";
                    Response.Redirect("FAQListings");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update. Failed to update.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, strMessage);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteFAQ())
            {
                Session["DeleteFAQ"] = "Yes";
                Response.Redirect("FAQListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to delete. No such record.");
            }
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

        private bool isEmptyField()
        {
            if (string.IsNullOrEmpty(txtFAQTitle.Text))
            {
                strMessage += "- Semester name \\n";
            }

            if (rdExisting.Checked)
            {
                if (string.IsNullOrEmpty(ddlCategory.SelectedValue))
                {
                    strMessage += "- Semester study duration \\n";
                }
            }

            if (rdNew.Checked)
            {
                if (string.IsNullOrEmpty(txtNewCategory.Text))
                {
                    strMessage += "- Semester examination duration \\n";
                }
            }

            if (string.IsNullOrEmpty(ddlFAQStatus.SelectedValue))
            {
                strMessage += "- Semester break duration \\n";
            }

            if (string.IsNullOrEmpty(txtFAQDesc.Text))
            {
                strMessage += "- Semester break duration \\n";
            }

            if (!string.IsNullOrEmpty(strMessage))
            {
                string tempMessage = "Please complete all the required field as below : \\n" + strMessage;
                strMessage = tempMessage;
                return false;
            }

            return true;
        }
    }
}