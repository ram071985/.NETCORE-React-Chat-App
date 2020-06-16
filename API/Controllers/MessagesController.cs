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
        public async System.Threading.Tasks.Task<List<MessageModel>> GetAsync()
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT * FROM messages", conn))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<MessageModel> messageList = new List<MessageModel>();

                    while (await reader.ReadAsync())
                    {
                        var message = new MessageModel();
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
        public async System.Threading.Tasks.Task PostAsync(MessageInput messageInput)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO messages (user_id, text, created_date) VALUES (@user_id, @text, @created_date)", conn))
            {
                var random = new Random();
                cmd.Parameters.AddWithValue("@user_id", messageInput.UserId);
                cmd.Parameters.AddWithValue("@text", messageInput.Text);
                cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }


        [HttpGet("{id}")]
        public MessageModel GetUser(int id)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            // Retrieve all rows
            using (var cmd = new NpgsqlCommand("SELECT * FROM messages WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    var message = new MessageModel();

                    while (reader.Read())
                    {
                        message.Id = (int)reader[0];
                        message.UserId = (int)reader[1];
                        message.Text = reader[2].ToString();
                        message.CreatedDate = (DateTime)reader[3];

                    }
                    return message;

                }
            }

        }

    }
}
