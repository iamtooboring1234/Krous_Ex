using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StudentMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (myCookie != null)
            {
                litLogonName.Text = clsLogin.GetLoginUserName();
                litLogonNameSidebar.Text = clsLogin.GetLoginUserName();

                imgProfile.ImageUrl = clsLogin.GetUserImage();
                imgProfileSidebar.ImageUrl = clsLogin.GetUserImage();
            }
            else
            {
                Response.Redirect("StudentLogin");
            }
        }
    }
}