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
    public partial class StudentViewSemesterResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadSession();
            }
        }

        private void loadSession()
        {
            try
            {
                ddlSession.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlSession.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamResult er LEFT JOIN Session S ON S.SessionGUID = er.SessionGUID WHERE StudentGUID = @StudentGUID ORDER BY SessionYear, SessionMonth", con);

                GetCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSession = new DataTable();
                dtSession.Load(reader);
                con.Close();

                if (dtSession.Rows.Count != 0)
                {
                    for (int i = 0; i < dtSession.Rows.Count; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dtSession.Rows[i]["SessionYear"].ToString() + dtSession.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                        oList.Value = dtSession.Rows[i]["SessionGUID"].ToString();
                        ddlSession.Items.Add(oList);
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadSemesterResult()
        {
            try
            {
                litSemesterResult.Text = "";
                string strTable = "";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamResult er LEFT JOIN ExamResultPerCourse ec ON er.ExamResultGUID = ec.ExamResultGUID LEFT JOIN Session S ON er.SessionGUID = S.SessionGUID" +
                    " LEFT JOIN Student St ON er.StudentGUID = St.StudentGUID LEFT JOIN Student_Programme_Register spr ON St.StudentGUID = spr.StudentGUID " +
                    " LEFT JOIN Programme P ON spr.ProgrammeGUID =  P.ProgrammeGUID LEFT JOIN Course C ON ec.CourseGUID = C.CourseGUID" +
                    " WHERE er.SessionGUID = @SessionGUID AND er.StudentGUID = @StudentGUID ", con);

                GetCommand.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                GetCommand.Parameters.AddWithValue("@StudentGUID", clsLogin.GetLoginUserGUID());

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSemesterResult = new DataTable();
                dtSemesterResult.Load(reader);
                con.Close();

                if (dtSemesterResult.Rows.Count != 0)
                {
                    strTable = "<tbody>";
                    strTable += "<tr>";
                    strTable += "<td width=\"135\"> Name: </td>";
                    strTable += "<td width=\"485\">" + dtSemesterResult.Rows[0]["StudentFullName"] + "</td>";
                    strTable += "</tr>";
                    strTable += "<tr>";
                    strTable += "<td>IC Number : </td>";
                    strTable += "<td>" + dtSemesterResult.Rows[0]["NRIC"] + "</td>";
                    strTable += "</tr>";
                    strTable += "<tr>";
                    strTable += "<td>Session :</td>";
                    strTable += "<td>" + dtSemesterResult.Rows[0]["NRIC"] + "</td>";
                    strTable += "</tr>";
                    strTable += "<tr>";
                    strTable += "<td>Session :</td>";
                    strTable += "<td style=\"text-transform: uppercase;\">" + dtSemesterResult.Rows[0]["ProgrammeName"] + "</td>";
                    strTable += "</tr>";

                    if (dtSemesterResult.Rows[0]["ProgrammeCategory"].ToString() == "Bachelor Degree")
                    {
                        strTable += "<tr>";
                        strTable += "<td>English Language Exit Requirement Achieved :</td>";
                        strTable += "<td>Bachelor's Degree students must achieve a valid MUET score of at least Band 3 to be eligible for graduation</td>";
                        strTable += "</tr>";
                    }

                    strTable += "<tr>";
                    strTable += "<td>Result: </td>";
                    strTable += "<td>";
                    strTable += "<table width=\"100%\">";
                    strTable += "<tbody>";

                    int ttlCreditHour = 0;

                    for(int i = 0; i< dtSemesterResult.Rows.Count; i++)
                    {
                        strTable += "<tr>";
                        strTable += "<td width=\"80px\">" + dtSemesterResult.Rows[i]["CourseAbbrv"] +"</td>";
                        strTable += "<td>" + dtSemesterResult.Rows[i]["CourseName"] + "</td>";
                        strTable += "<td>" + dtSemesterResult.Rows[i]["Grade"] + "</td>";
                        strTable += "</tr>";
                        ttlCreditHour += int.Parse(dtSemesterResult.Rows[i]["CreditHour"].ToString());
                    }

                    strTable += "</tbody>";
                    strTable += "</table>";
                    strTable += "<br>";
                    strTable += "<br>";
                    strTable += "GPA: " + dtSemesterResult.Rows[0]["GPA"] + "<br>";
                    strTable += "CGPA: " + dtSemesterResult.Rows[0]["CGPA"] + "<br>";
                    strTable += "Credits Hour Earned: " + ttlCreditHour;
                    strTable += "</td>";
                    strTable += "</tr>";
                    strTable += "<tr>";
                    strTable += "<td >Status :</td>";
                    strTable += "<td>GOOD</td>";
                    strTable += "</tr>";
                    strTable += "<tr>";
                    strTable += "<td >Remarks  :</td>";
                    strTable += "<td>Congratulations for being listed in the Dean's List for outstanding academic performance for this examination. Please collect your Dean's List certificate from your Faculty office one (1) month from the date of release of results.";
                    strTable += "</tr>";
                    strTable += "<tr>";
                    strTable += "<td >Date of Release : </td>";
                    strTable += "<td>" + dtSemesterResult.Rows[0]["ReleaseDate"] + "</td>";
                    strTable += "</tr>";
                    strTable += "</tbody>";

                    litSemesterResult.Text = strTable;
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSemesterResult();
        }
        public void generatePDF()
        {
            string fileName = "SemesterResult_" + Request.QueryString["AcademicCalenderGUID"] + ".pdf";

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