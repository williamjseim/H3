using HackGame.Api.Models;

namespace HackGame.Api
{
    public class Database
    {
        private static Database instance;
        public static Database Instance { get { if (instance == null) instance = new(); return instance; } }

        Dictionary<string, string> Users = new();

        //creates a new users
        public bool CreateNewUser(string username, string password)
        {
            return Users.TryAdd(username, PasswordHasher.HashPassword(password));
        }
        //checks if password and username exists
        public bool Login(string username, string password)
        {
            if(Users.TryGetValue(username, out string hash))
            {
                return hash == PasswordHasher.HashPassword(password);
            }
            return false;
        }
        //chekc if user exists
        public bool UserExists(string username)
        {
            return Users.ContainsKey(username);
        }
    }
}
