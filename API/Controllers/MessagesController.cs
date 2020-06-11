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
        

        [HttpGet]
        public async System.Threading.Tasks.Task<List<Messages>> GetAsync()
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT * FROM messages", conn))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Messages> messageList = new List<Messages>();

                    while (await reader.ReadAsync())
                    {
                        var message = new Messages();
                        message.Id = (int)reader[0];
                        message.UserId = (int)reader[1];
                        message.Text = reader[2].ToString();
                        message.CreatedDate = (DateTime)reader[3];
                        messageList.Add(message);
                    
                    }
                    return messageList;
                }
            }
                           
        }

        [HttpPost]
        public async System.Threading.Tasks.Task PostAsync(UserInputMessages userInputMessages)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO messages (text, created_date) VALUES (@text, @created_date)", conn))
            {
                var random = new Random();
                cmd.Parameters.AddWithValue("@text", userInputMessages.Text);
                cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }
      
    }
}
