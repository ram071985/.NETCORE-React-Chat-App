using System;
using Core.DataAccess;
using Core.Entities;

namespace Core.Services
{
    public interface IAuthorizeUserService
    {
        Session GetSession(int id, string username, string password);
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

        public Session GetSession(int id, string username, string password)
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

                _userDataAccess.CheckUserCredentials(conn, id, username, password);

                if (id == 0)
                {
                    throw new Exception("wrong credentials");
                }

                return _sessionDataAccess.CreateSession(conn, id);
            }
        }
    }
}
