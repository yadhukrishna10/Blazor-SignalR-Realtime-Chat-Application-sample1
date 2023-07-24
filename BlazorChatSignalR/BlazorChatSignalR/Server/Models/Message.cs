namespace BlazorChatSignalR.Server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string? MessageText { get; set; }
    }
}
