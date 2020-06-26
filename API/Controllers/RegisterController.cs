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
    [Route("api/register")]

    public class RegisterController : ControllerBase
    {
        string noText = "Please enter a valid username and password";




        [HttpPost]
        public async System.Threading.Tasks.Task<List<SessionModel>> PostAsync([FromForm] SessionModel sessionModel, UserModel userModel)
        {

            if (userModel.Username == "" || userModel.Password == "")
            {
                throw new Exception("Please enter a valid username or password.");
            }

            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";
            var user = new UserModel();

    
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
           

            if (userModel.Username == "")
            {
                throw new Exception("Please enter a valid username or password.");
            }

            using (var userInsertCommand = new NpgsqlCommand("INSERT INTO users (username, password, created_date) SELECT (@username, @password, @created_date) WHERE NOT EXISTS(SELECT 1 FROM users WHERE username=@username RETURNING id", conn))
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
            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id", conn))
            {
                sessionInsertCommand.Parameters.AddWithValue("@userId", user.Id);
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