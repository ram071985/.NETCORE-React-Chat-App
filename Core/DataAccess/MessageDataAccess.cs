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
