using System;
using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate
{
    public class HttpResponseBuilder : IHttpResponseBuilder
    {
        private readonly IMappingRepository _mappingRepository;
        private readonly IRequestHandlerBuilder _requestHandler;

        public HttpResponseBuilder(IMappingRepository mappingRepository, IRequestHandlerBuilder requestHandler)
        {
            _mappingRepository = mappingRepository;
            _requestHandler = requestHandler;
        }

        public IHttpResponse Build(IOwinRequest context)
        {
            var mappings=_mappingRepository.All();

            var handler = _requestHandler.Create(mappings);

            var httpResponse =handler.Process(context);

            throw new NotImplementedException();
        }
    }
}