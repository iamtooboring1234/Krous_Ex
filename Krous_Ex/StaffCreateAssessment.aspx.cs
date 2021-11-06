﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffCreateAssessment : System.Web.UI.Page
    {
        Guid userGuid;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGuid = Guid.Parse(clsLogin.GetLoginUserGUID());

            if (Session["CreateAssessment"] != null)
            {
                if (Session["CreateAssessment"].ToString() == "Yes")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                    Session["CreateAssessment"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to create new assessment!");
                    Session["CreateAssessment"] = null;
                }
            }

            if (IsPostBack != true)
            {
                loadGroup();
                loadSession();
            }
        }

        private void loadSession()
        {
            try
            {
                ddlSession.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlSession.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT S.SessionGUID, S.SessionMonth, S.SessionYear FROM AcademicCalender A, Session S WHERE S.SessionGUID = A.SessionGUID AND GetDate() BETWEEN A.SemesterStartDate AND A.SemesterEndDate ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSession = new DataTable();
                dtSession.Load(reader);
                con.Close();

                string monthString;

                for (int i = 0; i <= dtSession.Rows.Count - 1; i++)
                {
                    oList = new ListItem();

                    monthString = dtSession.Rows[i]["SessionMonth"].ToString();
                    if (monthString.Length < 2)
                        monthString = "0" + monthString;

                    //oList.Text = dtSession.Rows[i]["SessionYear"].ToString() + dtSession.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                    oList.Text = dtSession.Rows[i]["SessionYear"].ToString() + monthString;
                    oList.Value = dtSession.Rows[i]["SessionGUID"].ToString();
                    ddlSession.Items.Add(oList);
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadGroup()
        {
            try
            {
                ddlGroups.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlGroups.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT GroupGUID, GroupNo FROM [Group] GROUP BY GroupGUID, GroupNo ORDER BY GroupNo", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProgCat = new DataTable();
                dtProgCat.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProgCat.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProgCat.Rows[i]["GroupNo"].ToString();
                    oList.Value = dtProgCat.Rows[i]["GroupGUID"].ToString();
                    ddlGroups.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading group list.");
            }
        }

        protected bool createAssessment()
        {
            try
            {
                Guid AssessmentGUID = Guid.NewGuid();
                Guid AssessmentFileListGUID = Guid.NewGuid();
                SqlConnection con = new SqlConnection();
                SqlCommand createCmd = new SqlCommand();

                //String filename = Path.GetFileName(AsyncUploadMaterial.FileName); //get filename
                //String savePath = ConfigurationManager.AppSettings.Get("AssessmentUploadPath");
                //String ProfileSavePath = Server.MapPath(savePath);
                //String ProfileFullSavePath = ProfileSavePath + filename;

                string filename = "Assessment_" + Path.GetFileName(AsyncUploadMaterial.FileName);
                string folderName = "~/Uploads/AssessmentFolder/" + AssessmentGUID + "/";

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));
                }

                if (filename != "")
                {
                    createCmd = new SqlCommand("INSERT INTO Assessment (AssessmentGUID, StaffGUID, GroupGUID, SessionGUID, AssessmentTitle, AssessmentDesc, DueDate, CreatedDate, UploadMaterials) VALUES (@AssessmentGUID, @StaffGUID, @GroupGUID, @SessionGUID, @AssessmentTitle, @AssessmentDesc, @DueDate, @CreatedDate, @UploadMaterials)", con);
                    createCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                    createCmd.Parameters.AddWithValue("@StaffGUID", userGuid);
                    createCmd.Parameters.AddWithValue("@GroupGUID", ddlGroups.SelectedValue);
                    createCmd.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                    createCmd.Parameters.AddWithValue("@AssessmentTitle", txtAssTitle.Text);
                    createCmd.Parameters.AddWithValue("@AssessmentDesc", txtDesc.Text);

                    if(txtDueDate.Text != "")
                    {
                        createCmd.Parameters.AddWithValue("@DueDate", DateTime.ParseExact(txtDueDate.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        createCmd.Parameters.AddWithValue("@DueDate", "");
                    }

                    createCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    if (AsyncUploadMaterial.HasFile)
                    {
                        createCmd.Parameters.AddWithValue("@UploadMaterials", filename);
                    }
                    else
                    {
                        createCmd.Parameters.AddWithValue("@UploadMaterials", "none");
                    }

                    createCmd.ExecuteNonQuery();

                    AsyncUploadMaterial.SaveAs(Server.MapPath(folderName) + filename);
                }
               
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (assessmentValidation())
            {
                if (createAssessment())
                {
                    Session["CreateAssessment"] = "Yes";
                    Response.Redirect("StaffCreateAssessment");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to create assessment.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter all the required information.");
            }   
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffDashboard");
        }

        private bool assessmentValidation()
        {

            if(txtAssTitle.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the assessment title.");
                return false;
            }

            if(txtDesc.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the assessment description.");
                return false;
            }

            if(ddlSession.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the session.");
                return false;
            }

            if (ddlGroups.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the session.");
                return false;
            }

            return true;
        }



        //protected void txtDueDate_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int Days = int.Parse(txtDueDate.Text);            

        //        DateTime startDate = DateTime.ParseExact(txtDueDate.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        //        CalendarExtender1.SelectedDate = startDate.AddDays(Days);
        //    }
        //    catch (Exception ex)
        //    {
        //        clsFunction.DisplayAJAXMessage(this, ex.Message);
        //    }
        //}


        //protected void AjaxFileUpload2_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        //{
        //    try
        //    {
        //        Guid AssessmentGUID = Guid.NewGuid();
        //        SqlCommand cmdInsert = new SqlCommand();
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
        //        con.Open();

        //        string fileName = Path.GetFileName(e.FileName);
        //        String savePath = ConfigurationManager.AppSettings.Get("AssessmentUploadPath");
        //        string ProfileImgSavePath = Server.MapPath(savePath);
        //        String ProfileFullSavePath = ProfileImgSavePath + fileName;
        //        AjaxFileUpload2.SaveAs(ProfileFullSavePath);

        //        //if (Directory.Exists(ProfileImgSavePath))
        //        //{
        //        //    if (!String.IsNullOrEmpty(ProfileFullSavePath))
        //        //    {
        //        //        AjaxFileUpload2.SaveAs(ProfileFullSavePath);
        //        //        string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
        //        //        con = new SqlConnection(strCon);
        //        //        con.Open();

        //        //        cmdInsert = new SqlCommand("", con);
        //        //        cmdInsert.Parameters.AddWithValue("@email", txtEmail.Text);
        //        //        cmdInsert.Parameters.AddWithValue("@StudentGUID", userGUID);
        //        //        cmdInsert.Parameters.AddWithValue("@phoneNo", txtContact.Text);
        //        //        cmdInsert.Parameters.AddWithValue("@address", txtAddress.Text);
        //        //        cmdInsert.Parameters.AddWithValue("@profileImage", imgName);
        //        //        cmdInsert.Parameters.AddWithValue("@LastUpdateInfo", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //        //        cmdInsert.ExecuteNonQuery();

        //        //        con.Dispose();
        //        //        con.Close();


        //        //    }
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Trace.WriteLine(ex.Message);
        //    }

        //}


    }
}

