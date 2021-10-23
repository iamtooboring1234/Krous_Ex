using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class Chat : System.Web.UI.Page
    {
        public string UserName = "admin";
        public string UserImage = "img/logoKPM.png";
        public string UploadFolderPath = "~/Uploads/ChatFile";
        string strcon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                hdUserName.Value = clsLogin.GetLoginUserName();
            }

        }

        public void GetUserImage(string Username)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT ProfilePic from Teacher where TeacherGUID='" + Session["userGUID"] + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserImage = ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dr.GetValue(0).ToString();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }
        }

        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        protected void FileUploadComplete(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            AsyncFileUpload1.SaveAs(Server.MapPath(this.UploadFolderPath) + filename);
        }


    }
}