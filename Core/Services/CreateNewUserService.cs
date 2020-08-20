using System;
using Core.DataAccess;
using Core.Entities;
using Npgsql;

namespace Core.Services
{
    public interface ICreateNewUserService
    {
        Session PostNewUser(int id, int userId, string username, string password, DateTime createdDate);
    }
    public class CreateNewUserService : ICreateNewUserService
    {
        private IDbConnection _dbConnection;
        private ISessionDataAccess _sessionDataAccess;
        private IUserDataAccess _userDataAccess;

        public CreateNewUserService(IDbConnection dbConnection, ISessionDataAccess sessionDataAccess,
            IUserDataAccess userDataAccess)
        {
            _dbConnection = dbConnection;
            _sessionDataAccess = sessionDataAccess;
            _userDataAccess = userDataAccess;
        }

        public Session PostNewUser(int id, int userId, string username, string password, DateTime createdDate)
        {
            using (var conn = _dbConnection.GetConnection())
            {
                if (username == "")
                {
                    throw new Exception("empty username");
                }
                if (password == "")
                {
                    throw new Exception("empty password");
                }

                _userDataAccess.AddUser(conn, id, username, password);

                return _sessionDataAccess.CreateSession(conn, id, userId);
            }
        }
    }
}
