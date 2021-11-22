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
    public partial class StudentAssessmentSubmission : System.Web.UI.Page
    { 
        Guid AssessmentGUID;
        Guid userGuid;
        Guid SubmissionGUID = Guid.NewGuid();
        protected void Page_Load(object sender, EventArgs e)
        {

            userGuid = Guid.Parse(clsLogin.GetLoginUserGUID());
            if (IsPostBack != true)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["AssessmentGUID"]))
                {
                    AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]); //oh mei delet dao 
                    loadAssessmentDetails();
                }
                else
                {
                    btnSubmit.Visible = false;
                    btnBack.Visible = false;
                    btnCancel.Visible = false;
                }
            }
        }

        private void loadAssessmentDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);

                SqlCommand GetCommand = new SqlCommand("SELECT a.AssessmentGUID, s.StaffFullName, a.AssessmentTitle, a.AssessmentDesc, a.UploadMaterials, CONVERT(VARCHAR, a.CreatedDate, 100) as CreatedDate, CONVERT(VARCHAR, a.DueDate, 100) as DueDate FROM Assessment a LEFT JOIN Staff s ON a.StaffGUID = s.StaffGUID WHERE a.AssessmentGUID = @AssessmentGUID ", con);
                GetCommand.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtAssessment = new DataTable();
                dtAssessment.Load(reader);

                SqlCommand selectStatus = new SqlCommand("SELECT sub.SubmissionGUID, CONVERT(VARCHAR, sub.SubmissionDate, 100) as SubmissionDate, sub.SubmissionFile, sub.SubmissionStatus FROM Submission sub, Assessment a, Student s WHERE sub.AssessmentGUID = a.AssessmentGUID AND sub.StudentGUID = s.StudentGUID AND a.AssessmentGUID = @AssessmentGUID AND s.StudentGUID = @StudentGUID", con);
                selectStatus.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                selectStatus.Parameters.AddWithValue("@StudentGUID", userGuid);

                SqlDataReader dtrStatus = selectStatus.ExecuteReader();
                DataTable dtStatus = new DataTable();
                dtStatus.Load(dtrStatus);
                con.Close();

                DateTime submissionDate = DateTime.Now;
                DateTime dueDate = DateTime.Parse(dtAssessment.Rows[0]["DueDate"].ToString());

                if (dtAssessment.Rows.Count != 0)
                {
                    lblAssessmentTitle.Text = dtAssessment.Rows[0]["AssessmentTitle"].ToString();
                    lblStaffName.Text = dtAssessment.Rows[0]["StaffFullName"].ToString();
                    lblAssessmentDesc.Text = dtAssessment.Rows[0]["AssessmentDesc"].ToString();
                    lblCreatedDate.Text = "Created On : " + dtAssessment.Rows[0]["CreatedDate"].ToString();
                    lblDueDate.Text = "Due " + dtAssessment.Rows[0]["DueDate"].ToString();

                    string assessmentFile = "~/Uploads/AssessmentFolder/" + Request.QueryString["AssessmentGUID"] + "/" + dtAssessment.Rows[0]["UploadMaterials"].ToString();
                    hlAssessmentFile.Text = dtAssessment.Rows[0]["UploadMaterials"].ToString();
                    hlAssessmentFile.Attributes["href"] = ResolveUrl(assessmentFile);
                    lbAssFileDownload.Attributes["href"] = ResolveUrl(assessmentFile);
                    lbAssFileDownload.Attributes["download"] = dtAssessment.Rows[0]["UploadMaterials"].ToString();
                }

                //Display submission details if the student already submit
                if (dtStatus.Rows.Count != 0)
                {
                    btnSubmit.Visible = false;
                    btnUnSubmit.Visible = true;
                    lblSubmitDate.Visible = true;
                    btnCancel.Visible = false;

                    string status = dtStatus.Rows[0]["SubmissionStatus"].ToString();
                    string submissionGUID = dtStatus.Rows[0]["SubmissionGUID"].ToString();

                    if (status != "" && status != "Missing")
                    {
                        AsyncFileUpload1.Visible = false;
                        hlSubmitFile.Visible = true;
                        lbDownloadFile.Visible = true;
                        lblUploaded.Visible = true;

                        string submitFile = "~/Uploads/StudentSubmission/" + submissionGUID + "/" + dtStatus.Rows[0]["SubmissionFile"].ToString();
                        hlSubmitFile.Text = dtStatus.Rows[0]["SubmissionFile"].ToString();
                        hlSubmitFile.Attributes["href"] = ResolveUrl(submitFile);
                        lbDownloadFile.Attributes["href"] = ResolveUrl(submitFile);
                        lbDownloadFile.Attributes["download"] = dtStatus.Rows[0]["SubmissionFile"].ToString();

                        lblSubmitDate.Text = "Submitted : " + dtStatus.Rows[0]["SubmissionDate"].ToString();
                        if (status == "Submitted")
                        {
                            lblStatus.Text = dtStatus.Rows[0]["SubmissionStatus"].ToString();
                            lblStatus.ForeColor = System.Drawing.Color.Silver;
                        }
                        else if (status == "Submitted Late")
                        {
                            lblStatus.Text = dtStatus.Rows[0]["SubmissionStatus"].ToString();
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                    if(hlSubmitFile.Text != "")
                    {
                        lblUploaded.Visible = true;
                        lblUploadHere.Visible = false;
                    }
                }
                else
                {  
                    lblStatus.Text = "Assigned";
                    lblUploadHere.Visible = true;
                    lblStatus.ForeColor = System.Drawing.Color.LimeGreen;
                    btnSubmit.Visible = true;
                    btnBack.Visible = true;
                    lblSubmitDate.Visible = false;
                    btnCancel.Visible = false;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }


        private bool submitAssessment()
        {
            try
            {
                AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);
                SqlCommand submitCmd = new SqlCommand();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand selectCmd = new SqlCommand("SELECT DueDate FROM Assessment WHERE AssessmentGUID = @AssessmentGUID", con);
                selectCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                SqlDataReader reader = selectCmd.ExecuteReader();
                DataTable dtDueDate = new DataTable();
                dtDueDate.Load(reader);

                string filename = "Submission_" + Path.GetFileName(AsyncFileUpload1.FileName);
                string folderName = "~/Uploads/StudentSubmission/" + SubmissionGUID + "/";
                DateTime submissionDate = DateTime.Now;
                DateTime dueDate = DateTime.Parse(dtDueDate.Rows[0]["DueDate"].ToString());

                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));
                }
                if (filename != "")
                {
                    submitCmd = new SqlCommand("INSERT INTO Submission (SubmissionGUID, StudentGUID, AssessmentGUID, SubmissionDate, SubmissionFile, SubmissionStatus) VALUES (@SubmissionGUID, @StudentGUID, @AssessmentGUID, @SubmissionDate, @SubmissionFile, @SubmissionStatus)", con);
                    submitCmd.Parameters.AddWithValue("@SubmissionGUID", SubmissionGUID);
                    submitCmd.Parameters.AddWithValue("@StudentGUID", userGuid);
                    submitCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                    submitCmd.Parameters.AddWithValue("@SubmissionDate", DateTime.Now);
                    submitCmd.Parameters.AddWithValue("@SubmissionFile", filename);

                    //late and has submitted (submit late)
                    if (submissionDate > dueDate)
                    {
                        if (AsyncFileUpload1.HasFile)
                        {
                            submitCmd.Parameters.AddWithValue("@SubmissionStatus", "Submitted Late");
                            AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + filename);
                        }
                    }

                    //submit on time or before due date and has file (Submitted)
                    if (submissionDate == dueDate || submissionDate < dueDate)
                    {
                        if (AsyncFileUpload1.HasFile)
                        {
                            submitCmd.Parameters.AddWithValue("@SubmissionStatus", "Submitted");
                            AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + filename);
                        }
                    }

                    
                    submitCmd.ExecuteNonQuery();

                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (submitAssessment())
            {

                Session["SubmitAssessment"] = "Yes";
                loadAssessmentDetails();
                btnSubmit.Visible = false;
                btnUnSubmit.Visible = true;
            }
            else
            {
                Session["SubmitAssessment"] = "No"; 
                //clsFunction.DisplayAJAXMessage(this, "Unable to submit your assessment!");
                loadAssessmentDetails();
            }

            if (Session["SubmitAssessment"] != null)
            {
                if (Session["SubmitAssessment"].ToString() == "Yes")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAssessmentSubmitSuccessToast(); ", true);
                    Session["SubmitAssessment"] = null;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUnableSubmitDangerToast(); ", true);
                    Session["SubmitAssessment"] = null;
                }
            }
        }

        private bool unsubmitAssessment()
        { 
            try
            {
                AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);
                SqlCommand unsubmitCmd = new SqlCommand();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand selectDDCmd = new SqlCommand("SELECT DueDate FROM Assessment WHERE AssessmentGUID = @AssessmentGUID", con);
                selectDDCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                SqlDataReader reader = selectDDCmd.ExecuteReader();
                DataTable dtDueDate = new DataTable();
                dtDueDate.Load(reader);

                SqlCommand selectSubmissionGUID = new SqlCommand("SELECT s.SubmissionGUID FROM Submission s LEFT JOIN Assessment a ON s.AssessmentGUID = a.AssessmentGUID WHERE a.AssessmentGUID = @AssessmentGUID", con);
                selectSubmissionGUID.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                SqlDataReader dtrGUID = selectSubmissionGUID.ExecuteReader();
                DataTable dtGUID = new DataTable();
                dtGUID.Load(dtrGUID);
                string submissionGUID = dtGUID.Rows[0]["SubmissionGUID"].ToString();

                string filename = "Submission_" + Path.GetFileName(AsyncFileUpload1.FileName);
                string folderName = "~/Uploads/StudentSubmission/" + submissionGUID + "/";
                DateTime ResubmissionDate = DateTime.Now;
                DateTime DueDate = DateTime.Parse(dtDueDate.Rows[0]["DueDate"].ToString());

                //when click on resubmit, the old file will be deleted
                File.Delete(Server.MapPath(folderName + "/" + hlSubmitFile.Text));

                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));
                }
                if (filename != "")
                {
                    unsubmitCmd = new SqlCommand("UPDATE Submission SET SubmissionStatus = @SubmissionStatus, SubmissionDate = @SubmissionDate, SubmissionFile = @SubmissionFile WHERE SubmissionGUID = @SubmissionGUID", con);
                    unsubmitCmd.Parameters.AddWithValue("@SubmissionGUID", submissionGUID);
                    unsubmitCmd.Parameters.AddWithValue("@SubmissionDate", DateTime.Now);
                    unsubmitCmd.Parameters.AddWithValue("@SubmissionFile", filename);

                    //late and has resubmitted (submit late)
                    if (ResubmissionDate > DueDate)
                    {
                        if (AsyncFileUpload1.HasFile)
                        {
                            unsubmitCmd.Parameters.AddWithValue("@SubmissionStatus", "Submitted Late");
                            AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + filename);
                        }
                    }

                    //resubmit on time or before due date and has file (Submitted)
                    if (ResubmissionDate == DueDate || ResubmissionDate < DueDate)
                    {
                        if (AsyncFileUpload1.HasFile)
                        {
                            unsubmitCmd.Parameters.AddWithValue("@SubmissionStatus", "Submitted");
                            AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + filename);
                        }
                    }
                    
                    unsubmitCmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void btnUnSubmit_Click(object sender, EventArgs e)
        {
            hlSubmitFile.Visible = false;
            lbDownloadFile.Visible = false;
            AsyncFileUpload1.Visible = true;
            lblStatus.Visible = false;
            lblSubmitDate.Visible = false;
            btnResubmit.Visible = true;
            btnUnSubmit.Visible = false;
            lblUploadHere.Visible = true;
            lblUploaded.Visible = false;
            btnCancel.Visible = true;
            btnBack.Visible = false;
        }

        protected void btnResubmit_Click(object sender, EventArgs e)
        {
            if (unsubmitAssessment())
            {
                Session["ResubmitAssessment"] = "Yes";
                loadAssessmentDetails();
                btnSubmit.Visible = false;
                btnUnSubmit.Visible = true;
                btnResubmit.Visible = false;
                lblStatus.Visible = true;
                btnCancel.Visible = false;
                btnBack.Visible = true;
            } else
            {
                Session["ResubmitAssessment"] = "No";
            }

            if (Session["ResubmitAssessment"] != null)
            {
                if (Session["ResubmitAssessment"].ToString() == "Yes")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAssessmentReSubmitSuccessToast(); ", true);
                    Session["ResubmitAssessment"] = null;
                } 
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUnableResubmitDangerToast(); ", true);
                    Session["ResubmitAssessment"] = null;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentViewAssessment");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            loadAssessmentDetails();
            btnSubmit.Visible = false;
            btnUnSubmit.Visible = true;
            lblStatus.Visible = true;
            btnBack.Visible = true;
            btnCancel.Visible = false;
            btnResubmit.Visible = false;
        }
    }
}