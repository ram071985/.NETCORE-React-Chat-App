using Microsoft.AspNetCore.Mvc;
using Core.Services;
using System.Linq;
using System.Collections.Generic;

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


        [HttpPost]
        public List<MessageModel> GetBackMessage([FromBody] MessageModel messageModel)
        {

            var messages = _createMessageService.GetBackMessage(messageModel.SessionId, messageModel.Text, messageModel.CreatedDate);
            var messageModels = messages.Select(message => new MessageModel { Username = message.User.Username, Text = message.Text, CreatedDate = message.CreatedDate });

            _userUpdateService.UpdateLastActive(messageModel.UserId);

            return messageModels.ToList();
        }

        [HttpGet]
        public List<MessageModel> GetMessages()
        {

            var messages = _getMessagesService.GetMessages();

            var messageModels = messages.Select(message => new MessageModel { Username = message.User.Username, Text = message.Text, CreatedDate = message.CreatedDate });

            return messageModels.ToList();
        }
    }
}