namespace BlazorChatSignalR.Server.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string connectionId { get; set; }
        public string status { get; set; }
        public string lastseen { get; set; }
        public User()
        {
                
        }
    }
}
