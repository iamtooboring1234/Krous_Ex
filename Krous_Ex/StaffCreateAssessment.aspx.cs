using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffCreateAssessment : System.Web.UI.Page
    {
        Guid userGuid;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGuid = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (IsPostBack != true)
            {
                loadGroup();
                loadSession();
                txtDueDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
                SqlCommand GetCommand = new SqlCommand("SELECT GroupGUID, GroupNo FROM GROUPS GROUP BY GroupGUID, GroupNo ORDER BY GroupNo", con);
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
                clsFunction.DisplayAJAXMessage(this, "Error loading programme category.");
            }
        }

        protected bool createAssessment()
        {
            try
            {
                Guid imgGuid = Guid.NewGuid();
                SqlConnection con = new SqlConnection();
                SqlCommand createCmd = new SqlCommand();

                //upload file
                String filename = Path.GetFileName(UploadMaterials.FileName);
                String savePath = ConfigurationManager.AppSettings.Get("AssessmentUploadPath");
                string ProfileImgSavePath = Server.MapPath(savePath);
                String ProfileFullSavePath = ProfileImgSavePath + filename;

                if (Directory.Exists(ProfileImgSavePath))
                {
                    if (!String.IsNullOrEmpty(ProfileFullSavePath))
                    {
                        UploadMaterials.PostedFile.SaveAs(ProfileFullSavePath);
                        string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                        con = new SqlConnection(strCon);
                        con.Open();

                        //createCmd = new SqlCommand("INSERT INTO Assessment (AssessmentGUID, StaffGUID, )", con);
                        //createCmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        //createCmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                        //createCmd.Parameters.AddWithValue("@phoneNo", txtContact.Text);
                        //createCmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        //createCmd.Parameters.AddWithValue("@profileImage", imgName);
                        //createCmd.Parameters.AddWithValue("@LastUpdateInfo", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //createCmd.ExecuteNonQuery();

                        con.Dispose();
                        con.Close();


                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Not physical path.");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

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

