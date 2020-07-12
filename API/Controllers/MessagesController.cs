using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class MessagesController : ControllerBase
    {
        private string _databaseUserName;
        private string _databasePassword;

        public MessagesController(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<List<MessageModel>> PostAsync([FromBody] MessageModel messageModel)
        {

            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT user_id FROM sessions WHERE id = @id", conn))
            {

               checkUsernameCommand.Parameters.AddWithValue("@id", messageModel.SessionId);

                await using (var reader = await checkUsernameCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {

                        messageModel.SessionId = (int)reader[0];

                    }
                }
            }

            
            using (var messageInsertCommand = new NpgsqlCommand("INSERT INTO messages (user_id, text, created_date) VALUES (@userId, @text, @created_date)", conn))
            {
                messageInsertCommand.Parameters.AddWithValue("@userId", messageModel.SessionId);
                messageInsertCommand.Parameters.AddWithValue("@text", messageModel.Text);
                messageInsertCommand.Parameters.AddWithValue("@created_date", DateTime.Now);

                await using (var reader = await messageInsertCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {

                        messageModel.Text = reader[3].ToString();
                        messageModel.CreatedDate = (DateTime)reader[4];

                    }

                }
            } 

            using (var messageInsertCommand = new NpgsqlCommand("SELECT u.username, m.text, m.created_date FROM messages m JOIN users u ON u.id = m.user_id WHERE m.user_id = u.id", conn))
            {

                await using (var reader = await messageInsertCommand.ExecuteReaderAsync())
                {
                    var messages = new List<MessageModel>();

                    while (await reader.ReadAsync())
                    {
                        var message = new MessageModel();
                        message.Username = reader[0].ToString();
                        message.Text = reader[1].ToString();
                        message.CreatedDate = (DateTime)reader[2];
                        messages.Add(message);


                    }
                    return messages;
                }

            }
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<List<MessageModel>> GetMessages()
        {
            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var messageInsertCommand = new NpgsqlCommand("SELECT u.username, m.text, m.created_date FROM messages m JOIN users u ON u.id = m.user_id WHERE m.user_id = u.id", conn))
            {

                await using (var reader = await messageInsertCommand.ExecuteReaderAsync())
                {
                    var messages = new List<MessageModel>();

                    while (await reader.ReadAsync())
                    {
                        var message = new MessageModel();
                        message.Username = reader[0].ToString();
                        message.Text = reader[1].ToString();
                        message.CreatedDate = (DateTime)reader[2];
                        messages.Add(message);


                    }
                    return messages;
                }

            }

        }


    }
}
