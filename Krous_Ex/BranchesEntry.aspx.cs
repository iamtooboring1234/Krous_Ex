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
    public partial class BranchesEntry : System.Web.UI.Page
    {
        Guid branchesGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["InsertBranch"] != null)
                {
                    if (Session["InsertBranch"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showBranchAddSuccessToast(); ", true);
                        Session["InsertBranch"] = null;
                    }
                }

                if (Session["updateBranch"] != null)
                {
                    if (Session["updateBranch"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showBranchUpdateSuccessToast(); ", true);
                        Session["updateBranch"] = null;
                    }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["BranchesGUID"]))
                {
                    branchesGUID = Guid.Parse(Request.QueryString["BranchesGUID"]);
                    loadBranchInfo();
                    btnSave.Visible = false;
                    btnBack.Visible = true;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                    btnBack.Visible = false;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        protected void loadBranchInfo()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCourseCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCourseCmd = new SqlCommand("SELECT * FROM BRANCHES WHERE BranchesGUID = @BranchesGUID", con);
                loadCourseCmd.Parameters.AddWithValue("@BranchesGUID", branchesGUID);
                SqlDataReader dtrLoad = loadCourseCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                if (dt.Rows.Count != 0)
                {
                    txtBranchName.Text = dt.Rows[0]["BranchesName"].ToString();
                    txtBranchAddress.Text = dt.Rows[0]["BranchesAddress"].ToString();
                    txtBranchEmail.Text = dt.Rows[0]["BranchesEmail"].ToString();
                    txtBranchPhone.Text = dt.Rows[0]["BranchesTel"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }


        private bool insertBranches()
        {
            Guid BranchesGUID = Guid.NewGuid();

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

        protected bool updateBranches()
        {
            branchesGUID = Guid.Parse(Request.QueryString["BranchesGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand updateCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                updateCmd = new SqlCommand("UPDATE Branches SET BranchesName = @BranchesName, BranchesAddress = @BranchesAddress, BranchesEmail = @BranchesEmail, BranchesTel = @BranchesTel WHERE BranchesGUID = @BranchesGUID", con);
                updateCmd.Parameters.AddWithValue("@BranchesGUID", branchesGUID);
                updateCmd.Parameters.AddWithValue("@BranchesName", txtBranchName.Text);
                updateCmd.Parameters.AddWithValue("@BranchesAddress", txtBranchAddress.Text);
                updateCmd.Parameters.AddWithValue("@BranchesEmail", txtBranchEmail.Text);
                updateCmd.Parameters.AddWithValue("@BranchesTel", txtBranchPhone.Text);
                updateCmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected bool deleteBranches()
        {
            branchesGUID = Guid.Parse(Request.QueryString["BranchesGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand deleteCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                deleteCmd = new SqlCommand("DELETE FROM Branches WHERE BranchesGUID = @BranchesGUID", con);
                deleteCmd.Parameters.AddWithValue("@BranchesGUID", branchesGUID);
                deleteCmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validateDuplicate())
            {
                if (validateBranches())
                {
                    if (insertBranches())
                    {
                        Session["InsertBranch"] = "Yes";
                        Response.Redirect("BranchesEntry");
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
                        txtBranchName.Text = string.Empty;
                        txtBranchPhone.Text = string.Empty;
                        txtBranchEmail.Text = string.Empty;
                        txtBranchAddress.Text = string.Empty;
                        txtBranchName.Focus();
                    }
                }            
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (validateBranches())
            {
                if (updateBranches())
                {
                    Session["updateBranch"] = "Yes";
                    Response.Redirect("BranchesEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update branch details.");
                    loadBranchInfo();
                }
                
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteBranches())
            {
                Session["deleteBranch"] = "Yes";
                Response.Redirect("BranchListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "No such records to be deleted.");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("BranchesListings");
        }

        private bool validateBranches()
        {
            if (txtBranchName.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the branch name.");
                return false;
            }

            if (txtBranchAddress.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the branch address.");
                return false;
            }

            if (txtBranchEmail.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the branch email address.");
                return false;
            }

            if (clsValidation.IsEmail(txtBranchEmail.Text) == false)
            {
                clsFunction.DisplayAJAXMessage(this, "Wrong email format");
                return false;
            }

            if (txtBranchPhone.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the branch phone number.");
                return false;
            }

            return true;
        }

        protected bool validateDuplicate()
        {
            if (clsValidation.CheckDuplicateBranchName(txtBranchName.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "This Branch Name is already exists in the database!");
                return false;
            }

            if (clsValidation.CheckDuplicateBranchEmail(txtBranchEmail.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "This Branch Email is already exists in the database!");
                return false;
            }

            return true;
        }


    }
}