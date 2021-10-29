using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class KrousExViewAcademicCalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadIntake();
        }

        private void loadIntake()
        {
            try
            {
                string sqlQuery;
                string strTable = "";

                sqlQuery = "WITH CTE AS( ";
                sqlQuery += "SELECT ROW_NUMBER() OVER(ORDER BY SemesterStartDate) as Row, *FROM AcademicCalender A WHERE Year(getdate()) - Year(SemesterStartDate) < 3 ";
                sqlQuery += ") ";
                sqlQuery += "SELECT * FROM CTE WHERE Row = ((select Row from CTE where AcademicCalenderGUID = @AcademicCalenderGUID)) ";
                sqlQuery += "OR Row = ((select Row from CTE where AcademicCalenderGUID = @AcademicCalenderGUID) +1) ";
                sqlQuery += "OR Row = ((select Row from CTE where AcademicCalenderGUID = @AcademicCalenderGUID) +2); ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                GetCommand.Parameters.AddWithValue("@AcademicCalenderGUID", Request.QueryString["AcademicCalenderGUID"]);

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        double datediff = (double)(DateTime.Parse(dt.Rows[i]["SemesterEndDate"].ToString()) - DateTime.Parse(dt.Rows[i]["SemesterStartDate"].ToString())).TotalDays + 1;
                        datediff = datediff - int.Parse(dt.Rows[i]["SemesterBreakDuration"].ToString());
                        DateTime startDate = DateTime.Parse(dt.Rows[i]["SemesterStartDate"].ToString());
                        DateTime endDate = DateTime.Parse(dt.Rows[i]["SemesterEndDate"].ToString());
                        DateTime semBreakStartDate = DateTime.Parse(dt.Rows[i]["SemesterEndDate"].ToString()).AddDays(-int.Parse(dt.Rows[i]["SemesterBreakDuration"].ToString()) + 1);
                        DateTime semExamStartDate = semBreakStartDate.AddDays(-int.Parse(dt.Rows[i]["SemesterExaminationDuration"].ToString()));
                        int x = 1;

                        strTable += "<table class=\"table table-striped mt-3 table-" + i + "\" style=\"border:2px solid\">";
                        strTable += "<thead>";
                        strTable += "<tr>";
                        if (dt.Rows[i]["CalenderType"].ToString() == "DipUnderPost")
                        {
                            if (i % 3 == 0)
                            {
                                strTable += "<th colspan=\"4\" style=\"font-weight:normal\">FIRST SEMESTER : " + startDate.ToString("dddd, dd MMMM yyyy") + " - " + endDate.AddDays(6).ToString("dddd, dd MMMM yyyy") + "</th>";
                            }
                            else if (i % 3 == 1)
                            {
                                strTable += "<th colspan=\"4\" style=\"font-weight:normal\">SECOND SEMESTER : " + startDate.ToString("dddd, dd MMMM yyyy") + " - " + endDate.ToString("dddd, dd MMMM yyyy") + "</th>";
                            }
                            else
                            {
                                strTable += "<th colspan=\"4\" style=\"font-weight:normal\">THIRD SEMESTER : " + startDate.ToString("dddd, dd MMMM yyyy") + " - " + endDate.ToString("dddd, dd MMMM yyyy") + "</th>";
                            }
                        }
                        strTable += "</tr>";
                        strTable += "<tr>";
                        strTable += "<th style=\"text-align:center\">Week</th>";
                        strTable += "<th>Date</th>";
                        strTable += "<th>Study Leave</th>";
                        strTable += "<th>Semester Examination</th>";
                        strTable += "</tr>";
                        strTable += "</thead>";

                        strTable += "<tbody>";

                        for (int j = 0; j < Math.Ceiling(datediff /7); j++)
                        {
                            strTable += "<tr><td style=\"text-align:center\">" + x + "</td><td>" + startDate.ToString("dd MMMM yyyy") + " - " + startDate.AddDays(6).ToString("dd MMMM yyyy") + "</td>";
                            if (dt.Rows[i]["SemesterStudyDuration"].ToString() != "0")
                            {
                                if (x == 15)
                                {
                                    strTable += "<td class=\"text-center\">" + startDate.ToString("dd MMM") + " - " + startDate.AddDays(3).ToString("dd MMM yyyy") + "<br /> (" + dt.Rows[i]["SemesterStudyDuration"].ToString() + " days)" + "</td>";
                                    strTable += "<td class=\"text-center\" rowspan=\"3\">" + semExamStartDate.ToString("dd MMM") + " - " + semExamStartDate.AddDays(int.Parse(dt.Rows[i]["SemesterExaminationDuration"].ToString()) - 1).ToString("dd MMM yyyy") + "<br /> (" + dt.Rows[i]["SemesterExaminationDuration"].ToString() + " days)" + "</td>";
                                }
                                else if ( x < 15 || x > 17)
                                {
                                    strTable += "<td></td>";
                                    strTable += "<td></td>";
                                } else
                                {
                                    strTable += "<td></td>";
                                }
                            }
                            else
                            {
                                strTable += "<td></td>";
                                strTable += "<td></td>";
                            }
                            strTable += "</tr>";
                            x++;
                            startDate = startDate.AddDays(7);
                        }

                        strTable += "</tbody>";
                        strTable += "<tfoot>";
                        if (dt.Rows[i]["CalenderType"].ToString() == "DipUnderPost")
                        {
                            if (i % 3 == 0)
                            {
                                strTable += "<tr><td colspan=\"4\"  style=\"color:inherit\">FIRST SEMESTER HOLIDAYS : " + semBreakStartDate.ToString("dddd, dd MMMM yyyy") + " - " + endDate.ToString("dddd, dd MMMM yyyy") + " (" + dt.Rows[i]["SemesterBreakDuration"].ToString() + " days)" +"</td></tr>";
                            }
                            else if (i % 3 == 1)
                            {
                                strTable += "<tr><td colspan=\"4\" style=\"color:inherit\">SECOND SEMESTER HOLIDAYS : " + semBreakStartDate.ToString("dddd, dd MMMM yyyy") + " - " + endDate.ToString("dddd, dd MMMM yyyy") + " (" + dt.Rows[i]["SemesterBreakDuration"].ToString() + " days)" + "</td></tr>";
                            }
                            else
                            {
                                strTable += "<tr><td colspan=\"4\" style=\"color:inherit\">THIRD SEMESTER HOLIDAYS : " + semBreakStartDate.ToString("dddd, dd MMMM yyyy") + " - " + endDate.ToString("dddd, dd MMMM yyyy") + " (" + dt.Rows[i]["SemesterBreakDuration"].ToString() + " days)" + "</td></tr>";
                            }
                        }
                        strTable += "</tfoot>";
                        strTable += "</table>";
                    }

                    litTest.Text = strTable;

                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        public void generatePDF()
        {
            string fileName = "Invoice" + DateTime.Now.ToString() + ".pdf";

            Response.Clear();
            Response.ContentType = "Application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
            HtmlConverter.ConvertToPdf(getPanelHtml(), Response.OutputStream);
            Response.Flush();
            Response.Close();
            Response.End();
        }

        public string getPanelHtml()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            Panel1.RenderControl(hw);
            sb.AppendLine("<style>" +
                " tr td, tr th {border: 0.75px solid} " +
                "table {width:100%} " +
                ".table-responsive {display: block;width: 100%;overflow-x: auto;} " +
                ".table-2 {margin-top: 10px} " +
                ".table-1 {margin-top: 10px} " +
                "" +
                "</style>");
            var html = sb.ToString();

            return html;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            generatePDF();
        }
    }
}