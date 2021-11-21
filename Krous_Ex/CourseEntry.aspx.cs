using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class CourseEntry : System.Web.UI.Page
    {
        Guid courseGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["AddNewCourse"] != null)
                {
                    if (Session["AddNewCourse"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showCourseAddSuccessToast(); ", true);
                        Session["AddNewCourse"] = null;
                    }
                }

                if (Session["updateCourse"] != null)
                {
                    if (Session["updateCourse"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showCourseUpdateSuccessToast(); ", true);
                        Session["updateCourse"] = null;
                    }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["CourseGUID"]))
                {
                    courseGUID = Guid.Parse(Request.QueryString["CourseGUID"]);
                    loadCourseInfo();
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

        protected void loadCourseInfo()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCourseCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCourseCmd = new SqlCommand("SELECT * FROM Course WHERE courseGUID = @courseGUID", con);
                loadCourseCmd.Parameters.AddWithValue("@courseGUID", courseGUID);
                SqlDataReader dtrLoad = loadCourseCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                if (dt.Rows.Count != 0)
                {
                    txtCourseName.Text = dt.Rows[0]["CourseName"].ToString();
                    txtCourseAbbrv.Text = dt.Rows[0]["CourseAbbrv"].ToString();
                    txtCourseDesc.Text = dt.Rows[0]["CourseDesc"].ToString();
                    txtCreditHour.Text = dt.Rows[0]["CreditHour"].ToString();
                    rbCourseCategory.SelectedValue = dt.Rows[0]["Category"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private bool insertCourse()
        {
            Guid courseGUID = Guid.NewGuid();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand insertCourCmd = new SqlCommand("INSERT INTO COURSE VALUES(@CourseGUID, @CourseAbbrv, @CourseName, @CourseDesc, @CreditHour, @Category)", con);

                insertCourCmd.Parameters.AddWithValue("@CourseGUID", courseGUID);
                insertCourCmd.Parameters.AddWithValue("@CourseAbbrv", txtCourseAbbrv.Text.ToUpper());
                insertCourCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text.ToUpper());
                insertCourCmd.Parameters.AddWithValue("@CourseDesc", txtCourseDesc.Text);
                insertCourCmd.Parameters.AddWithValue("@CreditHour", txtCreditHour.Text);
                insertCourCmd.Parameters.AddWithValue("@Category", rbCourseCategory.SelectedValue);
                insertCourCmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected bool updateCourse()
        {
            courseGUID = Guid.Parse(Request.QueryString["CourseGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand updateCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                updateCmd = new SqlCommand("UPDATE Course SET CourseAbbrv = @CourseAbbrv, CourseName = @CourseName, CourseDesc = @CourseDesc, CreditHour = @CreditHour, Category = @Category WHERE CourseGUID = @CourseGUID", con);
                updateCmd.Parameters.AddWithValue("@CourseGUID", courseGUID);
                updateCmd.Parameters.AddWithValue("@CourseAbbrv", txtCourseAbbrv.Text.ToUpper());
                updateCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                updateCmd.Parameters.AddWithValue("@CourseDesc", txtCourseDesc.Text);
                updateCmd.Parameters.AddWithValue("@CreditHour", txtCreditHour.Text);
                updateCmd.Parameters.AddWithValue("@Category", rbCourseCategory.SelectedValue);
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

        protected bool deleteCourse()
        {
            courseGUID = Guid.Parse(Request.QueryString["CourseGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand deleteCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                deleteCmd = new SqlCommand("DELETE FROM Course WHERE CourseGUID = @CourseGUID", con);
                deleteCmd.Parameters.AddWithValue("@CourseGUID", courseGUID);
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
            if (validateDuplicate())
            {
                if (validateCourse())
                {
                    if (insertCourse())
                    {
                        Session["AddNewCourse"] = "Yes";
                        Response.Redirect("CourseEntry");
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Unable to add new course entry.");
                        txtCourseAbbrv.Text = string.Empty;
                        txtCourseName.Text = string.Empty;
                        txtCourseDesc.Text = string.Empty;
                        txtCreditHour.Text = string.Empty;
                        rbCourseCategory.ClearSelection();
                        txtCourseName.Focus();
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (validateCourse())
            {
                if (updateCourse())
                {
                    Session["updateCourse"] = "Yes";
                    Response.Redirect("CourseEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update course details.");
                    loadCourseInfo();
                }
            }
            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteCourse())
            {
                Session["deleteCourse"] = "Yes"; 
                Response.Redirect("CourseListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "No such records to be deleted.");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CourseListings");
        }

        private bool validateCourse()
        {
            if(txtCourseName.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the course name.");
                return false;
            }

            if(txtCourseAbbrv.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the course abbreviation.");
                return false;
            }

            if (txtCourseAbbrv.Text.Length > 9)
            {
                clsFunction.DisplayAJAXMessage(this, "Please make sure the course abbreviation must within 9 character.");
                return false;
            }

            if (txtCourseDesc.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the course description.");
                return false;
            }

            if (rbCourseCategory.SelectedIndex == -1)
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the course category.");
                return false;
            }

            if (txtCreditHour.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the credit hour for this course.");
                return false;
            }

            if (!(double.TryParse(txtCreditHour.Text, out _)))
            {
                clsFunction.DisplayAJAXMessage(this, "The credit hour should be in numeric form.");
                return false;
            }

            return true;
        }

        protected bool validateDuplicate()
        {
            if (clsValidation.CheckDuplicateCourseName(txtCourseName.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "The Course name is already exists in the database!");
                txtCourseName.Focus();
                return false;
            }

            if (clsValidation.CheckDuplicateCourseAbbrv(txtCourseAbbrv.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "The Course Abbreviation is already exists in the database!");
                txtCourseAbbrv.Focus();
                return false;
            }

            return true;
        }
        
    }
}