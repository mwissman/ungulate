using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Ungulate;
using Ungulate.Application;

namespace Tests
{
    [TestFixture]
    public class HttpStubMiddleWareTests
    {
        private Mock<IHttpResponseBuilder> _builderMock;
        private Mock<IHttpResponse> _httpResponse;
        private Mock<IOwinContext> _contextMock;
        private Mock<IOwinRequest> _requestMock;
        private Mock<IOwinResponse> _responseMock;

        [SetUp]
        public void Setup()
        {
            _builderMock = new Mock<IHttpResponseBuilder>();

            _contextMock = new Mock<IOwinContext>();
            _requestMock = new Mock<IOwinRequest>();
            _responseMock = new Mock<IOwinResponse>();

            _contextMock.SetupGet(c => c.Request).Returns(_requestMock.Object);
            _contextMock.SetupGet(c => c.Response).Returns(_responseMock.Object);

            _httpResponse = new Mock<IHttpResponse>();
            _builderMock.Setup(b => b.Build(_requestMock.Object)).Returns(_httpResponse.Object);



          
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void MiddlewareUsesResponseBuilderToConsturctResponse()
        {
            var middleware = new HttpStubMiddleWare(null, _builderMock.Object);
  
            middleware.Invoke(_contextMock.Object).Wait();

            _httpResponse.Verify(r=>r.ApplyTo(_responseMock.Object));
        }

        [Test]
        public void CallsNextMiddleware()
        {
            var fakeMiddleware = new FakeMiddleWare();
            var middleware = new HttpStubMiddleWare(fakeMiddleware, _builderMock.Object);

            middleware.Invoke(_contextMock.Object).Wait();

            _httpResponse.Verify(r => r.ApplyTo(_responseMock.Object));
            Assert.IsTrue(fakeMiddleware.Invoked);
        
        }
    }

    public class FakeMiddleWare : OwinMiddleware
    {
        public bool Invoked=false;

        public FakeMiddleWare() : base(null)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            Invoked = true;
        }
    }
}
