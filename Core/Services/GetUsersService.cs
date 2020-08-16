using System;
using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Core.Services
{
    public interface IGetUsersService
    {
        List<User> GetUserObject(string username);
    }
    public class GetUsersService : IGetUsersService
    {
        private IDbConnection _dbConnection;
        private IUserDataAccess _userDataAccess;

        public GetUsersService(IDbConnection dbConnection, IUserDataAccess userDataAccess)
        {
            _dbConnection = dbConnection;
            _userDataAccess = userDataAccess;
        }

        public List<User> GetUserObject(string username)
        {
            using (var conn = _dbConnection.GetConnection())
            {
                return _userDataAccess.UserLastActive(conn, username);
        
            }
        }
    }
}
