﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class ExaminationReleaseResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRelease_Click(object sender, EventArgs e)
        {
            ReleaseResult();
        }

        private bool ReleaseResult()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamResult er LEFT JOIN Session S ON er.SessionGUID = s.SessionGUID ORDER BY s.SessionYear, s.SessionMonth, er.StudentGUID ", con);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtExamResult = new DataTable();
                dtExamResult.Load(reader);
                con.Close();

                if (dtExamResult.Rows.Count != 0)
                {
                    DataTable dtCss = new DataTable();
                    double totalGradePoints = 0;
                    double totalCreditHour = 0;
                    double grandTotalGradePoints = 0;
                    double grandTotalCreditHour = 0;
                    double GPA = 0;
                    double CGPA = 0;
                    string lastStudentGUID = "";

                    DataTable dtExamResultPerCourse = new DataTable();

                    for (int i = 0; i < dtExamResult.Rows.Count; i++)
                    {

                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                        { 
                            dtExamResultPerCourse = new DataTable();

                            con.Open();
                            GetCommand = new SqlCommand("SELECT * FROM ExamResultPerCourse epc LEFT JOIN Course C ON epc.CourseGUID = C.CourseGUID WHERE ExamResultGUID = @ExamResultGUID ", con);

                            GetCommand.Parameters.AddWithValue("@ExamResultGUID", dtExamResult.Rows[i]["ExamResultGUID"].ToString());

                            reader = GetCommand.ExecuteReader();

                            dtExamResultPerCourse.Load(reader);
                            con.Close();

                            totalGradePoints = 0;
                            totalCreditHour = 0;
                            GPA = 0;
                            CGPA = 0;

                            for (int j = 0; j < dtExamResultPerCourse.Rows.Count; j++)
                            {
                                double gradePoints = 0;

                                if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "A")
                                {
                                    gradePoints = 4.0000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "A-")
                                {
                                    gradePoints = 3.7500;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "B+")
                                {
                                    gradePoints = 3.5000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "B")
                                {
                                    gradePoints = 3.0000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "B-")
                                {
                                    gradePoints = 2.7500;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "C+")
                                {
                                    gradePoints = 2.5000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "C")
                                {
                                    gradePoints = 2.0000;
                                }
                                else if (dtExamResultPerCourse.Rows[j]["Grade"].ToString() == "F")
                                {
                                    gradePoints = 0.0000;
                                }

                                totalGradePoints += double.Parse(dtExamResultPerCourse.Rows[j]["CreditHour"].ToString()) * gradePoints;
                                totalCreditHour += double.Parse(dtExamResultPerCourse.Rows[j]["CreditHour"].ToString());
                            }

                            grandTotalGradePoints += totalGradePoints;
                            grandTotalCreditHour += totalCreditHour;
                        }

                        GPA = totalGradePoints / totalCreditHour;
                        CGPA = grandTotalGradePoints / grandTotalCreditHour;

                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                        { 
                            con.Open();

                            SqlCommand updateCommand = new SqlCommand("UPDATE ExamResult SET GPA = @GPA, CGPA = @CGPA, Status = 'Release', ReleaseDate = @ReleaseDate WHERE ExamResultGUID = @ExamResultGUID ", con);

                            updateCommand.Parameters.AddWithValue("@ExamResultGUID", dtExamResult.Rows[i]["ExamResultGUID"]);
                            updateCommand.Parameters.AddWithValue("@GPA", String.Format("{0:0.0000}", GPA));
                            updateCommand.Parameters.AddWithValue("@CGPA", String.Format("{0:0.0000}", CGPA));
                            updateCommand.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);

                            updateCommand.ExecuteNonQuery();

                            if (lastStudentGUID != dtExamResult.Rows[i]["StudentGUID"].ToString())
                            {
                                GetCommand = new SqlCommand("SELECT * FROM CurrentSessionSemester css WHERE StudentGUID = @StudentGUID", con);
                                GetCommand.Parameters.AddWithValue("@StudentGUID", dtExamResult.Rows[i]["StudentGUID"]);
                                reader = GetCommand.ExecuteReader();
                                dtCss.Load(reader);
                            }
                            con.Close();

                            lastStudentGUID = dtExamResult.Rows[i]["StudentGUID"].ToString();
                        }
                    }


                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                    {
                        con.Open();

                        if (dtCss.Rows.Count != 0)
                        {
                            for (int i = 0; i < dtCss.Rows.Count; i++)
                            {
                                string sqlQuery;

                                sqlQuery = "WITH CTE AS( ";
                                sqlQuery += "SELECT ROW_NUMBER() OVER(ORDER BY SessionYear, SessionMonth) as Row, * FROM Session S) ";
                                sqlQuery += "SELECT* FROM CTE WHERE Row = ((select Row from CTE where SessionGUID = @SessionGUID) +1) ";

                                GetCommand = new SqlCommand(sqlQuery, con);

                                GetCommand.Parameters.AddWithValue("@SessionGUID", dtCss.Rows[i]["SessionGUID"]);

                                reader = GetCommand.ExecuteReader();
                                DataTable dtSession = new DataTable();
                                dtSession.Load(reader);

                                sqlQuery = "WITH CTE AS( ";
                                sqlQuery += "SELECT ROW_NUMBER() OVER(ORDER BY SemesterYear, SemesterSem) as Row, * FROM Semester S) ";
                                sqlQuery += "SELECT* FROM CTE WHERE Row = ((select Row from CTE where SemesterGUID = @SemesterGUID) +1) ";

                                GetCommand = new SqlCommand(sqlQuery, con);

                                GetCommand.Parameters.AddWithValue("@SemesterGUID", dtCss.Rows[i]["SemesterGUID"]);

                                reader = GetCommand.ExecuteReader();
                                DataTable dtSemester = new DataTable();
                                dtSemester.Load(reader);

                                SqlCommand updateCommand = new SqlCommand("UPDATE CurrentSessionSemester SET SessionGUID = @SessionGUID, SemesterGUID = @SemesterGUID, Status = @Status, Reason = @Reason WHERE StudentGUID = @StudentGUID ", con);

                                updateCommand.Parameters.AddWithValue("@SessionGUID", dtSession.Rows[0]["SessionGUID"]);
                                updateCommand.Parameters.AddWithValue("@SemesterGUID", dtSemester.Rows[0]["SemesterGUID"]);
                                updateCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@Reason", DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@StudentGUID", dtCss.Rows[i]["StudentGUID"]);

                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        con.Close();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }
    }
}