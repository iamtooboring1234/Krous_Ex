using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class ForumEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool insertForum()
        {
            Guid ForumGUID = Guid.NewGuid();

            string ForumCategory;

            //string Username = clsLogin.GetLoginUserName;

            if (rdExisting.Checked == true)
                ForumCategory = ddlCategory.SelectedValue;
            else
                ForumCategory = txtNewCategory.Text;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO FORUM VALUES(@ForumGUID,@StaffGUID,@ForumTopic,@ForumDesc,@ForumCategory,@ForumStatus,@ForumCreatedDate,@ForumCreatedBy,@ForumLastUpdatedDate,@ForumLastUpdatedBy)", con);

                InsertCommand.Parameters.AddWithValue("@ForumGUID", ForumGUID);
                InsertCommand.Parameters.AddWithValue("@ForumTopic", txrForumTopic.Text);
                InsertCommand.Parameters.AddWithValue("@ForumDesc", txtForumDesc.Text);
                InsertCommand.Parameters.AddWithValue("@ForumCategory", ForumCategory);
                InsertCommand.Parameters.AddWithValue("@ForumStatus", ddlForumStatus.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@ForumCreatedBy", "Admin");
                InsertCommand.Parameters.AddWithValue("@ForumCreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@ForumLastUpdatedBy", "Admin");
                InsertCommand.Parameters.AddWithValue("@ForumLastUpdatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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
            if (insertForum())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "ShowPopup();", true);
                Response.Redirect("FAQEntry");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
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
    }
}