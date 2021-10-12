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
    public partial class SemesterEntry : System.Web.UI.Page
    {

        private Guid SemesterGUID;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtSemesterName.Text = "asd";
            CalendarExtender1.SelectedDate = DateTime.Today;
        }

        private bool insertFAQ()
        {
            Guid SemesterGUID = Guid.NewGuid();

            //string Username = clsLogin.GetLoginUserName;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO SEMESTER VALUES(@SemesterGUID,@SemesterName,@SemesterStartDate,@SemesterEndDate,@SemesterWeekDuration,@SemesterStudyDayDuration,@SemesterExamDayDuration,@SemesterBreakDayDuration)", con);

                InsertCommand.Parameters.AddWithValue("@SemesterGUID", SemesterGUID);
                InsertCommand.Parameters.AddWithValue("@SemesterName", txtSemesterName.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterStartDate", dtpSemesterStartDate.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterEndDate", dtpSemesterEndDate.Text);
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
    }
}