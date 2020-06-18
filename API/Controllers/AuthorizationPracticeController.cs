using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authorization")]

    public class AuthorizationPracticeController : ControllerBase 
    {

        [HttpGet]
        public async System.Threading.Tasks.Task<List<SessionModel>> GetAsync()
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT * FROM sessions", conn))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<SessionModel> sessionList = new List<SessionModel>();

                    while (await reader.ReadAsync())
                    {
                        var session = new SessionModel();
                        session.Id = (int)reader[0];
                        session.UserId = (int)reader[1];            
                        sessionList.Add(session);

                    }
                    return sessionList;
                }
            }

        }

        [HttpPost("/createUser")]
        public async System.Threading.Tasks.Task PostAsync(SessionModel session)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@user_id)", conn))
            {

                cmd.ExecuteNonQuery();
            }
        }

    }
}
