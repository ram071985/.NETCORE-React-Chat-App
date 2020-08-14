using System;
using Core.DataAccess;
using Core.Entities;
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
        private string _databaseHost;
        private string _databaseName;

        private ISessionDataAccess _sessionDataAccess;


        public AuthorizeUserService(IConfiguration configuration,
            ISessionDataAccess sessionDataAccess)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
            _databaseHost = configuration["Database:Host"];
            _databaseName = configuration["Database:Name"];

            _sessionDataAccess = sessionDataAccess;

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

            var connString = "Host=" + _databaseHost + ";Username =" + _databaseUserName + ";Password=" + _databasePassword + ";Database=" + _databaseName;


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

                throw new Exception("wrong credentials");

            }
            return _sessionDataAccess.CreateSession(conn, id);

        }
    }
}
