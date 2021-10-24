using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Krous_Ex.Hubs
{
    public class ChatHub : Hub
    {
        /*Class Group Chat*/
        static List<Users> ConnectedUsers = new List<Users>();
        static List<Messages> CurrentMessage = new List<Messages>();
        string strcon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                string UserImg = GetUserImage(userName);
                string logintime = DateTime.Now.ToString();
                ConnectedUsers.Add(new Users { ConnectionId = id, UserName = userName, UserImage = UserImg, LoginTime = logintime });

                // send to caller
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, UserImg, logintime);
            }
        }

        public void SendMessageToAll(string userName, string message, string time)
        {
            string UserImg = GetUserImage(userName);
            // store last 100 messages in cache
            AddMessageinCache(userName, message, time, UserImg);

            // Broad cast message
            Clients.All.messageReceived(userName, message, time, UserImg);

        }

        private void AddMessageinCache(string userName, string message, string time, string UserImg)
        {
            CurrentMessage.Add(new Messages { UserName = userName, Message = message, Time = time, UserImage = UserImg });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);

            // Refresh();
        }

        public string GetUserImage(string username)
        {
            string RetimgName = "Assests/main/images/faces/face1.jpg";
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT ProfilePic from Teacher where FullName='" + username + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();

                String ImageName = "";

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ImageName = "Uploads/Profile/" + dr.GetValue(0).ToString();
                    }
                }

                if (ImageName != "")
                    RetimgName = ImageName;

                con.Close();
            }
            catch (Exception ex)
            {

            }
            return RetimgName;
        }


        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }
            return base.OnDisconnected(stopCalled);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {

            string fromUserId = Context.ConnectionId;

            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {
                string CurrentDateTime = DateTime.Now.ToString();
                string UserImg = GetUserImage(fromUser.UserName);
                // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, UserImg, CurrentDateTime);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, UserImg, CurrentDateTime);
            }

        }

        /*FAQ Chat*/
        public void Send(string CurrentUserGUID, string userType, string message, string ChatGUID, string newChat, string MessageType)
        {
            var SendTimeDB = DateTime.Now;
            string SendTimeStr = SendTimeDB.ToString("hh:mm: tt");
            ManageMessageDB(CurrentUserGUID, userType, message, ChatGUID, newChat, MessageType, SendTimeDB);
            Clients.Group(ChatGUID).broadcastMessage(userType, message, MessageType, SendTimeStr);
        }

        public void Join(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }

        public void alertEndChatMsg(string ChatGUID, string userType, string message)
        {
            Clients.Group(ChatGUID).alertEndChat(userType, message);
        }

        private void ManageMessageDB(string CurrentUserGUID, string userType, string message, string ChatGUID, string newChat, string MessageType, DateTime SendDate)
        {
            if (newChat == "True")
            {
                InsertChat(CurrentUserGUID, ChatGUID);
            }

            InsertMessage(userType, message, ChatGUID, MessageType, SendDate);
        }

        private void InsertChat(string CurrentUserGUID, string ChatGUID)
        {
            try
            {
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                var InsertCommand = new SqlCommand("INSERT INTO Chat VALUES(@ChatGUID,@StudentGUID,@StaffGUID,@ChatStatus,@createdDate,@EndDate)", con);
                InsertCommand.Parameters.AddWithValue("@ChatGUID", Guid.Parse(ChatGUID.ToString()));
                InsertCommand.Parameters.AddWithValue("@StudentGUID", Guid.Parse(CurrentUserGUID.ToString()));
                InsertCommand.Parameters.AddWithValue("@StaffGUID", DBNull.Value);
                InsertCommand.Parameters.AddWithValue("@ChatStatus", "Pending");
                InsertCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
                InsertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
            }
        }

        private void InsertMessage(string userType, string message, string ChatGUID, string MessageType, DateTime SendDate)
        {
            try
            {
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                var InsertCommand = new SqlCommand("INSERT INTO [Message] VALUES(@MessageGUID,@ChatGUID,@MessageDetails,@MessageType,@UserType,@SendDate)", con);
                InsertCommand.Parameters.AddWithValue("@MessageGUID", Guid.NewGuid());
                InsertCommand.Parameters.AddWithValue("@ChatGUID", Guid.Parse(ChatGUID.ToString()));
                InsertCommand.Parameters.AddWithValue("@MessageDetails", message);
                InsertCommand.Parameters.AddWithValue("@MessageType", MessageType);
                InsertCommand.Parameters.AddWithValue("@UserType", userType);
                InsertCommand.Parameters.AddWithValue("@SendDate", SendDate.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}