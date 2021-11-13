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
    public partial class StudentViewTimeTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadWeek();
                loadTimetable();
            }
        }

        private void loadWeek()
        {
            try
            {
                ddlWeek.Items.Clear();

                ListItem oList = new ListItem();

                string sqlQuery;

                sqlQuery = "SELECT * FROM AcademicCalender WHERE AcademicCalenderGUID = @AcademicCalenderGUID";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand getCommand = new SqlCommand(sqlQuery, con);

                getCommand.Parameters.AddWithValue("@AcademicCalenderGUID", Request.QueryString["AcademicCalenderGUID"]);

                SqlDataReader reader = getCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    DateTime semesterStartDate = DateTime.Parse(dt.Rows[0]["SemesterStartDate"].ToString());
                    DateTime semesterEndDate = DateTime.Parse(dt.Rows[0]["SemesterEndDate"].ToString()).AddDays(-(int.Parse(dt.Rows[0]["SemesterExaminationDuration"].ToString()) + int.Parse(dt.Rows[0]["SemesterBreakDuration"].ToString()) + int.Parse(dt.Rows[0]["SemesterStudyDuration"].ToString())));
                    int i = 1;
                    while (semesterStartDate <= semesterEndDate){
                        oList = new ListItem();
                        oList.Text = "Week " + i + " : " + semesterStartDate.ToString("dd-MM-yyyy") + " ~ " + semesterStartDate.AddDays(6).ToString("dd-MM-yyyy");
                        oList.Value = semesterStartDate.ToString();
                        ddlWeek.Items.Add(oList);
                        semesterStartDate = semesterStartDate.AddDays(7);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void loadTimetable()
        {
            
        }
    }
}