using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (myCookie != null)
                {
                    if (clsLogin.GetLoginUserType() == "Staff")
                    {
                        litLogonName.Text = clsLogin.GetLoginUserName();
                    } else
                    {
                        //acces denied
                        Response.Redirect("Homepage");
                    }
                }
                else
                {
                    Response.Redirect("StaffLogin");
                }
            }
        }
    }
}