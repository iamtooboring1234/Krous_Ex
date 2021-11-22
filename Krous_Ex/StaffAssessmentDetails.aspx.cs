using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffAssessmentDetails : System.Web.UI.Page
    {
        Guid AssessmentGUID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UpdateAssessment"] != null)
            {
                if (Session["UpdateAssessment"].ToString() == "Yes")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUpdateSuccessToast(); ", true);
                    Session["UpdateAssessment"] = null;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showUnableUpdateDangerToast(); ", true);
                    Session["UpdateAssessment"] = null;
                }
            }

            if (IsPostBack != true)
            {
                dateTimePicker.Visible = false;
                AsyncFileUpload1.Visible = false;
                lbRemove.Visible = false;

                if (!String.IsNullOrEmpty(Request.QueryString["AssessmentGUID"]))
                {
                    AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);
                    loadAssessmentDetails();
                    loadSubmissionList();
                    btnBack.Visible = false;
                    btnUpdate.Visible = false;
                    lbMenu.Visible = true;
                }
                else
                {
                    btnBack.Visible = false;
                    btnUpdate.Visible = false;
                }
            }
        }

        private void loadSubmissionList()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);

                SqlCommand submissionCmd = new SqlCommand("SELECT s.SubmissionGUID, s.SubmissionFile, st.StudentFullName, s.SubmissionDate, s.SubmissionStatus FROM Submission s LEFT JOIN Student st ON s.StudentGUID = st.StudentGUID WHERE AssessmentGUID = @AssessmentGUID ORDER BY st.StudentFullName", con);
                submissionCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                SqlDataReader dtrSubmission = submissionCmd.ExecuteReader();
                DataTable dtSub = new DataTable();
                dtSub.Load(dtrSubmission);

                if (dtSub.Rows.Count != 0)
                {
                    gvSubmissionList.DataSource = dtSub;
                    gvSubmissionList.DataBind();
                    gvSubmissionList.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvSubmissionList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }

        }

        protected void gvSubmissionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                LinkButton lbDownload = e.Row.FindControl("lbDownload") as LinkButton;

                string file = DataBinder.Eval(e.Row.DataItem, "SubmissionFile").ToString();

                string getExtension = Path.GetExtension(file);
                if(getExtension == ".docx")
                {
                    hyperLink.Visible = false;
                }

                if (hyperLink != null)
                {
                    string submitFilePath = "~/Uploads/StudentSubmission/" + DataBinder.Eval(e.Row.DataItem, "SubmissionGUID") + "/" + DataBinder.Eval(e.Row.DataItem, "SubmissionFile");
                    hyperLink.Attributes["href"] = ResolveUrl(submitFilePath);
                    lbDownload.Attributes["href"] = ResolveUrl(submitFilePath);
                    lbDownload.Attributes["download"] = DataBinder.Eval(e.Row.DataItem, "SubmissionFile").ToString();

                    if (DataBinder.Eval(e.Row.DataItem, "SubmissionFile").ToString() == "")
                    {
                        hyperLink.Visible = false;
                        lbDownload.Visible = false;
                    }
                }
            }
        }


        protected void loadAssessmentDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadInfoCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);
                loadInfoCmd = new SqlCommand("SELECT a.AssessmentTitle, a.AssessmentDesc, CONVERT(VARCHAR, a.CreatedDate, 100) as CreatedDate, CONVERT(VARCHAR, a.DueDate, 100) as DueDate,  CONVERT(VARCHAR, a.LastUpdateDate, 100) as LastUpdateDate, a.UploadMaterials, g.GroupNo, s.SessionYear, s.SessionMonth, st.StaffFullName FROM Assessment a LEFT JOIN[Group] g ON a.GroupGUID = g.GroupGUID LEFT JOIN[Session] s ON a.SessionGUID = s.SessionGUID LEFT JOIN Staff st ON a.StaffGUID = st.StaffGUID WHERE AssessmentGUID = @AssessmentGUID", con);
                loadInfoCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                SqlDataReader dtrLoad = loadInfoCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                if (dt.Rows.Count != 0)
                {
                    lblAssessmentTitle.Text = dt.Rows[0]["AssessmentTitle"].ToString();
                    lblAssessmentDesc.Text = dt.Rows[0]["AssessmentDesc"].ToString();
                    lblCreatedDate.Text = dt.Rows[0]["CreatedDate"].ToString();
                    lblAssessmentDueDate.Text = dt.Rows[0]["DueDate"].ToString();
                    lblCreatedBy.Text = dt.Rows[0]["StaffFullName"].ToString();
                    lblGroupSession.Text = "Group " + dt.Rows[0]["GroupNo"].ToString() + " (" + dt.Rows[0]["SessionYear"].ToString() + dt.Rows[0]["SessionMonth"].ToString().PadLeft(2, '0') + ")";

                    if (dt.Rows[0]["LastUpdateDate"].ToString() == "")
                    {
                        lblLastUpdate.Text = "No Update Before";
                    }
                    else
                    {
                        lblLastUpdate.Text = dt.Rows[0]["LastUpdateDate"].ToString();
                    }

                    if(dt.Rows[0]["UploadMaterials"].ToString() == "none")
                    {
                        lblNoFile.Visible = true;
                        lbDownload.Visible = false;
                        lblNoFile.Text = "No file attached for this assessment.";
                    }
                    else
                    {
                        //view and download
                        lblNoFile.Visible = false;
                        lbDownload.Visible = true;
                        string FilePath = "~/Uploads/AssessmentFolder/" + Request.QueryString["AssessmentGUID"] + "/" + dt.Rows[0]["UploadMaterials"].ToString();
                        hlFile.Text = dt.Rows[0]["UploadMaterials"].ToString();
                        hlFile.Attributes["href"] = ResolveUrl(FilePath);
                        lbDownload.Attributes["href"] = ResolveUrl(FilePath);
                        lbDownload.Attributes["download"] = dt.Rows[0]["UploadMaterials"].ToString();

                    }


                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkUpdateValidation())
            {
                if (updateAssessment())
                {
                    Session["UpdateAssessment"] = "Yes";
                    Response.Redirect("StaffAssessmentDetails?AssessmentGUID=" + Request.QueryString["AssessmentGUID"]);
                }
                else
                {
                    Session["UpdateAssessment"] = "No";
                }
            }
        }

        private bool updateAssessment()
        {
            try
            {
                lbRemove.Visible = true;
                lbDownload.Visible = false;
                AsyncFileUpload1.Visible = false;
                SqlConnection con = new SqlConnection();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();
                AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);
                string filename = "Assessment_" + Path.GetFileName(AsyncFileUpload1.FileName);
                string folderName = "~/Uploads/AssessmentFolder/" + AssessmentGUID + "/";

                SqlCommand updateCmd = new SqlCommand("UPDATE Assessment SET AssessmentTitle = @AssessmentTitle, AssessmentDesc = @AssessmentDesc, DueDate = @DueDate, UploadMaterials = @UploadMaterials, LastUpdateDate = @LastUpdateDate WHERE AssessmentGUID = @AssessmentGUID ", con);
                updateCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                updateCmd.Parameters.AddWithValue("@AssessmentTitle", txtAssessmentTitle.Text);
                updateCmd.Parameters.AddWithValue("@AssessmentDesc", txtAssessmentDesc.Text);

                if (txtDueDate.Text != "")
                {
                    updateCmd.Parameters.AddWithValue("@DueDate", DateTime.ParseExact(txtDueDate.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                }
                else
                {
                    updateCmd.Parameters.AddWithValue("@DueDate", DateTime.Parse(lblAssessmentDueDate.Text));
                }

                updateCmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));
                }

                if (!(AsyncFileUpload1.HasFile)) //if did not update file, update the old file
                {
                    updateCmd.Parameters.AddWithValue("@UploadMaterials", hlFile.Text);
                }
                else
                {
                    if(hlFile.Text == "none" || hlFile.Text == "")
                    {
                        updateCmd.Parameters.AddWithValue("@UploadMaterials", filename);
                        AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + filename);
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath(folderName + "/" + hlFile.Text)); //delete here
                        updateCmd.Parameters.AddWithValue("@UploadMaterials", filename);
                        AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + filename);
                    }
                }

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

        protected void btnBack_Click(object sender, EventArgs e)
        {
           
            Page.Response.Redirect(Page.Request.Url.ToString(), false);
            //Response.Redirect("StaffAssessmentListings");
        }

        protected void lbModify_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
            con = new SqlConnection(strCon);
            con.Open();
            AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);
            //check got file or not
            SqlCommand loadfile = new SqlCommand("SELECT a.UploadMaterials FROM Assessment a  WHERE AssessmentGUID = @AssessmentGUID", con);
            loadfile.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
            SqlDataReader dtrLoadFile = loadfile.ExecuteReader();
            DataTable dtFile = new DataTable();
            dtFile.Load(dtrLoadFile);

            if (dtFile.Rows[0]["UploadMaterials"].ToString() == "none")
            {
                lblAssessmentTitle.Visible = false;
                txtAssessmentTitle.Visible = true;
                txtAssessmentTitle.Text = lblAssessmentTitle.Text;
                lblAssessmentDesc.Visible = false;
                txtAssessmentDesc.Visible = true;
                txtAssessmentDesc.Text = lblAssessmentDesc.Text;
                lblAssessmentDueDate.Visible = false;
                txtDueDate.Text = DateTime.Parse(lblAssessmentDueDate.Text).ToString("dd/MM/yyyy HH:mm");
                dateTimePicker.Visible = true;
                btnUpdate.Visible = true;
                btnBack.Visible = true;
                btnBackListing.Visible = false;
                AsyncFileUpload1.Visible = true;
                lbDownload.Visible = false;
                lblNoFile.Visible = false;
            }
            else
            {
                lblAssessmentTitle.Visible = false;
                txtAssessmentTitle.Visible = true;
                txtAssessmentTitle.Text = lblAssessmentTitle.Text;
                lblAssessmentDesc.Visible = false;
                txtAssessmentDesc.Visible = true;
                txtAssessmentDesc.Text = lblAssessmentDesc.Text;
                lblAssessmentDueDate.Visible = false;
                txtDueDate.Text = DateTime.Parse(lblAssessmentDueDate.Text).ToString("dd/MM/yyyy HH:mm");
                dateTimePicker.Visible = true;
                lbDownload.Visible = false;
                lbRemove.Visible = true;
                btnUpdate.Visible = true;
                lbMenu.Visible = false;
                btnBack.Visible = true;
                btnBackListing.Visible = false;

            }
        }

        protected void lbDelete_Click(object sender, EventArgs e) 
        {
            if (deleteAssessment())
            {
                Session["DeleteAssessment"] = "Yes";
                Response.Redirect("StaffAssessmentListings");
            }
            else
            {
                Session["DeleteAssessment"] = "No";
            }
        }

        private bool deleteAssessment()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                AssessmentGUID = Guid.Parse(Request.QueryString["AssessmentGUID"]);

                SqlCommand deleteCmd = new SqlCommand("DELETE FROM Assessment WHERE AssessmentGUID = @AssessmentGUID", con);
                deleteCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                deleteCmd.ExecuteNonQuery();

                //delete the assessment file
                string folderName = "~/Uploads/AssessmentFolder/" + AssessmentGUID;

                System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(folderName + "/"));
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }
                //delete assessment folder (if the file inside the folder is empty, then delete the folder also) 
                if (!Directory.Exists(folderName + "/"))
                {
                    System.IO.Directory.Delete(Server.MapPath(folderName));
                }

                //delete submission, if got student submit
                SqlCommand getStudSub = new SqlCommand("SELECT * FROM Submission WHERE AssessmentGUID = @AssessmentGUID", con);
                getStudSub.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                SqlDataReader dtrStudSub = getStudSub.ExecuteReader();
                DataTable dtStudSub = new DataTable();
                dtStudSub.Load(dtrStudSub);

                if(dtStudSub.Rows.Count != 0)
                {
                    Guid submissionGUID = Guid.Parse(dtStudSub.Rows[0]["SubmissionGUID"].ToString());
                    string submissionFolder = "~/Uploads/StudentSubmission/" + submissionGUID;

                    SqlCommand deleteSubmission = new SqlCommand("DELETE FROM Submission WHERE AssessmentGUID = @AssessmentGUID", con);
                    deleteSubmission.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                    deleteSubmission.ExecuteNonQuery();

                    System.IO.DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath(submissionFolder + "/"));
                    foreach (FileInfo file in dInfo.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in dInfo.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                    //delete assessment folder (if the file inside the folder is empty, then delete the folder also
                    if (!Directory.Exists(submissionFolder + "/"))
                    {
                        System.IO.Directory.Delete(Server.MapPath(submissionFolder));
                    }

                }

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void lbRemove_Click(object sender, EventArgs e)
        {
            hlFile.Visible = false;
            lbRemove.Visible = false;
            AsyncFileUpload1.Visible = true;

        }

        private bool checkUpdateValidation()
        {
            if(txtAssessmentTitle.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the assessment title.");
                txtAssessmentTitle.Focus();
                return false;
            }

            if(txtAssessmentDesc.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the assessment description.");
                txtAssessmentDesc.Focus();
                return false;
            }

            if(txtDueDate.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the assessment due date.");
                txtDueDate  .Focus();
                return false;
            }
            return true;
        }

        protected void btnBackListing_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffAssessmentListings");
        }


    }   
}