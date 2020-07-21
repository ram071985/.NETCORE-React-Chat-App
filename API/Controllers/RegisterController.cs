using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Core.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/register")]

    public class RegisterController : ControllerBase
    {
        private ICreateNewUserService _createNewUserService;

        public RegisterController(IConfiguration configuration, ICreateNewUserService createNewUserService)
        {
            _createNewUserService = createNewUserService;
        }

        [HttpPost]
        public UserRegister PostNewUser([FromBody] UserModel userModel)
        {

            var session = _createNewUserService.PostNewUser(userModel.Id, userModel.UserId, userModel.Username, userModel.Password, userModel.CreatedDate);
            return new UserRegister
            {
                Id = session.Id,
                UserId = session.UserId
            };
        }

    }
}