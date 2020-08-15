using System;
using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities;

namespace Core.Services
{
    public interface IGetMessagesService
    {
        List<Message> GetMessages();
    }

    public class GetMessagesService : IGetMessagesService
    {
        private IDbConnection _dbConnection;
        private IMessageDataAccess _messageDataAccess;

        public GetMessagesService(IDbConnection dbConnection, IMessageDataAccess messageDataAccess)
        {
            _dbConnection = dbConnection;
            _messageDataAccess = messageDataAccess;
        }

        public List<Message> GetMessages()
        {
            using (var conn = _dbConnection.GetConnection())
            {
                return _messageDataAccess.GetMessages(conn);
            }
        }
    }
}