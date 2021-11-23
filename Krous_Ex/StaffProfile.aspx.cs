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
    public partial class StaffProfile : System.Web.UI.Page
    {
        Guid userGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
                if (IsPostBack != true)
                {
                    if (userGUID != null)
                    {
                        //wont display
                        if (Session["updateProfile"] != null)
                        {
                            if (Session["updateProfile"].ToString() == "Yes")
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showProfileUpdateSuccessToast(); ", true);
                                Session["updateProfile"] = null;
                            }
                        }

                        loadData();
                    }
                }
            }
            else
            {
                Response.Redirect("StaffLogin.aspx");
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

                cmd = new SqlCommand("SELECT s.StaffGUID, s.StaffFullName, s.Gender, s.PhoneNumber, s.Email, s.NRIC, s.StaffRole, s.StaffPositiion, s.Specialization, CONCAT(f.facultyname, ' ', (f.facultyAbbrv)) AS FacultyName, b.BranchesName, s.ProfileImage, s.LastUpdateInfo FROM Staff s LEFT JOIN Branches b ON s.BranchesGUID = b.BranchesGUID LEFT JOIN Faculty f ON s.FacultyGUID = f.FacultyGUID WHERE s.StaffGUID = @StaffGUID", con);
                cmd.Parameters.AddWithValue("@StaffGUID", userGUID);
                SqlDataReader dtrStaff = cmd.ExecuteReader();
                DataTable dtStaff = new DataTable();
                dtStaff.Load(dtrStaff);

                con.Close();

                //if got, then load data
                if (dtStaff.Rows.Count > 0)
                {
                    txtFullname.Text = dtStaff.Rows[0][1].ToString();
                    txtGender.Text = dtStaff.Rows[0][2].ToString();
                    txtContact.Text = dtStaff.Rows[0][3].ToString();
                    txtEmail.Text = dtStaff.Rows[0][4].ToString();
                    txtNRIC.Text = dtStaff.Rows[0][5].ToString();
                    txtStaffRole.Text = dtStaff.Rows[0][6].ToString();
                    txtStaffPosition.Text = dtStaff.Rows[0][7].ToString();
                    txtSpecialization.Text = dtStaff.Rows[0][8].ToString();
                    txtFaculty.Text = dtStaff.Rows[0][9].ToString();
                    txtBranch.Text = dtStaff.Rows[0][10].ToString();
                    lblUpdateTime.Text = dtStaff.Rows[0][12].ToString();

                    string profileImg = "";
                    if (!String.IsNullOrEmpty(dtStaff.Rows[0][11].ToString()))
                    {
                        profileImg = ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dtStaff.Rows[0][11].ToString();
                    }
                    else
                    {
                        profileImg = ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + "defaultUserProfile.png";
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
                    if (!String.IsNullOrEmpty(ProfileFullSavePath))
                    {
                        imageUpload.PostedFile.SaveAs(ProfileFullSavePath);
                        string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                        con = new SqlConnection(strCon);
                        con.Open();

                        //get user image
                        SqlCommand getUserProfileName = new SqlCommand("SELECT ProfileImage FROM Staff WHERE StaffGUID = @StaffGUID", con);
                        getUserProfileName.Parameters.AddWithValue("@StaffGUID", userGUID);
                        SqlDataReader dtrImg = getUserProfileName.ExecuteReader();
                        DataTable dtImg = new DataTable();
                        dtImg.Load(dtrImg);

                        string userImage = dtImg.Rows[0]["ProfileImage"].ToString();

                        
                        cmdUpdate = new SqlCommand("UPDATE Staff SET Email = @email, PhoneNumber = @phoneNo, ProfileImage = @profileImage, LastUpdateInfo = @LastUpdateInfo WHERE StaffGUID = @StaffGUID", con);
                        cmdUpdate.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmdUpdate.Parameters.AddWithValue("@phoneNo", txtContact.Text);

                        if (!(imageUpload.HasFile))
                        {
                            cmdUpdate.Parameters.AddWithValue("@profileImage", userImage);
                        }
                        else
                        {
                            cmdUpdate.Parameters.AddWithValue("@profileImage", imgName);
                        }

                        cmdUpdate.Parameters.AddWithValue("@StaffGUID", userGUID);
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

                cmdPassword = new SqlCommand("SELECT StaffPassword FROM Staff WHERE StaffGUID = @StaffGUID", con);
                cmdPassword.Parameters.AddWithValue("@StaffGUID", userGUID);
                SqlDataReader dtrPass = cmdPassword.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrPass);

                //get the encrypted password and decrypt it 
                encryptedPassword = dt.Rows[0]["StaffPassword"].ToString();

                if (!(encryptedPassword.Equals("")))
                {
                    decryptPassword = Decrypt(encryptedPassword);

                    if(txtCurrentPass.Text == decryptPassword)
                    {
                        encryptedPassword = Encrypt(txtNewPass.Text);
                        cmdPassword = new SqlCommand("UPDATE Staff SET StaffPassword = @StaffPassword WHERE StaffGUID = @StaffGUID", con);
                        cmdPassword.Parameters.AddWithValue("@StaffPassword", encryptedPassword);
                        cmdPassword.Parameters.AddWithValue("@StaffGUID", userGUID);

                        int affectedRows = cmdPassword.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            changePassBool = true;
                        }
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Your current password is incorrect!");
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
                    Session["updateProfile"] = "Yes";
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
            if (!(txtCurrentPass.Text == "" || txtNewPass.Text == "" || txtConfNewPass.Text == ""))
            {
                if(!(txtNewPass.Text != txtConfNewPass.Text))
                {
                    if (ChangePassword())
                    {
                        Session["StaffChangePass"] = "Yes";
                        Response.Redirect("StaffLogin");
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
                    clsFunction.DisplayAJAXMessage(this, "Your password entered does not match! Please re-enter again.");
                }   
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please make sure all password are entered!");
            }
        }

        protected bool updateValidation()
        {
            if (txtEmail.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter a valid email address.");
                return false;
            }
            
            if (!(clsValidation.IsEmail(txtEmail.Text)))
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter a valid email address format.");
                return false;
            }

            if (txtContact.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter your contact number.");
                return false;
            }

            if (!(clsValidation.IsPhoneNumber(txtContact.Text)))
            {
                clsFunction.DisplayAJAXMessage(this, "Invalid contact number entered. It should be 10 or 11 digits without a dash.");
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
