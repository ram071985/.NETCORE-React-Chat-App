using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using API;
using System.Linq;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Core.Services;
using Core.Entities;

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

        [HttpGet]
        public List<UserModel> GetUserObject(string username)
        {
            var users = _getUsersService.GetUserObject(username);
            var userModels = users.Select(user => new UserModel { Username = user.Username });
            return userModels.ToList();   
        }

        [HttpPut("last_active")]
        public User UpdateLastActive(int userId, UserModel userModel)
        {
           return _userUpdateService.UpdateLastActive(userModel.UserId);
       
        }

        [HttpPut]
        public User PutNewUsername([FromBody] UserModel userModel)
        {
            var user = _userUpdateService.PutNewUsername(userModel.UserId, userModel.Password, userModel.Username, userModel.NewUsername, userModel.CreatedDate);
            return new User
            {
                Username = user.Username
            };
        }
    }
}
