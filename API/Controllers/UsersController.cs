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

        public UsersController(IConfiguration configuration, ICreateNewUserService createNewUserService)
        {
            _createNewUserService = createNewUserService;
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
        public UserModel GetUser(int id)
        {
            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            // Retrieve all rows
            using (var cmd = new NpgsqlCommand("SELECT username FROM users WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    var user = new UserModel();

                    while (reader.Read())
                    {

                        user.Username = reader[0].ToString();

                    }
                    return user;

                }
            }

        }
    }
}
