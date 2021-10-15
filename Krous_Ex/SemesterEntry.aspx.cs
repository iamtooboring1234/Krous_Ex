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
            if (!Session["Role"].Equals("Student"))
            {

                if (IsPostBack != true)
                {
                    txtSemesterName.Text = "asd";
                    txtSemesterStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtSemesterEndDate.Text = DateTime.Today.AddDays(9).ToString("dd/MM/yyyy");
                }
            } else
            {
                Response.Redirect("Error_Page/Error_404.aspx"); 
            }
        }

        private bool insertFAQ()
        {
            Guid SemesterGUID = Guid.NewGuid();

            string SemesterCategory;

            //string Username = clsLogin.GetLoginUserName;

            if (rdExisting.Checked == true)
                SemesterCategory = ddlSemesterCategory.SelectedValue;
            else
                SemesterCategory = txtNewCategory.Text;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO SEMESTER VALUES(@SemesterGUID,@SemesterName,@SemesterCatogory,@SemesterStartDate,@SemesterEndDate,@SemesterWeekDuration,@SemesterStudyDayDuration,@SemesterExamDayDuration,@SemesterBreakDayDuration)", con);

                InsertCommand.Parameters.AddWithValue("@SemesterGUID", SemesterGUID);
                InsertCommand.Parameters.AddWithValue("@SemesterName", txtSemesterName.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterCategory", SemesterCategory);
                InsertCommand.Parameters.AddWithValue("@SemesterStartDate", txtSemesterStartDate.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterEndDate", txtSemesterEndDate.Text);
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

        protected void txtSemesterDuration_TextChanged(object sender, EventArgs e)
        {
            int Days = (int.Parse(txtSemesterDuration.Text) * 6) + int.Parse(txtExamDuration.Text) + int.Parse(txtSemesterBreakDuration.Text) + int.Parse(txtStudyDuration.Text);
            DateTime startDate = DateTime.Parse(txtSemesterStartDate.Text);
            CalendarExtender2.SelectedDate = startDate.AddDays(Days);
        }

        protected void txtStudyDuration_TextChanged(object sender, EventArgs e)
        {

        }
         
        protected void txtSemesterEndDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtSemesterStartDate_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(txtSemesterDuration.Text) > 0 || int.Parse(txtExamDuration.Text) > 0 || int.Parse(txtSemesterBreakDuration.Text) > 0 || int.Parse(txtStudyDuration.Text) > 0) {

                int Days = (int.Parse(txtSemesterDuration.Text) * 6) + int.Parse(txtExamDuration.Text) + int.Parse(txtSemesterBreakDuration.Text) + int.Parse(txtStudyDuration.Text);

                DateTime startDate = DateTime.Parse(txtSemesterStartDate.Text);
                CalendarExtender2.SelectedDate = startDate.AddDays(Days);
            }
        }

        protected void rdExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (rdExisting.Checked == true)
            {
                txtNewCategory.Text = "";
                txtNewCategory.Enabled = false;
                ddlSemesterCategory.Enabled = true;
            }
        }

        protected void rdNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNew.Checked == true)
            {
                ddlSemesterCategory.SelectedValue = "";
                ddlSemesterCategory.Enabled = false;
                txtNewCategory.Enabled = true;
            }
        }
    }
}