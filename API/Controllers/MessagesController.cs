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
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                    Console.WriteLine(reader.GetString(0));

        }

        [HttpPost]
        public void Post(Messages val)
        {
            message.Add(val);
        }
      
    }
}
