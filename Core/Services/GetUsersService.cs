using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Core.Services
{
    public interface IGetUsersService
    {
        List<GetUser> GetUserObject(string username);
    }
    public class GetUsersService : IGetUsersService
    {
        private string _databaseUserName;
        private string _databasePassword;
        private string _databaseHost;
        private string _databaseName;

        public GetUsersService(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
            _databaseHost = configuration["Database:Host"];
            _databaseName = configuration["Database:Name"];
        }

        public List<GetUser> GetUserObject(string username)
        {


            var connString = "Host=" + _databaseHost + ";Username =" + _databaseUserName + ";Password=" + _databasePassword + ";Database=" + _databaseName;

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE last_active_at > NOW() - interval '20 minutes'", conn))
            {
               

                using (var reader = cmd.ExecuteReader())
                {

                    var users = new List<GetUser>();

                    while (reader.Read())
                    {
                        var user = new GetUser();
                        user.Username = reader[1].ToString();
                        users.Add(user);

                    }
                    return users;

                }
            }
        }
    }

    public class GetUser
    {
        public string Username { get; set; }

    }
}
