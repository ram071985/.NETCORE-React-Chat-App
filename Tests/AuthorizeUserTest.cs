using System;
using Core.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AuthorizeUserTest
    {
        private IConfiguration _configuration;
        private IAuthorizeUserService _authorizeUserService;

        [SetUp]
        public void Setup()
        {
            _configuration = Substitute.For<IConfiguration>();
            _authorizeUserService = Substitute.For<IAuthorizeUserService>();
        }

        [Test]
        public void should_users_table_id_column_is_equal_to_no_number_is_false()
        {
            var session = _authorizeUserService.GetSession(1, 12, "random-username", "random-password", DateTime.Now, DateTime.Now);

            Assert.That(session.UserId, Is.GreaterThan(0));
        }
    }
}
