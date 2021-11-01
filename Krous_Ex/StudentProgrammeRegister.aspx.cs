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
                Guid registerGUID = Guid.NewGuid();
                SqlConnection con = new SqlConnection();
                SqlCommand insertCmd = new SqlCommand();

                //upload here
                string IcNumberImage = Path.GetFileNameWithoutExtension(UploadNRIC.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(UploadNRIC.FileName);
                string ResultSlipImage = Path.GetFileNameWithoutExtension(UploadResultSlip.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(UploadResultSlip.FileName);
                string MedicalImage = Path.GetFileNameWithoutExtension(UploadMedical.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(UploadMedical.FileName);

                String savePath = ConfigurationManager.AppSettings.Get("RegisterUploadPath");
                string uploadSavePath = Server.MapPath(savePath);

                String IcFullSavePath = uploadSavePath + IcNumberImage;
                String ResultFullSavePath = uploadSavePath + ResultSlipImage;
                String MedicalFullSavePath = uploadSavePath + MedicalImage;

                if (Directory.Exists(uploadSavePath))
                {
                    if (!String.IsNullOrEmpty(IcFullSavePath))
                    {
                        UploadNRIC.PostedFile.SaveAs(IcFullSavePath);
                        UploadResultSlip.PostedFile.SaveAs(ResultFullSavePath);
                        UploadMedical.PostedFile.SaveAs(MedicalFullSavePath);

                        string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                        con = new SqlConnection(strCon);
                        con.Open();

                        insertCmd = new SqlCommand("INSERT INTO Student_Programme_Register VALUES (@RegisterGUID, @StudentGUID, @ProgrammeGUID, @SessionGUID, @ProgrammeRegisterDate, @Status, @UploadIcImage, @UploadResult, @UploadMedical)", con);
                        insertCmd.Parameters.AddWithValue("@RegisterGUID", registerGUID);
                        insertCmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                        insertCmd.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                        insertCmd.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                        insertCmd.Parameters.AddWithValue("@ProgrammeRegisterDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        insertCmd.Parameters.AddWithValue("@Status", "Pending");
                        insertCmd.Parameters.AddWithValue("@UploadIcImage", IcNumberImage);
                        insertCmd.Parameters.AddWithValue("@UploadResult", ResultSlipImage);

                        if (UploadMedical.HasFile)
                        {
                            insertCmd.Parameters.AddWithValue("@UploadMedical", MedicalImage);
                        }
                        else
                        {
                            insertCmd.Parameters.AddWithValue("@UploadMedical", "none");
                        }

                        insertCmd.ExecuteNonQuery();

                        con.Dispose();
                        con.Close();

                        insertBool = true;
                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Not physical path.");
                    return false;
                }
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
            if (registerValidation())
            {
                if (insertRegister())
                {
                    clsFunction.DisplayAJAXMessage(this, "Your programme has been registered successfully! Please wait for the staff to approve it.");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to register.");
                    ddlProgrammCategory.SelectedIndex = -1;
                    ddlProgramme.SelectedIndex = -1;
                    ddlSession.SelectedIndex = -1; 
                    UploadNRIC.Dispose();
                    UploadResultSlip.Dispose();
                    UploadMedical.Dispose();

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlProgrammCategory.SelectedIndex = -1;
            ddlProgramme.SelectedIndex = -1;
            ddlSession.SelectedIndex = -1;
            UploadNRIC.Dispose();
            UploadResultSlip.Dispose();
            UploadMedical.Dispose();
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

            if (!(UploadNRIC.HasFile))
            {
                clsFunction.DisplayAJAXMessage(this, "Please upload the copy of your MyKad image.");
                return false;
            }

            if (!(UploadResultSlip.HasFile))
            {
                clsFunction.DisplayAJAXMessage(this, "Please upload your result slip. (exp: SPM, STPM, O-Level and etc.)");
                return false;
            }

            return true;
        }




        //can save into database, need to do validation
        //if i click on radiobutton list foundation, the rbl for diploma and degree will be disable (still not yet do)
        //and if i did not upload medical, it will still insert the new guid into database (need to fix) -done
        //do upload ic, and result slip like spm / o-level  - done
        //add icImage, resultSlip into Student_Programme_Register table to save the uploaded file - done

    }
}