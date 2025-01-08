using System.Collections.Concurrent;

namespace Services
{
    public interface IAuthService
    {
        bool Login(string username, string password);
        bool IsLoggedIn();
        string GetLoggedInUser();
    }

    public class AuthService : IAuthService
    {
        private readonly ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();
        private string loggedInUser;

        public AuthService()
        {
            // Predefined admin user
            users.TryAdd("admin", "password123");
        }

        public bool Login(string username, string password)
        {
            if (users.TryGetValue(username, out var storedPassword) && storedPassword == password)
            {
                loggedInUser = username;
                return true;
            }
            return false;
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(loggedInUser);
        }

        public string GetLoggedInUser()
        {
            return loggedInUser;
        }
    }
}
