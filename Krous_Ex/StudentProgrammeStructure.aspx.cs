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
    public partial class StudentProgrammeStructure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadCalendar();
            }
        }

        private void loadCalendar()
        {
            try
            {
                string sqlQuery;
                string strTable = "";

                sqlQuery = "SELECT ss.SessionMonth FROM Student S, Session SS WHERE SS.SessionGUID = S.SessionGUID AND S.SessionGUID = '4b45bed0-7615-4190-a71c-55347cbfa0ea' ";

                // 202009
                //5da74545-0324-4006-9198-f1a23c26cdd2 202005

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtFirst = new DataTable();
                dtFirst.Load(reader);
                con.Close();

                if(dtFirst.Rows.Count != 0 )
                {
                    if (dtFirst.Rows[0]["SessionMonth"].ToString() == "1" || dtFirst.Rows[0]["SessionMonth"].ToString() == "5" || dtFirst.Rows[0]["SessionMonth"].ToString() == "9")
                    {
                        sqlQuery = "SELECT * FROM Semester ORDER BY SemesterYear, SemesterSem ";

                        con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                        con.Open();

                        GetCommand = new SqlCommand(sqlQuery, con);

                        reader = GetCommand.ExecuteReader();
                        DataTable dtSecond = new DataTable();
                        dtSecond.Load(reader);
                        con.Close();

                        int lastYear = 1;
                        int lastSemester = 1;
                        int ttlCredit = 0;
                        bool isNewYear = true;
                        bool isNewSemester = true;

                        for (int i = 0; i < dtSecond.Rows.Count; i++)
                        {
                            sqlQuery = "SELECT * FROM Course C, ProgrammeCourse PC, Semester S ";
                            sqlQuery += "WHERE PC.SemesterGUID = S.SemesterGUID ";
                            sqlQuery += "AND C.CourseGUID = PC.CourseGUID ";
                            sqlQuery += "AND PC.SessionMonth = " + dtFirst.Rows[0]["SessionMonth"] + " ";
                            sqlQuery += "AND S.SemesterYear = " + dtSecond.Rows[i]["SemesterYear"] + " AND S.SemesterSem = " + dtSecond.Rows[i]["SemesterSem"] + " ";
                            sqlQuery += "ORDER BY S.SemesterYear, S.SemesterSem ";

                            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                            con.Open();

                            GetCommand = new SqlCommand(sqlQuery, con);

                            reader = GetCommand.ExecuteReader();
                            DataTable dtThird = new DataTable();
                            dtThird.Load(reader);
                            con.Close();

                            if (lastYear != int.Parse(dtSecond.Rows[i]["SemesterYear"].ToString()))
                            {
                                isNewYear = true;
                                lastYear = int.Parse(dtSecond.Rows[i]["SemesterYear"].ToString());
                            }

                            for (int j = 0; j < dtThird.Rows.Count; j++)
                            {
                                lastSemester = int.Parse(dtSecond.Rows[i]["SemesterSem"].ToString());

                                if (dtThird.Rows[j]["CourseGUID"].ToString() != null && isNewYear) {
                                    strTable += "<tr>";
                                    strTable += "<td colspan=\"3\"><strong>Year " + dtSecond.Rows[i]["SemesterYear"] + "</strong></td>";
                                    strTable += "</tr>";
                                    strTable += "<tr class=\"hi\">";
                                }

                                if (dtThird.Rows.Count > 0 && isNewSemester) {
                                    strTable += "<td width=\"33%\">";
                                    strTable += "<div>";
                                    strTable += "<table width=\"100%\"  border=\"0\">";
                                    strTable += "<tbody>";
                                    strTable += "<tr>";
                                    strTable += "<td colspan=\"4\">";
                                    strTable += "<span style=\"font-size:11px; font-weight:bold\">Year " + dtThird.Rows[j]["SemesterYear"] + " Semester " + dtThird.Rows[j]["SemesterSem"] + "</span>";
                                    strTable += "</td>";
                                    strTable += "</tr>";
                                }

                                strTable += "<tr class=\"font-size-11\"><td width=\"20%\">" + dtThird.Rows[j]["CourseAbbrv"] + "</td>";
                                strTable += "<td><span style=\"font-size:10px\">" + dtThird.Rows[j]["CourseName"] + "</span></td>";
                                strTable += "<td width=\"5%\">";
                                strTable += dtThird.Rows[j]["CreditHour"] + "&nbsp;</td>";
                                strTable += "</tr>";

                                ttlCredit += int.Parse(dtThird.Rows[j]["CreditHour"].ToString());

                                if (j == dtThird.Rows.Count - 1) {
                                    strTable += "<tr>";
                                    strTable += "<td colspan=\"2\" style=\"text-align:right\"><span style=\"text-align:right; font-size:10px; font-weight:bold\">Total Credit Hour(s) &nbsp; : &nbsp;</span></td>";
                                    strTable += "<td><span style=\"font-size:10px; font-weight:bold\">"+ ttlCredit +"&nbsp;</span></td>";
                                    strTable += "</tr>";
                                    strTable += "</tbody>";
                                    strTable += "</table>";
                                    strTable += "</div>";
                                    strTable += "</td>";
                                    ttlCredit = 0;
                                }

                                if (dtThird.Rows.Count > 1 && lastYear == int.Parse(dtThird.Rows[j]["SemesterYear"].ToString()))
                                {
                                    isNewYear = false;
                                } else if (lastYear == int.Parse(dtThird.Rows[j]["SemesterYear"].ToString()))
                                {
                                    isNewYear = false;
                                }

                                if (dtThird.Rows.Count > 1 && lastSemester == int.Parse(dtThird.Rows[j]["SemesterSem"].ToString()))
                                {
                                    isNewSemester = false;
                                }

                            }

                            isNewSemester = true;
                            lastYear = int.Parse(dtSecond.Rows[i]["SemesterYear"].ToString());
                        }
                        strTable += "</tr>";
                    }
                }

                litTest.Text = strTable;

            } catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}




