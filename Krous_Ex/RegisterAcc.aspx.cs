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

namespace Krous_Ex
{
    public partial class RegisterAcc : System.Web.UI.Page
    {
        String EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];

        protected void Page_Load(object sender, EventArgs e)
        {
            btnPrevious.Visible = false;
        }

        private string Encrypt(string clearText)
        {
            string encryptoKey = EncryptionKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptoKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 
                                                                                          0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
            if (btnNext.Enabled)
            {
                btnPrevious.Visible = true;
            }
        }
    }
}