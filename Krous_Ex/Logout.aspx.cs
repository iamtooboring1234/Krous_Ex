﻿using System;
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
        String userType;

        protected void Page_Load(object sender, EventArgs e)
        {
            var myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            
            if (myCookie != null)
            {
                userType = clsLogin.GetLoginUserType();

                if (!Page.IsPostBack)
                {
                    FormsAuthentication.SignOut();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (userType == "Staff")
            {
                Response.Redirect("StaffLogin.aspx");
            } else
            {
                Response.Redirect("StudentLogin.aspx");
            }
        }
    }
}