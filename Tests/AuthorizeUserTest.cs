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
        private Session _session;

        public void Setup()
        {
            _session = new Session();
        }

        [Test]
        public void should_users_table_id_column_is_equal_to_no_number_is_true()
        {
            _session = new Session();
            var result = _session.Id;
            Assert.That(result, Is.LessThan(1));
        }

        [Test]
        public void should_users_table_id_column_is_equal_to_no_number_is_false()
        {
            _session = new Session();
            var result = _session.Id;
            Assert.That(result, Is.GreaterThan(0));
        }


    }
}
