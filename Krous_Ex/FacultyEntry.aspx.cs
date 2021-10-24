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
    public partial class FacultyEntry : System.Web.UI.Page
    {
        Guid facultyGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["FacultyGUID"]))
                {
                    facultyGUID = Guid.Parse(Request.QueryString["FacultyGUID"]);
                    loadFacultyInfo();
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

        protected void loadFacultyInfo()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCourseCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCourseCmd = new SqlCommand("SELECT * FROM Faculty WHERE FacultyGUID = @FacultyGUID", con);
                loadCourseCmd.Parameters.AddWithValue("@FacultyGUID", facultyGUID);
                SqlDataReader dtrLoad = loadCourseCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                if (dt.Rows.Count != 0)
                {                    
                    txtFacultyAbbrv.Text = dt.Rows[0]["FacultyAbbrv"].ToString();
                    txtFacultyName.Text = dt.Rows[0]["FacultyName"].ToString();
                    txtFacultyDesc.Text = dt.Rows[0]["FacultyDesc"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private bool insertFaculty()
        {
            Guid FacultyGUID = Guid.NewGuid();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO FACULTY VALUES(@FacultyGUID,@FacultyAbbrv,@FacultyName,@FacultyDesc)", con);

                InsertCommand.Parameters.AddWithValue("@FacultyGUID", FacultyGUID);
                InsertCommand.Parameters.AddWithValue("@FacultyAbbrv", txtFacultyAbbrv.Text);
                InsertCommand.Parameters.AddWithValue("@FacultyName", txtFacultyName.Text);
                InsertCommand.Parameters.AddWithValue("@FacultyDesc", txtFacultyDesc.Text);


                InsertCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        protected bool updateFaculty()
        {
            facultyGUID = Guid.Parse(Request.QueryString["FacultyGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand updateCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                updateCmd = new SqlCommand("UPDATE Faculty SET FacultyAbbrv = @FacultyAbbrv, FacultyName = @FacultyName, FacultyDesc = @FacultyDesc WHERE FacultyGUID = @FacultyGUID", con);
                updateCmd.Parameters.AddWithValue("@FacultyGUID", facultyGUID);
                updateCmd.Parameters.AddWithValue("@FacultyAbbrv", txtFacultyAbbrv.Text);
                updateCmd.Parameters.AddWithValue("@FacultyName", txtFacultyName.Text);
                updateCmd.Parameters.AddWithValue("@FacultyDesc", txtFacultyDesc.Text);
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

        protected bool deleteFaculty()
        {
            facultyGUID = Guid.Parse(Request.QueryString["FacultyGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand deleteCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                deleteCmd = new SqlCommand("DELETE FROM Faculty WHERE FacultyGUID = @FacultyGUID", con);
                deleteCmd.Parameters.AddWithValue("@FacultyGUID", facultyGUID);
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


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (updateFaculty())
            {
                clsFunction.DisplayAJAXMessage(this, "Faculty details has been updated!");
                Response.Redirect("FacultyListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to update faculty details.");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteFaculty())
            {
                clsFunction.DisplayAJAXMessage(this, "Faculty details has been deleted!");
                Response.Redirect("FacultyListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "No such records to be deleted.");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validateDuplicate())
            {
                if (insertFaculty())
                {
                    clsFunction.DisplayAJAXMessage(this, "Added new faculty successfully!");
                    Response.Redirect("FacultyEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FacultyListings");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["FacultyGUID"]))
            {
                Response.Redirect("FacultyEntry?FacultyGUID=" + Request.QueryString["FacultyGUID"]);
            }
            else
            {
                Response.Redirect("FacultyEntry");
            }
        }

        public string GetInitials(string MyText)
        {
            string Initials = "";
            string[] AllWords = MyText.Split(' ');
            foreach (string Word in AllWords)
            {
                if (Word.Length > 0)
                    Initials = Initials + Word[0].ToString().ToUpper();
            }
            return Initials;
        }


        protected void txtFacultyName_TextChanged(object sender, EventArgs e)
        {
            txtFacultyAbbrv.Text = GetInitials(txtFacultyName.Text);
        }

        protected bool validateDuplicate()
        {
            if (clsValidation.CheckDuplicateFacultyName(txtFacultyName.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "This Faculty Name is already exists in the database!");
                return false;
            }

            if (clsValidation.CheckDuplicateFacultyAbbrv(txtFacultyAbbrv.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "This Faculty Abbreviation is already exists in the database!");
                return false;
            }

            return true;
        }

      
    }
}