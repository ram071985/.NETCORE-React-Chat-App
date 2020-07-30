using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;



namespace Core.Services

{
    public interface IGetMessagesService
    {
        List<RetrieveMessage> GetMessages();
    }
    public class GetMessagesService : IGetMessagesService
    {
        private string _databaseUserName;
        private string _databasePassword;
        private string _databaseHost;
        private string _databaseName;

        public GetMessagesService(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
            _databaseHost = configuration["Database:Host"];
            _databaseName = configuration["Database:Name"];
        }


        public List<RetrieveMessage> GetMessages()
        {
            var connString = "Host=" + _databaseHost + ";Username =" + _databaseUserName + ";Password=" + _databasePassword + ";Database=" + _databaseName;

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using (var messageInsertCommand = new NpgsqlCommand("SELECT u.username, m.text, m.created_date FROM messages m JOIN users u ON u.id = m.user_id WHERE m.user_id = u.id", conn))
            {

                using (var reader = messageInsertCommand.ExecuteReader())
                {
                    var messages = new List<RetrieveMessage>();

                    while (reader.Read())
                    {
                        var message = new RetrieveMessage();
                        message.Username = reader[0].ToString();
                        message.Text = reader[1].ToString();
                        message.CreatedDate = (DateTime)reader[2];
                        messages.Add(message);


                    }
                    return messages;
                }

            }
        }
    }

    public class RetrieveMessage
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}