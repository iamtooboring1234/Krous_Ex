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
    public partial class ResetPassword : System.Web.UI.Page
    {
        String EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
        Guid ResetPasswordGUID = new Guid();
        Guid userGUID = new Guid();
        string userType = "";
        string alertMessage = "";
  
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["UserGUID"] == null || Request.QueryString["ResetPasswordGUID"] == null || Request.QueryString["UserType"] == null)
            {
                userType = Request.QueryString["UserType"].ToString();
                if (userType == "Student")
                {
                    Response.Redirect("StudentLogin.aspx");
                }
                else
                {
                    Response.Redirect("StaffLogin.aspx");
                }
            }
            else
            {
                if (!Guid.TryParse(Request.QueryString["ResetPasswordGUID"], out ResetPasswordGUID) || !Guid.TryParse(Request.QueryString["UserGUID"], out userGUID))
                {
                    if (userType == "Student")
                    {
                        Response.Redirect("StudentLogin.aspx");
                    }
                    else
                    {
                        Response.Redirect("StaffLogin.aspx");
                    }
                }
            }

            if (!IsPostBack)
            {
                if (!CheckResetStatus())
                {
                    Response.Write(alertMessage);
                }
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if(!(txtNewPassword.Text == "" || txtConfPassword.Text == ""))
            {
                if(!(txtConfPassword.Text != txtNewPassword.Text))
                {
                    ChangePassword();
                    Session["resetPassword"] = "Yes";
                    Response.Redirect("StudentLogin.aspx");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this,"Your password unable to change!");
                }
            }
        }

        protected bool CheckResetStatus()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand checkCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                if(Request.QueryString["UserType"] == "Student")
                {
                    userType = "Student";
                }
                else
                {
                    userType = "Staff";
                }

                checkCmd = new SqlCommand("SELECT * FROM ResetPassword WHERE " + userType + "GUID = @userGUID AND Status = 'Pending' AND LinkToken = @LinkToken ",con);
                checkCmd.Parameters.AddWithValue("@userGUID", Guid.Parse(Request.QueryString["UserGUID"]));
                checkCmd.Parameters.AddWithValue("@LinkToken", Request.QueryString["LinkToken"]);
                SqlDataReader dtrCheck = checkCmd.ExecuteReader();
                DataTable dtCheck = new DataTable();
                dtCheck.Load(dtrCheck);

                DateTime expiredDate = DateTime.Parse(dtCheck.Rows[0]["ExpiredTime"].ToString());
                string status = dtCheck.Rows[0]["Status"].ToString();

                if (dtCheck.Rows.Count == 0)
                {
                    alertMessage = "Invalid reset password request. Please request again at the login page by clicking on the Forgot Password.";
                }
                else if(DateTime.Now >= expiredDate)
                {
                    alertMessage = "The reset password link has been passed 15 minutes (EXPIRED). Please request again at the login page by clicking on the Forgot Password.";
                }
                else if(status == "Completed")
                {
                    alertMessage = "You have used the link to reset password successfully.";
                }
                return false;

            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }

        }

        protected bool ChangePassword()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand updPassCmd = new SqlCommand();
                SqlCommand updResetStatusCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                Guid userGUID = Guid.Parse(Request.QueryString["UserGUID"].ToString());

                if (Request.QueryString["UserType"] == "Student")
                {
                    userType = "Student";
                }
                else
                {
                    userType = "Staff";
                }

                String encryptedPassword = "";
                encryptedPassword = Encrypt(txtNewPassword.Text);

                //UPDATE Student and Staff password
                //updPassCmd = new SqlCommand("UPDATE " + userType + " SET " + userType + "Password = @Password WHERE " + userType + "GUID = @UserGUID AND Email = " + userEmail + "", con);
                updPassCmd = new SqlCommand("UPDATE " + userType + " SET " + userType + "Password = @Password WHERE " + userType + "GUID = @UserGUID", con);
                //updPassCmd.Parameters.AddWithValue("@Password", txtNewPassword.Text); 
                updPassCmd.Parameters.AddWithValue("@Password", encryptedPassword);
                updPassCmd.Parameters.AddWithValue("@UserGUID", userGUID);
                updPassCmd.ExecuteNonQuery();

                //UPDATE ResetPassword status to Completed
                updResetStatusCmd = new SqlCommand("UPDATE ResetPassword SET Status = 'Completed' WHERE " + userType + "GUID = @UserGUID AND Status = 'Pending'",con);
                updResetStatusCmd.Parameters.AddWithValue("@UserGUID", userGUID);
                updResetStatusCmd.ExecuteNonQuery();

                con.Close();

                clsFunction.DisplayAJAXMessage(this,"Your password has been changed successfully! Please use your new reset password to login to your account.");

                txtNewPassword.Text = string.Empty;
                txtConfPassword.Text = string.Empty;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }

            return false;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["UserType"] == "Student")
            {
                Response.Redirect("StudentLogin.aspx");
            }
            else
            {
                Response.Redirect("StaffLogin.aspx");
            }
        }
    }
}