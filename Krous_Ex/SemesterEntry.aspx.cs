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

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack != true)
            {
                if (Session["InsertSemester"] != null)
                {
                    if (Session["InsertSemester"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                        Session["InsertSemester"] = null;
                    }
                }

            }
        }

        private bool insertSemester()
        {
            Guid SemesterGUID = Guid.NewGuid();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO SEMESTER VALUES(@SemesterGUID,@SemesterYear,@SemesterSem,@SemesterType)", con);

                InsertCommand.Parameters.AddWithValue("@SemesterGUID", SemesterGUID);
                InsertCommand.Parameters.AddWithValue("@SemesterYear", txtSemesterYear.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterSem", txtSemesterYear.Text);
                InsertCommand.Parameters.AddWithValue("@SemesterType", ddlSemesterType.SelectedValue);

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
            if(insertSemester())
            {
                Session["InsertSemester"] = "Yes";
                Response.Redirect("SemesterEntry");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Error! Unable to insert.");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}