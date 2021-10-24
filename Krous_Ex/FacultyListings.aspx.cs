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
    public partial class FacultyListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadFacultyAbbrv();
                loadFacultyGV();
            }
        }


        protected void loadFacultyAbbrv()
        {
            try
            {
                ddlFacultyAbbrv.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT FacultyGUID, FacultyAbbrv FROM Faculty GROUP BY FacultyGUID, FacultyAbbrv ORDER BY FacultyAbbrv", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlFacultyAbbrv.DataSource = ds;
                ddlFacultyAbbrv.DataTextField = "FacultyAbbrv";
                ddlFacultyAbbrv.DataValueField = "FacultyAbbrv";
                ddlFacultyAbbrv.DataBind();
                ddlFacultyAbbrv.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }


        protected void loadFacultyGV()
        {
            try
            {
                string loadQuery;
                loadQuery = "SELECT * FROM Faculty ";
                loadQuery += "WHERE CASE WHEN @FacultyName = '' THEN @FacultyName ELSE FacultyName END LIKE '%'+@FacultyName+'%' AND "; //text
                loadQuery += "FacultyAbbrv = @FacultyAbbrv "; //ddl
                loadQuery += "ORDER BY FacultyName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);
                loadGVCmd.Parameters.AddWithValue("@FacultyName", txtFacultyName.Text);
                loadGVCmd.Parameters.AddWithValue("@FacultyAbbrv", ddlFacultyAbbrv.SelectedValue);  

                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvFaculty.DataSource = dt;
                    gvFaculty.DataBind();
                    gvFaculty.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvFaculty.Visible = false;
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

                searchQuery = "SELECT * FROM Faculty ";
                searchQuery += "WHERE CASE WHEN @FacultyName = '' THEN @FacultyName ELSE FacultyName END LIKE '%'+@FacultyName+'%' AND "; //text
                searchQuery += "CASE WHEN @FacultyAbbrv = '' then @FacultyAbbrv ELSE FacultyAbbrv END = @FacultyAbbrv ";
                searchQuery += "ORDER BY FacultyName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand searchCmd = new SqlCommand(searchQuery, con);

                searchCmd.Parameters.AddWithValue("@FacultyName", txtFacultyName.Text);
                searchCmd.Parameters.AddWithValue("@FacultyAbbrv", ddlFacultyAbbrv.SelectedValue);
                SqlDataReader dtSearch = searchCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtSearch);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvFaculty.DataSource = dt;
                    gvFaculty.DataBind();
                    gvFaculty.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvFaculty.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("FacultyEntry");
        }
    }
}