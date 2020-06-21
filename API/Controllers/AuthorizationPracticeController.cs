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

        [HttpGet]
        public async System.Threading.Tasks.Task<List<SessionModel>> GetAsync(SessionModel sessionModel)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

 
            await using (var cmd = new NpgsqlCommand("SELECT id, session_id, created_date FROM sessions ORDER BY id DESC LIMIT 1;", conn))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<SessionModel> sessionList  = new List<SessionModel>();

                    while (await reader.ReadAsync())
                    {
                        var session = new SessionModel();
                        session.Id = (int)reader[0];
                        session.SessionId = (int)reader[1];
                        session.CreatedDate = (DateTime)reader[2];
                        sessionList.Add(session);

                    }
                    return sessionList;
                }
            }

        }


        [HttpPost]
        public async System.Threading.Tasks.Task PostAsync(SessionModel sessionModel)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

       
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO sessions (session_id, created_date) VALUES (@session_id, @created_date)", conn))
            {
                var sessionIdCopy = new SessionModel();
                var random = new Random();
                cmd.Parameters.AddWithValue("@session_id", random.Next());
                cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

        }

      

    }
}