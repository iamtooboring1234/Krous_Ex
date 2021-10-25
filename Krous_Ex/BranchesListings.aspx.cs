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
    public partial class BranchesListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadBranchName();
                loadBranchGV();
              
            }
        }

        protected void loadBranchName()
        {
            try
            {
                ddlBranchName.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT BranchesGUID, BranchesName FROM Branches GROUP BY BranchesGUID, BranchesName ORDER BY BranchesName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlBranchName.DataSource = ds;
                ddlBranchName.DataTextField = "BranchesName";
                ddlBranchName.DataValueField = "BranchesName";
                ddlBranchName.DataBind();
                ddlBranchName.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }


        protected void loadBranchGV()
        {
            try
            {
                string loadQuery;
                loadQuery = "SELECT * FROM Branches ";
                loadQuery += "WHERE CASE WHEN @BranchesName = '' then @BranchesName ELSE BranchesName END = @BranchesName AND "; //text
                loadQuery += "CASE WHEN @BranchesAddress = '' THEN @BranchesAddress ELSE BranchesAddress END LIKE '%'+@BranchesAddress+'%' AND "; //text
                loadQuery += "CASE WHEN @BranchesEmail = '' THEN @BranchesEmail ELSE BranchesEmail END LIKE '%'+@BranchesEmail+'%' AND "; //text
                loadQuery += "CASE WHEN @BranchesTel = '' THEN @BranchesTel ELSE BranchesTel END LIKE '%'+@BranchesTel+'%' "; //text
                loadQuery += "ORDER BY BranchesName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);
                loadGVCmd.Parameters.AddWithValue("@BranchesName", ddlBranchName.SelectedValue);
                loadGVCmd.Parameters.AddWithValue("@BranchesAddress", txtBranchAddress.Text);
                loadGVCmd.Parameters.AddWithValue("@BranchesEmail", txtBranchEmail.Text);
                loadGVCmd.Parameters.AddWithValue("@BranchesTel", txtBranchTel.Text);

                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvBranches.DataSource = dt;
                    gvBranches.DataBind();
                    gvBranches.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvBranches.Visible = false;
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
                searchQuery = "SELECT * FROM Branches ";
                searchQuery += "WHERE CASE WHEN @BranchesName = '' then @BranchesName ELSE BranchesName END = @BranchesName AND ";
                searchQuery += "CASE WHEN @BranchesAddress = '' THEN @BranchesAddress ELSE BranchesAddress END LIKE '%'+@BranchesAddress+'%' AND "; //text
                searchQuery += "CASE WHEN @BranchesEmail = '' THEN @BranchesEmail ELSE BranchesEmail END LIKE '%'+@BranchesEmail+'%' AND "; //text
                searchQuery += "CASE WHEN @BranchesTel = '' THEN @BranchesTel ELSE BranchesTel END LIKE '%'+@BranchesTel+'%' "; //text
                searchQuery += "ORDER BY BranchesName";
     
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand searchCmd = new SqlCommand(searchQuery, con);
                searchCmd.Parameters.AddWithValue("@BranchesName", ddlBranchName.SelectedValue);
                searchCmd.Parameters.AddWithValue("@BranchesAddress", txtBranchAddress.Text);
                searchCmd.Parameters.AddWithValue("@BranchesEmail", txtBranchEmail.Text);
                searchCmd.Parameters.AddWithValue("@BranchesTel", txtBranchTel.Text);
                SqlDataReader dtSearch = searchCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtSearch);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvBranches.DataSource = dt;
                    gvBranches.DataBind();
                    gvBranches.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvBranches.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("BranchesEntry");
        }
    }
}