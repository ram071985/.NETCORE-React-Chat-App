using System;
using System.Collections.Generic;
using Core.Entities;
using Npgsql;

namespace Core.DataAccess
{
    public interface IMessageDataAccess
    {
        List<Message> GetMessages(NpgsqlConnection conn);
    }
    public class MessageDataAccess : IMessageDataAccess
    {
        public Message AddMessage(NpgsqlConnection conn, int sessionId, string text, DateTime createdDate)
        {
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
        }

        public List<Message> GetMessages(NpgsqlConnection conn)
        {
            using (var messageInsertCommand = new NpgsqlCommand("SELECT u.id, u.username, u.password, u.last_active_at, u.created_date, m.text, m.created_date FROM messages m JOIN users u ON u.id = m.user_id WHERE m.user_id = u.id", conn))
            {

                using (var reader = messageInsertCommand.ExecuteReader())
                {
                    var messages = new List<Message>();

                    while (reader.Read())
                    {
                        var message = new Message
                        {
                            User = new User
                            {
                                Id = (int)reader[0],
                                Username = reader[1].ToString(),
                                Password = reader[2].ToString(),
                                LastActiveAt = (DateTime)reader[3],
                                CreatedDate = (DateTime)reader[4]
                            },
                            Text = reader[1].ToString(),
                            CreatedDate = (DateTime)reader[2],
                        };
                        messages.Add(message);
                    }
                    return messages;
                }
            }
        }
    }
}
