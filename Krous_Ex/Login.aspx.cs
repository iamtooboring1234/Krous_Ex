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
using System.Data;
using System.Data.SqlClient;

namespace Krous_Ex
{
    public partial class Login : System.Web.UI.Page
    {
        //private Guid StudGUID;
        //private Guid StaffGUID;

        String EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnStaffStud_Click(object sender, EventArgs e)
        {

            if (btnStaffStud.Text == "Login as Student")
            {
                btnStaffStud.Text = "Login as Staff";
            }
            else
            {
                btnStaffStud.Text = "Login as Student";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (validateDetails())
            {
                if (validateUser(txtUsername.Text, txtPassword.Text))
                {
                    if (btnStaffStud.Text == "Login as Student")
                    {
                        Session["Username"] = txtUsername.Text;
                        Session["Password"] = txtPassword.Text;
                        Response.Redirect("Homepage.aspx");
                    }
                    else
                    {
                        Session["Username"] = txtUsername.Text;
                        Session["Password"] = txtPassword.Text;
                        Response.Redirect("StaffDashboard.aspx");
                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "User account not found. Please enter correct username and password.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please fill in the required details.");
            }
          
        }

        protected bool validateUser(String username, String password)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            String encryptedPassword = "";
            String lookupPassword = "";

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                if (btnStaffStud.Text == "Login as Student")
                {
                    cmd = new SqlCommand("SELECT StudentUsername, StudentPassword FROM Student WHERE StudentUsername = @username AND StudentPassword = @password", con);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    SqlDataReader dtrStudent = cmd.ExecuteReader();
                    DataTable dtStudent = new DataTable();
                    dtStudent.Load(dtrStudent);

                    encryptedPassword = dtStudent.Rows[0]["StudPassword"].ToString();
                }
                else if (btnStaffStud.Text == "Login as Staff")
                {
                    cmd = new SqlCommand("SELECT StaffUsername, StaffPassword FROM Staff WHERE StaffUsername = @username AND StaffPassword = @password", con);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    SqlDataReader dtrStaff = cmd.ExecuteReader();
                    DataTable dtStaff = new DataTable();
                    dtStaff.Load(dtrStaff);

                    encryptedPassword = dtStaff.Rows[0]["StaffPassword"].ToString();
                }
                   
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }

            //If no password is found, return false
            if (encryptedPassword != null)
            {
                if (!(encryptedPassword.Equals("")))
                {
                    lookupPassword = Decrypt(encryptedPassword);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return (String.Compare(lookupPassword, password, false) == 0);
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
            return true;
        }

        private string Decrypt(string cipherText)
        {
            string cryptoKey = EncryptionKey;
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(cryptoKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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

        protected void DisplayAlertMsg(string msg)
        {
            string myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
        }

    }

}