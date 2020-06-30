using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using System.Net.Http;
using Microsoft.Net.Http.Headers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authorize")]

    public class AuthorizationController : ControllerBase
    {


        [HttpPost]
        public async System.Threading.Tasks.Task<List<SessionModel>> PostAsync([FromForm] AuthorizationModel authorizationModel)
        {

            if (authorizationModel.Username == "")
            {
                throw new Exception("empty username");
            }
            if (authorizationModel.Password == "")
            {
                throw new Exception("empty password");
            }

            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";
            var authorizeUser = new AuthorizationModel();
            var user = new UserModel();
  

    
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT username FROM users WHERE username = @username", conn))
            {

                checkUsernameCommand.Parameters.AddWithValue("@username", authorizationModel.Username);

                await using (var reader = await checkUsernameCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        authorizeUser.Username = reader[1].ToString();
                        if (authorizationModel.Username == authorizeUser.Username)
                        {
                          

                        }
                        else
                        {

                            throw new Exception("false username");
                        }
                    }
                }
            }

            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id", conn))
            {
                sessionInsertCommand.Parameters.AddWithValue("@userId", user.Id);
                await using (var reader = await sessionInsertCommand.ExecuteReaderAsync())
                {
                    var sessions = new List<SessionModel>();

                    while (await reader.ReadAsync())
                    {
                        var session = new SessionModel();
                        session.Id = (int)reader[0];

                        sessions.Add(session);

                    }
                    return sessions;
                }
            }

        }

    }
}
