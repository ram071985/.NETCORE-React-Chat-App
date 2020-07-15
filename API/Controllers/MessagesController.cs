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

        private string _databaseUserName;
        private string _databasePassword;

        private ICreateMessageService _createMessageService;
        private IGetMessagesService _getMessagesService;

        public MessagesController(IConfiguration configuration, ICreateMessageService createMessageService)
        {
            _createMessageService = createMessageService;
            _getMessagesService = getMessagesService;
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
        public System.Threading.Tasks.Task<List<MessageModel>> GetMessages(MessageModel messageModel)
        {
            var messageList = new List<MessageModel>();
            var messages = _getMessagesService.GetMessages(messageModel.SessionId, messageModel.Text, messageModel.CreatedDate);

            return new List<MessageModel>()
            {
            Username = messages.Username
            
           }

        }

    }
}
