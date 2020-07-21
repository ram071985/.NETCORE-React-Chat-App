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
        private IGetUsersService _getUsersService;

        public UsersController(IConfiguration configuration, IGetUsersService getUsersService)
        {
            _getUsersService =getUsersService;
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
