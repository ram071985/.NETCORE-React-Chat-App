using System;
using System.IO;
using Core.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
namespace Tests
{
    [TestFixture]
    public class CreateNewUserTest
    {
        private readonly Random _random = new Random();

        private CreateNewUserService _createNewUserService;

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        [SetUp]
        public void Setup()
        {
            var configuration = Substitute.For<IConfiguration>();
            _createNewUserService = new CreateNewUserService(configuration);
        }

        [Test]
        public void should_session_id_is_not_equal_to_null()
        {
            Random rnd = new Random();
            var sessionId = _createNewUserService.PostNewUser(rnd.Next(), 0, RandomUtil.GetRandomString(), RandomUtil.GetRandomString(), DateTime.Now);

            Assert.That(sessionId.Id, Is.Not.EqualTo(null));
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