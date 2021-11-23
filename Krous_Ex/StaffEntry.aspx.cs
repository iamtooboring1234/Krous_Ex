using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffEntry : System.Web.UI.Page
    {
        String EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
        Guid staffGUID;
        string userType = clsLogin.GetLoginUserType();
        Guid userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["addNewStaff"] != null)
                {
                    if (Session["addNewStaff"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showStaffAddSuccessToast(); ", true);
                        Session["addNewStaff"] = null;
                    }
                }

                if (Session["updateStaff"] != null)
                {
                    if (Session["updateStaff"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showStaffAddSuccessToast(); ", true);
                        Session["updateStaff"] = null;
                    }
                }


                loadFacultyList();
                loadBranchList();
                lblStaffStatus.Visible = false;
                txtStaffStatus.Visible = false;
                if (!String.IsNullOrEmpty(Request.QueryString["StaffGUID"]))
                {
                    staffGUID = Guid.Parse(Request.QueryString["StaffGUID"]);
                    loadStaffInfo();
                    btnSave.Visible = false;
                    btnBack.Visible = true;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                    txtUsername.Enabled = false;
                    txtStaffEmail.Enabled = true;
                    txtNRIC.Enabled = true;
                    txtPhoneNo.Enabled = true;
                    txtFullName.Enabled = false;
                    lblStaffStatus.Visible = true;
                    txtStaffStatus.Visible = true;
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

        protected void loadStaffInfo()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadStaffCmd = new SqlCommand();
                SqlCommand loadCourse = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadStaffCmd = new SqlCommand("SELECT * FROM Staff WHERE StaffGUID = @StaffGUID", con);
                loadStaffCmd.Parameters.AddWithValue("@StaffGUID", staffGUID);
                SqlDataReader dtrLoad = loadStaffCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                if (dt.Rows.Count != 0)
                {
                    txtUsername.Text = dt.Rows[0]["StaffUsername"].ToString();
                    txtFullName.Text = dt.Rows[0]["StaffFullName"].ToString();
                    rblGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                    ddlExistRole.SelectedValue = dt.Rows[0]["StaffRole"].ToString();
                    txtStaffPosition.Text = dt.Rows[0]["StaffPositiion"].ToString();
                    txtStaffStatus.Text = dt.Rows[0]["StaffStatus"].ToString();
                    txtPhoneNo.Text = dt.Rows[0]["PhoneNumber"].ToString();
                    txtStaffEmail.Text = dt.Rows[0]["Email"].ToString();
                    txtNRIC.Text = dt.Rows[0]["NRIC"].ToString();
                    txtSpecialization.Text = dt.Rows[0]["Specialization"].ToString();
                    ddlBranch.SelectedValue = dt.Rows[0]["BranchesGUID"].ToString();
                    ddlFaculty.SelectedValue = dt.Rows[0]["FacultyGUID"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadFacultyList()
        {
            try
            {
                ddlFaculty.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT FacultyGUID, FacultyName FROM Faculty GROUP BY FacultyGUID, FacultyName ORDER BY FacultyName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlFaculty.DataSource = ds;
                ddlFaculty.DataTextField = "FacultyName";
                ddlFaculty.DataValueField = "FacultyGUID";
                ddlFaculty.DataBind();
                ddlFaculty.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadBranchList()
        {
            try
            {
                ddlBranch.Items.Clear();
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT BranchesGUID, BranchesName FROM Branches GROUP BY BranchesGUID, BranchesName ORDER BY BranchesName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlBranch.DataSource = ds;
                ddlBranch.DataTextField = "BranchesName";
                ddlBranch.DataValueField = "BranchesGUID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private bool insertStaff()
        {
            Guid StaffGUID = Guid.NewGuid();
            Guid CourseIncGUID = Guid.NewGuid();
            string StaffRole;

            if (rdExistRole.Checked == true)
                StaffRole = ddlExistRole.SelectedValue;
            else
                StaffRole = txtNewStaffRole.Text;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO STAFF(StaffGUID, StaffUsername, StaffPassword, StaffFullName, Gender, StaffRole, StaffPositiion, StaffStatus, PhoneNumber, Email, NRIC, Specialization, BranchesGUID, FacultyGUID)" +
                                                                    "VALUES(@StaffGUID, @StaffUsername, @StaffPassword, @StaffFullName, @Gender, @StaffRole, @StaffPositiion, @StaffStatus, @PhoneNumber, @Email, @NRIC, @Specialization, @BranchesGUID, @FacultyGUID)", con);

                InsertCommand.Parameters.AddWithValue("@StaffGUID", StaffGUID);
                InsertCommand.Parameters.AddWithValue("@StaffUsername", txtUsername.Text);
                InsertCommand.Parameters.AddWithValue("@StaffPassword", Encrypt(txtNRIC.Text));
                InsertCommand.Parameters.AddWithValue("@StaffFullName", txtFullName.Text);
                InsertCommand.Parameters.AddWithValue("@Gender", rblGender.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@StaffRole", StaffRole);
                InsertCommand.Parameters.AddWithValue("@StaffPositiion", txtStaffPosition.Text);
                InsertCommand.Parameters.AddWithValue("@StaffStatus", "Active");
                InsertCommand.Parameters.AddWithValue("@PhoneNumber", txtPhoneNo.Text);
                InsertCommand.Parameters.AddWithValue("@Email", txtStaffEmail.Text);
                InsertCommand.Parameters.AddWithValue("@NRIC", txtNRIC.Text);
                InsertCommand.Parameters.AddWithValue("@Specialization", txtSpecialization.Text);
                InsertCommand.Parameters.AddWithValue("@BranchesGUID", ddlBranch.SelectedValue);
                InsertCommand.Parameters.AddWithValue("@FacultyGUID", ddlFaculty.SelectedValue);
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

        //can update gender, phone number, staff role, position, specialization, faculty and branch
        protected bool updateStaff()
        {
            staffGUID = Guid.Parse(Request.QueryString["StaffGUID"]);
            try
            {
                string StaffRole;

                if (rdExistRole.Checked == true)
                    StaffRole = ddlExistRole.SelectedValue;
                else
                    StaffRole = txtNewStaffRole.Text;

                SqlConnection con = new SqlConnection();
                SqlCommand updateCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                updateCmd = new SqlCommand("UPDATE Staff SET Gender = @Gender, NRIC = @NRIC, PhoneNumber = @PhoneNumber, Email = @Email, StaffRole = @StaffRole, StaffPositiion = @StaffPositiion, StaffStatus = @StaffStatus, Specialization = @Specialization, BranchesGUID = @BranchesGUID, FacultyGUID = @FacultyGUID  WHERE StaffGUID = @StaffGUID", con);
              
                updateCmd.Parameters.AddWithValue("@StaffGUID", staffGUID);
                updateCmd.Parameters.AddWithValue("@Gender", rblGender.SelectedValue);
                updateCmd.Parameters.AddWithValue("@NRIC", txtNRIC.Text);
                updateCmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNo.Text);
                updateCmd.Parameters.AddWithValue("@Email", txtStaffEmail.Text);
                updateCmd.Parameters.AddWithValue("@StaffRole", StaffRole);
                updateCmd.Parameters.AddWithValue("@StaffPositiion", txtStaffPosition.Text);
                updateCmd.Parameters.AddWithValue("@StaffStatus", "Active");
                updateCmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text);
                updateCmd.Parameters.AddWithValue("@BranchesGUID", ddlBranch.SelectedValue);
                updateCmd.Parameters.AddWithValue("@FacultyGUID", ddlFaculty.SelectedValue);
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

        protected bool deleteStaff()
        {
            staffGUID = Guid.Parse(Request.QueryString["StaffGUID"]);
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand deleteCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                deleteCmd = new SqlCommand("UPDATE Staff SET StaffStatus = 'Inactive' WHERE StaffGUID = @StaffGUID", con);
                deleteCmd.Parameters.AddWithValue("@StaffGUID", staffGUID);
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffListings");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validateDuplicate())
            {
                if (validateEmpty())
                {
                    if (insertStaff())
                    {
                        Session["addNewStaff"] = "Yes";
                        Response.Redirect("StaffEntry");
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Unable to add new staff entry.");
                    }
                } 
            }
        }


        //can update gender, phone number, staff role, position, specialization, faculty and branch
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (validateEmpty())
            {
                if (updateStaff())
                {
                    Session["updateStaff"] = "Yes";
                    Response.Redirect("StaffEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update staff details.");
                    loadStaffInfo();
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteStaff())
            {
                Session["deleteStaff"] = "Yes";
                Response.Redirect("StaffListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "No such records to be deleted.");
            }
        }

        protected bool validateEmpty()
        {
            if(txtUsername.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the staff username.");
                return false;
            }

            if(txtFullName.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the staff full name.");
                return false;
            }

            if(rblGender.SelectedIndex == -1)
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the staff gender.");
                return false;
            }

            if(txtNRIC.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the staff nric.");
                return false;
            }

            if (txtNRIC.Text.Length > 14)
            {
                clsFunction.DisplayAJAXMessage(this, "Invalid IC number entered.");
                return false;
            }

            if (!(txtPhoneNo.Text.Equals("")))
            {
                if (!(int.TryParse(txtPhoneNo.Text, out _)))
                {
                    clsFunction.DisplayAJAXMessage(this, "The phone number can only be in numeric form.");
                    return false;
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your phone number.");
                return false;
            }

            if (txtPhoneNo.Text.Length > 12)
            {
                clsFunction.DisplayAJAXMessage(this, "Invalid phone number entered.");
                return false;
            }

            if (txtStaffEmail.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter staff email address.");
                return false;
            }

            if (!(clsValidation.IsEmail(txtStaffEmail.Text)))
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter a valid email address format.");
                return false;
            }

            if (rdExistRole.Checked == true)
            {
                if (ddlExistRole.SelectedValue == "")
                {
                    clsFunction.DisplayAJAXMessage(this, "Please select the staff role.");
                    return false;
                }
            }

            if (rdNewRole.Checked == true)
            {
                if (txtNewStaffRole.Text == "")
                {
                    clsFunction.DisplayAJAXMessage(this, "Please enter the new staff role.");
                    return false;
                }
            }

            if (txtStaffPosition.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the staff position.");
                return false;
            }

            if(txtSpecialization.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the staff specialization.");
                return false;
            }

            if (ddlFaculty.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the faculty the staff work.");
                return false;
            }

            if (ddlBranch.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the branch the staff work.");
                return false;
            }

            return true;
        }

        protected bool validateDuplicate()
        {
            if (clsValidation.CheckStaffEntryDuplicateICNo(userType, txtNRIC.Text, userGUID))
            {
                clsFunction.DisplayAJAXMessage(this, "Duplicated NRIC entered!");
                return false;
            }

            if (clsValidation.CheckStaffEntryDuplicateEmail(userType, txtStaffEmail.Text))
            {
                clsFunction.DisplayAJAXMessage(this, "Duplicated Email entered!");
                return false;
            }

            return true;
        }

        private string Encrypt(string clearText)
        {
            string encryptoKey = EncryptionKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptoKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        protected void rdExistRole_CheckedChanged(object sender, EventArgs e)
        {
            if (rdExistRole.Checked == true)
            {
                txtNewStaffRole.Text = "";
                txtNewStaffRole.Enabled = false;
                ddlExistRole.Enabled = true;
            }
        }

        protected void rdNewRole_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNewRole.Checked == true)
            {
                ddlExistRole.SelectedValue = "";
                ddlExistRole.Enabled = false;
                txtNewStaffRole.Enabled = true;
            }
        }
    }
}