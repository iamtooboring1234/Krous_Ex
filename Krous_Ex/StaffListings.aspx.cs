using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["deleteStaff"] != null)
            {
                if (Session["deleteStaff"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "Staff details has been deleted!");
                    Session["deleteStaff"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Staff details unable to delete!");
                    Session["deleteStaff"] = null;
                }
            }

            if (IsPostBack != true)
            {
                loadStaffRole();
                loadStaffPosition();
                loadStaffSpecialization();
                loadStaffGV();
            }
        }

        protected void loadStaffRole()
        {
            try
            {
                ddlStaffRole.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT StaffRole FROM Staff GROUP BY StaffRole ORDER BY StaffRole", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlStaffRole.DataSource = ds;
                ddlStaffRole.DataTextField = "StaffRole";
                ddlStaffRole.DataValueField = "StaffRole";
                ddlStaffRole.DataBind();
                ddlStaffRole.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadStaffPosition()
        {
            try
            {
                ddlPosition.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT StaffPositiion FROM Staff GROUP BY StaffPositiion ORDER BY StaffPositiion", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlPosition.DataSource = ds;
                ddlPosition.DataTextField = "StaffPositiion";
                ddlPosition.DataValueField = "StaffPositiion";
                ddlPosition.DataBind();
                ddlPosition.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadStaffSpecialization()
        {
            try
            {
                ddlSpecialization.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT Specialization FROM Staff GROUP BY Specialization ORDER BY Specialization", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlSpecialization.DataSource = ds;
                ddlSpecialization.DataTextField = "Specialization";
                ddlSpecialization.DataValueField = "Specialization";
                ddlSpecialization.DataBind();
                ddlSpecialization.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadStaffGV()
        {
            try
            {
                string loadQuery;
                loadQuery = "SELECT * FROM Staff ";
                loadQuery += "WHERE CASE WHEN @StaffUsername = '' THEN @StaffUsername ELSE StaffUsername END LIKE '%'+@StaffUsername+'%' AND "; 
                loadQuery += "CASE WHEN @StaffFullName = '' THEN @StaffFullName ELSE StaffFullName END LIKE '%'+@StaffFullName+'%' AND "; 
                loadQuery += "CASE WHEN @StaffRole = '' THEN @StaffRole ELSE StaffRole END = @StaffRole AND ";
                loadQuery += "CASE WHEN @StaffPositiion = '' THEN @StaffPositiion ELSE StaffPositiion END = @StaffPositiion AND "; 
                loadQuery += "CASE WHEN @Specialization = '' THEN @Specialization ELSE Specialization END = @Specialization "; 
                loadQuery += "ORDER BY StaffFullName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);
                loadGVCmd.Parameters.AddWithValue("@StaffUsername", txtUsername.Text);
                loadGVCmd.Parameters.AddWithValue("@StaffFullName", txtFullName.Text);
                loadGVCmd.Parameters.AddWithValue("@StaffRole", ddlStaffRole.SelectedValue);
                loadGVCmd.Parameters.AddWithValue("@StaffPositiion", ddlPosition.SelectedValue);
                loadGVCmd.Parameters.AddWithValue("@Specialization", ddlSpecialization.SelectedValue);

                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvStaff.DataSource = dt;
                    gvStaff.DataBind();
                    gvStaff.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvStaff.Visible = false;
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

                searchQuery = "SELECT * FROM Staff ";
                searchQuery += "WHERE CASE WHEN @StaffUsername = '' THEN @StaffUsername ELSE StaffUsername END LIKE '%'+@StaffUsername+'%' AND ";
                searchQuery += "CASE WHEN @StaffFullName = '' THEN @StaffFullName ELSE StaffFullName END LIKE '%'+@StaffFullName+'%' AND ";
                searchQuery += "CASE WHEN @StaffRole = '' THEN @StaffRole ELSE StaffRole END = @StaffRole AND ";
                searchQuery += "CASE WHEN @StaffPositiion = '' THEN @StaffPositiion ELSE StaffPositiion END = @StaffPositiion AND ";
                searchQuery += "CASE WHEN @Specialization = '' THEN @Specialization ELSE Specialization END = @Specialization ";
                searchQuery += "ORDER BY StaffFullName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand searchCmd = new SqlCommand(searchQuery, con);

                searchCmd.Parameters.AddWithValue("@StaffUsername", txtUsername.Text);
                searchCmd.Parameters.AddWithValue("@StaffFullName", txtFullName.Text);
                searchCmd.Parameters.AddWithValue("@StaffRole", ddlStaffRole.SelectedValue);
                searchCmd.Parameters.AddWithValue("@StaffPositiion", ddlPosition.SelectedValue);
                searchCmd.Parameters.AddWithValue("@Specialization", ddlSpecialization.SelectedValue);
                SqlDataReader dtSearch = searchCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtSearch);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvStaff.DataSource = dt;
                    gvStaff.DataBind();
                    gvStaff.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvStaff.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffEntry");
        }
    }
}