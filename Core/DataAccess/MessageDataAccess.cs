using System;
using System.Collections.Generic;
using Core.Entities;
using Npgsql;

namespace Core.DataAccess
{
    public interface IMessageDataAccess
    {
        Message AddMessage(NpgsqlConnection conn, int userId, string text, DateTime createdDate);
        
        List<Message> GetMessages(NpgsqlConnection conn);
    }
    public class MessageDataAccess : IMessageDataAccess
    {
        private ISessionDataAccess _sessionDataAccess;
        public MessageDataAccess(ISessionDataAccess sessionDataAccess)
        {
            _sessionDataAccess = sessionDataAccess;
        }
        public Message AddMessage(NpgsqlConnection conn, int userId, string text, DateTime createdDate)
        {
            using (var messageInsertCommand = new NpgsqlCommand("INSERT INTO messages (user_id, text, created_date) VALUES (@userId, @text, @created_date)", conn))
            {
                messageInsertCommand.Parameters.AddWithValue("@userId", userId);
                messageInsertCommand.Parameters.AddWithValue("@text", text);
                messageInsertCommand.Parameters.AddWithValue("@created_date", DateTime.Now);

                using (var reader = messageInsertCommand.ExecuteReader())
                {
                    var message = new Message();

                    while (reader.Read())
                    {
                        userId = (int)reader[0];
                        message.Text = reader[3].ToString();
                        message.CreatedDate = (DateTime)reader[4];                     
                    }
                    return message;             
                }
            }
        }

        public List<Message> GetMessages(NpgsqlConnection conn)
        {
            using (var messageInsertCommand = new NpgsqlCommand("SELECT u.id, u.username, u.password, u.last_active_at, u.created_date, m.id, m.text, m.created_date FROM messages m " +
                "JOIN users u ON u.id = m.user_id WHERE m.user_id = u.id", conn))
            {
                using (var reader = messageInsertCommand.ExecuteReader())
                {
                    var messages = new List<Message>();

                    while (reader.Read())
                    {
                        var message = new Message
                        {
                            Id = (int)reader[5],
                            User = new User
                            {
                                Id = (int)reader[0],
                                Username = reader[1].ToString(),                          
                                Password = reader[2].ToString(),
                                CreatedDate = (DateTime)reader[4],
                                LastActiveAt = (DateTime)reader[3],
                                
                            },
                            Text = reader[6].ToString(),
                            CreatedDate = (DateTime)reader[7],
                                                                   
                        };
                        messages.Add(message);
                    }
                    return messages;
                }
            }
        }
    }
}
