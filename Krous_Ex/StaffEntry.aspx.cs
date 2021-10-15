using System;
using System.Collections.Generic;
using System.Configuration;
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool insettStaff()
        {
            Guid StaffGUID = Guid.NewGuid();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand InsertCommand = new SqlCommand("INSERT INTO STAFF VALUES(@StaffGUID,@StaffUsername,@StaffPassword,@StaffFullName,@Gender,@StaffRole,@StaffStatus,@PhoneNumber,@Email,@NRIC,@Specialization,@BranchesGUID,@FacultyGUID)", con);

                InsertCommand.Parameters.AddWithValue("@StaffGUID", StaffGUID);
                InsertCommand.Parameters.AddWithValue("@StaffUsername", txtUsername.Text);
                InsertCommand.Parameters.AddWithValue("@StaffPassword", Encrypt(txtPassword.Text.Trim()));
                InsertCommand.Parameters.AddWithValue("@StaffFullName", "Jerry Lau");
                InsertCommand.Parameters.AddWithValue("@Gender", "Male");
                InsertCommand.Parameters.AddWithValue("@StaffRole", "Head Admin");
                InsertCommand.Parameters.AddWithValue("@StaffStatus", "Active");
                InsertCommand.Parameters.AddWithValue("@PhoneNumber", "0123456789");
                InsertCommand.Parameters.AddWithValue("@Email", "jerrylau0725@gmail.com");
                InsertCommand.Parameters.AddWithValue("@NRIC", "000725-10-0739");
                InsertCommand.Parameters.AddWithValue("@Specialization", "Computer Science");
                InsertCommand.Parameters.AddWithValue("@BranchesGUID", "c44f8b97-b8f9-44e1-94ab-b21d4518cb29");
                InsertCommand.Parameters.AddWithValue("@FacultyGUID", "c913ca56-1809-4792-b5e7-f3f2b0eebd9c");

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            insettStaff();
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
    }
}