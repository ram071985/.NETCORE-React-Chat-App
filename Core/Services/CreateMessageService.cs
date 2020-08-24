using System;
using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities;

namespace Core.Services

{
    public interface ICreateMessageService
    {
        List<Message> GetBackMessage(int userId, string text, DateTime createdDate);
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

        public List<Message> GetBackMessage(int userId, string text, DateTime createdDate)
        {
            using (var conn = _dbConnection.GetConnection())
            { 
                _messageDataAccess.AddMessage(conn, userId, text, createdDate);

              if (text == "")
              {
                  throw new Exception("no text");
              }
                return _messageDataAccess.GetMessages(conn);              
            }
        }
    }
}