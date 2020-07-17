using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Core.Services
{
    public interface ICreateNewUserService
    {
        Users CreateUser(string username, DateTime createdDate);
    }
    public class CreateNewUserService : ICreateNewUserService
    {
        private string _databaseUserName;
        private string _databasePassword;

        public CreateNewUserService(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
        }

        public Users CreateUser(string username, DateTime createdDate)
        {
            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using (var cmd = new NpgsqlCommand("INSERT INTO users (user_name, created_date) VALUES (@user_name, @created_date)", conn))
            {
                cmd.Parameters.AddWithValue("@user_name", username);
                cmd.Parameters.AddWithValue("@created_date", DateTime.Now);

                using (var reader = cmd.ExecuteReader())
                {
                    var user = new Users();

                    while (reader.Read())
                    {

                        user.Username = reader[0].ToString();

                    }
                    return user;

                }

            }
        }
    }

    public class Users
    {
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
