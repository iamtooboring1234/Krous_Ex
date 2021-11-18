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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StudentLogin : System.Web.UI.Page
    {
        String EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
        Guid userGuid = new Guid();
        String userType = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["FromURL"]))
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ForumGUID"]))
                {
                    HyperLink1.NavigateUrl = "StaffLogin.aspx?FromURL=" + Request.QueryString["FromURL"] + "&ForumGUID=" + Request.QueryString["ForumGUID"] + "&ForumGUID=" + Request.QueryString["ForumCategory"];
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"]))
                {
                    HyperLink1.NavigateUrl = "StaffLogin.aspx?FromURL=" + Request.QueryString["FromURL"] + "&ForumGUID=" + Request.QueryString["DiscGUID"];
                }
            }
            else
            {
                HyperLink1.NavigateUrl = "StaffLogin";
            }

            if (Session["StudentChangePass"] != null)
            {
                if (Session["StudentChangePass"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "Your password has been changed successfully! Please login with your new password.");
                    Session["StudentChangePass"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Your password unable to be changed!");
                    Session["StudentChangePass"] = null;
                }
            }


            if (Session["sendEmail"] != null)
            {
                if (Session["sendEmail"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "You will receive an email to change your password by clicking on link provided.");
                    Session["sendEmail"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to reset password!");
                    Session["sendEmail"] = null;
                }
            }

            if (Session["resetPassword"] != null)
            {
                if (Session["resetPassword"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "Your password has been reset. Please login using your new reset password.");
                    Session["resetPassword"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to reset password!");
                    Session["resetPassword"] = null;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" || txtPassword.Text != "")
            {
                if (validateUser(txtUsername.Text, txtPassword.Text))
                {
                    FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, userGuid.ToString(), DateTime.Now, DateTime.Now.AddMinutes(2880), false, userType, FormsAuthentication.FormsCookiePath);
                    //FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, userGuid.ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, userType);
                    string cookiestr = FormsAuthentication.Encrypt(tkt);
                    HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                    ck.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(ck);

                    if (String.IsNullOrEmpty(Request.QueryString["FromURL"]))
                    {
                        Response.Redirect("StudentDashboard.aspx", true);
                    } else {
                        if (!String.IsNullOrEmpty(Request.QueryString["ForumGUID"]))
                        {
                            Response.Redirect(Request.QueryString["FromURL"] + "?ForumGUID=" + Request.QueryString["ForumGUID"] + "&ForumCategory=" + Request.QueryString["ForumCategory"], true);
                        } else if (!String.IsNullOrEmpty(Request.QueryString["DiscGUID"]))
                        {
                            Response.Redirect(Request.QueryString["FromURL"] + "?DiscGUID=" + Request.QueryString["DiscGUID"], true);
                        }
                    }
                } 
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "User account not found. Please enter correct username or password.");
                    txtUsername.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtUsername.Focus();
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
            String studentPassword = "";

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                cmd = new SqlCommand("SELECT * FROM Student WHERE StudentUsername = @username", con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                userType = "Student";
                SqlDataReader dtrStudent = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrStudent);

                //get password
                encryptedPassword = dt.Rows[0]["StudentPassword"].ToString();
                userGuid = (Guid)dt.Rows[0]["StudentGUID"];

                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }

            //If no password is found, return false
           if (encryptedPassword != null)
			{
				if (!(encryptedPassword.Equals("")))
				{
                    studentPassword = Decrypt(encryptedPassword);
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
			return (String.Compare(studentPassword, password, false) == 0);

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

        protected void DisplayAlertMsg(string msg)
        {
            string myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
        }
    }
}