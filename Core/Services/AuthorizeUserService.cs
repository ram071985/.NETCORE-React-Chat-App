using System;
namespace Core.Services
{

    public interface IAuthorizeUserService
    {
        AuthorizeUserService.Session GetSession(string username, string password);
    }

    public class AuthorizeUserService : IAuthorizeUserService
    {
        public AuthorizeUserService()
        {
        }

        public Session GetSession(string username, string password)
        {
            if (username == "")
            {
                throw new Exception("empty username");
            }
            if (password == "")
            {
                throw new Exception("empty password");
            }

            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";


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
                        authorizationModel.Username = reader[1].ToString();
                        authorizationModel.Password = reader[2].ToString();

                    }
                }
            }

            if (authorizationModel.Id == 0)
            {

                throw new Exception("false username");

            }

            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id, user_id", conn))
            {

                sessionInsertCommand.Parameters.AddWithValue("@userId", authorizationModel.Id);
                await using (var reader = await sessionInsertCommand.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        var session = new Session();

                        session.Id = (int)reader[0];
                        session.UserId = (int)reader[1];

                    }
                    return session;

                }
            }
        }
        
        public class Session
        {

           public int Id { get; set; }
           public int UserId { get; set; }

        }
    }
}
