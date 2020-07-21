using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Core.Services
{
    public interface ICreateNewUserService
    {
        UserRegister PostNewUser(int id, int userId, string username, string password, DateTime createdDate);
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

        public UserRegister PostNewUser(int id, int userId, string username, string password, DateTime createdDate)
        {
            if (username == "")
            {
                throw new Exception("empty username");
            }
            if (password == "")
            {
                throw new Exception("empty password");
            }

            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

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

                    while (reader.Read())
                    {

                        id = (int)reader[0];

                    }
                }
            }

            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id", conn))
            {
                sessionInsertCommand.Parameters.AddWithValue("@userId", id);
                using (var reader = sessionInsertCommand.ExecuteReader())
                {
                    var newSession = new UserRegister();

                    while (reader.Read())
                    {

                        newSession.Id = (int)reader[0];
   

                    }
                    return newSession;

                }
            }
        }

    }

    public class UserRegister
    {
      public int Id { get; set; }
      public int UserId { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
      public DateTime CreatedDate { get; set; }
    }
}
