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
    public partial class StudentTimetableListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadAcademicCalendar();
            }
        }

        private void loadAcademicCalendar()
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT AcademicCalenderGUID, S.SessionGUID, CONCAT(SessionYear, REPLACE(STR(SessionMonth,2),' ','0')) AS SessionYearMonth, CONCAT(DATENAME(WEEKDAY, SemesterStartDate), ', ', SemesterStartDate, ' - ', DATENAME(WEEKDAY, SemesterEndDate), ', ', SemesterEndDate) AS DurationDate, SemesterEndDate, SemesterExaminationDuration, SemesterBreakDuration, ";
                sqlQuery += "DATEDIFF(Week, SemesterStartDate, DATEADD(Day, -(SemesterExaminationDuration + SemesterBreakDuration + SemesterStudyDuration), SemesterEndDate)) AS TotalWeek ";
                sqlQuery += "FROM Session S LEFT JOIN  ";
                sqlQuery += "CurrentSessionSemester css ON S.SessionGUID = css.SessionGUID LEFT JOIN ";
                sqlQuery += "AcademicCalender A ON S.SessionGUID = A.SessionGUID ";
                sqlQuery += "WHERE Cast(CONCAT(SessionYear, SessionMonth) AS INT) >= (SELECT Cast(CONCAT(SessionYear, SessionMonth) AS INT) FROM Session S LEFT JOIN ";
                sqlQuery += "Student st ON S.SessionGUID = st.SessionGUID WHERE st.StudentGUID = @StudentGUID) AND ";
                sqlQuery += "Cast(CONCAT(SessionYear, SessionMonth) AS INT) <= (SELECT Cast(CONCAT(SessionYear, SessionMonth) AS INT) FROM Session S LEFT JOIN ";
                sqlQuery += "CurrentSessionSemester css ON S.SessionGUID = css.SessionGUID WHERE css.StudentGUID = @StudentGUID) ";
                sqlQuery += "GROUP BY AcademicCalenderGUID, S.SessionGUID, SessionYear, SessionMonth, SemesterStartDate, SemesterEndDate, SemesterExaminationDuration, SemesterBreakDuration, SemesterStudyDuration ";
                sqlQuery += "ORDER BY SemesterStartDate DESC ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand getCommand = new SqlCommand(sqlQuery, con);

                getCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());

                SqlDataReader reader = getCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvTimetable.DataSource = dt;
                    gvTimetable.DataBind();
                    gvTimetable.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvTimetable.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void gvTimetable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Srl = (HyperLink)e.Row.FindControl("hlViewTimetable");

                if (!string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SemesterEndDate"))))
                {
                    DateTime SemesterEndDate = DateTime.Parse(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SemesterEndDate")));

                    if(DateTime.Now >= SemesterEndDate)
                    {
                        Srl.Enabled = false;
                        Srl.CssClass = "btn btn-dark pr-4 pl-4 pt-2 pb-2";
                    }
                }

            }
        }
    }
}