using System;
using Core.DataAccess;
using Core.Entities;
using Npgsql;

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

        public AuthorizeUserService(IDbConnection dbConnection,
            ISessionDataAccess sessionDataAccess)
        {
            _dbConnection = dbConnection;
            _sessionDataAccess = sessionDataAccess;
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
                using (var checkUsernameCommand = new NpgsqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", conn))
                {
                    checkUsernameCommand.Parameters.AddWithValue("@username", username);
                    checkUsernameCommand.Parameters.AddWithValue("@password", password);

                    using (var reader = checkUsernameCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = (int)reader[0];
                            username = reader[1].ToString();
                            password = reader[2].ToString();
                        }
                    }
                }

                if (id == 0)
                {
                    throw new Exception("wrong credentials");
                }
                return _sessionDataAccess.CreateSession(conn, id);
            }
        }
    }
}
