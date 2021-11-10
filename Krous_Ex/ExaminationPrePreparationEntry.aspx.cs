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
    public partial class ExaminationPrePreparationEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["InsertExamFile"] != null)
                {
                    if (Session["InsertExamFile"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                        Session["InsertExamFile"] = null;
                    }
                }

                if (!string.IsNullOrEmpty(Request.QueryString["ExaminationPreparationGUID"]))
                {
                    ddlExamination.Enabled = false;
                    loadSession();
                    loadExistingExamCourse();
                }
                else
                {
                    loadSession();
                    loadExamCourse();
                }
            }
        }

        private void loadSession()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT S.SessionGUID, S.SessionMonth, S.SessionYear FROM AcademicCalender A, Session S WHERE S.SessionGUID = A.SessionGUID AND GetDate() BETWEEN A.SemesterStartDate AND A.SemesterEndDate;", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSession = new DataTable();
                dtSession.Load(reader);
                con.Close();

                if (dtSession.Rows.Count != 0)
                {
                    txtSession.Text = dtSession.Rows[0]["SessionYear"].ToString() + dtSession.Rows[0]["SessionMonth"].ToString().PadLeft(2, '0');
                    hdSession.Value = dtSession.Rows[0]["SessionGUID"].ToString();
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadExistingExamCourse()
        {
            try
            {
                ddlExamination.Items.Clear();

                ListItem oList = new ListItem();

                string sqlQuery = "SELECT * FROM ExamTimetable et, Course C, ExamPreparation ep WHERE et.CourseGUID = C.CourseGUID AND et.ExamTimetableGUID = ep.ExamTimetableGUID AND SessionGUID = @SessionGUID AND " +
                    " ExaminationPreparationGUID = @ExaminationPreparationGUID" +
                    " ORDER BY C.CourseName ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);
                GetCommand.Parameters.AddWithValue("@ExaminationPreparationGUID", Request.QueryString["ExaminationPreparationGUID"]);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtExamCourse = new DataTable();
                dtExamCourse.Load(reader);
                con.Close();

                if (dtExamCourse.Rows.Count != 0)
                {

                    oList = new ListItem();
                    oList.Text = dtExamCourse.Rows[0]["CourseName"].ToString() + " (" + dtExamCourse.Rows[0]["CourseAbbrv"].ToString() + ")";
                    oList.Value = dtExamCourse.Rows[0]["ExamTimetableGUID"].ToString();
                    ddlExamination.Items.Add(oList);

                    txtExamStartTime.Text = dtExamCourse.Rows[0]["ExamStartDateTime"].ToString();
                    txtExamEndTime.Text = dtExamCourse.Rows[0]["ExamEndDateTime"].ToString();

                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadExamCourse()
        {
            try
            {
                ddlExamination.Items.Clear();

                ListItem oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlExamination.Items.Add(oList);

                string sqlQuery = "SELECT * FROM ExamTimetable et, Course C WHERE et.CourseGUID = C.CourseGUID AND SessionGUID = @SessionGUID ORDER BY C.CourseName ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtExamCourse = new DataTable();
                dtExamCourse.Load(reader);
                con.Close();

                if (dtExamCourse.Rows.Count != 0)
                {
                    for (int i = 0; i <= dtExamCourse.Rows.Count - 1; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dtExamCourse.Rows[i]["CourseName"].ToString() + " (" + dtExamCourse.Rows[i]["CourseAbbrv"].ToString() + ")";
                        oList.Value = dtExamCourse.Rows[i]["ExamTimetableGUID"].ToString();
                        ddlExamination.Items.Add(oList);
                    }
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void ddlExamination_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery = "SELECT * FROM ExamTimetable et WHERE ExamTimetableGUID = @ExamTimetableGUID ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@ExamTimetableGUID", ddlExamination.SelectedValue);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtExamCourse = new DataTable();
                dtExamCourse.Load(reader);
                con.Close();

                if (dtExamCourse.Rows.Count != 0)
                {
                    txtExamStartTime.Text = dtExamCourse.Rows[0]["ExamStartDateTime"].ToString();
                    txtExamEndTime.Text = dtExamCourse.Rows[0]["ExamEndDateTime"].ToString();
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (InsertExamFile())
            {
                Session["InsertExamFile"] = "Yes";
                Response.Redirect("ExaminationPrePreparationEntry");
            } else
            {
                if (Session["EmptyFile"] != null)
                {
                    if (Session["EmptyFile"].ToString() == "Yes")
                    {
                        clsFunction.DisplayAJAXMessage(this, "Selected exam is required both question papaer and answer sheet file.");
                        Session["EmptyFile"] = null;
                    }
                }
            }
        }

        private bool InsertExamFile()
        {
            try
            {
                string questionPaperFileName = "QuestionPaper_" + Path.GetFileName(FileUploadQuestion.FileName);
                string answerSheetFileName = "AnswerSheet_" + Path.GetFileName(FileUploadAnswerSheet.FileName);
                string ExistingExamFilePath = "";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM ExamPreparation WHERE ExamTimetableGUID = @ExamTimetableGUID ";

                    SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                    GetCommand.Parameters.AddWithValue("@ExamTimetableGUID", ddlExamination.SelectedValue);
                    SqlDataReader reader = GetCommand.ExecuteReader();

                    DataTable dtExamPre = new DataTable();
                    dtExamPre.Load(reader);
                    con.Close();

                    if (dtExamPre.Rows.Count != 0)
                    {
                        ExistingExamFilePath = "~/Uploads/ExaminationPreparationFolder/" + dtExamPre.Rows[0]["ExaminationPreparationGUID"] + "/";

                        if (Directory.Exists(Server.MapPath(ExistingExamFilePath)))
                        {
                            if (!String.IsNullOrEmpty(questionPaperFileName) && !String.IsNullOrEmpty(answerSheetFileName))
                            {
                                con.Open();

                                SqlCommand InsertCommand = new SqlCommand("UPDATE ExamPreparation SET QuestionPaper = @QuestionPaper, AnswerSheet = @AnswerSheet WHERE ExaminationPreparationGUID = @ExaminationPreparationGUID ", con);

                                InsertCommand.Parameters.AddWithValue("@ExaminationPreparationGUID", dtExamPre.Rows[0]["ExaminationPreparationGUID"]);
                                InsertCommand.Parameters.AddWithValue("@QuestionPaper", questionPaperFileName);
                                InsertCommand.Parameters.AddWithValue("@AnswerSheet", answerSheetFileName);

                                InsertCommand.ExecuteNonQuery();
                                con.Close();

                                DeleteFile(Server.MapPath(ExistingExamFilePath));

                                FileUploadQuestion.SaveAs(Server.MapPath(ExistingExamFilePath) + questionPaperFileName);
                                FileUploadAnswerSheet.SaveAs(Server.MapPath(ExistingExamFilePath) + answerSheetFileName);

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
                        Guid ExamPreparationGUID = Guid.NewGuid();
    
                        string ExamFilePath = "~/Uploads/ExaminationPreparationFolder/" + ExamPreparationGUID + "/";
                        
                        if (!Directory.Exists(ExamFilePath))
                        {
                            Directory.CreateDirectory(Server.MapPath(ExamFilePath));

                            con.Open();
                            SqlCommand InsertCommand = new SqlCommand("INSERT INTO ExamPreparation VALUES(@ExamPreparationGUID, @ExamTimetableGUID, @QuestionPaper, @AnswerSheet) ", con);

                            InsertCommand.Parameters.AddWithValue("@ExamPreparationGUID", ExamPreparationGUID);
                            InsertCommand.Parameters.AddWithValue("@ExamTimetableGUID", ddlExamination.SelectedValue);
                            InsertCommand.Parameters.AddWithValue("@QuestionPaper", questionPaperFileName);
                            InsertCommand.Parameters.AddWithValue("@AnswerSheet", answerSheetFileName);

                            InsertCommand.ExecuteNonQuery();
                            con.Close();

                            return true;
                        } else
                        {
                            clsFunction.DisplayAJAXMessage(this, "Unable to create directory or folder. Please contact KrousEx for support.");
                            return false;
                        }
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

            Directory.Delete(path);
        }
    }
}