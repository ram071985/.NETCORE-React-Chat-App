using System;

namespace Core.Services
{
    public interface ICreateNewUserService
    {
        List<Users> GetUser(int sessionId, string text, DateTime createdDate);
    }
    public class CreateNewUserService : ICreateNewUserService
    {
        private string _databaseUserName;
        private string _databasePassword;

        public CreateNewUserService(IConfiguration configuration)
        {
            _databaseUserName = configuration["Database:Username"];
            _databasePassword = configuration["Database:Password"];
        }

        public List<Users> GetUser(int sessionId, string text, DateTime createdDate)
        {

        }
    }
}
