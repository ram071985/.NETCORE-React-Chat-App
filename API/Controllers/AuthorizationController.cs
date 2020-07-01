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
        public async System.Threading.Tasks.Task<SessionModel> PostAsync([FromBody] AuthorizationModel authorizationModel)
        {
            var session = new SessionModel();

            if (authorizationModel.Username == "")
            {
                throw new Exception("empty username");
            }
            if (authorizationModel.Password == "")
            {
                throw new Exception("empty password");
            }

            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";


            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", conn))
            {

                checkUsernameCommand.Parameters.AddWithValue("@username", authorizationModel.Username);
                checkUsernameCommand.Parameters.AddWithValue("@password", authorizationModel.Password);

                await using (var reader = await checkUsernameCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        authorizationModel.Id = (int)reader[0];                    
                    
                    }
                }
            }

            if (authorizationModel.Id == 0)
            {

                throw new Exception("false username");

            }
            
            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id", conn))
            {
                
                sessionInsertCommand.Parameters.AddWithValue("@userId", authorizationModel.Id);
                await using (var reader = await sessionInsertCommand.ExecuteReaderAsync())
                {                   

                    while (await reader.ReadAsync())
                    {
                        session.Id = (int)reader[0];  
                    }
                    
                }
            }
            return session;
        }

    }
}
