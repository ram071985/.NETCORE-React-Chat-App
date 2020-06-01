using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;

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

        public List<Messages> Get()
        {
            return message;
        }
    }
}
