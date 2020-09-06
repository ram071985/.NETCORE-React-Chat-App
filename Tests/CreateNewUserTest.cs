using System;
using System.IO;
using Core.DataAccess;
using Core.Services;
using NSubstitute;
using NUnit.Framework;
namespace Tests
{
    [TestFixture]
    public class CreateNewUserTest
    {
        private readonly Random _random = new Random();

        private ICreateNewUserService _createNewUserService;
        private IUserDataAccess _userDataAccess;
        private IDbConnection _dbConnection;
        private ISessionDataAccess _sessionDataAccess;

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        [SetUp]
        public void Setup()
        {       
           _dbConnection = Substitute.For<IDbConnection>();
           _userDataAccess = Substitute.For<IUserDataAccess>();
           _sessionDataAccess = Substitute.For<ISessionDataAccess>();
           _createNewUserService = new CreateNewUserService(_dbConnection, _sessionDataAccess, _userDataAccess);
        }

        [Test]
        public void should_add_user_return()
        {
            Random rnd = new Random();
            var id = rnd.Next();
            var username = RandomUtil.GetRandomString();
            var password = RandomUtil.GetRandomString();
            var userId = rnd.Next();
            var createdDate = DateTime.Now;
            var lastActiveAt = DateTime.Now;

            _createNewUserService.PostNewUser(id, userId, username, password, createdDate, lastActiveAt);

            _userDataAccess.Received(1).AddUser(
                Arg.Any<Npgsql.NpgsqlConnection>(),
                Arg.Is(id),
                Arg.Is(username),
                Arg.Is(password));               
        }
    }

    static class RandomUtil
    {
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); 
            return path;
        }
    }
}