using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using System.Net.Http;
using Microsoft.Net.Http.Headers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authorize")]

    public class AuthorizationController : ControllerBase
    {


        [HttpPost]
        public async System.Threading.Tasks.Task<List<SessionModel>> PostAsync([FromForm] UserInputUser userInputUser, UserModel userModel)
        {

            if (userModel.Username == "")
            {
                throw new Exception("empty username");
            }
            if (userModel.Password == "")
            {
                throw new Exception("empty password");
            }

            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";
            var user = new UserModel();
  

    
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT username,password FROM users WHERE username = @username AND password = @password", conn))
            {

                checkUsernameCommand.Parameters.AddWithValue("@username", userModel.Username);
                checkUsernameCommand.Parameters.AddWithValue("@password", userModel.Password);

                await using (var reader = await checkUsernameCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        user.Username = reader[0].ToString();
                        user.Password = reader[1].ToString();
                        if (userModel.Username != user.Username)
                        {
                            throw new Exception("false username");

                        }
                        if (userInputUser.Password != user.Password)
                        {
                            throw new Exception("false password");

                        }

                    }
                }
            }


            using (var checkPasswordCommand = new NpgsqlCommand("SELECT * FROM users WHERE password = @password", conn))
            {
            
         
                checkPasswordCommand.Parameters.AddWithValue("@password", userModel.Password);
               

                await using (var reader = await checkPasswordCommand.ExecuteReaderAsync()) 
                {
                   
                    while (await reader.ReadAsync())
                    {
                        user.Id = (int)reader[0];
                        if (userModel.Password != user.Password)
                        {
                            throw new Exception("incorrect password");
                        }
                                  
                    }
                }
            }

            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id", conn))
            {
                sessionInsertCommand.Parameters.AddWithValue("@userId", userModel.Id);
                await using (var reader = await sessionInsertCommand.ExecuteReaderAsync())
                {
                    var sessions = new List<SessionModel>();

                    while (await reader.ReadAsync())
                    {
                        var session = new SessionModel();
                        session.Id = (int)reader[0];
                        sessions.Add(session);

                    }
                    return sessions;
                }
            }

        }

    }
}
