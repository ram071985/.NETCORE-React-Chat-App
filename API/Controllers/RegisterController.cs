using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;

namespace API.Controllers
{
    [ApiController]
    [Route("api/register")]

    public class RegisterController : ControllerBase
    {

        [HttpPost]
        public async System.Threading.Tasks.Task PostAsync([FromForm] RegisterModel registerModel)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var cmd = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (1) RETURNING id", conn))
            {

                cmd.ExecuteNonQuery();
            }
        }

    }
}
