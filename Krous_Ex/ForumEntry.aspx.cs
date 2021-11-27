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
        private string strMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["InsertForum"] != null)
                {
                    if (Session["InsertForum"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                        Session["InsertForum"] = null;
                    }
                }

                var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (myCookie != null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["ForumType"]))
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

                        loadForumCategory();

                        if (!String.IsNullOrEmpty(Request.QueryString["ForumGUID"]))
                        {
                            btnSave.Visible = false;
                            loadForum();
                        }
                        else
                        {
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                        }

                    }
                    else
                    {
                        Response.Redirect("ForumEntry?ForumType=Public");
                    }
                }
                else
                {
                    Response.Redirect("Homepage");
                }
            }
        }

        private void loadForumCategory()
        {
            try
            {
                ddlCategory.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlCategory.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ForumCategory FROM FORUM GROUP BY ForumCategory ORDER BY ForumCategory", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtForumCat = new DataTable();
                dtForumCat.Load(reader);
                con.Close();

                for (int i = 0; i <= dtForumCat.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtForumCat.Rows[i]["ForumCategory"].ToString();
                    oList.Value = dtForumCat.Rows[i]["ForumCategory"].ToString();
                    ddlCategory.Items.Add(oList);
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadForum()
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT * FROM Forum WHERE ForumGUID= @ForumGUID AND ForumType = @ForumType ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@ForumGUID", Request.QueryString["ForumGUID"]);
                GetCommand.Parameters.AddWithValue("@ForumType", Request.QueryString["ForumType"]);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0)
                {
                    txtForumTopic.Text = dtFAQ.Rows[0]["ForumTopic"].ToString();
                    txtForumDesc.Text = dtFAQ.Rows[0]["ForumDesc"].ToString();
                    if (ddlCategory.Items.FindByValue(dtFAQ.Rows[0]["ForumCategory"].ToString()).Value != null)
                    {
                        ddlCategory.SelectedValue = dtFAQ.Rows[0]["ForumCategory"].ToString().Trim();
                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "No such record.");
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

            if (rdExisting.Checked == true)
                ForumCategory = ddlCategory.SelectedValue;
            else
                ForumCategory = txtNewCategory.Text;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO FORUM VALUES(@ForumGUID,@ForumTopic,@ForumDesc,@ForumCategory,@ForumStatus,@ForumType,@ForumCreatedBy,@ForumCreatedDate,@ForumLastUpdatedBy,@ForumLastUpdatedDate)", con);

                InsertCommand.Parameters.AddWithValue("@ForumGUID", ForumGUID);
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

        private bool updateForum()
        {
            Guid ForumGUID = Guid.Parse(Request.QueryString["ForumGUID"]);

            string ForumCategory;

            string Username = clsLogin.GetLoginUserName();

            if (rdExisting.Checked == true)
                ForumCategory = ddlCategory.SelectedValue;
            else
                ForumCategory = txtNewCategory.Text;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand updateCommand = new SqlCommand("UPDATE Forum SET ForumTopic = @ForumTopic, ForumDesc = @ForumDesc, ForumCategory = @ForumCategory, ForumStatus = @ForumStatus, ForumLastUpdatedBy = @ForumLastUpdatedBy, ForumLastUpdatedDate = @ForumLastUpdatedDate WHERE ForumGUID = @ForumGUID;", con);

                updateCommand.Parameters.AddWithValue("@ForumGUID", ForumGUID);
                updateCommand.Parameters.AddWithValue("@ForumTopic", txtForumTopic.Text);
                updateCommand.Parameters.AddWithValue("@ForumDesc", txtForumDesc.Text);
                updateCommand.Parameters.AddWithValue("@ForumCategory", ForumCategory);
                updateCommand.Parameters.AddWithValue("@ForumStatus", ddlForumStatus.SelectedValue);
                updateCommand.Parameters.AddWithValue("@ForumLastUpdatedBy", Username);
                updateCommand.Parameters.AddWithValue("@ForumLastUpdatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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

        private bool deleteForum()
        {
            Guid ForumGUID = Guid.Parse(Request.QueryString["ForumGUID"]);

            string Username = clsLogin.GetLoginUserName();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand updateCommand = new SqlCommand("UPDATE Forum SET ForumStatus = @ForumStatus, ForumLastUpdatedBy = @ForumLastUpdatedBy, ForumLastUpdatedDate = @ForumLastUpdatedDate WHERE ForumGUID = @ForumGUID;", con);

                updateCommand.Parameters.AddWithValue("@ForumGUID", ForumGUID);
                updateCommand.Parameters.AddWithValue("@ForumStatus", "Inactive");
                updateCommand.Parameters.AddWithValue("@ForumLastUpdatedBy", Username);
                updateCommand.Parameters.AddWithValue("@ForumLastUpdatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (isEmptyField())
            {
                if (insertForum())
                {
                    Session["InsertForum"] = "Yes";
                    Response.Redirect("ForumEntry?ForumType=Public");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
                }
            } else
            {
                clsFunction.DisplayAJAXMessage(this, strMessage);
            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isEmptyField())
            {
                if (updateForum())
                {
                    Session["UpdateForum"] = "Yes";
                    Response.Redirect("ForumListings");
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
            if (deleteForum())
            {
                Session["DeleteForum"] = "Yes";
                Response.Redirect("ForumListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to delete. Failed to delete.");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForumListings");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ForumGUID"]))
            {
                loadForum();
            }
            else
            {
                Response.Redirect("ForumEntry");
            }
        }

        protected void rdExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (rdExisting.Checked == true)
            {
                loadForum();
                txtNewCategory.Text = "";
                txtNewCategory.Enabled = false;
                ddlCategory.Enabled = true;
            }
        }

        protected void rdNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNew.Checked == true)
            {
                ddlCategory.SelectedValue = null;
                ddlCategory.Enabled = false;
                txtNewCategory.Enabled = true;
            }
        }

        private bool isEmptyField()
        {
            if (string.IsNullOrEmpty(txtForumTopic.Text))
            {
                strMessage += "- Forum topic \\n";
            }

            if (string.IsNullOrEmpty(txtForumDesc.Text))
            {
                strMessage += "- Forum description \\n";
            }

            if (rdNew.Checked)
            {
                if (txtNewCategory.Text == "")
                {
                    strMessage += "- Category \\n";
                }
            }

            if (rdExisting.Checked)
            {
                if (ddlCategory.Text == "")
                {
                    strMessage += "- Cateogry \\n";
                }
            }

            if (ddlForumStatus.Text == "")
            {
                strMessage += "- Forum status \\n";
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