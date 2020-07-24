using System;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Core.Services
{
    public interface IUserUpdateService
    {
        UserUpdate PutNewUsername(int userId, string username, string newUsername, DateTime createdDate);
        UserUpdate UpdateLastActive(int userId, DateTime lastActiveAt);
    }
    public class UserUpdateService : IUserUpdateService
    {
        private string _databaseUserName;
        private string _databasePassword;

        public UserUpdateService(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
        }

        public UserUpdate UpdateLastActive(int userId, DateTime lastActiveAt)
        {
            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var lastActiveInsert = new NpgsqlCommand("SELECT * FROM users WHERE userId = @userId RETURNING * ", conn))
            {
                lastActiveInsert.Parameters.AddWithValue("@lastActiveAt", DateTime.Now);
                using (var reader = lastActiveInsert.ExecuteReader())
                {
                    var userRow = new UserUpdate();
                    while (reader.Read())
                    {
                        userRow.UserId = (int)reader[3];
                        userRow.Username = reader[2].ToString();

                    }
                  
                }

            }


            using (var lastActiveInsert = new NpgsqlCommand("INSERT INTO users (last_active_at) VALUES (@lastActiveAt)", conn))
            {
                lastActiveInsert.Parameters.AddWithValue("@lastActiveAt", DateTime.Now);
                using (var reader = lastActiveInsert.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    

                    }
                }
      
            }   
        }

        public UserUpdate PutNewUsername(int userId, string username, string newUsername, DateTime createdDate)
        {


            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var getUsernameCommand = new NpgsqlCommand("SELECT username FROM users WHERE id = @id", conn))
            {

                getUsernameCommand.Parameters.AddWithValue("@id", userId);


                using (var reader = getUsernameCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        username = reader[0].ToString();
                       
                    }
                }
            }

            using (var cmd = new NpgsqlCommand("UPDATE users SET username = @newUsername WHERE username = @username RETURNING username", conn))
            {
                cmd.Parameters.AddWithValue("@newUsername", newUsername);
                cmd.Parameters.AddWithValue("@username", username);
                using (var reader = cmd.ExecuteReader())
                {
                    var user = new UserUpdate();

                    while (reader.Read())
                    {

                        user.Username = reader[0].ToString();
                        if (username == newUsername)
                        {

                            throw new Exception("redundant username");

                        }

                    }
                    return user;

                }
            }
        }
    }

    public class UserUpdate
    {
        public int UserId { get; set; }
        public string NewUsername { get; set; }
        public string Username { get; set; }
        public DateTime LastActiveAt { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
