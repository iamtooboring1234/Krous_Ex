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
    public partial class ExaminationInvigilatorsEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadSession();
                loadCourse();
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
                ddlExam.Items.Clear();

                ListItem oList = new ListItem();

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
                    ddlExam.Items.Add(oList);
                }

                con.Close();
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlExam.Items.Clear();

                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamTimeTable WHERE CourseGUID = @CourseGUID AND SessionGUID = @SessionGUID", con);

                GetCommand.Parameters.AddWithValue("@CourseGUID", ddlExam.SelectedValue);
                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtCourse = new DataTable();
                dtCourse.Load(reader);


                for (int i = 0; i <= dtCourse.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtCourse.Rows[i]["CourseName"].ToString() + " (" + dtCourse.Rows[i]["CourseAbbrv"].ToString() + ")";
                    oList.Value = dtCourse.Rows[i]["CourseGUID"].ToString();
                    ddlExam.Items.Add(oList);
                }

                con.Close();
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}