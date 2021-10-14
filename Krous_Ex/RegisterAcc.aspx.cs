using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Krous_Ex
{
    public partial class RegisterAcc : System.Web.UI.Page
    {
        String EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validateDetails())
            {
                if (insertNewUser())
                {
                    clsFunction.DisplayAJAXMessage(this, "Your new account has been successfully registered!");
                    Response.Redirect("StudentLogin");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to register acocunt. Please check if there's empty field not fill up.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
            }
        }
        
        private bool insertNewUser()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            Guid studentGUID = new Guid();

            try
            {
                studentGUID = Guid.NewGuid();
                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                cmd = new SqlCommand("INSERT INTO Student(StudGUID, StudUsername, StudPassword, StudFullName, Gender, DOB, PhoneNumber, Email, NRIC, YearIntake, AccountRegisterDate, BranchesID, FacultyID) " +
                                                "VALUES (@StudGUID, @StudUsername, @StudPassword, @StudFullName, @Gender, @DOB, @PhoneNumber, @Email, @NRIC, @YearIntake, @AccountRegisterDate, @BranchesID, @FacultyID)", con);
                cmd.Parameters.AddWithValue("@StudGUID", studentGUID);
                cmd.Parameters.AddWithValue("@StudUsername", txtUsername.Text);
                cmd.Parameters.AddWithValue("@StudPassword", Encrypt(txtPassword.Text.Trim()));
                cmd.Parameters.AddWithValue("@StudFullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@Gender", rbGender.SelectedValue);
                cmd.Parameters.AddWithValue("@DOB", dob_date.SelectedValue + "-" + dob_month.SelectedValue + "-" + dob_year.SelectedValue);
                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@NRIC", txtNRIC.Text);
                cmd.Parameters.AddWithValue("@YearIntake", 2021);
                cmd.Parameters.AddWithValue("@AccountRegisterDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@BranchesID", "B1001");
                cmd.Parameters.AddWithValue("@FacultyID", "F1001");

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
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

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your username.");
            }
            else if(txtPassword.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your password.");
            }
            else if (txtConfPass.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your confirm password.");
            }
            else if (txtConfPass.Text != txtPassword.Text)
            {
                clsFunction.DisplayAJAXMessage(this, "Your confirm password does not match!");
            }
            else
            {
                pnlLoginInfo.Visible = false;
                pnlLoginInfo.Enabled = false;
                pnlPersonalInfo.Visible = true;
                pnlPersonalInfo.Enabled = true;
            }
           
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            pnlLoginInfo.Visible = true;
            pnlLoginInfo.Enabled = true;
            pnlPersonalInfo.Visible = false;
            pnlPersonalInfo.Enabled = false;
        }

        private bool validateDetails()
        {
            if (txtUsername.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your username.");
                return false;
            }

            if (txtPassword.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your password.");
                return false;
            }

            if (txtConfPass.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your confirm password.");
                return false;
            }

            if (txtConfPass.Text != txtPassword.Text)
            {
                clsFunction.DisplayAJAXMessage(this, "Your confirm password does not match!");
                return false;
            }

            if (txtFullName.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your full name.");
                return false;
            }

            if (rbGender.SelectedIndex == -1)
            {
                clsFunction.DisplayAJAXMessage(this, "Please select your gender.");
                return false;
            }

            if (dob_date.SelectedIndex == 0)
            {
                clsFunction.DisplayAJAXMessage(this, "Please select date.");
                return false;
            }

            if (dob_month.SelectedIndex == 0)
            {
                clsFunction.DisplayAJAXMessage(this, "Please select month.");
                return false;
            }

            if (dob_year.SelectedIndex == 0)
            {
                clsFunction.DisplayAJAXMessage(this, "Please select year");
                return false;
            }

            if (txtNRIC.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your IC number.");
                return false;
            }

            if (txtPhoneNo.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your phone number.");
                return false;
            }

            if (txtEmail.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your email.");
                return false;
            }

            return true;
        }

        protected void DisplayAlertMsg(string msg)
        {
            string myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Hai" + "');", true);
        //}
    }
}