using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                        litLogonNameSidebar.Text = clsLogin.GetLoginUserName();
                        imgProfile.ImageUrl = clsLogin.GetUserImage();
                        imgProfileSidebar.ImageUrl = clsLogin.GetUserImage();
                        loadNotification();
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

        private void loadNotification()
        {
            string sqlQuery = "";
            string notification = "";
            try
            {
                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                sqlQuery = "SELECT * from Notification ";
                sqlQuery += "WHERE userGUID = @userGUID ";
                sqlQuery += "ORDER BY SentDate ";

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                GetCommand.Parameters.AddWithValue("@userGUID", clsLogin.GetLoginUserGUID());

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    notification += "<a class=\"dropdown-item preview-item\">";
                    notification += "<div class=\"preview-thumbnail\">";
                    notification += "<div class=\"preview-icon bg-dark rounded-circle\">";
                    notification += "<i class=\"mdi mdi-calendar text-success\"></i>";
                    notification += "</div>";
                    notification += "</div>";
                    notification += "<div class=\"preview-item-content\">";
                    notification += "<p class=\"preview-subject mb-1\">" + dt.Rows[i]["NotificationSubject"] + "</p>";
                    notification += "<p class=\"text-muted ellipsis mb-0\">" + dt.Rows[i]["NotificationContent"] + "</p>";
                    notification += "</div>";
                    notification += "</a>";
                }

                litNotification.Text = notification;
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}