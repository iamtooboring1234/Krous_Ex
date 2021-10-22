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
    public partial class KrousExFAQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGV();
        }

        private void loadGV()
        {
            try
            {
                string sqlQuery;
                string strTable = "";
                string lastCategory = "";
                bool isNewCategory = true;
                int y = 1;

                sqlQuery = "SELECT * FROM FAQ WHERE FAQStatus = 'Active' ORDER BY FAQCategory";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0)
                {
                    for (int i = 0; i < dtFAQ.Rows.Count; i++)
                    {
                        if (lastCategory != dtFAQ.Rows[i]["FAQCategory"].ToString())
                        {
                            strTable += "<div class=\"container-fluid bg-success py-2\">";
                            strTable += "<p class=\"mb-0 text-white\">" + dtFAQ.Rows[i]["FAQCategory"] + "</p>";
                            strTable += "</div>";
                            isNewCategory = true;
                            y++;
                        } else
                        {
                            isNewCategory = false;
                        }
                        strTable += "<div class=\"card\">";
                        strTable += "<div id=\"accordion-" + y + "\" class=\"accordion\">";
                        strTable += "<div class=\"\" id=\"heading1\">";
                        strTable += "<h5 class=\"mb-0\">";
                        strTable += "<a class=\"faq-title\" data-toggle=\"collapse\" data-target=\"#collapse" + y + "\" aria-expanded=\"true\" aria-controls=\"collapseOne\">" + dtFAQ.Rows[i]["FAQTitle"] + "</a>";
                        strTable += "</h5>";
                        strTable += "</div>";
                        if (isNewCategory)
                        {
                            strTable += "<div id = \"collapse" + y + "\" class=\"collapse show\" aria-labelledby=\"heading1\" data-parent=\"#accordion-" + y + "\">";
                        } else
                        {
                            strTable += "<div id = \"collapse" + y + "\" class=\"collapse\" aria-labelledby=\"heading1\" data-parent=\"#accordion-" + y + "\">";
                        }
                        strTable += "<div class=\"card-body pt-0\" style=\"font-size: 14px; text-align:justify;\">" + dtFAQ.Rows[i]["FAQDescription"] + "<p class=\"pt-3\"><span class=\"font-weight-bold\">Last Updated Date: </span>" + dtFAQ.Rows[i]["LastUpdatedDate"] + "</p></div>";
                        strTable += "</div>";
                        strTable += "</div>";
                        strTable += "</div>";

                       
                        lastCategory = dtFAQ.Rows[i]["FAQCategory"].ToString();
                    }

                    litFAQ.Mode = LiteralMode.PassThrough;
                    litFAQ.Text = strTable;
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}