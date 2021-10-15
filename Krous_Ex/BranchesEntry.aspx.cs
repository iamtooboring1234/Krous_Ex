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
    public partial class BranchesEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private bool insertBranches()
        {
            Guid BranchesGUID = Guid.NewGuid();

            //string Username = clsLogin.GetLoginUserName;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO BRANCHES VALUES(@BranchesGUID,@BranchesName,@BranchesAddress,@BranchesEmail,@BranchesTel)", con);

                InsertCommand.Parameters.AddWithValue("@BranchesGUID", BranchesGUID);
                InsertCommand.Parameters.AddWithValue("@BranchesName", txtBranchName.Text);
                InsertCommand.Parameters.AddWithValue("@BranchesAddress", txtBranchAddress.Text);
                InsertCommand.Parameters.AddWithValue("@BranchesEmail", txtBranchEmail.Text);
                InsertCommand.Parameters.AddWithValue("@BranchesTel", txtBranchPhone.Text);

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

        private bool validateBranches()
        {
            if(txtBranchName.Text == "")
            {
                return false;
            }

            if (txtBranchAddress.Text == "")
            {
                return false;
            }

            if (txtBranchEmail.Text == "")
            {
                return false;
            }

            if (clsValidation.IsEmail(txtBranchEmail.Text) == false)
            {
                clsFunction.DisplayAJAXMessage(this, "Wrong email format");
                return false;
            }

            if (txtBranchPhone.Text == "")
            {
                return false;
            }

            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validateBranches())
            {
                if (insertBranches())
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "ShowPopup();", true);
                    Response.Redirect("BranchesEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
                }

            } else
            {
                clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}