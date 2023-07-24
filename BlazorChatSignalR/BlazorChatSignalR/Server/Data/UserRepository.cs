using BlazorChatSignalR.Server.Models;
using System.Data;

namespace BlazorChatSignalR.Server.Data
{
    public sealed class UserRepository 
    {
        UserRepository()
        {

        }
        private static UserRepository instance = null;
        private static readonly object padlock = new object();
        private static User loggedUser = new User();

        private List<User> users = new List<User>();
        private int nextId = 2;
        public static UserRepository Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserRepository();
                    }
                    return instance;
                }
            }
        }

        public List<User> getAll()
        {
            return users.ToList();
        }


        public bool register(User user)
        {
            user.id = users.Count + 1;
           
           
                users.Add(user);
                return true;
          
        }

        public User login(string email, string password)
        {
            loggedUser= users.FirstOrDefault(e => e.email == email && e.password == password, new User());
            return loggedUser;
        }
        public User getLoggedUser()
        {
            return loggedUser;
        }

        internal void logout()
        {
            loggedUser=new User();
        }
    }
}
