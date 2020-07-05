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

        [HttpPost]
        public async System.Threading.Tasks.Task<MessageModel> PostAsync([FromBody] MessageModel messageModel)
        {

            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";
            


            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT user_id FROM sessions WHERE id = @id", conn))
            {

                checkUsernameCommand.Parameters.AddWithValue("@id", messageModel.SessionId);

                await using (var reader = await checkUsernameCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        
                        messageModel.UserId = (int)reader[0];
               
                    }
                }
            }

            var message = new MessageModel();
            using (var messageInsertCommand = new NpgsqlCommand("INSERT INTO messages (user_id, text, created_date) VALUES (@userId, @text, @created_date)", conn))
            {
                messageInsertCommand.Parameters.AddWithValue("@userId", messageModel.UserId);
                messageInsertCommand.Parameters.AddWithValue("@text", messageModel.Text);
                messageInsertCommand.Parameters.AddWithValue("@created_date", DateTime.Now);

                await using (var reader = await messageInsertCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
              
                        messageModel.UserId = (int)reader[1];
                        messageModel.Text = reader[2].ToString();
                        messageModel.CreatedDate = (DateTime)reader[3];

                    }

                }
            }
            return message;
        }

        [HttpGet("text")]
        public async System.Threading.Tasks.Task<List<MessageModel>> GetMessages()
        {
            var message = new MessageModel();

            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";



            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();



            using (var messageInsertCommand = new NpgsqlCommand("SELECT * FROM messages", conn))
            {
             
                await using (var reader = await messageInsertCommand.ExecuteReaderAsync())
                {
  

                    while (await reader.ReadAsync())
                    {

                        message.Id = (int)reader[0];
                        message.UserId = (int)reader[1];
                     
                   

                    }
            
                }
                
            }

            using (var messageInsertCommand = new NpgsqlCommand("SELECT text FROM messages WHERE user_id = @user_id", conn))
            {

                   messageInsertCommand.Parameters.AddWithValue("@id", message.UserId);

                await using (var reader = await messageInsertCommand.ExecuteReaderAsync())
                {
                    var messages = new List<MessageModel>();

                    while (await reader.ReadAsync())
                    {

                        message.Text = reader[0].ToString();
                        messages.Add(message);
                       
                
                    }
                    return messages;
                }

            }

        }


    }
}
