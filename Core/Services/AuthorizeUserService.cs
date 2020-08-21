using System;
using Core.DataAccess;
using Core.Entities;

namespace Core.Services
{
    public interface IAuthorizeUserService
    {
        Session GetSession(int id, int userId, string username, string password, DateTime createdDate, DateTime lastActiveAt);
    }

    public class AuthorizeUserService : IAuthorizeUserService
    {
        private IDbConnection _dbConnection;
        private ISessionDataAccess _sessionDataAccess;
        private IUserDataAccess _userDataAccess;

        public AuthorizeUserService(IDbConnection dbConnection,
            ISessionDataAccess sessionDataAccess, IUserDataAccess userDataAccess)
        {
            _dbConnection = dbConnection;
            _sessionDataAccess = sessionDataAccess;
            _userDataAccess = userDataAccess;
        }

        public Session GetSession(int id, int userId, string username, string password,  DateTime createdDate, DateTime lastActiveAt)
        {
            if (username == "")
            {
                throw new Exception("empty username");
            }
            if (password == "")
            {
                throw new Exception("empty password");
            }

            using (var conn = _dbConnection.GetConnection())
            {
                var user = _userDataAccess.CheckUserCredentials(conn, id, username, password, createdDate, lastActiveAt);

                if (user.Password != password)
                {
                    throw new Exception("wrong credentials");
                }
                var session = _sessionDataAccess.CreateSession(conn, user.Id, userId, lastActiveAt);

                return session;
               
            }
        }
    }
}
