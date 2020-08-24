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
        private ICreateNewUserService _createNewUserService;
        private IConfiguration _configuration;
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        [SetUp]
        public void Setup()
        {
           _configuration = Substitute.For<IConfiguration>();
           _createNewUserService = Substitute.For<ICreateNewUserService>();
        }

        [Test]
        public void should_session_id_is_not_equal_to_null()
        {
            Random rnd = new Random();
            var sessionId = _createNewUserService.PostNewUser(rnd.Next(), 0, RandomUtil.GetRandomString(), RandomUtil.GetRandomString(), DateTime.Now, DateTime.Now);

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