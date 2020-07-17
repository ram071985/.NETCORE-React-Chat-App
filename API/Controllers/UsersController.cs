using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using API;
using System.Linq;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Core.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private ICreateNewUserService _createNewUserService;
        private IGetUsersService _getUsersService;

        public UsersController(IConfiguration configuration, ICreateNewUserService createNewUserService, IGetUsersService getUsersService)
        {
            _createNewUserService = createNewUserService;
            _getUsersService =getUsersService;
        }

        [HttpPost]

        public Users CreateUser([FromBody] UserModel userModel)
        {
            var user = _createNewUserService.CreateUser(userModel.Username, userModel.CreatedDate);
            return new Users
            {
                Username = user.Username,
                CreatedDate = user.CreatedDate
            };
        }

        [HttpGet("{id}")]
        public GetUser GetUserObject(int id, string username)
        {
            var user = _getUsersService.GetUserObject(id, username);
            return new GetUser
            {
                Username = user.Username
             
            };

        }
    }
}
