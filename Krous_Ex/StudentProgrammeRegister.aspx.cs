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
    public partial class StudentCourseRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack != true)
            {

            }
        }


        protected void loadProgCourse()
        {
            try
            {
                ddlProgrammeCourse.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT BranchesName FROM ProgrammeCourse GROUP BY BranchesGUID, BranchesName ORDER BY BranchesName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlProgrammeCourse.DataSource = ds;
                ddlProgrammeCourse.DataTextField = "BranchesName";
                ddlProgrammeCourse.DataValueField = "BranchesName";
                ddlProgrammeCourse.DataBind();
                ddlProgrammeCourse.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }


    }
}