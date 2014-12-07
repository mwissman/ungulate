using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public interface IHttpResponseFactory
    {
        IHttpResponse Create(Mapping mapping, IOwinRequest context);
    }
}