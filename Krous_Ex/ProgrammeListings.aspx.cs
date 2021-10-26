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
    public partial class ProgrammeListings : System.Web.UI.Page
    {
        Guid userGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (IsPostBack != true)
            {
                loadFacultyCategory();
                loadProgAbbrv();
                loadProgGV();
            }
        }

        //load faculty 
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
                DataSet dsName = new DataSet();
                da.Fill(dsName);
                ddlFacultyInChg.DataSource = dsName;
                ddlFacultyInChg.DataTextField = "FacultyName";
                ddlFacultyInChg.DataValueField = "FacultyGUID";
                ddlFacultyInChg.DataBind();
                ddlFacultyInChg.Items.Insert(0, new ListItem("", "")); 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        //load programme abbrv
        protected void loadProgAbbrv()
        {
            try
            {
                ddlProgCode.Items.Clear();
                ListItem progCodeList = new ListItem();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT ProgrammeGUID, ProgrammeAbbrv FROM Programme GROUP BY ProgrammeGUID, ProgrammeAbbrv ORDER BY ProgrammeAbbrv", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet dsAbbrv = new DataSet();
                da.Fill(dsAbbrv);
                ddlProgCode.DataSource = dsAbbrv;
                ddlProgCode.DataTextField = "ProgrammeAbbrv";
                ddlProgCode.DataValueField = "ProgrammeAbbrv";
                ddlProgCode.DataBind();
                ddlProgCode.Items.Insert(0, new ListItem("", "")); //the first drop down value will be empty
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        //load gridview
        protected void loadProgGV()
        {
            try
            {
                string loadQuery;
                loadQuery = "SELECT p.ProgrammeGUID, p.ProgrammeAbbrv, p.ProgrammeName, p.ProgrammeFullorPart, p.ProgrammeCategory, f.FacultyName FROM Programme p, Faculty f WHERE p.FacultyGUID = f.FacultyGUID AND";
                loadQuery += "CASE WHEN @ProgrammeAbbrv = '' THEN @ProgrammeAbbrv ELSE p.ProgrammeAbbrv END = @ProgrammeAbbrv AND "; //ddl
                loadQuery += "CASE WHEN @ProgrammeName = '' THEN @ProgrammeName ELSE p.ProgrammeName END LIKE '%'+@ProgrammeName+'%' AND "; //text 
                loadQuery += "CASE WHEN @FacultyName = '' then @FacultyName ELSE f.FacultyName END = @FacultyName AND "; //ddl 
                loadQuery += "CASE WHEN @ProgrammeFullorPart = '' then @ProgrammeFullorPart ELSE p.ProgrammeFullorPart END = @ProgrammeFullorPart AND "; //ddl 
                loadQuery += "ProgrammeCategory = @ProgrammeCategory "; //all ddl
                loadQuery += "ORDER BY ProgrammeName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);
                loadGVCmd.Parameters.AddWithValue("@ProgrammeAbbrv", ddlProgCode.SelectedValue);
                loadGVCmd.Parameters.AddWithValue("@ProgrammeName", txtProgName.Text);
                loadGVCmd.Parameters.AddWithValue("@FacultyName", ddlFacultyInChg.SelectedValue);
                loadGVCmd.Parameters.AddWithValue("@ProgrammeFullorPart", ddlFullorPart.SelectedValue);
                loadGVCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);
                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvProgramme.DataSource = dt;
                    gvProgramme.DataBind();
                    gvProgramme.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvProgramme.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchQuery;

                if(ddlProgCategory.SelectedValue != "All")
                {
                    searchQuery = "SELECT p.ProgrammeGUID, p.ProgrammeAbbrv, p.ProgrammeName, p.ProgrammeFullorPart, p.ProgrammeCategory, f.FacultyName FROM Programme p, Faculty f WHERE p.FacultyGUID = f.FacultyGUID AND ";
                    searchQuery += "CASE WHEN @ProgrammeAbbrv = '' THEN @ProgrammeAbbrv ELSE p.ProgrammeAbbrv END = @ProgrammeAbbrv AND "; //ddl
                    searchQuery += "CASE WHEN @ProgrammeName = '' THEN @ProgrammeName ELSE p.ProgrammeName END LIKE '%'+@ProgrammeName+'%' AND "; //text 
                    searchQuery += "CASE WHEN @FacultyName = '' then @FacultyName ELSE f.FacultyName END = @FacultyName AND "; //ddl 
                    searchQuery += "CASE WHEN @ProgrammeFullorPart = '' then @ProgrammeFullorPart ELSE p.ProgrammeFullorPart END = @ProgrammeFullorPart AND "; //ddl 
                    searchQuery += "ProgrammeCategory = @ProgrammeCategory "; //all ddl
                    searchQuery += "ORDER BY ProgrammeName";
                }
                else
                {
                    searchQuery = "SELECT p.ProgrammeGUID, p.ProgrammeAbbrv, p.ProgrammeName, p.ProgrammeFullorPart, p.ProgrammeCategory, f.FacultyName FROM Programme p, Faculty f WHERE p.FacultyGUID = f.FacultyGUID AND ";
                    searchQuery += "CASE WHEN @ProgrammeAbbrv = '' THEN @ProgrammeAbbrv ELSE p.ProgrammeAbbrv END = @ProgrammeAbbrv AND ";
                    searchQuery += "CASE WHEN @ProgrammeName = '' THEN @ProgrammeName ELSE p.ProgrammeName END LIKE '%'+@ProgrammeName+'%' AND ";
                    searchQuery += "CASE WHEN @FacultyName = '' then @FacultyName ELSE f.FacultyName END = @FacultyName AND ";
                    searchQuery += "CASE WHEN @ProgrammeFullorPart = '' then @ProgrammeFullorPart ELSE p.ProgrammeFullorPart END = @ProgrammeFullorPart ";
                    searchQuery += "ORDER BY ProgrammeName";
                }
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand searchCmd = new SqlCommand(searchQuery, con);
                searchCmd.Parameters.AddWithValue("@ProgrammeAbbrv", ddlProgCode.SelectedValue);
                searchCmd.Parameters.AddWithValue("@ProgrammeName", txtProgName.Text);
                searchCmd.Parameters.AddWithValue("@FacultyName", ddlFacultyInChg.SelectedValue);
                searchCmd.Parameters.AddWithValue("@ProgrammeFullorPart", ddlFullorPart.SelectedValue);
                searchCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgCategory.SelectedValue);
                SqlDataReader dtSearch = searchCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtSearch);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvProgramme.DataSource = dt;
                    gvProgramme.DataBind();
                    gvProgramme.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvProgramme.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }
}