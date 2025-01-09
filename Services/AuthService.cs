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



        public bool Login(string username, string password)
        {
            using (var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=/Users/taraninderdjiet/BackendRazor/Database.db"))
            {
            connection.Open();
            using (var command = new Microsoft.Data.Sqlite.SqliteCommand("SELECT Password FROM admin WHERE Username = @username", connection))
            {
                command.Parameters.AddWithValue("@username", username);
                var storedPassword = command.ExecuteScalar() as string;
                if (storedPassword != null && storedPassword == password)
                {
                loggedInUser = username;
                return true;
                }
            }
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
