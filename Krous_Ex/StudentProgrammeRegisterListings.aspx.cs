using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StudentProgrammeRegisterListings : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadStudListGV();
            }
        }


        protected void loadStudListGV()
        {
            try
            {
                string loadQuery;
                loadQuery = "SELECT spr.RegisterGUID, s.StudentGUID, p.ProgrammeGUID, s.StudentFullName, s.NRIC, p.ProgrammeName, spr.Status FROM Student_Programme_Register spr ";
                loadQuery += "LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID ";
                loadQuery += "LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID ";
                loadQuery += "WHERE CASE WHEN @StudentFullName = '' THEN @StudentFullName ELSE s.StudentFullName END LIKE '%'+@StudentFullName+'%' AND ";
                loadQuery += "CASE WHEN @NRIC = '' THEN @NRIC ELSE s.NRIC END LIKE '%'+@NRIC+'%' ";
                loadQuery += "ORDER BY s.StudentFullName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);
                loadGVCmd.Parameters.AddWithValue("@StudentFullName", txtStudName.Text);
                loadGVCmd.Parameters.AddWithValue("@NRIC", txtNRIC.Text);

                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                    gvCourse.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvCourse.Visible = false;
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

                searchQuery = "SELECT spr.RegisterGUID, spr.Status, s.StudentGUID, p.ProgrammeGUID, s.StudentFullName, s.NRIC, p.ProgrammeName, spr.Status FROM Student_Programme_Register spr ";
                searchQuery += "LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID ";
                searchQuery += "LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID ";
                searchQuery += "WHERE CASE WHEN @StudentFullName = '' THEN @StudentFullName ELSE s.StudentFullName END LIKE '%'+@StudentFullName+'%' AND "; 
                searchQuery += "CASE WHEN @NRIC = '' THEN @NRIC ELSE s.NRIC END LIKE '%'+@NRIC+'%' AND ";
                searchQuery += "CASE WHEN @RegisterStatus = '' THEN @RegisterStatus ELSE spr.Status END = @RegisterStatus ";
                searchQuery += "ORDER BY s.StudentFullName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand searchCmd = new SqlCommand(searchQuery, con);

                searchCmd.Parameters.AddWithValue("@StudentFullName", txtStudName.Text);
                searchCmd.Parameters.AddWithValue("@NRIC", txtNRIC.Text);
                searchCmd.Parameters.AddWithValue("@RegisterStatus", ddlRegisterStatus.SelectedValue);

                SqlDataReader dtSearch = searchCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtSearch);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                    gvCourse.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvCourse.Visible = false;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }
}