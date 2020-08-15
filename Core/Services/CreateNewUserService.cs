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

        public CreateNewUserService(IDbConnection dbConnection, ISessionDataAccess sessionDataAccess)
        {
            _dbConnection = dbConnection;
            _sessionDataAccess = sessionDataAccess;
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

                using (var checkUsernameCommand = new NpgsqlCommand("SELECT * FROM users WHERE username = @username", conn))
                {
                    checkUsernameCommand.Parameters.AddWithValue("@username", username);

                    using (var reader = checkUsernameCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            username = reader[1].ToString();
                            if (username != "")
                            {
                                throw new Exception("redundant username");
                            }
                        }
                    }
                }

                using (var userInsertCommand = new NpgsqlCommand("INSERT INTO users (username, password, created_date) VALUES (@username, @password, @created_date) RETURNING id", conn))
                {

                    userInsertCommand.Parameters.AddWithValue("@username", username);
                    userInsertCommand.Parameters.AddWithValue("@password", password);
                    userInsertCommand.Parameters.AddWithValue("@created_date", DateTime.Now);

                    using (var reader = userInsertCommand.ExecuteReader())
                    {
                       while (reader.Read())
                        {
                            id = (int)reader[0];
                        }
                    }
                }
                return _sessionDataAccess.CreateSession(conn, id);
            }
        }
    }
}
