using System;
using Core.DataAccess;
using Core.Entities;
using Npgsql;

namespace Core.Services
{
    public interface IUserUpdateService
    {
        User PutNewUsername(int userId, string username, string newUsername, DateTime createdDate);
        User UpdateLastActive(int userId);
    }
    public class UserUpdateService : IUserUpdateService
    {
        private IDbConnection _dbConnection;

        public UserUpdateService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public User UpdateLastActive(int userId)
        {
            using (var conn = _dbConnection.GetConnection())
            {

                using (var lastActiveInsert = new NpgsqlCommand("UPDATE users SET last_active_at = @lastActiveAt WHERE id = @id", conn))
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
        }

        public User PutNewUsername(int userId, string username, string newUsername, DateTime createdDate)
        {
            using (var conn = _dbConnection.GetConnection())
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
        }
    }
}

