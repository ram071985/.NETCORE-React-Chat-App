using System;
using System.IO;
using Core.DataAccess;
using Core.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
namespace Tests
{
    [TestFixture]
    public class UserUpdateTest
    {
        private IUserUpdateService _userUpdateService;
        private IConfiguration _configuration;
    
        [SetUp]
        public void Setup()
        {
            _configuration = Substitute.For<IConfiguration>();
            _userUpdateService = Substitute.For<IUserUpdateService>();
        }

        [Test]
        public void should_updated_username_column_not_be_empty()
        {
            var username = _userUpdateService.PutNewUsername(81, "","", UserRandomUtil.GetRandomString(), DateTime.Now);

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