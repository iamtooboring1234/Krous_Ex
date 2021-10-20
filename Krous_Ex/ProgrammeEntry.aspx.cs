using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class ProgrammeEntry : System.Web.UI.Page
    {
        Guid userGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
                if (!(IsPostBack))
                {
                    if (userGUID != null)
                    {
                        //loadData();
                    }
                }
            }
            else
            {
                Response.Redirect("StudentLogin.aspx");
            }

            rblFullorPart.Visible = false;
            if(ddlProgCategory.SelectedItem.Value == "Master")
            {
                rblFullorPart.Visible = true;
            }
        }

        protected bool addNewProgramme()
        {
            Guid progGUID = Guid.NewGuid();

            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand insertCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                insertCmd = new SqlCommand("INSERT INTO Programme VALUES (@ProgrammeGUID, @ProgrammeAbbrv, @ProgrammeName, @ProgrammeDesc, @ProgrammeDuration, @ProgrammeCategory)", con);
                insertCmd.Parameters.AddWithValue("@ProgrammeGUID", progGUID);
                insertCmd.Parameters.AddWithValue("@ProgrammeAbbrv", txtProgAbbrv.Text);
                insertCmd.Parameters.AddWithValue("@ProgrammeName", txtProgName.Text);
                insertCmd.Parameters.AddWithValue("@ProgrammeDesc", txtProgDesc.Text);
                insertCmd.Parameters.AddWithValue("@ProgrammeDuration", ddlProgDuration.SelectedValue);
                insertCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);
                insertCmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (addNewProgramme())
            {
                clsFunction.DisplayAJAXMessage(this, "Added new programme successfully!");
                txtProgName.Text = string.Empty;
                txtProgAbbrv.Text = string.Empty;
                txtProgDesc.Text = string.Empty;
                ddlProgDuration.SelectedIndex = 0;
                ddlProgCategory.SelectedIndex = 0;
                rblFullorPart.Visible = false;
                txtProgName.Focus();
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to add new programme entry.");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //reset all fields
            txtProgName.Text = string.Empty;
            txtProgAbbrv.Text = string.Empty;
            txtProgDesc.Text = string.Empty;
            ddlProgDuration.SelectedIndex = 0;
            ddlProgCategory.SelectedIndex = 0;
            rblFullorPart.Visible = false;
            txtProgName.Focus();
        }
    }
}