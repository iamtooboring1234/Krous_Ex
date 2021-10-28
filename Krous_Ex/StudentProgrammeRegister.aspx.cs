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
                    loadDiploma();
                    loadFoundation();
                    loadDegree();
                    loadSession();
                }
              
            }
        }

        protected void loadFoundation()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT p.ProgrammeGUID, p.ProgrammeName FROM Programme p, ProgrammeCourse pc WHERE pc.ProgrammeGUID = p.ProgrammeGUID AND p.ProgrammeCategory = 'Foundation' GROUP BY p.ProgrammeGUID, p.ProgrammeName ORDER BY p.ProgrammeName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                rblFoundation.DataSource = ds;
                rblFoundation.DataTextField = "ProgrammeName";
                rblFoundation.DataValueField = "ProgrammeGUID";
                rblFoundation.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadDiploma()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT p.ProgrammeGUID, p.ProgrammeName FROM Programme p, ProgrammeCourse pc WHERE pc.ProgrammeGUID = p.ProgrammeGUID AND p.ProgrammeCategory = 'Diploma' GROUP BY p.ProgrammeGUID, p.ProgrammeName ORDER BY p.ProgrammeName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                rblDiploma.DataSource = ds;
                rblDiploma.DataTextField = "ProgrammeName";
                rblDiploma.DataValueField = "ProgrammeGUID";
                rblDiploma.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadDegree()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT p.ProgrammeGUID, p.ProgrammeName FROM Programme p, ProgrammeCourse pc WHERE pc.ProgrammeGUID = p.ProgrammeGUID AND p.ProgrammeCategory = 'Bachelor Degree' GROUP BY p.ProgrammeGUID, p.ProgrammeName ORDER BY p.ProgrammeName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                rblDegree.DataSource = ds;
                rblDegree.DataTextField = "ProgrammeName";
                rblDegree.DataValueField = "ProgrammeGUID";
                rblDegree.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
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
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM session WHERE DATEADD(Day, 14, GETDATE()) < convert(varchar, concat(sessionYear, '-', SessionMonth, '-', DAY(getdate())), 22) ORDER BY SessionYear, SessionMonth ", con);
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
                string IcNumberImage= Path.GetFileNameWithoutExtension(UploadNRIC.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(UploadNRIC.FileName);
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

                        if(rblFoundation.SelectedValue != null)
                        {
                            insertCmd = new SqlCommand("INSERT INTO Student_Programme_Register VALUES (@RegisterGUID, @StudentGUID, @ProgrammeGUID, @SessionGUID, @CourseRegisterDate, @Status, @UploadIcImage, @UploadResult, @UploadMedical)", con);
                            insertCmd.Parameters.AddWithValue("@RegisterGUID", registerGUID);
                            insertCmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                            insertCmd.Parameters.AddWithValue("@ProgrammeGUID", rblFoundation.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@CourseRegisterDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            insertCmd.Parameters.AddWithValue("@Status", "Pending");
                            insertCmd.Parameters.AddWithValue("@UploadIcImage", IcNumberImage);
                            insertCmd.Parameters.AddWithValue("@UploadResult", ResultSlipImage);
                        }
                        else if(rblDiploma.SelectedValue != null)
                        {
                            insertCmd = new SqlCommand("INSERT INTO Student_Programme_Register VALUES (@RegisterGUID, @StudentGUID, @ProgrammeGUID, @SessionGUID, @CourseRegisterDate, @Status, @UploadIcImage, @UploadResult, @UploadMedical)", con);
                            insertCmd.Parameters.AddWithValue("@RegisterGUID", registerGUID);
                            insertCmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                            insertCmd.Parameters.AddWithValue("@ProgrammeGUID", rblDiploma.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@CourseRegisterDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            insertCmd.Parameters.AddWithValue("@Status", "Pending");
                            insertCmd.Parameters.AddWithValue("@UploadIcImage", IcNumberImage);
                            insertCmd.Parameters.AddWithValue("@UploadResult", ResultSlipImage);
                        }
                        else if(rblDegree.SelectedValue != null)
                        {
                            insertCmd = new SqlCommand("INSERT INTO Student_Programme_Register VALUES (@RegisterGUID, @StudentGUID, @ProgrammeGUID, @SessionGUID, @CourseRegisterDate, @Status, @UploadIcImage, @UploadResult, @UploadMedical)", con);
                            insertCmd.Parameters.AddWithValue("@RegisterGUID", registerGUID);
                            insertCmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                            insertCmd.Parameters.AddWithValue("@ProgrammeGUID", rblDegree.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                            insertCmd.Parameters.AddWithValue("@CourseRegisterDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            insertCmd.Parameters.AddWithValue("@Status", "Pending");
                            insertCmd.Parameters.AddWithValue("@UploadIcImage", IcNumberImage);
                            insertCmd.Parameters.AddWithValue("@UploadResult", ResultSlipImage);
                        }
                  
                        if(UploadMedical.HasFile)
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
            if (insertRegister())
            {
                clsFunction.DisplayAJAXMessage(this, "insert successfully.");
            }
        }



        protected void rblFoundation_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (rblFoundation.SelectedValue != "")
            {
                rblDegree.Enabled = false;
                rblDiploma.Enabled = false;
            }
        }

        protected void rblDiploma_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (rblDiploma.SelectedValue != "")
            {
                rblDegree.Enabled = false;
                rblFoundation.Enabled = false;
            }

        }

        protected void rblDegree_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (rblDegree.SelectedValue != "")
            {
                rblFoundation.Enabled = false;
                rblDiploma.Enabled = false;
            }

        }

      


        //can save into database, need to do validation
        //if i click on radiobutton list foundation, the rbl for diploma and degree will be disable (still not yet do)
        //and if i did not upload medical, it will still insert the new guid into database (need to fix) -done
        //do upload ic, and result slip like spm / o-level  - done
        //add icImage, resultSlip into Student_Programme_Register table to save the uploaded file - done

    }
}