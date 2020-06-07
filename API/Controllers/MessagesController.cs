using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class MessagesController : ControllerBase
    {
        List<Messages> message = new List<Messages>();


        public MessagesController()
        {
            message.Add(new Messages { id = 1, username = "Reid", text = "Hey there." });
            message.Add(new Messages { id = 2, username = "Reid", text = "Hey there." });
            message.Add(new Messages { id = 3, username = "Reid", text = "Hey there." });
        }

        [HttpGet]
        public async System.Threading.Tasks.Task GetAsync()
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT * FROM messages", conn))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var id = reader[0]; 
                        var userId = reader[1];
                        var text = reader[2];
                        var createdDate = reader[3];

                    }
                }
            }
                           
        }

        [HttpPost("/post")]
        public async System.Threading.Tasks.Task PostAsync()
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO messages (id, user_id, text) VALUES (@p)", conn))
            {
                cmd.Parameters.AddWithValue("p", (NpgsqlTypes.NpgsqlDbType)2, 24, "Here is a post.");
                cmd.ExecuteNonQuery();
            }
        }
      
    }
}
