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
    [Route("api/authorize")]

    public class AuthorizationController : ControllerBase
    {


        private IAuthorizeUserService _authorizeUserService;

        public AuthorizationController(IConfiguration configuration, IAuthorizeUserService authorizeUserService)
        {           
            _authorizeUserService = authorizeUserService;
        }


        [HttpPost]
        public SessionModel Post([FromBody] AuthorizationModel authorizationModel)
        {
            var session = _authorizeUserService.GetSession(authorizationModel.Id, authorizationModel.Username, authorizationModel.Password);

            return new SessionModel
            {
                Id = session.Id,
                UserId = session.UserId
            };
        }

    }
}
