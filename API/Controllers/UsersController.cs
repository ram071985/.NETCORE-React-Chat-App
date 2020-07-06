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

    [HttpPost]
    public async System.Threading.Tasks.Task PostAsync(UserModel userModel)
    {
        var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();
        using (var cmd = new NpgsqlCommand("INSERT INTO users (user_name, created_date) VALUES (@user_name, @created_date)", conn))
        {
            var random = new Random();
            cmd.Parameters.AddWithValue("@user_name", userModel.Username);
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

