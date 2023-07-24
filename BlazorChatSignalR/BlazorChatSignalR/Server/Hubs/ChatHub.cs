using BlazorChatSignalR.Server.Models;
using Microsoft.AspNetCore.SignalR;

namespace BlazorChatSignalR.Server.Hubs
{
	public class ChatHub:Hub
	{
        private static Dictionary<string, string> Users = new Dictionary<string, string>();
        private static Dictionary<int, User> AppUsersList = new Dictionary<int, User>();
        List<Message> messagesList=new();
        private User currentUser = new();
        static int i = 1;
      
        public override async Task OnConnectedAsync()
		{
            string username = Context.GetHttpContext().Request.Query["username"];
            var id=i++;
            currentUser=new User() { id=id, email=username, username=username, connectionId=Context.ConnectionId, status="online" };
            AppUsersList.Add(id, currentUser);
          
            Users.Add(Context.ConnectionId, username);
            await AddMessageToChat(string.Empty, $"{username} joined the party! \n Connection Id : {Context.ConnectionId}");
            await base.OnConnectedAsync();
		}
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            currentUser.status="Offline";
            //AppUsersList[currentUser.id]=currentUser;
            await AddMessageToChat(string.Empty, $"{username} left!");
        }
        public async Task AddMessageToChat(string user,string message)
		{
			await Clients.All.SendAsync("GetMessage",user, message);
		}

        public async Task AddMessageToPrivateChat(string fromuser, string touser, string message)
        {
            var user = AppUsersList.Where(e=>e.Value.username==touser).FirstOrDefault();
            Message message1 = new();
            message1.FromUserId=currentUser.id;
            message1.ToUserId=user.Value.id;
            message1.MessageText=message;
            messagesList.Add(message1);

            await Clients.Client(user.Value.connectionId).SendAsync("GetMessagetouser", fromuser, message);
            //await Clients.Client(touser).SendAsync("GetMessagetouser", fromuser, message);

            //await Clients.All.SendAsync("GetMessage", user, message);
        }
    }
}
