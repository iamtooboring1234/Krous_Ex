using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Krous_Ex
{
    /// <summary>
    /// Summary description for KrousExWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class KrousExWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string endChat(string ChatGUID)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ChatStatus FROM Chat WHERE ChatGUID = '" + ChatGUID + "'", con);
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtChat = new DataTable();
                dtChat.Load(reader);
                con.Close();
                if (dtChat.Rows[0]["ChatStatus"].ToString() != "Ended")
                {
                    con.Open();
                    var UpdateCommand = new SqlCommand("UPDATE Chat SET ChatStatus = 'Ended', EndDate = @EndDate WHERE ChatGUID = '" + ChatGUID + "'", con);
                    UpdateCommand.Parameters.AddWithValue("@EndDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    UpdateCommand.ExecuteNonQuery();
                    con.Close();
                    return "Chat ended successfully";
                }
                else
                {
                    return "Chat already ended";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
