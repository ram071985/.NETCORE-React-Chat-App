using System;
using Core;
using NUnit.Framework;
using static Core.Services.AuthorizeUserService;

namespace Tests
{
    [TestFixture]
    public class AuthorizeUserTest
    {       
        private Session _session;

        public void Setup()
        {
            _session = new Session();
        }

        [Test]
        public void should_users_table_id_column_is_equal_to_no_number_is_true()
        {
            _session = new Session();
            var result = _session.id;
        }

       
    }
}
