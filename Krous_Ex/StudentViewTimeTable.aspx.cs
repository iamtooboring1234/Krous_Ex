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
                        if (i == 15)
                        {
                            break;
                        }
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
            try
            {
                string sqlQuery;
                string[] strWeekday = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
                string strTable = "";

                sqlQuery = "SELECT* FROM TimetableCourse t ";
                sqlQuery += "LEFT JOIN ProgrammeCourse PC ON T.ProgrammeCourseGUID = PC.ProgrammeCourseGUID ";
                sqlQuery += "LEFT JOIN Course C ON Pc.CourseGUID = C.CourseGUID ";
                sqlQuery += "LEFT JOIN GroupStudentList gsl ON t.GroupGUID = gsl.GroupGUID ";
                sqlQuery += "LEFT JOIN Student st ON gsl.StudentGUID = st.StudentGUID ";
                sqlQuery += "LEFT JOIN Staff s ON t.StaffGUID = s.StaffGUID ";
                sqlQuery += "WHERE t.SessionGUID = @SessionGUID AND ";
                sqlQuery += "st.StudentGUID = @StudentGUID ";
                sqlQuery += "ORDER BY ClassStartTime ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand getCommand = new SqlCommand(sqlQuery, con);

                getCommand.Parameters.AddWithValue("@SessionGUID", Request.QueryString["SessionGUID"]);
                getCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());

                SqlDataReader reader = getCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count !=0)
                {
                    DateTime selectedWeekRange = DateTime.Parse(ddlWeek.SelectedValue.ToString());

                    for (int i = 0; i < 7; i++) //weekday
                    {
                        int currentCol = 0;
                        int leftCol = 0;

                        DateTime dtTime = DateTime.Parse("1/1/1900 8:00 AM");

                        strTable += "<tr>";
                        strTable += "<td class=\"text-center\">" + strWeekday[i].Substring(0,3) + "<br />" + selectedWeekRange.ToString("dd-MMM-yyyy") + "</td>";

                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            string test = strWeekday[i].ToString();
                            string test1 = dt.Rows[x]["DaysOfWeek"].ToString();

                            if (strWeekday[i].Equals(dt.Rows[x]["DaysOfWeek"].ToString()))
                            {
                                int intHourColSpan = 0;

                                for (int j = currentCol; j < 28; j++) //hour
                                {
                                    DateTime classStartTime = DateTime.Parse(dt.Rows[x]["ClassStartTime"].ToString());
                                    DateTime classEndTime = DateTime.Parse(dt.Rows[x]["ClassEndTime"].ToString());

                                    if (TimeSpan.Parse(dtTime.ToString("HH:mm ")) >= TimeSpan.Parse(classStartTime.ToString("HH:mm")) && TimeSpan.Parse(dtTime.ToString("HH:mm")) <= TimeSpan.Parse(classEndTime.ToString("HH:mm")))
                                    {
                                        if (TimeSpan.Parse(dtTime.ToString("hh:mm")) == TimeSpan.Parse(classEndTime.ToString("hh:mm")))
                                        {
                                            if (dt.Rows[x]["ClassType"].ToString() == "Main")
                                            {
                                                strTable += "<td colspan=\"" + intHourColSpan + "\" style=\"background-color:#46854d;color:#FFFFFF;text-align:center\">";
                                                strTable += "<div data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Tooltip on bottom\">";
                                                strTable += "<span class=\"small\">" + dt.Rows[x]["CourseAbbrv"].ToString() + " (" + dt.Rows[x]["ClassCategory"].ToString().Substring(0, 1) + ")" + "</span>";
                                                strTable += "<br>";
                                                strTable += "<span class=\"small\">-</span><br>";
                                                strTable += "<span class=\"small\">" + dt.Rows[x]["StaffFullName"].ToString() + "</span><br>";
                                                strTable += "<span style=\"font-size: 10px\">" + classStartTime.ToString("hh:mm tt") + " - " + classEndTime.ToString("hh:mm tt") + "</span>";
                                                strTable += "</div>";
                                                strTable += "</td>";
                                            } 
                                            else if (dt.Rows[x]["ClassType"].ToString() == "Replacement")
                                            {
                                                leftCol = leftCol - intHourColSpan;
                                                if (selectedWeekRange.ToString("dd-MMM-yyyy") == DateTime.Parse(dt.Rows[x]["ClassStartTime"].ToString()).ToString("dd-MMM-yyyy"))
                                                {
                                                    strTable += "<td colspan=\"" + intHourColSpan + "\" style=\"background-color:#aEA434;color:#FFFFFF;text-align:center\">";
                                                    strTable += "<div data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Tooltip on bottom\">";
                                                    strTable += "<span class=\"small\">" + dt.Rows[x]["CourseAbbrv"].ToString() + " (" + dt.Rows[x]["ClassCategory"].ToString().Substring(0, 1) + ")" + "</span>";
                                                    strTable += "<br>";
                                                    strTable += "<span class=\"small\">-</span><br>";
                                                    strTable += "<span class=\"small\">" + dt.Rows[x]["StaffFullName"].ToString() + "</span><br>";
                                                    strTable += "<span style=\"font-size: 10px\">" + classStartTime.ToString("hh:mm tt") + " - " + classEndTime.ToString("hh:mm tt") + "</span>";
                                                    strTable += "</div>";
                                                    strTable += "</td>";
                                                    leftCol = leftCol + intHourColSpan;
                                                }
                                            }

                                            intHourColSpan = 1;
                                            break;
                                        }

                                        intHourColSpan++;
                                        currentCol++;
                                        leftCol++;
                                    }
                                    else
                                    {
                                        strTable += "<td colspan=\"1\" width=\"18\"></td>";
                                        leftCol++;
                                    }

                                    dtTime = dtTime.AddMinutes(30);
                                    
                                }
                            }
                        }

                        if (leftCol < 28)
                        {
                            for (int a = leftCol; a < 28; a++)
                            {
                                strTable += "<td colspan=\"1\" width=\"18\"></td>";
                            }
                        }

                        strTable += "</tr>";
                        litTest.Text = strTable;

                        selectedWeekRange = selectedWeekRange.AddDays(1);
                    }
                }
            }
            catch (Exception ex){
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadTimetable();
        }
    }
}