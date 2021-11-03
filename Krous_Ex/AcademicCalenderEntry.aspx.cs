using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class AcademicCalenderEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["InsertCalendar"] != null)
                {
                    if (Session["InsertCalendar"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                        Session["InsertCalendar"] = null;
                    }
                }

                txtSemesterStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtSemesterEndDate.Text = DateTime.Today.AddDays(139).ToString("dd/MM/yyyy");
                loadSession();
            }
        }

        private bool insertCalendar()
        {
            Guid academicCalenderGUID = Guid.NewGuid();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO AcademicCalender VALUES(@AcademicCalenderGUID,@SessionGUID,@CalenderName,@CalenderType,@SemesterStartDate,@SemesterEndDate,@SemesterStudyDuration,@SemesterExaminationDuration,@SemesterBreakDuration)", con);

                InsertCommand.Parameters.AddWithValue("@AcademicCalenderGUID", academicCalenderGUID);
                InsertCommand.Parameters.AddWithValue("@SessionGUID", ddlSession.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@CalenderName", txtSemesterName.Text);
                InsertCommand.Parameters.AddWithValue("@CalenderType", ddlCalenderType.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@SemesterStartDate", DateTime.ParseExact(txtSemesterStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                InsertCommand.Parameters.AddWithValue("@SemesterEndDate", DateTime.ParseExact(txtSemesterEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                InsertCommand.Parameters.AddWithValue("@SemesterStudyDuration", txtSemesterStudyDuration.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterExaminationDuration", txtSemesterExamDuration.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterBreakDuration", txtSemesterBreakDuration.Text);

                InsertCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (entryValidation())
            {
                if (insertCalendar())
                {
                    Session["InsertCalendar"] = "Yes";
                    Response.Redirect("AcademicCalenderEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter all the required information.");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        //protected void txtYear_TextChanged(object sender, EventArgs e)
        //{
        //    ddlSession.Items.Clear();

        //    try
        //    {
        //        string year = txtYear.Text;
        //        string[] str = new string[] { "05", "09", "01" };

        //        ListItem oList = new ListItem();

        //        oList = new ListItem();
        //        oList.Text = "";
        //        oList.Value = "";
        //        ddlSession.Items.Add(oList);

        //        foreach (string s in str)
        //        {
        //            oList = new ListItem();

        //            if (!s.Equals("01"))
        //            {
        //                oList.Text = year + s;
        //                oList.Value = year + s;
        //            } else
        //            {
        //                int intYear = int.Parse(year) + 1;
        //                oList.Text = intYear + s;
        //                oList.Value = intYear + s;
        //            }

        //            ddlSession.Items.Add(oList);
        //        }

        //    } catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        protected void radSemesterDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radSemesterDuration.SelectedValue == "1")
            {
                txtSemesterDay.Text = "139";
                int intDays = int.Parse(txtSemesterDay.Text);

                txtSemesterStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtSemesterEndDate.Text = DateTime.Today.AddDays(intDays).ToString("dd/MM/yyyy");
            }
            else
            {
                txtSemesterDay.Text = "83";
                int intDays = int.Parse(txtSemesterDay.Text);

                txtSemesterStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtSemesterEndDate.Text = DateTime.Today.AddDays(intDays).ToString("dd/MM/yyyy");
            }
        }

        protected void txtSemesterDay_TextChanged(object sender, EventArgs e)
        {
            int intDays = int.Parse(txtSemesterDay.Text);

            txtSemesterStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtSemesterEndDate.Text = DateTime.Today.AddDays(intDays).ToString("dd/MM/yyyy");
        }

        private void loadSession()
        {
            ddlSession.Items.Clear();
            string sqlQuery = "";
            try
            {
                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                sqlQuery = "select * from Session S LEFT JOIN AcademicCalender A ON S.SessionGUID = A.SessionGUID ";
                sqlQuery += "WHERE a.SessionGUID IS NULL ";
                sqlQuery += "order by S.SessionYear, S.SessionMonth ";

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSession = new DataTable();
                dtSession.Load(reader);
                con.Close();

                for (int i = 0; i <= dtSession.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtSession.Rows[i]["SessionYear"].ToString() + dtSession.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                    oList.Value = dtSession.Rows[i]["SessionGUID"].ToString();
                    ddlSession.Items.Add(oList);
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void txtSemesterEndDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.ParseExact(txtSemesterStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(txtSemesterEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (endDate.Subtract(startDate).Days < 0)
                {
                    clsFunction.DisplayAJAXMessage(this, "Error! End date must be higher than start date.");
                    int Days = int.Parse(txtSemesterDay.Text);
                    CalendarExtender2.SelectedDate = startDate.AddDays(Days);
                }
                else
                {
                    txtSemesterDay.Text = endDate.Subtract(startDate).Days.ToString();
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void txtSemesterStartDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int Days = int.Parse(txtSemesterDay.Text);
                DateTime startDate = DateTime.ParseExact(txtSemesterStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                CalendarExtender2.SelectedDate = startDate.AddDays(Days);
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected bool entryValidation()
        {
            if (txtSemesterName.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the semester name.");
                return false;
            }

            if (txtSemesterStudyDuration.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the semester study duration.");
                return false;
            }

            if (txtSemesterExamDuration.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the semester examination duration.");
                return false;
            }

            if (txtSemesterBreakDuration.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the semester break duration.");
                return false;
            }
            return true;
        }   

    }
}