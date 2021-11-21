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
    public partial class StudentViewOverallResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadOverallSemesterResult();
            }
        }

        private void loadOverallSemesterResult()
        {
            litSemesterResult.Text = "";
            string strTable = "";
            string sqlQuery = "SELECT * FROM ExamResult er ";
            sqlQuery += "LEFT JOIN ExamResultPerCourse ec ON er.ExamResultGUID = ec.ExamResultGUID ";
            sqlQuery += "LEFT JOIN Session S ON er.SessionGUID = s.SessionGUID ";
            sqlQuery += "LEFT JOIN Student St ON er.StudentGUID = st.StudentGUID ";
            sqlQuery += "LEFT JOIN Student_Programme_Register spr ON st.StudentGUID = spr.StudentGUID ";
            sqlQuery += "LEFT JOIN Programme P ON spr.ProgrammeGUID = P.ProgrammeGUID ";
            sqlQuery += "LEFT JOIN Course C ON ec.CourseGUID = C.CourseGUID ";
            sqlQuery += "WHERE er.StudentGUID = @StudentGUID AND er.Status = 'Release'";
            sqlQuery += "ORDER BY SessionYear, SessionMonth, CourseName";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            con.Open();
            SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

            GetCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());

            SqlDataReader reader = GetCommand.ExecuteReader();

            DataTable dtSemesterResult = new DataTable();
            dtSemesterResult.Load(reader);
            con.Close();

            if(dtSemesterResult.Rows.Count!= 0)
            {
                strTable = "<table class=\"table table-bordered table-hover OverallResultTable\">";
                strTable += "<tbody>";
                strTable += "<tr>";
                strTable += "<td width=\"200px\"><strong>Name : </strong></td>";
                strTable += "<td>" + dtSemesterResult.Rows[0]["StudentFullName"] + "</td>";
                strTable += "</tr>";
                strTable += "<tr>";
                strTable += "<td><strong>MyKad Number : </strong></td>";
                strTable += "<td>" + dtSemesterResult.Rows[0]["NRIC"] + "</td>";
                strTable += "</tr>";
                strTable += "<tr>";
                strTable += "<td><strong>Programme : </strong></td>";
                strTable += "<td>" + dtSemesterResult.Rows[0]["ProgrammeName"] + "</td>";
                strTable += "</tr>";
                if (dtSemesterResult.Rows[0]["ProgrammeCategory"].ToString() == "Bachelor Degree")
                {
                    strTable += "<tr>";
                    strTable += "<td>English Language Exit Requirement Achieved :</td>";
                    strTable += "<td>Bachelor's Degree students must achieve a valid MUET score of at least Band 3 to be eligible for graduation</td>";
                    strTable += "</tr>";
                }
                strTable += "</tbody>";
                strTable += "</table>";
                strTable += "<div class=\"mt-3\">";
                strTable += "<table border=\"1\" width=\"100%\" class=\"table table-bordered table-hover OverallResultTable\">";
                strTable += "<thead>";
                strTable += "<tr>";
                strTable += "<th width=\"150\" class=\"text-center\">Paper Type </th>";
                strTable += "<th width=\"150\" class=\"text-center\">Course </th>";
                strTable += "<th class=\"hidden-480 text-center\">Course Description </th>";
                strTable += "<th width=\"80\" class=\"text-center\">Grade </th>";
                strTable += "<th width=\"100\" class=\"text-center\">Remarks </th>";
                strTable += "</tr>"; 
                strTable += "</thead>";

                string lastSession = "";

                for (int i = 0; i < dtSemesterResult.Rows.Count; i++)
                {
                    if (dtSemesterResult.Rows[i]["SessionGUID"].ToString() != lastSession)
                    {
                        strTable += "<tbody>";
                        strTable += "<tr>";
                        strTable += "<td colspan=\"5\"><strong>" + dtSemesterResult.Rows[i]["SessionYear"].ToString() + dtSemesterResult.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0') + "</strong></td>";
                        strTable += "</tr>";
                    } 

                    strTable += "<tr>";
                    strTable += "<td class=\"text-center\">" + dtSemesterResult.Rows[i]["Category"].ToString().Substring(0, 4) + "</td>";
                    strTable += "<td class=\"text-center\">" + dtSemesterResult.Rows[i]["CourseAbbrv"].ToString() + "</td>";
                    strTable += "<td class=\"hidden-480\">" + dtSemesterResult.Rows[i]["CourseName"].ToString() + "</td>";
                    strTable += "<td class=\"text-center\">" + dtSemesterResult.Rows[i]["Grade"].ToString() + "</td>";
                    strTable += "<td class=\"text-center\">" + dtSemesterResult.Rows[i]["Remarks"].ToString() + "</td>";
                    strTable += "</tr>";

                    lastSession = dtSemesterResult.Rows[i]["SessionGUID"].ToString();

                    if (i != dtSemesterResult.Rows.Count -1)
                    {
                        if (dtSemesterResult.Rows[i+1]["SessionGUID"].ToString() != lastSession)
                        {
                            strTable += "<tr>";
                            strTable += "<td colspan=\"5\"><strong>GPA: " + "</strong>" + dtSemesterResult.Rows[i]["GPA"].ToString() + "<br /><strong>CGPA: " + "</strong>" + dtSemesterResult.Rows[i]["CGPA"].ToString().PadLeft(2, '0') + "</td>";
                            strTable += "</tr>";
                        }
                    } 
                    else
                    {
                        strTable += "<tr>";
                        strTable += "<td colspan=\"5\"><strong>GPA: " + "</strong>" + dtSemesterResult.Rows[i]["GPA"].ToString() + "<br /><strong>CGPA: " + "</strong>" + dtSemesterResult.Rows[i]["CGPA"].ToString().PadLeft(2, '0') + "</td>";
                        strTable += "</tr>";
                    }
                }

                strTable += "</div>";
                strTable += "</tbody>";
                strTable += "</table>";

                litSemesterResult.Text = strTable;
            }
        }

        public void generatePDF()
        {
            string fileName = "OverallResult_" + clsLogin.GetLoginUserName() + "_" + DateTime.Now + ".pdf";

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