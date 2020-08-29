using System;
using Core.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Core.Entities;
using Core.DataAccess;

namespace Tests
{
    [TestFixture]
    public class AuthorizeUserTest
    {
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

            var 
            
            Session session = _authorizeUserService.GetSession(1, 12, "random-username", "random-password", DateTime.Now, DateTime.Now);

            Assert.That(session.UserId, Is.GreaterThan(0));
        }
    }
}
