using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
                txtFirstSemesterStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtFirstSemesterEndDate.Text = DateTime.Today.AddDays(139).ToString("dd/MM/yyyy");
            }
        }

        private bool insertFAQ()
        {
            Guid SemesterGUID = Guid.NewGuid();

            //string Username = clsLogin.GetLoginUserName;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO SEMESTER VALUES(@SemesterGUID,@SemesterName,@SemesterCatogory,@SemesterStartDate,@SemesterEndDate,@SemesterWeekDuration,@SemesterStudyDayDuration,@SemesterExamDayDuration,@SemesterBreakDayDuration)", con);

                InsertCommand.Parameters.AddWithValue("@SemesterGUID", SemesterGUID);
                InsertCommand.Parameters.AddWithValue("@SemesterStartDate", txtFirstSemesterStartDate.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterEndDate", txtFirstSemesterEndDate.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterWeekDuration", 1);
                InsertCommand.Parameters.AddWithValue("@SemesterStudyDayDuration", 1);
                InsertCommand.Parameters.AddWithValue("@SemesterExamDayDuration", 1);
                InsertCommand.Parameters.AddWithValue("@SemesterBreakDayDuration", 1);

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
            insertFAQ();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void txtYear_TextChanged(object sender, EventArgs e)
        {
            ddlSession.Items.Clear();

            try
            {
                string year = txtYear.Text;
                string[] str = new string[] { "05", "09", "01" };

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlSession.Items.Add(oList);

                foreach (string s in str)
                {
                    oList = new ListItem();

                    if (!s.Equals("01"))
                    {
                        oList.Text = year + s;
                        oList.Value = year + s;
                    } else
                    {
                        int intYear = int.Parse(year) + 1;
                        oList.Text = intYear + s;
                        oList.Value = intYear + s;
                    }

                    ddlSession.Items.Add(oList);
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void txtFirstSemesterDays_TextChanged(object sender, EventArgs e)
        {
            int intDays = int.Parse(txtFirstSemesterDays.Text);

            txtFirstSemesterStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtFirstSemesterEndDate.Text = DateTime.Today.AddDays(intDays).ToString("dd/MM/yyyy");
        }
    }
}