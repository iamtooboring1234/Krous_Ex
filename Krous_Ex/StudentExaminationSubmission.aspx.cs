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
    public partial class StudentExaminationSubmission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["AnswerSubmitted"] != null)
                {
                    if (Session["AnswerSubmitted"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showFileUploadedSuccessfulAndAttendanceTaked(); ", true);
                        Session["AnswerSubmitted"] = null;
                    }
                }

                loadExaminationDetails();
                loadSubmittedFile();
            }
        }

        private void loadSubmittedFile()
        {
            try
            {

                SqlCommand loadInfoCmd = new SqlCommand();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    con.Open();

                    loadInfoCmd = new SqlCommand("SELECT * FROM ExamSubmission es RIGHT JOIN ExamTimetable et ON es.ExamTimetableGUID = et.ExamTimetableGUID WHERE StudentGUID = @StudentGUID AND es.ExamTimetableGUID = @ExamTimetableGUID ", con);

                    loadInfoCmd.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());
                    loadInfoCmd.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);

                    SqlDataReader dtrLoad = loadInfoCmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dtrLoad);

                    con.Close();

                    if (dt.Rows.Count != 0)
                    {
                        btnUnSubmit.Visible = true;
                        btnSubmit.Visible = false;
                        Panel1.Visible = false;
                        Panel2.Visible = true;
                        Panel3.Visible = false;
                        hlPreviousFile.Text = dt.Rows[0]["SubmissionFile"].ToString();
                        hlPreviousFile.Attributes["href"] = ResolveUrl("~/Uploads/ExaminationSubmissionFolder/" + dt.Rows[0]["ExamTimetableGUID"] + "/" + dt.Rows[0]["ExamSubmissionGUID"] + "/" + dt.Rows[0]["SubmissionFile"]);
                        lbPreviousFile.Attributes["href"] = ResolveUrl("~/Uploads/ExaminationSubmissionFolder/" + dt.Rows[0]["ExamTimetableGUID"] + "/" + dt.Rows[0]["ExamSubmissionGUID"] + "/" + dt.Rows[0]["SubmissionFile"]);
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        btnUnSubmit.Visible = false;
                        Panel1.Visible = true;
                        Panel2.Visible = false;
                    }

                }



            } catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadExaminationDetails()
        {
            try
            {
                Guid ExamTimetableGUID = Guid.Parse(Request.QueryString["ExamTimetableGUID"]);
                SqlCommand loadInfoCmd = new SqlCommand();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string sqlQuery = "SELECT * FROM ExamTimetable et ";
                sqlQuery += "LEFT JOIN ExamPreparation ep ON et.ExamTimetableGUID = ep.ExamTimetableGUID ";
                sqlQuery += "LEFT JOIN Course C ON et.CourseGUID = C.CourseGUID ";
                sqlQuery += "WHERE et.ExamTimetableGUID = @ExamTimetableGUID ";

                loadInfoCmd = new SqlCommand(sqlQuery, con);
                loadInfoCmd.Parameters.AddWithValue("@ExamTimetableGUID", ExamTimetableGUID);
                SqlDataReader dtrLoad = loadInfoCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                if (dt.Rows.Count != 0)
                {
                    lblCourseTitle.Text = dt.Rows[0]["CourseName"].ToString() + " (" + dt.Rows[0]["CourseAbbrv"].ToString() + ")";

                    if (!string.IsNullOrEmpty(dt.Rows[0]["QuestionPaper"].ToString()))
                    {
                        string icFilePath = "~/Uploads/ExaminationPreparationFolder/" + dt.Rows[0]["ExaminationPreparationGUID"].ToString() + "/" + dt.Rows[0]["QuestionPaper"].ToString();
                        hlQuestionPaper.Text = dt.Rows[0]["QuestionPaper"].ToString();
                        hlQuestionPaper.Attributes["href"] = ResolveUrl(icFilePath);
                        lbQuestionPaper.Attributes["href"] = ResolveUrl(icFilePath);
                        lbQuestionPaper.Attributes["download"] = dt.Rows[0]["QuestionPaper"].ToString();
                    }
                    else
                    {
                        lblNoQuestionPaper.Visible = true;
                        lbQuestionPaper.Visible = false;
                        lblNoQuestionPaper.Text = "No question paper is uploaded. Please ask invigilators for more information.";
                    }

                    if (!string.IsNullOrEmpty(dt.Rows[0]["AnswerSheet"].ToString()))
                    {
                        string icFilePath = "~/Uploads/ExaminationPreparationFolder/" + dt.Rows[0]["ExaminationPreparationGUID"].ToString() + "/" + dt.Rows[0]["AnswerSheet"].ToString();
                        hlAnswerSheet.Text = dt.Rows[0]["AnswerSheet"].ToString();
                        hlAnswerSheet.Attributes["href"] = ResolveUrl(icFilePath);
                        lbAnswerSheet.Attributes["href"] = ResolveUrl(icFilePath);
                        lbAnswerSheet.Attributes["download"] = dt.Rows[0]["AnswerSheet"].ToString();
                    }
                    else
                    {
                        lblNoQuestionPaper.Visible = true;
                        lbAnswerSheet.Visible = false;
                        lblNoQuestionPaper.Text = "No answer sheet is provided. Please ask invigilators for more information.";
                    }
                }
                con.Close();

                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    con.Open();

                    loadInfoCmd = new SqlCommand("SELECT * FROM ExamSubmission es RIGHT JOIN ExamTimetable et ON es.ExamTimetableGUID = et.ExamTimetableGUID WHERE StudentGUID = @StudentGUID AND es.ExamTimetableGUID = @ExamTimetableGUID ", con);

                    loadInfoCmd.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());
                    loadInfoCmd.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);

                    dtrLoad = loadInfoCmd.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dtrLoad);

                    con.Close();

                    if (dt.Rows.Count != 0)
                    {
                        if (DateTime.Parse(dt.Rows[0]["SubmissionDate"].ToString()) >= DateTime.Parse(dt.Rows[0]["ExamEndDateTime"].ToString()))
                        {
                            lblStatus.Text = "Submitted Late";
                            lblStatus.CssClass = "float-right text-danger";
                        } else
                        {
                            lblStatus.Text = "Submitted";
                            lblStatus.CssClass = "float-right text-success";
                        }

                    } else
                    {
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString)) {

                            con.Open();

                            loadInfoCmd = new SqlCommand("SELECT* FROM ExamTimetable et WHERE et.ExamTimetableGUID = @ExamTimetableGUID ", con);

                            loadInfoCmd.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);

                            dtrLoad = loadInfoCmd.ExecuteReader();
                            dt = new DataTable();
                            dt.Load(dtrLoad);

                            if (DateTime.Now <= DateTime.Parse(dt.Rows[0]["ExamEndDateTime"].ToString()))
                            {
                                lblStatus.Text = "Assigned";
                                lblStatus.CssClass = "float-right text-warning";
                            }
                            else if (DateTime.Now >= DateTime.Parse(dt.Rows[0]["ExamEndDateTime"].ToString()).AddMinutes(30))
                            {
                                lblStatus.Text = "Missing";
                                lblStatus.CssClass = "float-right text-danger";
                            }

                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (InsertExamFile())
            {
                Session["AnswerSubmitted"] = "Yes";
                Response.Redirect("StudentExaminationSubmission?ExamTimetableGUID=" + Request.QueryString["ExamTimetableGUID"]);
            } else
            {
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showDangerToast(); ", true);
            }
        }

        private bool InsertExamFile()
        {
            try
            {
                string submissionAnswerFileName = "ExamSubmissionAnswer_" + Path.GetFileName(FileUploadAnswer.FileName);
                string ExistingSubmissionFilePath = "";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM ExamSubmission WHERE ExamTimetableGUID = @ExamTimetableGUID AND StudentGUID = @StudentGUID ";

                    SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                    GetCommand.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);
                    GetCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());
                    SqlDataReader reader = GetCommand.ExecuteReader();

                    DataTable dtSubmission = new DataTable();
                    dtSubmission.Load(reader);
                    con.Close();

                    if (!string.IsNullOrEmpty(Path.GetFileName(FileUploadAnswer.FileName)))
                    {
                        if (dtSubmission.Rows.Count != 0)
                        {
                            ExistingSubmissionFilePath = "~/Uploads/ExaminationSubmissionFolder/" + dtSubmission.Rows[0]["ExamTimetableGUID"] + "/" + dtSubmission.Rows[0]["ExamSubmissionGUID"] + "/";

                            if (Directory.Exists(Server.MapPath(ExistingSubmissionFilePath)))
                            {
                                if (!String.IsNullOrEmpty(submissionAnswerFileName))
                                {
                                    con.Open();

                                    SqlCommand InsertCommand = new SqlCommand("UPDATE ExamSubmission SET SubmissionFile = @SubmissionFile WHERE ExamSubmissionGUID = @ExamSubmissionGUID ", con);

                                    InsertCommand.Parameters.AddWithValue("@ExamSubmissionGUID", dtSubmission.Rows[0]["ExamSubmissionGUID"]);
                                    InsertCommand.Parameters.AddWithValue("@SubmissionFile", submissionAnswerFileName);

                                    InsertCommand.ExecuteNonQuery();
                                    con.Close();

                                    DeleteFile(Server.MapPath(ExistingSubmissionFilePath));

                                    FileUploadAnswer.SaveAs(Server.MapPath(ExistingSubmissionFilePath) + submissionAnswerFileName);

                                    return true;
                                }
                                else
                                {
                                    Session["EmptyFile"] = "Yes";
                                    return false;
                                }
                            }
                            else
                            {
                                clsFunction.DisplayAJAXMessage(this, "Unable to find the directory or folder. Folder may be deleted or moved. Please contact KrousEx for support.");
                                return false;
                            }
                        }
                        else
                        {
                            Guid ExamSubmissionGUID = Guid.NewGuid();

                            string ExamFilePath = "~/Uploads/ExaminationSubmissionFolder/" + Request.QueryString["ExamTimetableGUID"] + "/" + ExamSubmissionGUID + "/";

                            if (!Directory.Exists(ExamFilePath))
                            {
                                Directory.CreateDirectory(Server.MapPath(ExamFilePath));

                                con.Open();
                                SqlCommand InsertCommand = new SqlCommand("INSERT INTO ExamSubmission VALUES(@ExamSubmissionGUID, @StudentGUID, @ExamTimetableGUID, @SubmissionDate, @SubmissionFile, 'Submitted') ", con);

                                InsertCommand.Parameters.AddWithValue("@ExamSubmissionGUID", ExamSubmissionGUID);
                                InsertCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());
                                InsertCommand.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);
                                InsertCommand.Parameters.AddWithValue("@SubmissionDate", DateTime.Now);
                                InsertCommand.Parameters.AddWithValue("@SubmissionFile", submissionAnswerFileName);

                                InsertCommand.ExecuteNonQuery();
                                con.Close();

                                con.Open();

                                InsertCommand = new SqlCommand("INSERT INTO  Attendance VALUES (newID(), @ExamTimetableGUID, @StudentGUID, 'Exam', 'Present', @AttendanceDateTime, @LastUpdatedDate)", con);

                                InsertCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());
                                InsertCommand.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);
                                InsertCommand.Parameters.AddWithValue("@AttendanceDateTime", DateTime.Now);
                                InsertCommand.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now);

                                InsertCommand.ExecuteNonQuery();
                                con.Close();

                                return true;
                            }
                            else
                            {
                                clsFunction.DisplayAJAXMessage(this, "Unable to create directory or folder. Please contact KrousEx for support.");
                                return false;
                            }
                        }
                    }
                    else
                    {

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }

        private void DeleteFile(string path)
        {
            // Delete all files from the Directory  
            foreach (string filename in Directory.GetFiles(path))
            {
                File.Delete(filename);
            }
        }

        protected void btnUnSubmit_Click(object sender, EventArgs e)
        {
            Panel3.Visible = true;
            Panel2.Visible = true;
            btnSubmit.Visible = true;
            btnUnSubmit.Visible = false;
        }
    }
}