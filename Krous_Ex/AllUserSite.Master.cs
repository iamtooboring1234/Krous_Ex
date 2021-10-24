using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class AllUserSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (myCookie != null)
                {
                    panelLogon.Visible = true;
                    panelLogin.Visible = false;
                    litLogonName.Text = clsLogin.GetLoginUserName();
                    imgProfile.ImageUrl = clsLogin.GetUserImage();

                    if (clsLogin.GetLoginUserType() == "Student")
                    {
                        HyperLink1.NavigateUrl = "StudentDashboard.aspx";
                    } else
                    {
                        HyperLink1.NavigateUrl = "StaffDashboard.aspx";
                    }
                }
                else
                {
                    panelLogon.Visible = false;
                    panelLogin.Visible = true;
                }
            }
        }
    }
}