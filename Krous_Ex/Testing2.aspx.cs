using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class Testing2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGV();
        }

        private bool NewMeeting()
        {
            try
            {
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var now = DateTime.Now;
                var apiSecret = "Q0LoySQi1Q5qvNGQpcS3AjvmkUgoTfen7dKt";
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "fft_NLb7TBqnGMdiEXaMoA",
                    Expires = now.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var client = new RestClient("https://api.zoom.us/v2/users/me/meetings");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new { topic = "Topic", duration = "60", start_time = DateTime.Now, type = "2" });
                request.AddHeader("authorization", "Bearer" + tokenString);

                IRestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;
                var jObject = JObject.Parse(restResponse.Content);
                Host.Text = (string)jObject["start_url"];
                Join.Text = (string)jObject["join_url"];
                Code.Text = Convert.ToString(numericStatusCode);

                string RoomID = Join.Text.Substring(26);
                RoomID = RoomID.Substring(0, 11);
                string RoomPass = Join.Text.Substring(42);


                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                string sqlQuery = "INSERT INTO MeetingLink VALUES (newid(), 'Topic', @RoomID, @RoomPass, @CreatedDate)";

                SqlCommand insertCmd = new SqlCommand(sqlQuery, con);

                insertCmd.Parameters.AddWithValue("@RoomID", RoomID);
                insertCmd.Parameters.AddWithValue("@RoomPass", RoomPass);
                insertCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                insertCmd.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NewMeeting();
        }

        private void loadGV()
        {
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT * FROM MeetingLink ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                if (dtFAQ.Rows.Count != 0)
                {
                    gvFAQ.DataSource = dtFAQ;
                    gvFAQ.DataBind();
                    gvFAQ.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvFAQ.Visible = false;
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}