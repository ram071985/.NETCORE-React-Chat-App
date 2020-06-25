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
    [Route("api/authpractice")]

    public class AuthorizationPracticeController : ControllerBase
    {


        [HttpPost]
        public async System.Threading.Tasks.Task<List<SessionModel>> PostAsync([FromForm] SessionModel sessionModel, UserModel userModel)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var userInsertCommand = new NpgsqlCommand("INSERT INTO users (username, password) VALUES (@username, @password) RETURNING id", conn))
            {
                await using (var reader = await userInsertCommand.ExecuteReaderAsync())
                {
                    List<UserModel> sessionList = new List<UserModel>();
                    while (await reader.ReadAsync())
                    {
                        var userId = new UserModel();
                        userInsertCommand.Parameters.AddWithValue("@username", userModel.Username);
                        userInsertCommand.Parameters.AddWithValue("@password", userModel.Password);
                        userId.Id = (int)reader[0];
                        sessionList.Add(userId);
                    }
                }
            }
            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id", conn))
            {

                await using (var reader = await sessionInsertCommand.ExecuteReaderAsync())
                {
                     List<SessionModel> sessionList = new List<SessionModel>();

                     while (await reader.ReadAsync())
                     {
                          var session = new SessionModel();
                          session.Id = (int)reader[0];
                          sessionList.Add(session);

                     }
                     return sessionList;
                }
            }

        }
        
    }
}
