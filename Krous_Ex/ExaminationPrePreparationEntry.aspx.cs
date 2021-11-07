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
                loadSession();
                loadExamCourse();
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

            }
        }

        private bool InsertExamFile()
        {
            try
            {
                String questionPaper = Path.GetFileName(FileUploadQuestion.FileName);
                String answerSheet = Path.GetFileName(FileUploadAnswerSheet.FileName);
                String savePath = ConfigurationManager.AppSettings.Get("ExaminationUploadPath");
                String ExamFilePath = Server.MapPath(savePath);
                String ExamQuestionPaperFullSavePath = ExamFilePath + questionPaper;
                String ExamAnswerSheetFullSavePath = ExamFilePath + answerSheet;

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    con.Open();
                    if (Directory.Exists(ExamFilePath))
                    {
                        if (!String.IsNullOrEmpty(ExamQuestionPaperFullSavePath) && !String.IsNullOrEmpty(ExamAnswerSheetFullSavePath))
                        {
                            string sqlQuery = "INSERT INTO ExamPreparation VALUES (NEWID(), @ExamTimetableGUID, @AnswerSheet, @QuestionPaper)";
                            insertFileList.Parameters.AddWithValue("@ExamTimetableGUID", ddlExamination.SelectedValue);
                            insertFileList.Parameters.AddWithValue("@AnswerSheet", filename);
                            insertFileList.Parameters.AddWithValue("@QuestionPaper", filename);
                            SqlCommand InsertCommand = new SqlCommand(sqlQuery, con);



                            InsertCommand.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            Session["EmptyFile"] = "Yes";
                            return false;
                        }
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Not physical path.");
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
    }
}