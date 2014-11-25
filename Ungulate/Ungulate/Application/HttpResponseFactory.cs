using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public class HttpResponseFactory : IHttpResponseFactory
    {
        public IHttpResponse Create(Mapping mapping)
        {
            var status = new StatusHttpResponse(mapping);
            var headers = new HeadersHttpResponse(mapping);
            var body = new BodyHttpResponse(mapping);

            IList<IHttpResponse> responseAppliers = new List<IHttpResponse>()
            {
                status,
                headers,
                body,
            };

            return new AllHttpResponse(responseAppliers);
        }
    }

    public class AllHttpResponse : IHttpResponse
    {
        private readonly IList<IHttpResponse> _responseAppliers;

        public AllHttpResponse(IList<IHttpResponse> responseAppliers)
        {
            _responseAppliers = responseAppliers;
        }

        public void ApplyTo(IOwinResponse owinResponse)
        {
            foreach (var responseApplier in _responseAppliers)
            {
                responseApplier.ApplyTo(owinResponse);
            }
        }
    }

    public class StatusHttpResponse : IHttpResponse
    {
        private readonly Mapping _mapping;

        public StatusHttpResponse(Mapping mapping)
        {
            _mapping = mapping;
        }

        public void ApplyTo(IOwinResponse owinResponse)
        {
            owinResponse.StatusCode = _mapping.Response.Status;
        }
    }
    
    public class HeadersHttpResponse : IHttpResponse
    {
        private readonly Mapping _mapping;

        public HeadersHttpResponse(Mapping mapping)
        {
            _mapping = mapping;
        }

        public void ApplyTo(IOwinResponse owinResponse)
        {
            foreach (var header in _mapping.Response.Headers)
            {
                owinResponse.Headers.Add(header.Key,new []{header.Value});
            }
        }
    }

    public class BodyHttpResponse : IHttpResponse
    {
        private readonly Mapping _mapping;

        public BodyHttpResponse(Mapping mapping)
        {
            _mapping = mapping;
        }

        public void ApplyTo(IOwinResponse owinResponse)
        {
           throw new NotImplementedException();
        }
    }
}