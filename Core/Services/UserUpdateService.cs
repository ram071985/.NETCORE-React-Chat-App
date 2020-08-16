using System;
using Core.DataAccess;
using Core.Entities;
using Npgsql;

namespace Core.Services
{
    public interface IUserUpdateService
    {
        User PutNewUsername(int userId, string username, string password, string newUsername, DateTime createdDate);
        User UpdateLastActive(int userId);
    }
    public class UserUpdateService : IUserUpdateService
    {
        private IDbConnection _dbConnection;
        private IUserDataAccess _userDataAccess;

        public UserUpdateService(IDbConnection dbConnection, IUserDataAccess userDataAccess)
        {
            _dbConnection = dbConnection;
            _userDataAccess = userDataAccess;
        }

        public User UpdateLastActive(int userId)
        {
            using (var conn = _dbConnection.GetConnection())
            {

               return _userDataAccess.UserLastActiveUpdate(conn, userId);
                
            }
        }

        public User PutNewUsername(int userId, string username, string password, string newUsername, DateTime createdDate)
        {
            using (var conn = _dbConnection.GetConnection())
            {
                return _userDataAccess.EditUsername(conn, userId, username, newUsername, password, createdDate);
            }
        }
    }
}

