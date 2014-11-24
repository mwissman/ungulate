using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Ungulate;

namespace Tests
{
    [TestFixture]
    public class HttpStubMiddleWareTests
    {
        private HttpStubMiddleWare _middleware;
        private Mock<IHttpResponseBuilder> _builderMock;

        [SetUp]
        public void Setup()
        {
            _builderMock = new Mock<IHttpResponseBuilder>();
            
            OwinMiddleware next=null;
            _middleware = new HttpStubMiddleWare(next, _builderMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void MiddlewareUsesResponseBuilderToConsturctResponse()
        {
            var contextMock = new Mock<IOwinContext>();
            var requestMock = new Mock<IOwinRequest>();
            var responseMock = new Mock<IOwinResponse>();

            contextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
            contextMock.SetupGet(c => c.Response).Returns(responseMock.Object);

            var httpResponse=new Mock<IHttpResponse>();
            _builderMock.Setup(b => b.Build(requestMock.Object)).Returns(httpResponse.Object);

            _middleware.Invoke(contextMock.Object).Wait();

            httpResponse.Verify(r=>r.ApplyTo(responseMock.Object));
        }
    }
}
