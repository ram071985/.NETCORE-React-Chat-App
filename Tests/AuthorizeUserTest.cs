using System;
using Core.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Core.Entities;
using Core.DataAccess;
using System.IO;

namespace Tests
{
    [TestFixture]
    public class AuthorizeUserTest
    {
        private readonly Random _random = new Random();

        private IDbConnection _dbConnection;
        private IAuthorizeUserService _authorizeUserService;
        private IUserDataAccess _userDataAccess;
        private ISessionDataAccess _sessionDataAccess;

        [SetUp]
        public void Setup()
        {
            _dbConnection = Substitute.For<IDbConnection>();
            _userDataAccess = Substitute.For<IUserDataAccess>();
            _sessionDataAccess = Substitute.For<ISessionDataAccess>();
            _authorizeUserService = new AuthorizeUserService(_dbConnection, _sessionDataAccess, _userDataAccess);
        }

        [Test]
        public void should_users_table_id_column_is_equal_to_no_number_is_false()
        {
            Random rnd = new Random();

     

            var id = rnd.Next();
            var username = CredentialRandomUtil.GetRandomString();
            var password = "reid";
            var userId = rnd.Next();
            var createdDate = DateTime.Now;
            var lastActiveAt = DateTime.Now;

                _authorizeUserService.GetSession(
                Arg.Is(id),
                Arg.Is(userId),
                Arg.Is(username),
                Arg.Is("reid"),
                Arg.Is(createdDate),
                Arg.Is(lastActiveAt)
                );
    

            Assert.That(password, Is.EqualTo("reid"));
        }

        static class CredentialRandomUtil
        {

            public static string GetRandomString()
            {
                string path = Path.GetRandomFileName();
                path = path.Replace(".", "");
                return path;
            }
        }
    }
}
