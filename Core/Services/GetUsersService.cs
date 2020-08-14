using System;
using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Core.Services
{
    public interface IGetUsersService
    {
        List<User> GetUserObject(string username);
    }
    public class GetUsersService : IGetUsersService
    {
        private IDbConnection _dbConnection;

        public GetUsersService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<User> GetUserObject(string username)
        {
            using (var conn = _dbConnection.GetConnection())
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
        }
    }
}
