using Moq;
using NUnit.Framework;
using Ungulate.Application;
using Ungulate.Domain;

namespace Tests
{
    [TestFixture]
    public class HttpResponseBuilderTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void LoadsMappingsFromRepositoryFindsCorrectMappingandCreatesResponse()
        {
            var responseFactoryMock = new Mock<IHttpResponseFactory>();
            var requestHandlerMock = new Mock<IRequestHandlerBuilder>();
            var mappingRepositoryMock = new Mock<IMappingRepository>();

            IHttpResponseFactory responseFactory=responseFactoryMock.Object;
            IRequestHandlerBuilder requestHandler=requestHandlerMock.Object;
            IMappingRepository mappingRepository=mappingRepositoryMock.Object;

            HttpResponseBuilder builder = new HttpResponseBuilder(mappingRepository,requestHandler,responseFactory);
        }
    }
}