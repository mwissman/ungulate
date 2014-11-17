using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using NUnit.Framework;
using Ungulate;

namespace Tests
{
    [TestFixture]
    public class HttpStubMiddleWareTests
    {
        private HttpStubMiddleWare _middleware;

        [SetUp]
        public void Setup()
        {
            IThing foo=null;
            OwinMiddleware next=null;
            _middleware = new HttpStubMiddleWare(next,foo);
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void MiddlewareUsesResponseBuilderToConsturctResponse()
        {
            IOwinContext context=null;
            _middleware.Invoke(context).Wait();
        }
    }
}
