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

        public MessagesController(ICreateMessageService createMessageService, IGetMessagesService getMessagesService)
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
        public List<MessageModel> GetMessages(MessageModel messageModel)
        {
            var messages = _getMessagesService.GetMessages(messageModel.Username, messageModel.Text, messageModel.CreatedDate);

            var messageModels = messages.Select(message => new MessageModel { Username = message.Username, Text = message.Text });

            return messageModels.ToList();
        }

    }
}
