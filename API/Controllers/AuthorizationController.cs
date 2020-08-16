﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Core.Services;
using Core.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authorize")]

    public class AuthorizationController : ControllerBase
    {
        private IAuthorizeUserService _authorizeUserService;

        public AuthorizationController(IConfiguration configuration, IAuthorizeUserService authorizeUserService)
        {           
            _authorizeUserService = authorizeUserService;
        }

        [HttpPost]
        public SessionModel Post([FromBody] User user)
        {
            var session = _authorizeUserService.GetSession(user.Id, user.Username, user.Password);

            return new SessionModel
            {
                Id = session.Id,
                UserId = session.UserId
            };
        }

    }
}
