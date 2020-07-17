using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/register")]

    public class RegisterController : ControllerBase
    {

        private string _databaseUserName;
        private string _databasePassword;

        public RegisterController(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<SessionModel> PostAsync([FromBody] UserModel userModel)
        {

            if (userModel.Username == "")
            {
                throw new Exception("empty username");
            }
            if (userModel.Password == "")
            {
                throw new Exception("empty password");
            }

            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";
            var user = new UserModel();

    
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT * FROM users WHERE username = @username", conn))
            {

                checkUsernameCommand.Parameters.AddWithValue("@username", userModel.Username);


                await using (var reader = await checkUsernameCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        user.Username = reader[1].ToString();
                        if(userModel.Username != user.Username)
                        {

                            throw new Exception("redundant username");

                        }                   
                    }
                }
            }


            using (var userInsertCommand = new NpgsqlCommand("INSERT INTO users (username, password, created_date) VALUES (@username, @password, @created_date) RETURNING id", conn))
            {
            
                userInsertCommand.Parameters.AddWithValue("@username", userModel.Username);
                userInsertCommand.Parameters.AddWithValue("@password", userModel.Password);
                userInsertCommand.Parameters.AddWithValue("@created_date", DateTime.Now);

                await using (var reader = await userInsertCommand.ExecuteReaderAsync()) 
                {
                   
                    while (await reader.ReadAsync())
                    {
                       
                        user.Id = (int)reader[0];
                                  
                    }
                }
            }

            var session = new SessionModel();
            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id", conn))
            {
                sessionInsertCommand.Parameters.AddWithValue("@userId", user.Id);
                await using (var reader = await sessionInsertCommand.ExecuteReaderAsync())
                {
                   

                    while (await reader.ReadAsync())
                    {
               
                        session.Id = (int)reader[0];
                        session.UserId = (int)reader[1];

                    }
                    
                }
            }
            return session;
        }

    }
}