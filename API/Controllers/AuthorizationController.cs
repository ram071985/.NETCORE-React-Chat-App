using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authorize")]

    public class AuthorizationController : ControllerBase
    {

        private string _databaseUserName;
        private string _databasePassword;

        public AuthorizationController(IConfiguration configuration)
        {

        }


        [HttpPost]
        public async System.Threading.Tasks.Task<List<SessionModel>> PostAsync([FromBody] AuthorizationModel authorizationModel)
        {

            if (authorizationModel.Username == "")
            {
                throw new Exception("empty username");
            }
            if (authorizationModel.Password == "")
            {
                throw new Exception("empty password");
            }

            var connString = "Host=localhost;Username=" + _databaseUserName + "Password=" + _databasePassword + "Database=chat_app";


            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", conn))
            {

                checkUsernameCommand.Parameters.AddWithValue("@username", authorizationModel.Username);
                checkUsernameCommand.Parameters.AddWithValue("@password", authorizationModel.Password);

                await using (var reader = await checkUsernameCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        authorizationModel.Id = (int)reader[0];
                        authorizationModel.Username = reader[1].ToString();
                        authorizationModel.Password = reader[2].ToString();

                    }
                }
            }

            if (authorizationModel.Id == 0)
            {

                throw new Exception("false username");

            }
            
            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id, user_id", conn))
            {
                var sessions = new List<SessionModel>();

                sessionInsertCommand.Parameters.AddWithValue("@userId", authorizationModel.Id);
                await using (var reader = await sessionInsertCommand.ExecuteReaderAsync())
                {
                 
                    while (await reader.ReadAsync())
                    {
                        var session = new SessionModel();

                        session.Id = (int)reader[0];
                        session.UserId = (int)reader[1];
                        sessions.Add(session);
                    }
                    return sessions;

                }
            }
            
        }

    }
}
