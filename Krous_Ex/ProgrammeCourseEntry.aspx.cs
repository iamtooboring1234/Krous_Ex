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
    public partial class ProgrammeCourseEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadProgrammeCategory();
                loadProgramme("");
                loadSemester();
            }
        }

        private void loadProgrammeCategory()
        {
            try
            {
                ddlProgrammCategory.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgrammCategory.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ProgrammeCategory FROM Programme GROUP BY ProgrammeCategory ORDER BY ProgrammeCategory", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProgCat = new DataTable();
                dtProgCat.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProgCat.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                    oList.Value = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                    ddlProgrammCategory.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading programme category.");
            }
        }

        private void loadProgramme(string programmeCategory)
        {
            try
            {
                ddlProgramme.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgramme.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ProgrammeGUID, ProgrammeAbbrv, ProgrammeName FROM Programme WHERE ProgrammeCategory = @ProgrammeCategory ORDER BY ProgrammeAbbrv", con);
                
                GetCommand.Parameters.AddWithValue("@ProgrammeCategory", programmeCategory);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProg = new DataTable();
                dtProg.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProg.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProg.Rows[i]["ProgrammeName"].ToString() + " (" + dtProg.Rows[i]["ProgrammeAbbrv"].ToString() + ")";
                    oList.Value = dtProg.Rows[i]["ProgrammeGUID"].ToString();
                    ddlProgramme.Items.Add(oList);
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }


        private void loadSemester()
        {
            try
            {
                ddlSemester.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlSemester.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Semester ORDER BY SemesterYear, SemesterSem ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSems = new DataTable();
                dtSems.Load(reader);
                con.Close();

                for (int i = 0; i <= dtSems.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = "Year " + dtSems.Rows[i]["SemesterYear"].ToString() + " Sem " + dtSems.Rows[i]["SemesterSem"].ToString();
                    oList.Value = dtSems.Rows[i]["SemesterGUID"].ToString();
                    ddlSemester.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading semester.");
            }
        }

        protected void ddlProgrammCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlProgrammCategory.SelectedValue != "")
            {
                loadProgramme(ddlProgrammCategory.SelectedValue);
                ddlProgramme.Enabled = true;
                ddlSemester.Enabled = true;
            } else
            {
                ddlProgramme.Enabled = false;
                ddlSemester.Enabled = false;
            }
        } 
    }
}