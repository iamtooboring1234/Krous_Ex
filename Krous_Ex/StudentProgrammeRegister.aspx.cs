using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StudentCourseRegister : System.Web.UI.Page
    {
        Guid userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
        Guid registerGUID = Guid.NewGuid();
        protected void Page_Load(object sender, EventArgs e)
        {


            //if (Session["RegisterProgramme"] != null)
            //{
            //    if (Session["RegisterProgramme"].ToString() == "Yes")
            //    {
            //        clsFunction.DisplayAJAXMessage(this, "Your programme has been registered successfully! Please wait for the staff to approve it.");
            //        Session["RegisterProgramme"] = null;
            //    }
            //    else
            //    {
            //        clsFunction.DisplayAJAXMessage(this, "Unable to register!");
            //        Session["RegisterProgramme"] = null;
            //    }
            //}

            if (IsPostBack != true)
            {
                if(userGUID != null)
                {
                    if (Session["RegisterProgramme"] != null)
                    {
                        if (Session["RegisterProgramme"].ToString() == "Yes")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showRegisterSuccessToast(); ", true);
                            Session["RegisterProgramme"] = null;
                        }
                        else
                        {
                            clsFunction.DisplayAJAXMessage(this, "Unable to register!");
                            Session["RegisterProgramme"] = null;
                        }
                    }

                    loadProgrammeCategory();
                    loadProgramme("");
                    loadSession();
                    loadBranch();
                }    
            }
        }

        private void loadProgrammeCategory()
        {
            try
            {
                ddlProgrammCategory.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgrammCategory.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ProgrammeCategory FROM Programme GROUP BY ProgrammeCategory ORDER BY ProgrammeCategory", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProgCat = new DataTable();
                dtProgCat.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProgCat.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                    oList.Value = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                    ddlProgrammCategory.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading programme category.");
            }
        }

        private void loadProgramme(string programmeCategory)
        {
            try
            {
                ddlProgramme.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgramme.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT p.ProgrammeGUID, p.ProgrammeAbbrv, p.ProgrammeName FROM Programme p LEFT JOIN ProgrammeCourse pc ON pc.ProgrammeGUID = p.ProgrammeGUID WHERE ProgrammeCategory = @ProgrammeCategory GROUP BY p.ProgrammeGUID, p.ProgrammeAbbrv, p.ProgrammeName ORDER BY ProgrammeAbbrv", con);
                GetCommand.Parameters.AddWithValue("@ProgrammeCategory", programmeCategory);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProg = new DataTable();
                dtProg.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProg.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProg.Rows[i]["ProgrammeName"].ToString() + " (" + dtProg.Rows[i]["ProgrammeAbbrv"].ToString() + ")";
                    oList.Value = dtProg.Rows[i]["ProgrammeGUID"].ToString();
                    ddlProgramme.Items.Add(oList);
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
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
                SqlCommand GetCommand = new SqlCommand("select * from session S, AcademicCalender a WHERE S.SessionGUID = a.SessionGUID AND DateAdd(Day, 14, GETDATE()) < a.SemesterStartDate order by SessionYear, SessionMonth; ", con);
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

        private void loadBranch()
        {
            try
            {
                ddlBranch.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlBranch.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Branches", con);

                SqlDataReader readerBranch = GetCommand.ExecuteReader();

                DataTable dtBranch = new DataTable();
                dtBranch.Load(readerBranch);
                con.Close();

                for (int i = 0; i <= dtBranch.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtBranch.Rows[i]["BranchesName"].ToString();
                    oList.Value = dtBranch.Rows[i]["BranchesGUID"].ToString();
                    ddlBranch.Items.Add(oList);
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected bool insertRegister()
        {
            bool insertBool = false;
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand insertCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                string icFileName = "IC_" + Path.GetFileName(AsyncFileUpload1.FileName);
                string resultFileName = "Result_" + Path.GetFileName(AsyncFileUpload2.FileName);
                string medicalFileName = "Medical_" + Path.GetFileName(AsyncFileUpload3.FileName);

                string folderName = "~/Uploads/StudentRegisterFile/" + registerGUID + "/";
                
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));               
                }
                if (icFileName != "")
                {
                    if (resultFileName != "")
                    {
                        if (medicalFileName != "")
                        {
                            insertCmd = new SqlCommand("INSERT INTO Student_Programme_Register (RegisterGUID, StudentGUID, ProgrammeGUID, SessionGUID, ProgrammeRegisterDate, Status, UploadIcImage, UploadResult, UploadMedical, BranchesGUID) VALUES (@RegisterGUID, @StudentGUID, @ProgrammeGUID, @SessionGUID, @ProgrammeRegisterDate, @Status, @UploadIcImage, @UploadResult, @UploadMedical, @BranchesGUID)", con);
                            insertCmd.Parameters.AddWithValue("@RegisterGUID", registerGUID);
                            insertCmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                            insertCmd.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@ProgrammeRegisterDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            insertCmd.Parameters.AddWithValue("@Status", "Pending");


                            if (AsyncFileUpload1.HasFile)
                            {
                                insertCmd.Parameters.AddWithValue("@UploadIcImage", icFileName);
                                AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + icFileName);
                            }

                            if (AsyncFileUpload2.HasFile)
                            {
                                insertCmd.Parameters.AddWithValue("@UploadResult", resultFileName);
                                AsyncFileUpload2.SaveAs(Server.MapPath(folderName) + resultFileName);
                            }

                            if (AsyncFileUpload3.HasFile)
                            {
                                insertCmd.Parameters.AddWithValue("@UploadMedical", medicalFileName);
                                AsyncFileUpload3.SaveAs(Server.MapPath(folderName) + medicalFileName);
                            }
                            else
                            {
                                medicalFileName = "none";
                                insertCmd.Parameters.AddWithValue("@UploadMedical", medicalFileName);
                                
                            }
                            insertCmd.Parameters.AddWithValue("@BranchesGUID", ddlBranch.SelectedValue);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
                con.Dispose();
                con.Close();

                insertBool = true;  
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
            return insertBool;
        }

        private bool checkDuplicateRegister()
        {
            try
            {
                SqlConnection con = new SqlConnection();              
                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                //check student record if they have register as foundation or others
                SqlCommand verifyCmd = new SqlCommand("SELECT p.ProgrammeCategory FROM Student_Programme_Register spr LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID WHERE StudentGUID = @StudentGUID ", con);
                verifyCmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                SqlDataReader dtrVerify = verifyCmd.ExecuteReader();
                DataTable dtVerify = new DataTable();
                dtVerify.Load(dtrVerify);

                if (dtVerify.Rows.Count != 0)
                {
                    string programme = dtVerify.Rows[0]["ProgrammeCategory"].ToString();
                    if (ddlProgrammCategory.SelectedValue == programme)
                    {
                        clsFunction.DisplayAJAXMessage(this, "You have registered " + programme + " programme previously. Please make sure you register the programme that you should be taking.");
                        ddlProgrammCategory.SelectedIndex = -1;
                        ddlProgramme.SelectedIndex = -1;
                        ddlSession.SelectedIndex = -1;
                        ddlBranch.SelectedIndex = -1;
                        AsyncFileUpload1.Dispose();
                        AsyncFileUpload2.Dispose();
                        AsyncFileUpload3.Dispose();
                        return false;
                    }
                }
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
            if (checkDuplicateRegister())
            {
                if (registerValidation())
                {
                    if (insertRegister())
                    {
                        Session["RegisterProgramme"] = "Yes";
                        Response.Redirect("StudentProgrammeRegister");
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Unable to register.");
                        ddlProgrammCategory.SelectedIndex = -1;
                        ddlProgramme.SelectedIndex = -1;
                        ddlSession.SelectedIndex = -1;
                        ddlBranch.SelectedIndex = -1;
                        AsyncFileUpload1.Dispose();
                        AsyncFileUpload2.Dispose();
                        AsyncFileUpload3.Dispose();
                    }
                }
            }
        }

        protected void ddlProgrammCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgrammCategory.SelectedValue != "")
            {
                loadProgramme(ddlProgrammCategory.SelectedValue);
                ddlProgramme.Enabled = true;

            }
        }

        protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgramme.SelectedValue != "")
            {
                loadSession();
                ddlSession.Enabled = true;
            }
        }

        protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSession.SelectedValue != "")
            {
                loadBranch();
                ddlBranch.Enabled = true;
            }
        }

   
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentDashboard");
        }

        protected bool registerValidation()
        {
            if (ddlProgrammCategory.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Plese select the programme category (level of study) before viewing the programmes available");
                return false;
            }

            if (ddlProgramme.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Plese select the programme to proceed to next selection.");
                return false;
            }

            if (ddlSession.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Plese select the current available session.");
                return false;
            }

            if (ddlBranch.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Plese select the branch you want to join.");
                return false;
            }

            if (!(AsyncFileUpload1.HasFile))
            {
                clsFunction.DisplayAJAXMessage(this, "Please upload the copy of your MyKad image.");
                return false;
            }

            if (!(AsyncFileUpload2.HasFile))
            {
                clsFunction.DisplayAJAXMessage(this, "Please upload your result slip. (exp: SPM, STPM, O-Level and etc.)");
                return false;
            }

            return true;
        }

      
    }
}