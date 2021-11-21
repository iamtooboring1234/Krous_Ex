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
    public partial class ProgrammeEntry : System.Web.UI.Page
    {
        Guid ProgrammeGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                //userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
                if (IsPostBack != true)
                {
                    if (Session["AddNewProgramme"] != null)
                    {
                        if (Session["AddNewProgramme"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showProgrammeAddSuccessToast(); ", true);
                            Session["AddNewProgramme"] = null;
                        }
                    }

                    if (Session["updateProgramme"] != null)
                    {
                        if (Session["updateProgramme"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showProgrammeUpdateSuccessToast(); ", true);
                            Session["updateProgramme"] = null;
                        }
                    }

                    loadFacultyCategory();
                    if (!String.IsNullOrEmpty(Request.QueryString["ProgrammeGUID"]))
                    {
                        ProgrammeGUID = Guid.Parse(Request.QueryString["ProgrammeGUID"]);
                        loadProgInfo();
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnBack.Visible = false;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("StaffLogin.aspx");
            }
        }

        //when select view on programme listing, will display the details at programme entry page 
        protected void loadProgInfo()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadInfoCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadInfoCmd = new SqlCommand("SELECT * FROM Programme WHERE ProgrammeGUID = @ProgrammeGUID", con);
                loadInfoCmd.Parameters.AddWithValue("@ProgrammeGUID", ProgrammeGUID);
                SqlDataReader dtrLoad = loadInfoCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                if (dt.Rows.Count != 0)
                {
                    txtProgName.Text = dt.Rows[0]["ProgrammeName"].ToString();
                    txtProgAbbrv.Text = dt.Rows[0]["ProgrammeAbbrv"].ToString();
                    txtProgDesc.Text = dt.Rows[0]["ProgrammeDesc"].ToString();
                    ddlProgCategory.SelectedValue = dt.Rows[0]["ProgrammeCategory"].ToString();
                    ddlProgDuration.SelectedValue = dt.Rows[0]["ProgrammeDuration"].ToString();
                    ddlFacultyInChg.SelectedValue = dt.Rows[0]["FacultyGUID"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadFacultyCategory()
        {
            try
            {
                ddlFacultyInChg.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT FacultyGUID, FacultyName FROM Faculty GROUP BY FacultyGUID, FacultyName ORDER BY FacultyName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlFacultyInChg.DataSource = ds;
                ddlFacultyInChg.DataTextField = "FacultyName";
                ddlFacultyInChg.DataValueField = "FacultyGUID";
                ddlFacultyInChg.DataBind();
                ddlFacultyInChg.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
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

                insertCmd = new SqlCommand("INSERT INTO Programme VALUES (@ProgrammeGUID, @ProgrammeAbbrv, @ProgrammeName, @ProgrammeDesc, @ProgrammeDuration, @ProgrammeCategory, @ProgrammeFullorPart, @FacultyGUID)", con);
                insertCmd.Parameters.AddWithValue("@ProgrammeGUID", progGUID);
                insertCmd.Parameters.AddWithValue("@ProgrammeAbbrv", txtProgAbbrv.Text.ToUpper());
                insertCmd.Parameters.AddWithValue("@ProgrammeName", txtProgName.Text);
                insertCmd.Parameters.AddWithValue("@ProgrammeDesc", txtProgDesc.Text);
                insertCmd.Parameters.AddWithValue("@ProgrammeDuration", ddlProgDuration.SelectedValue);
                insertCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);
                insertCmd.Parameters.AddWithValue("@ProgrammeFullorPart", "Full Time");
                insertCmd.Parameters.AddWithValue("@FacultyGUID", ddlFacultyInChg.SelectedValue);
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

        protected bool updateProgramme()
        {
            ProgrammeGUID = Guid.Parse(Request.QueryString["ProgrammeGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand updateCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                updateCmd = new SqlCommand("UPDATE Programme SET ProgrammeAbbrv = @ProgrammeAbbrv, ProgrammeName = @ProgrammeName, ProgrammeDesc = @ProgrammeDesc, ProgrammeDuration = @ProgrammeDuration, ProgrammeCategory = @ProgrammeCategory, FacultyGUID = @FacultyGUID WHERE ProgrammeGUID = @ProgrammeGUID", con);
                updateCmd.Parameters.AddWithValue("@ProgrammeGUID", ProgrammeGUID);
                updateCmd.Parameters.AddWithValue("@ProgrammeAbbrv", txtProgAbbrv.Text.ToUpper());
                updateCmd.Parameters.AddWithValue("@ProgrammeName", txtProgName.Text);
                updateCmd.Parameters.AddWithValue("@ProgrammeDesc", txtProgDesc.Text);
                updateCmd.Parameters.AddWithValue("@ProgrammeDuration", ddlProgDuration.SelectedValue);
                updateCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);
                updateCmd.Parameters.AddWithValue("@FacultyGUID", ddlFacultyInChg.SelectedValue);     
                updateCmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected bool deleteProgramme()
        {
            ProgrammeGUID = Guid.Parse(Request.QueryString["ProgrammeGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand deleteCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                deleteCmd = new SqlCommand("DELETE FROM Programme WHERE ProgrammeGUID = @ProgrammeGUID", con);
                deleteCmd.Parameters.AddWithValue("@ProgrammeGUID", ProgrammeGUID);
                deleteCmd.ExecuteNonQuery();

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
            if (validationEntry())
            {
                if (validateEmpty())
                {
                    if (addNewProgramme())
                    {
                        Session["AddNewProgramme"] = "Yes";
                        Response.Redirect("ProgrammeEntry");                     
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Unable to add new programme entry.");
                        txtProgName.Text = string.Empty;
                        txtProgAbbrv.Text = string.Empty;
                        txtProgDesc.Text = string.Empty;
                        ddlProgDuration.SelectedIndex = 0;
                        ddlProgCategory.SelectedIndex = 0;
                        ddlFacultyInChg.SelectedIndex = 0;
                        txtProgName.Focus();
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            if (validateEmpty())
            {
                if (updateProgramme())
                {
                    Session["updateProgramme"] = "Yes";
                    Response.Redirect("ProgrammeEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update programme details.");
                    loadProgInfo();
                }

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteProgramme())
            {
                Session["deleteProgramme"] = "Yes";
                Response.Redirect("ProgrammeListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "No such records to be deleted.");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProgrammeListings");
        }

        protected bool validateEmpty()
        {
            //programme name
            if (txtProgName.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the programme name.");
                return false;
            }

            //programme code
            if (txtProgAbbrv.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the programme abbreviation.");
                return false;
            }

            //programme code length
            if (txtProgAbbrv.Text.Length > 4)
            {
                clsFunction.DisplayAJAXMessage(this, "Programme abrreviation should within 4 characters.");
                return false;
            }

            //programme desc
            if (txtProgDesc.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the programme description.");
                return false;
            }

            //programe category
            if (ddlProgCategory.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the programme category.");
                return false;
            }

            //programme duration
            if (ddlProgDuration.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the programme duration.");
                return false;
            }

            //programme faculty 
            if (ddlFacultyInChg.SelectedIndex == 0)
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the faculty in-charge for this programme.");
                return false;
            }

            return true;
        }

        //validate duplicate name and abbrv
        protected bool validationEntry()
        {
            if (clsValidation.CheckDuplicateProgrammeName(txtProgName.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "The Programme name is already exists in the database!");
                txtProgName.Focus();
                return false;
            }

            if (clsValidation.CheckDuplicateProgrammeAbbrv(txtProgAbbrv.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "The Programme Abbreviation is already exists in the database!");
                txtProgAbbrv.Focus();
                return false;
            }
            return true;
        }

        
    }
}