﻿using System;
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
            if (!IsPostBack)
            {
                //loadProgrammeList();
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
                    //ddlProgramme.SelectedValue = dt.Rows[0]["CourseProgramme"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        //protected void loadProgrammeList()
        //{
        //    try
        //    {
        //        ddlProgramme.Items.Clear();
        //        ListItem facultyList = new ListItem();
        //        SqlConnection con = new SqlConnection();
        //        SqlCommand loadCmd = new SqlCommand();

        //        string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
        //        con = new SqlConnection(strCon);
        //        con.Open();

        //        loadCmd = new SqlCommand("SELECT ProgrammeGUID, ProgrammeName FROM Programme GROUP BY ProgrammeGUID, ProgrammeName ORDER BY ProgrammeName", con);
        //        SqlDataAdapter da = new SqlDataAdapter(loadCmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        ddlProgramme.DataSource = ds;
        //        ddlProgramme.DataTextField = "ProgrammeName";
        //        ddlProgramme.DataValueField = "ProgrammeName";
        //        ddlProgramme.DataBind();
        //        ddlProgramme.Items.Insert(0, new ListItem("", ""));
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Trace.WriteLine(ex.Message);
        //    }
        //}

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
                insertCourCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
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

        protected bool deleteProgramme()
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
                        clsFunction.DisplayAJAXMessage(this, "Added new course successfully!");
                        txtCourseAbbrv.Text = string.Empty;
                        txtCourseName.Text = string.Empty;
                        txtCourseDesc.Text = string.Empty;
                        txtCreditHour.Text = string.Empty;
                        rbCourseCategory.ClearSelection();
                        txtCourseName.Focus();
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Unable to add new course entry.");
                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CourseListings");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["CourseGUID"]))
            {
                Response.Redirect("CourseEntry?CourseGUID=" + Request.QueryString["CourseGUID"]);
            }
            else
            {
                Response.Redirect("CourseEntry");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (validateCourse())
            {
                if (updateCourse())
                {
                    clsFunction.DisplayAJAXMessage(this, "Course details has been updated!");
                    Response.Redirect("CourseListings");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update course details.");
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
                clsFunction.DisplayAJAXMessage(this, "Course details has been deleted!");
                Response.Redirect("CourseListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "No such records to be deleted.");
            }
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

            //if (ddlProgramme.SelectedIndex == 0)
            //{
            //    clsFunction.DisplayAJAXMessage(this, "Please select the programme that this course should belongs to.");
            //    return false;
            //}

            return true;
        }

        protected bool validateDuplicate()
        {
            if (clsValidation.CheckDuplicateCourseName(txtCourseName.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "The Course name is already exists in the database!");
                return false;
            }

            if (clsValidation.CheckDuplicateCourseAbbrv(txtCourseAbbrv.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "The Course Abbreviation is already exists in the database!");
                return false;
            }

            return true;
        }
        
    }
}