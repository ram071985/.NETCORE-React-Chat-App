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
        public async System.Threading.Tasks.Task PostAsync([FromForm] SessionModel sessionModel)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

       
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO sessions (session_id, created_date) VALUES (@session_id, @created_date) RETURNING id", conn))
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