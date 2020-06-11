using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using API;
using System.Linq;
using Npgsql;

[ApiController]
[Route("api/[controller]")]

 public class UsersController : ControllerBase
 {
        List<Users> users = new List<Users>();

    [HttpGet]
    public async System.Threading.Tasks.Task<List<Users>> GetAsync()
    {
        var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

        // Retrieve all rows
        await using (var cmd = new NpgsqlCommand("SELECT * FROM messages", conn))
        {
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                List<Users> userList = new List<Users>();

                while (await reader.ReadAsync())
                {
                    var user = new Users();
                    user.Id = (int)reader[0];
                    user.UserName = reader[1].ToString();       
                    user.CreatedDate = (DateTime)reader[3];
                    userList.Add(user);

                }
                return userList;
            }
        }

    }


    [HttpPost]
        public async System.Threading.Tasks.Task PostAsync(UserInputUser userInputUser)
    {
        var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();
        using (var cmd = new NpgsqlCommand("INSERT INTO messages (user_name, created_date) VALUES (@user_name, @created_date)", conn))
        {
            var random = new Random();
            cmd.Parameters.AddWithValue("@user_name", userInputUser.UserName);
            cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
            cmd.ExecuteNonQuery();
        }
    }




    [HttpGet("{id}")]
    public Users Get(int id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);
        return user;
    }
}

