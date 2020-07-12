using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static Core.Services.AuthorizeUserService;

namespace Core.Services
{

    public interface IAuthorizeUserService
    {
        Session GetSession(int id, string username, string password);
    }

    public class AuthorizeUserService : IAuthorizeUserService
    {
        private string _databaseUserName;
        private string _databasePassword;

        public AuthorizeUserService(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
        }

        public Session GetSession(int id, string username, string password)
        {

            var session = new Session();

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

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", conn))
            {

                checkUsernameCommand.Parameters.AddWithValue("@username", username);
                checkUsernameCommand.Parameters.AddWithValue("@password", password);

                using (var reader = checkUsernameCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        id = (int)reader[0];
                        username = reader[1].ToString();
                        password = reader[2].ToString();

                    }
                }
            }

            if (id == 0)
            {

                throw new Exception("false username");

            }

            
            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id, user_id", conn))
            {

                sessionInsertCommand.Parameters.AddWithValue("@userId", id);
                using (var reader = sessionInsertCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        

                        session.Id = (int)reader[0];
                        session.UserId = (int)reader[1];
                        
                    }
                    return session;

                }
            }
        }

        public class Session
        {

           public int Id { get; set; }
           public int UserId { get; set; }

        }
    }

 
}
