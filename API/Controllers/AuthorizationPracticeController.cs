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
        public async System.Threading.Tasks.Task PostAsync(SessionModel sessionModel)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO sessions (session_id,) VALUES (@session_id)", conn))
            {
                var random = new Random();
                cmd.Parameters.AddWithValue("@session_id", random.Next());
                cmd.ExecuteNonQuery();
            }

        }


    }
}