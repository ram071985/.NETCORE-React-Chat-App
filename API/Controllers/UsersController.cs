using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using API;
using System.Linq;
using Npgsql;

[ApiController]
[Route("api/users")]

 public class UsersController : ControllerBase
 {
        List<UserModel> users = new List<UserModel>();

   


            [HttpPost]
                public async System.Threading.Tasks.Task PostAsync(UserInputUser userInputUser)
            {
                var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

                await using var conn = new NpgsqlConnection(connString);
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("INSERT INTO users (user_name, created_date) VALUES (@user_name, @created_date)", conn))
                {
                    var random = new Random();
                    cmd.Parameters.AddWithValue("@user_name", userInputUser.UserName);
                    cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }




    [HttpGet("{id}")]
    public UserModel GetUser(int id)
    {
        var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        // Retrieve all rows
        using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE id = @id", conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            using (var reader = cmd.ExecuteReader())
            {
                var user = new UserModel();

                while (reader.Read())
                {
                    user.Id = (int)reader[0];
                    user.UserName = reader[1].ToString();
                    user.CreatedDate = (DateTime)reader[2];

                }
                return user;
               
            }
        }

    }
}

