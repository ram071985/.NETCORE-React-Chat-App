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
    [Route("api/register")]

    public class RegisterController : ControllerBase
    {


        [HttpPost]
        public async System.Threading.Tasks.Task<List<SessionModel>> PostAsync([FromForm] SessionModel sessionModel, UserModel userModel)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";
            var user = new UserModel();

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var userInsertCommand = new NpgsqlCommand("INSERT INTO users (username, password, created_date) VALUES (@username, @password, @created_date) RETURNING id", conn))
            {
                userInsertCommand.Parameters.AddWithValue("@username", userModel.Username);
                userInsertCommand.Parameters.AddWithValue("@password", userModel.Password);
                userInsertCommand.Parameters.AddWithValue("@created_date", DateTime.Now);

                await using (var reader = await userInsertCommand.ExecuteReaderAsync())
                {
                    var users = new List<UserModel>();
                    while (await reader.ReadAsync())
                    {
                        user.Id = (int)reader[0];

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