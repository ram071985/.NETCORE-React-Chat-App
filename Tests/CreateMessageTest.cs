using System;
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

        [SetUp]
        public void Setup()
        {
           _configuration = Substitute.For<IConfiguration>();
           _createMessageService = Substitute.For<ICreateMessageService>();
        }

        [Test]
        public void should_message_text_not_be_empty()
        {
            var messages = _createMessageService.GetBackMessage(1, "random-text", DateTime.Now);

            Assert.That(messages[0].Text, Is.Not.Empty);
        }
    }
}