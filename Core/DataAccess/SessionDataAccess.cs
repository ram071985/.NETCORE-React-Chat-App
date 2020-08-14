using System;
using Core.Entities;
using Npgsql;

namespace Core.DataAccess
{
    public interface ISessionDataAccess
    {
        Session CreateSession(NpgsqlConnection conn, int userId);
    }
    public class SessionDataAccess : ISessionDataAccess
    {
        private IDbConnection _dbConnection;

        public SessionDataAccess(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Session CreateSession(NpgsqlConnection conn, int userId)
        {
            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id", conn))
            {
                sessionInsertCommand.Parameters.AddWithValue("@userId", userId);
                using (var reader = sessionInsertCommand.ExecuteReader())
                {
                    var session = new Session();

                    while (reader.Read())
                    {
                        session.Id = (int)reader[0];
                        session.UserId = (int)reader[1];
                    }
                    return session;
                }
            }
        }
    }
}
