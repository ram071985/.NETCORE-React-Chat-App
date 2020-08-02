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
        private IUserUpdateService _userUpdateService;
        private IGetUsersService _getUsersService;

        public UsersController(IConfiguration configuration, IUserUpdateService userUpdateService, IGetUsersService getUsersService)
        {
            _userUpdateService = userUpdateService;
            _getUsersService = getUsersService;
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

        [HttpPut("last_active")]
        public UserUpdate UpdateLastActive(int userId, UserModel userModel)
        {
            var user = _userUpdateService.UpdateLastActive(userModel.UserId);
            return new UserUpdate
            {
                UserId = user.UserId
            };
        }

        [HttpPut]
        public UserUpdate PutNewUsername([FromBody] UserModel userModel)
        {
            var user = _userUpdateService.PutNewUsername(userModel.UserId, userModel.Username, userModel.NewUsername, userModel.CreatedDate);
            return new UserUpdate
            {
                Username = user.Username
            };
        }
    }
}
