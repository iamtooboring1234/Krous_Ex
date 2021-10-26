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
    public partial class StudentProfile : System.Web.UI.Page
    {
        Guid userGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
                if (!(IsPostBack))
                {
                    if (userGUID != null)
                    {
                        loadData();
                    }
                }
                else
                {
                    Response.Redirect("StudentLogin.aspx");
                }
            }  
        }

        protected void loadData()
        {
            try
            {
                String StudentGUID = Request.QueryString["UserGUID"];
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                //change the join date format  

                cmd = new SqlCommand("SELECT s.StudentGUID, s.StudentFullName, s.Gender, CONVERT(varchar, s.DOB,1) as DOB, s.PhoneNumber, s.Email, s.NRIC, s.Address, s.ProfileImage, s.YearIntake, CONVERT(varchar, s.AccountRegisterDate, 1) as DateJoined, s.LastUpdateInfo, CONCAT(f.facultyname, ' ', (f.facultyAbbrv)) AS FacultyName, b.BranchesName FROM Student s LEFT JOIN Branches b ON s.BranchesGUID = b.BranchesGUID LEFT JOIN Faculty f ON s.FacultyGUID = f.FacultyGUID WHERE s.StudentGUID = @StudentGUID", con);
                cmd.Parameters.AddWithValue("@StudentGUID", userGUID);
                SqlDataReader dtrStudent = cmd.ExecuteReader();
                DataTable dtStud = new DataTable();
                dtStud.Load(dtrStudent);

                con.Close();

                //if got, then load data
                if(dtStud.Rows.Count > 0)
                {
                    txtFullname.Text = dtStud.Rows[0][1].ToString();
                    txtGender.Text = dtStud.Rows[0][2].ToString();
                    txtDOB.Text = dtStud.Rows[0][3].ToString();
                    txtContact.Text = dtStud.Rows[0][4].ToString();
                    txtEmail.Text = dtStud.Rows[0][5].ToString();
                    txtNRIC.Text = dtStud.Rows[0][6].ToString();
                    txtAddress.Text = dtStud.Rows[0][7].ToString();
                    txtYearIntake.Text = dtStud.Rows[0][9].ToString();
                    txtDateJoined.Text = dtStud.Rows[0][10].ToString();
                    txtFaculty.Text = dtStud.Rows[0][12].ToString();
                    txtBranch.Text = dtStud.Rows[0][13].ToString();
                    lblUpdateTime.Text = dtStud.Rows[0][11].ToString();

                    string profileImg = "";
                    if(dtStud.Rows[0][8] != null)
                    {
                        profileImg = ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dtStud.Rows[0][8].ToString();
                    }
                    imgProfile.ImageUrl = profileImg;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected bool updateDetails()
        {
            bool updateBoolean = false;
            try
            {
                Guid imgGuid = Guid.NewGuid();
                SqlConnection con = new SqlConnection();
                SqlCommand cmdUpdate = new SqlCommand();

                //upload image 
                String imgName = "ProfileImg_" + imgGuid.ToString() + Path.GetExtension(imageUpload.FileName);
                String savePath = ConfigurationManager.AppSettings.Get("ProfileUploadPath");
                string ProfileImgSavePath = Server.MapPath(savePath);
                String ProfileFullSavePath = ProfileImgSavePath + imgName;

                if (Directory.Exists(ProfileImgSavePath))
                {
                    if (!String.IsNullOrEmpty(ProfileFullSavePath)) { 
                        imageUpload.PostedFile.SaveAs(ProfileFullSavePath);
                        string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                        con = new SqlConnection(strCon);
                        con.Open();

                        cmdUpdate = new SqlCommand("UPDATE Student SET Email = @email, PhoneNumber = @phoneNo, Address = @address, ProfileImage = @profileImage, LastUpdateInfo = @LastUpdateInfo WHERE StudentGUID = @StudentGUID", con);
                        cmdUpdate.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmdUpdate.Parameters.AddWithValue("@StudentGUID", userGUID);
                        cmdUpdate.Parameters.AddWithValue("@phoneNo", txtContact.Text);
                        cmdUpdate.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmdUpdate.Parameters.AddWithValue("@profileImage", imgName);
                        cmdUpdate.Parameters.AddWithValue("@LastUpdateInfo", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmdUpdate.ExecuteNonQuery();

                        con.Dispose();
                        con.Close();

                        updateBoolean = true;
                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Not physical path.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
            return updateBoolean;
        }

        protected bool ChangePassword()
        {
            bool changePassBool = false;
            SqlConnection con = new SqlConnection();
            SqlCommand cmdPassword = new SqlCommand();

            String encryptedPassword = "";
            String decryptPassword = "";

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                cmdPassword = new SqlCommand("SELECT StudentPassword FROM Student WHERE StudentGUID = @StudentGUID", con);
                cmdPassword.Parameters.AddWithValue("@StudentGUID", userGUID);
                SqlDataReader dtrPass = cmdPassword.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrPass);

                //get the encrypted password and decrypt it 
                encryptedPassword = dt.Rows[0]["StudentPassword"].ToString();

                if(!(encryptedPassword.Equals("")))
                {
                    decryptPassword = Decrypt(encryptedPassword);
                    encryptedPassword = Encrypt(txtNewPass.Text);
                    cmdPassword = new SqlCommand("UPDATE Student SET StudentPassword = @studentPassword WHERE StudentGUID = @StudentGUID", con);
                    cmdPassword.Parameters.AddWithValue("@studentPassword", encryptedPassword);
                    cmdPassword.Parameters.AddWithValue("@StudentGUID", userGUID);

                    int affectedRows = cmdPassword.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        changePassBool = true;
                    }
                }
                con.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
            return changePassBool;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (updateValidation())
            {
                if (updateDetails())
                {
                    clsFunction.DisplayAJAXMessage(this, "Your information has been updated successfully!");
                    loadData();

                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to update details!");
                    loadData();
                }
            }
        }

        //can update password
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            if(!(txtCurrentPass.Text == "" && txtNewPass.Text == "" && txtConfNewPass.Text == ""))
            {
                if (ChangePassword())
                {
                    clsFunction.DisplayAJAXMessage(this, "Your password has been changed successfully!");
                    txtCurrentPass.Text = "";
                    txtNewPass.Text = "";
                    txtConfNewPass.Text = "";
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Failed to change password! Please make sure you have entered the correct password.");
                    txtCurrentPass.Text = "";
                    txtNewPass.Text = "";
                    txtConfNewPass.Text = "";
                    txtCurrentPass.Focus();
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please entered all the three password fields.");
            } 
        }
        
        protected bool updateValidation()
        {
            if (!(imageUpload.HasFile))
            {
                clsFunction.DisplayAJAXMessage(this, "Please choose and upload an image as your profile.");
                return false;
            }

            if (txtAddress.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter a valid home address.");
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

            if (!(txtContact.Text.Equals("")))
            {
                if (!(int.TryParse(txtContact.Text, out _)))
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

            if (txtContact.Text.Length > 12)
            {
                clsFunction.DisplayAJAXMessage(this, "Invalid contact number entered.");
                return false;
            }
            return true;
        }

        protected string Encrypt(string cipherText)
        {
            string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            byte[] clearBytes = Encoding.Unicode.GetBytes(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    cipherText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return cipherText;
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

    }
}