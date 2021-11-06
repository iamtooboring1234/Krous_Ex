using System;
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
    public partial class ExaminationInvigilatorsListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["UpdateInvigilators"] != null)
                {
                    if (Session["UpdateInvigilators"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                        Session["UpdateInvigilators"] = null;
                    }
                }

                loadSession();
                loadCourse();
                loadGV();
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

        private void loadCourse()
        {
            try
            {
                ddlCourse.Items.Clear();

                ListItem oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlCourse.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamTimetable ex LEFT JOIN Course C ON ex.CourseGUID = c.CourseGUID WHERE SessionGUID = @SessionGUID ", con);

                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtCourse = new DataTable();
                dtCourse.Load(reader);


                for (int i = 0; i <= dtCourse.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtCourse.Rows[i]["CourseName"].ToString() + " (" + dtCourse.Rows[i]["CourseAbbrv"].ToString() + ")";
                    oList.Value = dtCourse.Rows[i]["CourseGUID"].ToString();
                    ddlCourse.Items.Add(oList);
                }

                con.Close();
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadGV()
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT ex.ExamTimetableGUID, C.CourseAbbrv, C.CourseName, ex.ExamStartDateTime, ex.ExamEndDateTime, Count(ei.StaffGUID) AS 'TotalInvi' FROM ";
                sqlQuery += "ExamTimeTable ex LEFT JOIN ExamInvigilatorsList ei ON ex.ExamTimetableGUID = ei.ExamTimetableGUID ";
                sqlQuery += "LEFT JOIN Course C ON ex.CourseGUID = C.CourseGUID WHERE ";
                sqlQuery += "ex.SessionGUID = @SessionGUID ";
                sqlQuery += "GROUP BY ex.ExamTimetableGUID, C.CourseAbbrv, C.CourseName, ex.ExamStartDateTime, ex.ExamEndDateTime ";
                sqlQuery += "ORDER BY ExamStartDateTime, ExamEndDateTime";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@StaffFullName", txtStaffName.Text);
                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtExam = new DataTable();
                dtExam.Load(reader);
                con.Close();

                if (dtExam.Rows.Count != 0)
                {
                    gvExamInvi.DataSource = dtExam;
                    gvExamInvi.DataBind();
                    gvExamInvi.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvExamInvi.Visible = false;
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT ex.ExamTimetableGUID, C.CourseAbbrv, C.CourseName, ex.ExamStartDateTime, ex.ExamEndDateTime, Count(ei.StaffGUID) AS 'TotalInvi' FROM ";
                sqlQuery += "ExamTimeTable ex LEFT JOIN ExamInvigilatorsList ei ON ex.ExamTimetableGUID = ei.ExamTimetableGUID ";
                sqlQuery += "LEFT JOIN Course C ON ex.CourseGUID = C.CourseGUID WHERE ";
                sqlQuery += "CASE WHEN @CourseGUID = '00000000-0000-0000-0000-000000000000' then '00000000-0000-0000-0000-000000000000' ELSE ex.CourseGUID END = @CourseGUID AND ";
                sqlQuery += "ex.SessionGUID = @SessionGUID AND ";
                sqlQuery += "(@StartDateTime BETWEEN ex.ExamStartDateTime AND ex.ExamEndDateTime) OR ";
                sqlQuery += "(@EndDateTime BETWEEN ex.ExamStartDateTime AND ex.ExamEndDateTime) ";
                sqlQuery += "GROUP BY ex.ExamTimetableGUID, C.CourseAbbrv, C.CourseName, ex.ExamStartDateTime, ex.ExamEndDateTime ";
                sqlQuery += "ORDER BY ExamStartDateTime, ExamEndDateTime";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                GetCommand.Parameters.AddWithValue("@StaffFullName", txtStaffName.Text);
                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);
                if (String.IsNullOrEmpty(ddlCourse.SelectedValue))
                {
                    GetCommand.Parameters.AddWithValue("@CourseGUID", Guid.Empty);
                } else
                {
                    GetCommand.Parameters.AddWithValue("@CourseGUID", ddlCourse.SelectedValue);
                }
                GetCommand.Parameters.AddWithValue("@StartDateTime", DateTime.Parse(txtExamStartDateTime.Text));
                GetCommand.Parameters.AddWithValue("@EndDateTime", DateTime.Parse(txtExamEndDateTime.Text));

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtExam = new DataTable();
                dtExam.Load(reader);
                con.Close();

                if (dtExam.Rows.Count != 0)
                {
                    gvExamInvi.DataSource = dtExam;
                    gvExamInvi.DataBind();
                    gvExamInvi.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvExamInvi.Visible = false;
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}