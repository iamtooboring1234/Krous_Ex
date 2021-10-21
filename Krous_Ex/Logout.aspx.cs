using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class Logout : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            
            if (myCookie != null)
            {
                if (!Page.IsPostBack)
                {
                    FormsAuthentication.SignOut();
                }
            } 
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["UserType"]))
            {
                if (Request.QueryString["UserType"].ToString() == "Staff")
                {
                    Response.Redirect("StaffLogin.aspx");
                }
                else
                {
                    Response.Redirect("StudentLogin.aspx");
                }
            } else
            {
                Response.Redirect("Homepage.aspx");
            }
        }
    }
}