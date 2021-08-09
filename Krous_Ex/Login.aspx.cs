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
        String EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];

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
            try
            {
                if(ValidateUser(txtUsername.Text, txtPassword.Text))
                {
                    Response.Redirect("Homepage.aspx");
                }
                else
                {
                    DisplayAlertMsg("User account not found. Please enter correct username and password");
                }
        }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, "Error");
            }
        }

        protected bool ValidateUser(String username, String password)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            String encryptedPassword = "";
            String lookupPassword;

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                if (btnStaffStud.Text == "Login as Student")
                {
                    String studCmd = "Select StudUsername, StudPassword from Student where StudUsername = @StudUsername";
                    SqlCommand cmdStud = new SqlCommand(studCmd, con);

                    //get password
                    encryptedPassword = dt.Rows[0]["StudPassword"].ToString();

                }
                else
                {
                    String staffCmd = "Select StaffUsername, StaffPassword from Staff where StaffUsername = @StaffUsername";
                    SqlCommand cmdStaff = new SqlCommand(staffCmd, con);

                    //get password
                    encryptedPassword = dt.Rows[0]["StaffPassword"].ToString();
                }
                
                SqlDataReader dtrSelect = cmd.ExecuteReader();
                dt.Load(dtrSelect);

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