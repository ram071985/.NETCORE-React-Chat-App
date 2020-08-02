using System;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Core.Services
{
    public interface IGetUsersService
    {
        GetUser GetUserObject(int id, string username);
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

        public GetUser GetUserObject(int id, string username)
        {


            var connString = "Host=" + _databaseHost + ";Username =" + _databaseUserName + ";Password=" + _databasePassword + ";Database=" + _databaseName;

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var cmd = new NpgsqlCommand("SELECT username FROM users WHERE id = @id", conn))
            {

                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    var user = new GetUser();

                    while (reader.Read())
                    {

                        user.Username = reader[0].ToString();

                    }
                    return user;

                }
            }
        }
    }

    public class GetUser
    {
        public string Username { get; set; }

    }
}
