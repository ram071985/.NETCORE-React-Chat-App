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
        public void should_throw_exception_when_username_is_empty_string()
        {
            Random rnd = new Random();

            var id = rnd.Next();
            var username = "";
            var password = "";
            var userId = rnd.Next();
            var createdDate = DateTime.Now;
            var lastActiveAt = DateTime.Now;

            var aus = new AuthorizeUserService(_dbConnection, _sessionDataAccess, _userDataAccess);

            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("empty username"), () => aus.GetSession(id, userId, username, password, createdDate, lastActiveAt));


        }

        [Test]
        public void should_throw_exception_when_password_is_empty_string()
        {
            Random rnd = new Random();

            var id = rnd.Next();
            var username = CredentialRandomUtil.GetRandomString();
            var password = "";
            var userId = rnd.Next();
            var createdDate = DateTime.Now;
            var lastActiveAt = DateTime.Now;

            var aus = new AuthorizeUserService(_dbConnection, _sessionDataAccess, _userDataAccess);

            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("empty password"), () => aus.GetSession(id, userId, username, password, createdDate, lastActiveAt));
        }

        [Test]
        public void should_throw_exception_when_passwords_dont_match()
        {
            Random rnd = new Random();          

            var user = new User
            {
                Id = 1,
                Username = CredentialRandomUtil.GetRandomString(),
                CreatedDate = DateTime.Now,
                Password = "password"                
            };

            _userDataAccess.CheckUserCredentials(
                Arg.Any<Npgsql.NpgsqlConnection>(),
                Arg.Is(user.Id),
                Arg.Is(user.Username),
                Arg.Is(user.Password),
                Arg.Is(user.CreatedDate),
                Arg.Is(user.LastActiveAt)        
                ).Returns(user);

            var aus = new AuthorizeUserService(_dbConnection, _sessionDataAccess, _userDataAccess);

            var id = 1;
            var userId = 1;
            var username = CredentialRandomUtil.GetRandomString();
            var password = "reid";
            var createdDate = DateTime.Now;
            var lastActiveAt = DateTime.Now;

            _authorizeUserService.GetSession(
               id,
                userId,
                username,
                password,
                createdDate,
                lastActiveAt
                );

            _userDataAccess.CheckUserCredentials(
              Arg.Any<Npgsql.NpgsqlConnection>(),
              Arg.Is(id),
              Arg.Is(username),
              Arg.Is(password),
              Arg.Is(createdDate),
              Arg.Is(lastActiveAt)
              );



            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("wrong credentials"), () => _authorizeUserService.GetSession(id, userId, username, password, createdDate, lastActiveAt));
            
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
