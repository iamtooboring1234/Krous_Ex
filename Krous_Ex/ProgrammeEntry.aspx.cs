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
                    if(dt.Rows[0]["ProgrammeCategory"].Equals("Master") || dt.Rows[0]["ProgrammeCategory"].Equals("Doctor of Philosophy"))
                    {
                        rblFullorPart.Visible = true;
                        txtProgName.Text = dt.Rows[0]["ProgrammeName"].ToString();
                        txtProgAbbrv.Text = dt.Rows[0]["ProgrammeAbbrv"].ToString();
                        txtProgDesc.Text = dt.Rows[0]["ProgrammeDesc"].ToString();
                        ddlProgCategory.SelectedValue = dt.Rows[0]["ProgrammeCategory"].ToString();
                        ddlProgDuration.SelectedValue = dt.Rows[0]["ProgrammeDuration"].ToString();
                        rblFullorPart.SelectedValue = dt.Rows[0]["ProgrammeFullorPart"].ToString();
                        ddlFacultyInChg.SelectedValue = dt.Rows[0]["ProgrammeFaculty"].ToString();
                    }
                    else
                    {
                        txtProgName.Text = dt.Rows[0]["ProgrammeName"].ToString();
                        txtProgAbbrv.Text = dt.Rows[0]["ProgrammeAbbrv"].ToString();
                        txtProgDesc.Text = dt.Rows[0]["ProgrammeDesc"].ToString();
                        ddlProgCategory.SelectedValue = dt.Rows[0]["ProgrammeCategory"].ToString();
                        ddlProgDuration.SelectedValue = dt.Rows[0]["ProgrammeDuration"].ToString();
                        rblFullorPart.SelectedValue = dt.Rows[0]["ProgrammeFullorPart"].ToString();
                        ddlFacultyInChg.SelectedValue = dt.Rows[0]["ProgrammeFaculty"].ToString();
                    }
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
                ListItem facultyList = new ListItem();
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
                ddlFacultyInChg.Items.Insert(0, new ListItem("", "0"));
                con.Close();

                //SqlDataReader dtrLoad = loadCmd.ExecuteReader();
                //DataTable dtLoad = new DataTable();
                //dtLoad.Load(dtrLoad);
                //con.Close();

                //for (int i = 0; i <= dtLoad.Rows.Count - 1; i++)
                //{
                //    facultyList = new ListItem();
                //    facultyList.Text = dtLoad.Rows[i]["FacultyName"].ToString();
                //    facultyList.Value = dtLoad.Rows[i]["FacultyName"].ToString();
                //    ddlFacultyIng.Items.Add(facultyList);
                //}

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

                if (ddlProgCategory.SelectedValue == "Foundation" || ddlProgCategory.SelectedValue == "Diploma" || ddlProgCategory.SelectedValue == "Bachelor Degree")
                {
                    insertCmd = new SqlCommand("INSERT INTO Programme VALUES (@ProgrammeGUID, @ProgrammeAbbrv, @ProgrammeName, @ProgrammeDesc, @ProgrammeDuration, @ProgrammeCategory, @ProgrammeFullorPart, @ProgrammeFaculty)", con);
                    insertCmd.Parameters.AddWithValue("@ProgrammeGUID", progGUID);
                    insertCmd.Parameters.AddWithValue("@ProgrammeAbbrv", txtProgAbbrv.Text.ToUpper());
                    insertCmd.Parameters.AddWithValue("@ProgrammeName", txtProgName.Text);
                    insertCmd.Parameters.AddWithValue("@ProgrammeDesc", txtProgDesc.Text);
                    insertCmd.Parameters.AddWithValue("@ProgrammeDuration", ddlProgDuration.SelectedValue);
                    insertCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);
                    insertCmd.Parameters.AddWithValue("@ProgrammeFullorPart", "Full Time");
                    insertCmd.Parameters.AddWithValue("@ProgrammeFaculty", ddlFacultyInChg.SelectedValue);
                    insertCmd.ExecuteNonQuery();
                }
                else
                {
                    insertCmd = new SqlCommand("INSERT INTO Programme VALUES (@ProgrammeGUID, @ProgrammeAbbrv, @ProgrammeName, @ProgrammeDesc, @ProgrammeDuration, @ProgrammeCategory, @ProgrammeFullorPart, @ProgrammeFaculty)", con);
                    insertCmd.Parameters.AddWithValue("@ProgrammeGUID", progGUID);
                    insertCmd.Parameters.AddWithValue("@ProgrammeAbbrv", txtProgAbbrv.Text.ToUpper());
                    insertCmd.Parameters.AddWithValue("@ProgrammeName", txtProgName.Text);
                    insertCmd.Parameters.AddWithValue("@ProgrammeDesc", txtProgDesc.Text);
                    insertCmd.Parameters.AddWithValue("@ProgrammeDuration", ddlProgDuration.SelectedValue);
                    insertCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);
                    insertCmd.Parameters.AddWithValue("@ProgrammeFullorPart", rblFullorPart.SelectedValue);
                    insertCmd.Parameters.AddWithValue("@ProgrammeFaculty", ddlFacultyInChg.SelectedValue);
                    insertCmd.ExecuteNonQuery();
                }
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

                updateCmd = new SqlCommand("UPDATE Programme SET ProgrammeAbbrv = @ProgrammeAbbrv, ProgrammeName = @ProgrammeName, ProgrammeDesc = @ProgrammeDesc, ProgrammeDuration = @ProgrammeDuration, ProgrammeCategory = @ProgrammeCategory, ProgrammeFullorPart = @ProgrammeFullorPart, ProgrammeFaculty = @ProgrammeFaculty WHERE ProgrammeGUID = @ProgrammeGUID", con);
                updateCmd.Parameters.AddWithValue("@ProgrammeGUID", ProgrammeGUID);
                updateCmd.Parameters.AddWithValue("@ProgrammeAbbrv", txtProgAbbrv.Text.ToUpper());
                updateCmd.Parameters.AddWithValue("@ProgrammeName", txtProgName.Text);
                updateCmd.Parameters.AddWithValue("@ProgrammeDesc", txtProgDesc.Text);
                updateCmd.Parameters.AddWithValue("@ProgrammeDuration", ddlProgDuration.SelectedValue);
                updateCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);
                updateCmd.Parameters.AddWithValue("@ProgrammeFullorPart", rblFullorPart.SelectedValue);
                updateCmd.Parameters.AddWithValue("@ProgrammeFaculty", ddlFacultyInChg.SelectedValue);     
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
                        clsFunction.DisplayAJAXMessage(this, "Added new programme successfully!");
                        txtProgName.Text = string.Empty;
                        txtProgAbbrv.Text = string.Empty;
                        txtProgDesc.Text = string.Empty;
                        ddlProgDuration.SelectedIndex = 0;
                        ddlProgCategory.SelectedIndex = 0;
                        rblFullorPart.ClearSelection();
                        rblFullorPart.Visible = false;
                        ddlFacultyInChg.SelectedIndex = 0;
                        txtProgName.Focus();
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Unable to add new programme entry.");
                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ProgrammeGUID"]))
            {
                Response.Redirect("ProgrammeEntry?ProgrammeGUID=" + Request.QueryString["ProgrammeGUID"]);
            }
            else
            {
                Response.Redirect("ProgrammeEntry");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProgrammeListings");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (validateEmpty())
            {
                if (updateProgramme())
                {
                    clsFunction.DisplayAJAXMessage(this, "Programme details has been updated!");
                    Response.Redirect("ProgrammeListings");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update programme details.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteProgramme())
            {
                clsFunction.DisplayAJAXMessage(this, "Programme details has been deleted!");
                Response.Redirect("ProgrammeListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "No such records to be deleted.");
            }
        }

        protected void ddlProgCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgCategory.SelectedValue == "Master" || ddlProgCategory.SelectedValue == "Doctor of Philosophy")
            {
                rblFullorPart.Visible = true;
            }
            else
            {
                rblFullorPart.Visible = false;
            }
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

            //if master and doctor, if radio button is not selected then error
            if (ddlProgCategory.SelectedValue == "Master" || ddlProgCategory.SelectedValue == "Doctor of Philosophy")
            {
                if (rblFullorPart.SelectedValue == "")
                {
                    clsFunction.DisplayAJAXMessage(this, "Please select Full time or Part time duration for Master and Doctor of Philosophy Category.");
                    return false;
                }
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

        //master and doctor
        protected bool validationEntry()
        {
            if(ddlProgCategory.SelectedValue == "Foundation" || ddlProgCategory.SelectedValue == "Diploma" || ddlProgCategory.SelectedValue == "Bachelor Degree")
            {
                if (clsValidation.CheckDuplicateProgrammeName(txtProgName.Text))
                {
                    clsFunction.DisplayAJAXMessage(this, "The Programme name is already exists in the database!");
                    return false;
                }

                if (clsValidation.CheckDuplicateProgrammeAbbrv(txtProgAbbrv.Text))
                {
                    clsFunction.DisplayAJAXMessage(this, "The Programme Abbreviation is already exists in the database!");
                    return false;
                }
            }
            else 
            {
                if (clsValidation.CheckDuplicateMasterOrDoctor(ddlProgCategory.SelectedValue, txtProgName.Text))
                {
                    if (clsValidation.CheckDuplicateProgrammeName(txtProgName.Text))
                    {
                        if (clsValidation.CheckDuplicateProgrammeAbbrv(txtProgAbbrv.Text))
                        {
                            clsFunction.DisplayAJAXMessage(this, "You can only insert 2 similar programme name for master and doctor category.");
                            return false;
                        }
                        else
                        {
                            clsFunction.DisplayAJAXMessage(this, "The Programme Abbreviation is already exists in the database!");
                            return false;
                        }
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "The Programme name is already exists in the database!");
                        return false;
                    }
                }
            }

            //if (Master || philosophy) { //fang zhe bian li  mian }
            //    else { //degree diploma foundation de validation fang zhe bian lo, ni jiu bu yong quan bu check guo }

            //只可以有两个一样name 和 code for master and doctor category
            //if other then master and category name and code dou yao check duplicate validation
            return true;
        }

        
    }
}