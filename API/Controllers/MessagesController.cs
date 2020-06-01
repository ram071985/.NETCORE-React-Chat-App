using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class MessagesController : ControllerBase
    {
        List<Messages> message = new List<Messages>();

        public MessagesController()
        {
            message.Add(new Messages { Username = "Reid", Text = "Hey there." });
        }

        public List<Messages> Get()
        {
            return message;
        }
    }
}
