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
    public partial class StudentProfile : System.Web.UI.Page
    {
        Guid userGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (!(IsPostBack))
            {
                if(userGUID != null)
                {
                    loadData();
                }
            }
        }

        protected void loadData()
        {
            try
            {
                String StudentGUID = Request.QueryString["UserGUID"];
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                //change the join date format  

                cmd = new SqlCommand("SELECT s.StudentGUID, s.StudentFullName, s.Gender, CONVERT(varchar, s.DOB) as DOB, s.PhoneNumber, s.Email, s.NRIC, s.YearIntake, s.AccountRegisterDate, CONCAT(f.facultyname, ' ', (f.facultyAbbrv)) AS FacultyName, b.BranchesName FROM Student s LEFT JOIN Branches b ON s.BranchesGUID = b.BranchesGUID LEFT JOIN Faculty f ON s.FacultyGUID = f.FacultyGUID WHERE s.StudentGUID = @StudentGUID", con);
                cmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                SqlDataReader dtrStudent = cmd.ExecuteReader();
                DataTable dtStud = new DataTable();
                dtStud.Load(dtrStudent);

                con.Close();

                //if got, then load
                if(dtStud.Rows.Count > 0)
                {
                    txtFullname.Text = dtStud.Rows[0][1].ToString();
                    txtGender.Text = dtStud.Rows[0][2].ToString();
                    txtDOB.Text = dtStud.Rows[0][3].ToString();
                    txtContact.Text = dtStud.Rows[0][4].ToString();
                    txtEmail.Text = dtStud.Rows[0][5].ToString();
                    txtNRIC.Text = dtStud.Rows[0][6].ToString();
                    txtYearIntake.Text = dtStud.Rows[0][7].ToString();
                    txtDateJoined.Text = dtStud.Rows[0][8].ToString();
                    txtFaculty.Text = dtStud.Rows[0][9].ToString();
                    txtBranch.Text = dtStud.Rows[0][10].ToString();
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmdUpdate = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                cmdUpdate = new SqlCommand("UPDATE Student SET Email = @email, PhoneNumber = @phoneNo WHERE StudentGUID = @StudentGUID", con);
                cmdUpdate.Parameters.AddWithValue("@email", txtEmail.Text);
                cmdUpdate.Parameters.AddWithValue("@phoneNo", txtContact.Text);
                cmdUpdate.ExecuteNonQuery();

                con.Close();

                lblUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            txtEmail.Enabled = true;
            txtContact.Enabled = true;
              
        }
    }
}