using System;
using Core.Entities;
using Npgsql;

namespace Core.DataAccess
{
    public interface ISessionDataAccess
    {
        Session CreateSession(NpgsqlConnection conn, int id, int userId, DateTime lastActiveAt);
        Session GetUserId(NpgsqlConnection conn, int sessionId);
    }
    public class SessionDataAccess : ISessionDataAccess
    {
        private IUserDataAccess _userDataAccess;

        public SessionDataAccess(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public Session GetUserId(NpgsqlConnection conn, int sessionId)
        {
            using (var checkUsernameCommand = new NpgsqlCommand("SELECT user_id FROM sessions WHERE id = @id", conn))
            {
                checkUsernameCommand.Parameters.AddWithValue("@id", sessionId);

                using (var reader = checkUsernameCommand.ExecuteReader())
                {
                    var session = new Session();
                    while (reader.Read())
                    {
                        session.SessionId = (int)reader[0];
                    }
                    return session;
                }
            }
        }

        public Session CreateSession(NpgsqlConnection conn, int id, int userId, DateTime lastActiveAt)
        {
            using (var sessionInsertCommand = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (@userId) RETURNING id, user_id", conn))
            {            
                sessionInsertCommand.Parameters.AddWithValue("@userId", id);
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
