using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class ForumEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ForumType"]))
                {
                    if (Request.QueryString["ForumType"] == "Public")
                    {
                        HyperLink lnkButton = (HyperLink)Master.FindControl("HyperLink1");
                        lnkButton.CssClass = lnkButton.CssClass.Replace("nav-link", "").Trim();
                        lnkButton.Attributes.Add("style", "background: transparent;" +
                            "color: white;" +
                            "padding: 0.5rem 0.35rem;" +
                            "font-size: 0.855rem;" +
                            "position: relative;" +
                            "line-height: 1;" +
                            "height: auto;" +
                            "border-top: 0;" +
                            "display: -webkit-flex;" +
                            "display: flex;" +
                            "-webkit-align-items: center;" +
                            "align-items: center;" +
                            "white-space: nowrap;" +
                            "-webkit-transition-duration: 0.45s;" +
                            "-moz-transition-duration: 0.45s;" +
                            "-o-transition-duration: 0.45s;" +
                            "transition-property: color;" +
                            "-webkit-transition-property: color;" +
                            "border-radius: 0px 100px 100px 0px;");
                        HyperLink lnkButton2 = (HyperLink)Master.FindControl("HyperLink2");
                        lnkButton2.CssClass = lnkButton2.CssClass.Replace("nav-link", "").Trim();
                        lnkButton2.Attributes.Add("style", "background: transparent;" +
                            "color: #6c7293;" +
                            "padding: 0.5rem 0.35rem;" +
                            "font-size: 0.855rem;" +
                            "position: relative;" +
                            "line-height: 1;" +
                            "height: auto;" +
                            "border-top: 0;" +
                            "display: -webkit-flex;" +
                            "display: flex;" +
                            "-webkit-align-items: center;" +
                            "align-items: center;" +
                            "white-space: nowrap;" +
                            "-webkit-transition-duration: 0.45s;" +
                            "-moz-transition-duration: 0.45s;" +
                            "-o-transition-duration: 0.45s;" +
                            "transition-property: color;" +
                            "-webkit-transition-property: color;" +
                            "border-radius: 0px 100px 100px 0px;");
                    }
                    else
                    {
                        HyperLink lnkButton = (HyperLink)Master.FindControl("HyperLink1");
                        lnkButton.CssClass = lnkButton.CssClass.Replace("nav-link", "").Trim();
                        lnkButton.Attributes.Add("style", "background: transparent;" +
                            "color: #6c7293;" +
                            "padding: 0.5rem 0.35rem;" +
                            "font-size: 0.855rem;" +
                            "position: relative;" +
                            "line-height: 1;" +
                            "height: auto;" +
                            "border-top: 0;" +
                            "display: -webkit-flex;" +
                            "display: flex;" +
                            "-webkit-align-items: center;" +
                            "align-items: center;" +
                            "white-space: nowrap;" +
                            "-webkit-transition-duration: 0.45s;" +
                            "-moz-transition-duration: 0.45s;" +
                            "-o-transition-duration: 0.45s;" +
                            "transition-property: color;" +
                            "-webkit-transition-property: color;" +
                            "border-radius: 0px 100px 100px 0px;");
                        HyperLink lnkButton2 = (HyperLink)Master.FindControl("HyperLink2");
                        lnkButton2.CssClass = lnkButton2.CssClass.Replace("nav-link", "").Trim();
                        lnkButton2.Attributes.Add("style", "background: transparent;" +
                            "color: white;" +
                            "padding: 0.5rem 0.35rem;" +
                            "font-size: 0.855rem;" +
                            "position: relative;" +
                            "line-height: 1;" +
                            "height: auto;" +
                            "border-top: 0;" +
                            "display: -webkit-flex;" +
                            "display: flex;" +
                            "-webkit-align-items: center;" +
                            "align-items: center;" +
                            "white-space: nowrap;" +
                            "-webkit-transition-duration: 0.45s;" +
                            "-moz-transition-duration: 0.45s;" +
                            "-o-transition-duration: 0.45s;" +
                            "transition-property: color;" +
                            "-webkit-transition-property: color;" +
                            "border-radius: 0px 100px 100px 0px;");
                    }

                    if (IsPostBack != true)
                    {

                        loadForumCategory();

                        if (!String.IsNullOrEmpty(Request.QueryString["FAQGUID"]))
                        {

                        }
                        else
                        {

                        }
                    }
                } else
                {
                    Response.Redirect("ForumEntry?ForumType=Public");
                }
            }
            else
            {
                Response.Redirect("Homepage");
            }
        }

        private void loadForumCategory()
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
                SqlCommand GetCommand = new SqlCommand("SELECT ForumCategory FROM FORUM GROUP BY ForumCategory ORDER BY ForumCategory", con);
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
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }


        private bool insertForum()
        {
            Guid ForumGUID = Guid.NewGuid();

            string ForumCategory;

            string Username = clsLogin.GetLoginUserName();
            String staffGUID = clsLogin.GetLoginUserGUID();

            if (rdExisting.Checked == true)
                ForumCategory = ddlCategory.SelectedValue;
            else
                ForumCategory = txtNewCategory.Text;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO FORUM VALUES(@ForumGUID,@StaffGUID,@ForumTopic,@ForumDesc,@ForumCategory,@ForumStatus,@ForumType,@ForumCreatedBy,@ForumCreatedDate,@ForumLastUpdatedBy,@ForumLastUpdatedDate)", con);

                InsertCommand.Parameters.AddWithValue("@ForumGUID", ForumGUID);
                InsertCommand.Parameters.AddWithValue("@StaffGUID", staffGUID);
                InsertCommand.Parameters.AddWithValue("@ForumTopic", txtForumTopic.Text);
                InsertCommand.Parameters.AddWithValue("@ForumDesc", txtForumDesc.Text);
                InsertCommand.Parameters.AddWithValue("@ForumCategory", ForumCategory);
                InsertCommand.Parameters.AddWithValue("@ForumStatus", ddlForumStatus.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@ForumType", Request.QueryString["ForumType"].ToString());
                InsertCommand.Parameters.AddWithValue("@ForumCreatedBy", Username);
                InsertCommand.Parameters.AddWithValue("@ForumCreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@ForumLastUpdatedBy", Username);
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
                Response.Redirect("ForumEntry");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
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