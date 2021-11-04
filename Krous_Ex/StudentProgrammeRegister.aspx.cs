using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
            if (IsPostBack != true)
            {
                if(userGUID != null)
                {
                    loadProgrammeCategory();
                    loadProgramme("");
                    loadSession();
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
                SqlCommand GetCommand = new SqlCommand("SELECT ProgrammeGUID, ProgrammeAbbrv, ProgrammeName FROM Programme WHERE ProgrammeCategory = @ProgrammeCategory ORDER BY ProgrammeAbbrv", con);

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
                            insertCmd = new SqlCommand("INSERT INTO Student_Programme_Register VALUES (@RegisterGUID, @StudentGUID, @ProgrammeGUID, @SessionGUID, @ProgrammeRegisterDate, @Status, @UploadIcImage, @UploadResult, @UploadMedical)", con);
                            insertCmd.Parameters.AddWithValue("@RegisterGUID", registerGUID);
                            insertCmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                            insertCmd.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@ProgrammeRegisterDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            insertCmd.Parameters.AddWithValue("@Status", "Pending");
                            insertCmd.Parameters.AddWithValue("@UploadIcImage", icFileName);
                            insertCmd.Parameters.AddWithValue("@UploadResult", resultFileName);

                            if (AsyncFileUpload3.HasFile)
                            {
                                insertCmd.Parameters.AddWithValue("@UploadMedical", medicalFileName);
                            }
                            else
                            {
                                insertCmd.Parameters.AddWithValue("@UploadMedical", "none");
                            }

                            insertCmd.ExecuteNonQuery();

                            AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + icFileName);
                            AsyncFileUpload2.SaveAs(Server.MapPath(folderName) + resultFileName);
                            AsyncFileUpload3.SaveAs(Server.MapPath(folderName) + medicalFileName);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckDuplicateStudentRegister()) //
            {
                if (registerValidation()) 
                {
                    if (insertRegister())
                    {
                        clsFunction.DisplayAJAXMessage(this, "Your programme has been registered successfully! Please wait for the staff to approve it.");
                        Response.Redirect("StudentProgrammeRegister");
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Unable to register.");
                        ddlProgrammCategory.SelectedIndex = -1;
                        ddlProgramme.SelectedIndex = -1;
                        ddlSession.SelectedIndex = -1;
                        AsyncFileUpload1.Dispose();
                        AsyncFileUpload2.Dispose();
                        AsyncFileUpload3.Dispose();
                    }
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Already have your record in database!");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlProgrammCategory.SelectedIndex = -1;
            ddlProgramme.SelectedIndex = -1;
            ddlSession.SelectedIndex = -1;
            AsyncFileUpload1.Dispose();
            AsyncFileUpload2.Dispose();
            AsyncFileUpload3.Dispose();
            //Response.Redirect("StudentDashboard");

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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentDashboard");
        }

        private bool CheckDuplicateStudentRegister() //after need to check if the student's programme category
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                var SelectCommand = new SqlCommand();

                SelectCommand = new SqlCommand("SELECT StudentGUID, ProgrammeGUID FROM Student_Programme_Register WHERE StudentGUID = @StudentGUD GROUP BY StudentGUID, ProgrammeGUID ", con);
                SelectCommand.Parameters.AddWithValue("@StudentGUID", userGUID);
                SqlDataReader reader = SelectCommand.ExecuteReader();
                DataTable dtFound = new DataTable();
                dtFound.Load(reader);
                con.Close();
                if (dtFound.Rows.Count != 5)
                {  
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //can save into database, need to do validation
        //and if i did not upload medical, it will still insert the new guid into database (need to fix) -done
        //do upload ic, and result slip like spm / o-level  - done
        //add icImage, resultSlip into Student_Programme_Register table to save the uploaded file - done

    }
}