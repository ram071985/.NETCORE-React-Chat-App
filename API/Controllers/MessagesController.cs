using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Core.Services;
using System.Linq;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class MessagesController : ControllerBase
    {

        private ICreateMessageService _createMessageService;
        private IGetMessagesService _getMessagesService;
        private IUserUpdateService _userUpdateService;

        public MessagesController(ICreateMessageService createMessageService, IGetMessagesService getMessagesService, IUserUpdateService userUpdateService)
        {
            _createMessageService = createMessageService;
            _getMessagesService = getMessagesService;
            _userUpdateService = userUpdateService;
        }

        [HttpPut("last_active")]
        public UserUpdate UpdateLastActive(int userId, UserModel userModel)
        {
            var user = _userUpdateService.UpdateLastActive(userModel.UserId);
            return new UserUpdate
            {
                UserId = user.UserId
            };
        }



        [HttpPost]
        public List<MessageModel> Post([FromBody] MessageModel messageModel)
        {
        
          var messages = _createMessageService.GetBackMessage(messageModel.SessionId, messageModel.Text, messageModel.CreatedDate);
          var messageModels = messages.Select(message => new MessageModel { Username = message.Username, Text = message.Text, CreatedDate = message.CreatedDate });

            return messageModels.ToList();
        }

        [HttpGet]
        public List<MessageModel> GetMessages()
        {

            var messages = _getMessagesService.GetMessages();

            var messageModels = messages.Select(message => new MessageModel { Username = message.Username, Text = message.Text, CreatedDate = message.CreatedDate });

            return messageModels.ToList();
        }
    }
}
