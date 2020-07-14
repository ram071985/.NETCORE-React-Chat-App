using System;
using Core;
using Core.Services;
using NUnit.Framework;
using static Core.Services.AuthorizeUserService;

namespace Tests
{
    [TestFixture]
    public class AuthorizeUserTest
    {
        private AuthorizeUserService _authorizeUserService;

        [SetUp]
        public void Setup()
        {
            _authorizeUserService = new AuthorizeUserService();
        }

        [Test]
        public void should_users_table_id_column_is_equal_to_no_number_is_true()
        { 
            var session = _authorizeUserService.GetSession(1, "random-username", "random-password");

            Assert.That(session, Is.LessThan(1));
        }

        [Test]
        public void should_users_table_id_column_is_equal_to_no_number_is_false()
        {
            var session = _authorizeUserService.GetSession(1, "random-username", "random-password");

            Assert.That(session, Is.LessThan(1));
        }





    }
}
