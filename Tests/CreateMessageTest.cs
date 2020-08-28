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
    public class CreateMessageTest
    {
        private IConfiguration _configuration;
        private ICreateMessageService _createMessageService;
        private IMessageDataAccess _messageDataAccess;

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        [SetUp]
        public void Setup()
        {
           _configuration = Substitute.For<IConfiguration>();
           _createMessageService = Substitute.For<ICreateMessageService>();
           _messageDataAccess = Substitute.For<IMessageDataAccess>();
        }

        [Test]
        public void should_message_sql_return()
        {

            var rnd = new Random();
            var userId = rnd.Next();
            var text = MessageRandomUtil.GetRandomString();
            var createdDate = DateTime.Now;

            _createMessageService.GetBackMessage(userId, text, createdDate);
           
            _messageDataAccess.Received(1).GetMessages();
        }

        static class MessageRandomUtil
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
