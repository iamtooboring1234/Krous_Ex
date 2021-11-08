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
    public partial class StaffAssessmentListings : System.Web.UI.Page
    {
        Guid userGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (IsPostBack != true)
            {
                loadGroupNo();
                loadGvAssessment();
            }
        }

        private void loadGroupNo()
        {
            try
            {
                ddlGroupNo.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT GroupGUID, GroupNo FROM [Group] GROUP BY GroupGUID, GroupNo ORDER BY GroupNo", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet dsGroup = new DataSet();
                da.Fill(dsGroup);
                ddlGroupNo.DataSource = dsGroup;
                ddlGroupNo.DataTextField = "GroupNo";
                ddlGroupNo.DataValueField = "GroupNo";
                ddlGroupNo.DataBind();
                ddlGroupNo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
 
        protected void loadGvAssessment()
        {
            try
            {
                string loadQuery;

                loadQuery = "SELECT a.AssessmentGUID, CONVERT(VARCHAR, a.CreatedDate, 100) as CreatedDate, s.StaffFullName, a.AssessmentTitle, CONVERT(VARCHAR, a.DueDate, 100) as DueDate, g.GroupNo ";
                loadQuery += "FROM Assessment a, Staff s, [Group] g WHERE a.StaffGUID = s.StaffGUID AND a.GroupGUID = g.GroupGUID ";
                loadQuery += "ORDER BY StaffFullName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);
                //loadGVCmd.Parameters.AddWithValue("@StaffFullName", txtStaffName.Text);
                //loadGVCmd.Parameters.AddWithValue("@GroupNo", ddlGroupNo.SelectedValue);
                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvAssessment.DataSource = dt;
                    gvAssessment.DataBind();
                    gvAssessment.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvAssessment.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            con.Open();

            try
            {
                string searchQuery;
                searchQuery = "SELECT a.AssessmentGUID, CONVERT(VARCHAR, a.CreatedDate, 100) as CreatedDate, s.StaffFullName, a.AssessmentTitle, CONVERT(VARCHAR, a.DueDate, 100) as DueDate, g.GroupNo ";
                searchQuery += "FROM Assessment a, Staff s, [Group] g WHERE a.StaffGUID = s.StaffGUID AND a.GroupGUID = g.GroupGUID AND ";
                searchQuery += "CASE WHEN @StaffFullName = '' THEN @StaffFullName ELSE s.StaffFullName END LIKE '%'+@StaffFullName+'%' AND "; //text 
                searchQuery += "CASE WHEN @GroupNo = '' then @GroupNo ELSE g.GroupNo END = @GroupNo "; //ddl 
                searchQuery += "ORDER BY StaffFullName";

                SqlCommand loadGVCmd = new SqlCommand(searchQuery, con);
                loadGVCmd.Parameters.AddWithValue("@StaffFullName", txtStaffName.Text);
                loadGVCmd.Parameters.AddWithValue("@GroupNo", ddlGroupNo.SelectedValue);
                SqlDataReader dtrGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvAssessment.DataSource = dt;
                    gvAssessment.DataBind();
                    gvAssessment.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvAssessment.Visible = false;
                }        
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffCreateAssessment");
        }
    }
}