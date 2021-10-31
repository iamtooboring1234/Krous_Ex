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
    public partial class KrousExAcademicCalendarListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadIntake();
            }
        }

        private void loadIntake()
        {
            try
            {
                string sqlQuery;
                string strHeader = "";
                string strContent = "";
                bool isActiveTab = true;
                bool isActiveContent = true;

                sqlQuery = "select * from AcademicCalender A, Session S WHERE A.SessionGUID = S.SessionGUID AND Year(getdate()) -  Year(SemesterStartDate) < 3 order by SemesterStartDate;";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                
                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i % 3 == 0)
                        {
                            if (isActiveTab)
                            {
                                strHeader += "<li class=\"nav-item\" role=\"presentation\">";
                                strHeader += "<button class=\"nav-link active w-100\" id=\"tab-" + i + "\" data-toggle=\"tab\" data-target=\"#tab" + i + "\" type=\"button\" role=\"tab\" aria-controls=\"home\" aria-selected=\"true\"> Academic Calendar" + dt.Rows[i]["SessionYear"] + "/" + (int.Parse(dt.Rows[i]["SessionYear"].ToString()) + 1) + "</button>";
                                strHeader += "</li>";
                            } else
                            {
                                strHeader += "<li class=\"nav-item\" role=\"presentation\">";
                                strHeader += "<button class=\"nav-link w-100\" id=\"tab-" + i + "\" data-toggle=\"tab\" data-target=\"#tab" + i + "\" type=\"button\" role=\"tab\" aria-controls=\"home\" aria-selected=\"true\"> Academic Calendar" + dt.Rows[i]["SessionYear"] + "/" + (int.Parse(dt.Rows[i]["SessionYear"].ToString()) + 1) + "</button>";
                                strHeader += "</li>";
                            }
                        } else
                        {
                            isActiveTab = false;
                        }

                    }

                    strContent += "<div class=\"tab-content\" id=\"myTabContent\">";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i % 3 == 0)
                        {
                            if (isActiveContent)
                            {
                                strContent += "<div class=\"tab-pane fade show active\" id=\"tab" + i + "\" role=\"tabpanel\" aria-labelledby=\"home-tab\">";
                                strContent += "<ul>";
                                strContent += "<li><a href=\"KrousExViewAcademicCalendar.aspx?AcademicCalenderGUID="+ dt.Rows[i]["AcademicCalenderGUID"] + "\">" + dt.Rows[i]["CalenderName"] + "</a></li>";
                            } else
                            {
                                strContent += "<div class=\"tab-pane fade show\" id=\"tab" + i + "\" role=\"tabpanel\" aria-labelledby=\"home-tab\">";
                                strContent += "<ul>";
                                strContent += "<li><a href=\"KrousExViewAcademicCalendar.aspx?AcademicCalenderGUID=" + dt.Rows[i]["AcademicCalenderGUID"] + "\">" + dt.Rows[i]["CalenderName"] + "</a></li>";
                            }
                        }
                        else if (i % 3 == 2)
                        {
                            strContent += "<li><a href=\"KrousExViewAcademicCalendar.aspx?AcademicCalenderGUID=" + dt.Rows[i]["AcademicCalenderGUID"] + "\">" + dt.Rows[i]["CalenderName"] + "</a></li>";
                            strContent += "</ul>";
                            strContent += "</div>";
                            isActiveContent = false;
                        }
                        else
                        {
                            strContent += "<li><a href=\"KrousExViewAcademicCalendar.aspx?AcademicCalenderGUID=" + dt.Rows[i]["AcademicCalenderGUID"] + "\">" + dt.Rows[i]["CalenderName"] + "</a></li>";
                        }
                    }

                    strContent += "</div>";

                    Literal1.Text = strHeader;
                    Literal2.Text = strContent;
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
    }
}