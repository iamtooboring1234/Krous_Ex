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
    public partial class FacultyEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool insertFaculty()
        {
            Guid FacultyGUID = Guid.NewGuid();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO FACULTY VALUES(@FacultyGUID,@FacultyAbbrv,@FacultyName,@FacultyDesc)", con);

                InsertCommand.Parameters.AddWithValue("@FacultyGUID", FacultyGUID);
                InsertCommand.Parameters.AddWithValue("@FacultyAbbrv", txtFacultyAbbrv.Text);
                InsertCommand.Parameters.AddWithValue("@FacultyName", txtFacultyName.Text);
                InsertCommand.Parameters.AddWithValue("@FacultyDesc", txtFacultyDesc.Text);


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
            if (insertFaculty())
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "ShowPopup();", true);
                Response.Redirect("FacultyEntry");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        public string GetInitials(string MyText)
        {
            string Initials = "";
            string[] AllWords = MyText.Split(' ');
            foreach (string Word in AllWords)
            {
                if (Word.Length > 0)
                    Initials = Initials + Word[0].ToString().ToUpper();
            }
            return Initials;
        }


        protected void txtFacultyName_TextChanged(object sender, EventArgs e)
        {
            txtFacultyAbbrv.Text = GetInitials(txtFacultyName.Text);
        }
    }
}