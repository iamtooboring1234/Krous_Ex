using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Krous_Ex
{
    public class clsLogin
    {
        class SurroundingClass
        {
            //public static string GetLoginUserGUID()
            //{
            //    string LoginUserGUID = "";

            //    try
            //    {
            //        HttpCookie authCookie;
            //        authCookie = HttpContext.Current.Session[FormsAuthentication.FormsCookieName];

            //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            //        if (ticket != null)
            //            LoginUserGUID = ticket.Name;
            //    }
            //    catch (Exception ex)
            //    {
            //        LoginUserGUID = "";
            //    }
            //    return LoginUserGUID;
            //}

            public static string GetLoginUserType()
            {
                string LoginUserType = "";

                try
                {
                    HttpCookie authCookie;
                    authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    if (ticket != null)
                        LoginUserType = ticket.UserData.ToString();
                }
                catch (Exception ex)
                {
                    LoginUserType = "";
                }
                return LoginUserType;
            }

            //public static string GetLoginUserName()
            //{
            //    try
            //    {
            //        string UserType;

            //        if (GetLoginUserType() != "Citizen")
            //            UserType = "Staff";
            //        else
            //            UserType = "Citizen";

            //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            //        con.Open();
            //        SqlCommand GetCommand = new SqlCommand("SELECT " + UserType + "Username FROM " + UserType + " WHERE " + UserType + "GUID = '" + GetLoginUserGUID() + "'", con);
            //        SqlDataReader reader = GetCommand.ExecuteReader();

            //        DataTable dtUser = new DataTable();
            //        dtUser.Load(reader);
            //        con.Close();

            //        if (dtUser.Rows.Count != 0)
            //            return dtUser.Rows[0]("" + UserType + "Username").ToString;
            //        else
            //            return "";
            //    }
            //    catch (Exception ex)
            //    {
            //        return "";
            //    }
            //}
        }

    }
}