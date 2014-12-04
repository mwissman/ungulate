using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public class HttpResponseFactory : IHttpResponseFactory
    {
        public IHttpResponse Create(Mapping mapping, IOwinRequest context)
        {
            var status = new StatusHttpResponse(mapping);
            var headers = new HeadersHttpResponse(mapping);
            var body = new BodyHttpResponse(mapping, context);

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
        private readonly IOwinRequest _context;

        public BodyHttpResponse(Mapping mapping, IOwinRequest context)
        {
            _mapping = mapping;
            _context = context;
        }

        public void ApplyTo(IOwinResponse owinResponse)
        {
            var bodyText = _mapping.Response.Body;
            if (!string.IsNullOrWhiteSpace(_mapping.Response.BodyFileName))
            {
                bodyText = File.ReadAllText(Path.Combine("__files", _mapping.Response.BodyFileName));
            }

            foreach (var parameter in _context.Query)
            {
                bodyText = bodyText.Replace("${" + parameter.Key + "}", string.Join(",",parameter.Value));
            }

            using (TextWriter textWriter = new StreamWriter(owinResponse.Body))
            {
                textWriter.Write(bodyText);
            }
        }
    }
}