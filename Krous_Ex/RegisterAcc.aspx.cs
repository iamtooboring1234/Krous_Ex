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
using System.Text.RegularExpressions;

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

                cmd = new SqlCommand("INSERT INTO Student(StudentGUID, StudentUsername, StudentPassword, StudentFullName, Gender, DOB, PhoneNumber, Email, NRIC, Address, YearIntake, AccountRegisterDate) " +
                                                "VALUES (@StudentGUID, @StudentUsername, @StudentPassword, @StudentFullName, @Gender, @DOB, @PhoneNumber, @Email, @NRIC, @Address, @YearIntake, @AccountRegisterDate)", con);
                cmd.Parameters.AddWithValue("@StudentGUID", studentGUID);
                cmd.Parameters.AddWithValue("@StudentUsername", txtUsername.Text);
                cmd.Parameters.AddWithValue("@StudentPassword", Encrypt(txtPassword.Text.Trim()));
                cmd.Parameters.AddWithValue("@StudentFullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@Gender", rbGender.SelectedValue);
                cmd.Parameters.AddWithValue("@DOB", dob_date.SelectedValue + "-" + dob_month.SelectedValue + "-" + dob_year.SelectedValue);
                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@NRIC", txtNRIC.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@YearIntake", DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@AccountRegisterDate", DateTime.Now);
                //cmd.Parameters.AddWithValue("@BranchesGUID", "c44f8b97-b8f9-44e1-94ab-b21d4518cb29");
                //cmd.Parameters.AddWithValue("@FacultyGUID", "c913ca56-1809-4792-b5e7-f3f2b0eebd9c");

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
           
            if (txtUsername.Text == "")
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
            else if (!(clsValidation.CheckPasswordFormat(txtPassword.Text)))
            {
                clsFunction.DisplayAJAXMessage(this, "Password should have 8 - 20 characters with at least 1 uppercase, 1 number !");
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

            if (!(clsValidation.CheckPasswordFormat(txtPassword.Text)))
            {
                clsFunction.DisplayAJAXMessage(this, "Password should have 8 - 20 characters with at least 1 uppercase, 1 number !");
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
                clsFunction.DisplayAJAXMessage(this, "Please enter your IC number");
                return false;
            }
            else if(txtNRIC.Text.Length > 14)
            {
                clsFunction.DisplayAJAXMessage(this, "Invalid IC number entered.");
                return false;
            }
            //else if(!(clsValidation.CheckDuplicateICNo(txtNRIC.Text)))
            //{
            //    clsFunction.DisplayAJAXMessage(this, "Duplicated NRIC entered!");
            //    return false;
            //}

            if (!(txtPhoneNo.Text.Equals("")))
            {
                if (!(int.TryParse(txtPhoneNo.Text, out _)))
                {
                    clsFunction.DisplayAJAXMessage(this, "Your contact number can only be in numeric form.");
                    return false;
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your contact number.");
                return false;
            }

            if(txtPhoneNo.Text.Length > 12)
            {
                clsFunction.DisplayAJAXMessage(this, "Invalid contact number entered.");
                return false;
            }

            if (txtEmail.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter a valid email address.");
                return false;
            }
            else if (!(clsValidation.IsEmail(txtEmail.Text)))
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter a valid email address format.");
                return false;
            }

            if (txtAddress.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your home address.");
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