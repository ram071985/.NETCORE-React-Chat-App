using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authpractice")]

    public class AuthorizationPracticeController : ControllerBase
    {

        [HttpPost]
        public async System.Threading.Tasks.Task PostAsync(SessionInput sessionInput)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var cmd = new NpgsqlCommand("INSERT INTO sessions (user_name, password) VALUES (@user_name, @password)", conn))
            {

                cmd.Parameters.AddWithValue("user_name", sessionInput.UserName);
                cmd.Parameters.AddWithValue("password", sessionInput.Password);
                cmd.ExecuteNonQuery();
            }
        }

    }
}