using System;
using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static Core.Services.AuthorizeUserService;

namespace Core.Services

{
    public interface ICreateMessageService
    {
        List<Message> GetBackMessage(int sessionId, string text, DateTime createdDate);
    }
    public class CreateMessageService : ICreateMessageService
    {
        private IDbConnection _dbConnection;
        private IMessageDataAccess _messageDataAccess;

        public CreateMessageService(IDbConnection dbConnection, IMessageDataAccess messageDataAccess)
        {
            _dbConnection = dbConnection;
            _messageDataAccess = messageDataAccess;
        }

        public List<Message> GetBackMessage(int sessionId, string text, DateTime createdDate)
        {
            using (var conn = _dbConnection.GetConnection())
            { 

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

                return _messageDataAccess.GetMessages(conn);

              
            }
        }
    }
}