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
        private CreateMessageService _createMessageService;
        [SetUp]
        public void Setup()
        {
            var configuration = Substitute.For<IConfiguration>();
            _createMessageService = new CreateMessageService(configuration);
        }

        [Test]
        public void should_message_text_not_be_empty()
        {
            var messages = _createMessageService.GetBackMessage(1, "random-text", DateTime.Now);

            Assert.That(messages[0].Text, Is.Not.Empty);
        }
    }
}