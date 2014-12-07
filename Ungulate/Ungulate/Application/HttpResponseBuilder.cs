using System;
using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public class HttpResponseBuilder : IHttpResponseBuilder
    {
        private readonly IMappingRepository _mappingRepository;
        private readonly IRequestHandlerBuilder _requestHandler;
        private readonly IHttpResponseFactory _responseFactory;

        public HttpResponseBuilder(IMappingRepository mappingRepository, IRequestHandlerBuilder requestHandler, IHttpResponseFactory responseFactory)
        {
            _mappingRepository = mappingRepository;
            _requestHandler = requestHandler;
            _responseFactory = responseFactory;
        }

        public IHttpResponse Build(IOwinRequest context)
        {
            var mappings = _mappingRepository.All();

            var handler = _requestHandler.Create(mappings);

            var mapping = handler.Process(context);

            return _responseFactory.Create(mapping, context);
        }
    }
}