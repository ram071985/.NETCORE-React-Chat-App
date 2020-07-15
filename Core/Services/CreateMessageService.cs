using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static Core.Services.AuthorizeUserService;



namespace Core.Services

{
    public interface ICreateMessageService
    {
        Message GetBackMessage(int sessionId, string text, DateTime createdDate);
    }
    public class CreateMessageService : ICreateMessageService
    {
        private string _databaseUserName;
        private string _databasePassword;

        public CreateMessageService(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
        }


        public Message GetBackMessage(int sessionId, string text, DateTime createdDate)
        {
            var message = new Message();

            var connString = "Host=localhost;Username=" + _databaseUserName + ";Password=" + _databasePassword + ";Database=chat_app";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var checkUsernameCommand = new NpgsqlCommand("SELECT user_id FROM sessions WHERE id = @id", conn))
            {

                checkUsernameCommand.Parameters.AddWithValue("@id", sessionId);

                using (var reader =checkUsernameCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        sessionId = (int)reader[0];

                    }
                }
            }


            using (var messageInsertCommand = new NpgsqlCommand("INSERT INTO messages (user_id, text, created_date) VALUES (@userId, @text, @created_date)", conn))
            {
                messageInsertCommand.Parameters.AddWithValue("@userId", sessionId);
                messageInsertCommand.Parameters.AddWithValue("@text", text);
                messageInsertCommand.Parameters.AddWithValue("@created_date", DateTime.Now);

                using (var reader = messageInsertCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        text = reader[3].ToString();
                        createdDate = (DateTime)reader[4];

                    }

                }
            }

            if (text == "")
            {

                throw new Exception("no text");

            }

            using (var messageInsertCommand = new NpgsqlCommand("SELECT u.username, m.text, m.created_date FROM messages m JOIN users u ON u.id = m.user_id WHERE m.user_id = u.id", conn))
            {

                using (var reader = messageInsertCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {                      
                        message.Username = reader[0].ToString();
                        message.Text = reader[1].ToString();
                        message.CreatedDate = (DateTime)reader[2];
                
                    }
                    return message;
                }

            }
        }

    }

    public class Message
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}