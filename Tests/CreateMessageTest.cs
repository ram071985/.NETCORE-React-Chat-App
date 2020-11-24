using System;
using System.IO;
using Core.DataAccess;
using Core.Services;
using NSubstitute;
using NUnit.Framework;
namespace Tests
{
    [TestFixture]
    public class CreateMessageTest
    {
        
        private readonly Random _random = new Random();

        private IDbConnection _dbConnection;
        private ICreateMessageService _createMessageService;
        private IMessageDataAccess _messageDataAccess;

      [SetUp]
        public void Setup()
        {
            _dbConnection = Substitute.For<IDbConnection>();
            _messageDataAccess = Substitute.For<IMessageDataAccess>();
            _createMessageService = new CreateMessageService(_dbConnection, _messageDataAccess);         
        }  

        [Test]
        public void should_add_message_sql_return()
        {
            Random rnd = new Random();

            var userId = rnd.Next();
            var text = MessageRandomUtil.GetRandomString();
            var createdDate = DateTime.Now;

            _createMessageService.GetBackMessage(userId, text, createdDate);
        
            _messageDataAccess.Received(1).AddMessage(
                 Arg.Any<Npgsql.NpgsqlConnection>(),
                 Arg.Is(userId),
                 Arg.Is(text),
                 Arg.Is(createdDate));         
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
