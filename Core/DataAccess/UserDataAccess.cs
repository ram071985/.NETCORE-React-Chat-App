using System;
using System.Collections.Generic;
using Core.Entities;
using Npgsql;

namespace Core.DataAccess
{
    public interface IUserDataAccess
    {
        User AddUser(NpgsqlConnection conn, int id, string username, string passsword);
        User CheckUserCredentials(NpgsqlConnection conn, int id, string username, string password, DateTime createdDate, DateTime lastActiveAt);
        User UserLastActiveUpdate(NpgsqlConnection conn, int userId);
        User EditUsername(NpgsqlConnection conn, int userId, string username, string password,
            string newUsername, DateTime createdDate);
        List<User> UserLastActive(NpgsqlConnection conn, string username);
    }

    public class UserDataAccess : IUserDataAccess
    {

        public User EditUsername(NpgsqlConnection conn, int userId, string username, string password,
        string newUsername, DateTime createdDate)
        {
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

                using (var cmd = new NpgsqlCommand("UPDATE users SET username = @newUsername WHERE username = @username RETURNING username", conn))
                {
                    cmd.Parameters.AddWithValue("@newUsername", newUsername);
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var user = new User();

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

        public List<User> UserLastActive(NpgsqlConnection conn, string username)
        {           
                using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE last_active_at > NOW() - interval '20 minutes'", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var users = new List<User>();

                        while (reader.Read())
                        {
                            var user = new User();
                            user.Username = reader[1].ToString();
                            users.Add(user);
                        }
                        return users;
                    }
                }
        }

        public User UserLastActiveUpdate(NpgsqlConnection conn, int userId)
        {
            using (var lastActiveInsert = new NpgsqlCommand("UPDATE users SET last_active_at = NOW() WHERE id = @id", conn))
            {
                lastActiveInsert.Parameters.AddWithValue("@lastActiveAt", DateTime.Now);
                lastActiveInsert.Parameters.AddWithValue("@id", userId);
                using (var reader = lastActiveInsert.ExecuteReader())
                {
                    var lastActive = new User();
                    while (reader.Read())
                    {
                    }
                    return lastActive;
                }
            }
        }

        public User CheckUserCredentials(NpgsqlConnection conn, int id, string username, string password, DateTime createdDate, DateTime lastActiveAt)
        {
            using (var checkUsernameCommand = new NpgsqlCommand("SELECT * FROM users WHERE username = @username", conn))
            {
                checkUsernameCommand.Parameters.AddWithValue("@username", username);

                using (var reader = checkUsernameCommand.ExecuteReader())
                {
                   var user = new User();
                    while (reader.Read())
                    {
                        user.Id = (int)reader[0];
                        user.Username = reader[1].ToString();
                        user.CreatedDate = (DateTime)reader[2];
                        user.Password = reader[3].ToString();
                     
                        if (user.Password != password)
                        {
                            throw new Exception("wrong credentials");
                        }
                    }
                    return user;
                }
            }
        }

        public User AddUser(NpgsqlConnection conn, int id, string username, string password)
        {

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT * FROM users WHERE username = @username", conn))
            {
                checkUsernameCommand.Parameters.AddWithValue("@username", username);

                using (var reader = checkUsernameCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        username = reader[1].ToString();
                        if (username != "")
                        {
                            throw new Exception("redundant username");
                        }
                    }
                }
            }

            using (var userInsertCommand = new NpgsqlCommand("INSERT INTO users (username, password, created_date) VALUES (@username, @password, @created_date) RETURNING id", conn))
            {
                userInsertCommand.Parameters.AddWithValue("@username", username);
                userInsertCommand.Parameters.AddWithValue("@password", password);
                userInsertCommand.Parameters.AddWithValue("@created_date", DateTime.Now);

                using (var reader = userInsertCommand.ExecuteReader())
                {
                    var user = new User();
                    while (reader.Read())
                    {
                        user.Id = (int)reader[0];
                    }
                    return user;
                }
            }
        }
    }
}
