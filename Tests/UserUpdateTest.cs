using System;
using System.IO;
using Core.DataAccess;
using Core.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
namespace Tests
{
    [TestFixture]
    public class UserUpdateTest
    {
        private IUserUpdateService _userUpdateService;
        private IConfiguration _configuration;
        private IDbConnection _dbConnection;
        private IUserDataAccess _userDataAccess;
    
        [SetUp]
        public void Setup()
        {
            _configuration = Substitute.For<IConfiguration>();
            _userDataAccess = Substitute.For<IUserDataAccess>();
            _dbConnection = Substitute.For<IDbConnection>();
            _userUpdateService = new UserUpdateService(_dbConnection, _userDataAccess);
        }

        [Test]
        public void should_updated_username_column_not_be_empty()
        {
            var userId = 81;
            var username = UserRandomUtil.GetRandomString();
            var password = UserRandomUtil.GetRandomString();
            var newUsername = UserRandomUtil.GetRandomString();
            var createdDate = DateTime.Now;

            _userUpdateService.PutNewUsername(userId, username, password, newUsername, createdDate);

            _userDataAccess.Received(1).EditUsername(
                Arg.Any<Npgsql.NpgsqlConnection>(),
                Arg.Is(userId),
                Arg.Is(username),
                Arg.Is(newUsername),
                Arg.Is(password),
                Arg.Is(createdDate));
        }
    }
    static class UserRandomUtil
    {

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }
    }
}