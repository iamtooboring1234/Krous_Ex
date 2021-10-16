using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void FileUploadComplete(object sender, EventArgs e)
        {
            //string SaveFolderPath = ConfigurationManager.AppSettings.Get("ChatSavePath");
            //string UploadChatFolderPath = ConfigurationManager.AppSettings.Get("ChatUploadPath");

            //string ChatImageName = System.IO.Path.GetFileNameWithoutExtension(AsyncFileUploadChat.FileName) + "_" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(AsyncFileUploadChat.FileName);
            //AsyncFileUploadChat.SaveAs(Server.MapPath(SaveFolderPath) + ChatImageName);

            //Response.Cookies("ImagePath").Value = UploadChatFolderPath;
            //Response.Cookies("ImagePath").Expires = DateTime.Now.AddMinutes(10);

            //Response.Cookies("ChatImageName").Value = ChatImageName;
            //Response.Cookies("ChatImageName").Expires = DateTime.Now.AddMinutes(10);
        }


    }
}