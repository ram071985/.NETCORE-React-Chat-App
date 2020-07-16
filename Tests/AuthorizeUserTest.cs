using Core.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AuthorizeUserTest
    {
        private AuthorizeUserService _authorizeUserService;
        [SetUp]
        public void Setup()
        {
            var configuration = Substitute.For<IConfiguration>();
            _authorizeUserService = new AuthorizeUserService(configuration);
        }

        [Test]
        public void should_users_table_id_column_is_equal_to_no_number_is_false()
        {
            var session = _authorizeUserService.GetSession(1, "random-username", "random-password");

            Assert.That(session.UserId, Is.GreaterThan(0));
        }
    }
}
