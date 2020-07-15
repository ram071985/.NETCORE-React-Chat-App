using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Core.Services;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class MessagesController : ControllerBase
    {
        private ICreateMessageService _createMessageService;

        public MessagesController(IConfiguration configuration, ICreateMessageService createMessageService)
        {
            _createMessageService = createMessageService;
        }

        [HttpPost]
        public MessageModel Post([FromBody] MessageModel messageModel)
        {

          var message = _createMessageService.GetBackMessage(messageModel.SessionId, messageModel.Text, messageModel.CreatedDate);

            return new MessageModel
            {
                Text = message.Text,
                CreatedDate = message.CreatedDate
            };
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
