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
       
    }
}
