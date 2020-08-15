using System;
using System.IO;
using Core.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
namespace Tests
{
    [TestFixture]
    public class UserUpdateTest
    {
        private UserUpdateService _userUpdateService;
        [SetUp]
        public void Setup()
        {
            var configuration = Substitute.For<IConfiguration>();
            //_userUpdateService = new UserUpdateService(configuration);
        }

        [Test]
        public void should_updated_username_column_not_be_empty()
        {
            var username = _userUpdateService.PutNewUsername(81, "", UserRandomUtil.GetRandomString(), DateTime.Now);

            Assert.That(username.Username, Is.Not.EqualTo(null));
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