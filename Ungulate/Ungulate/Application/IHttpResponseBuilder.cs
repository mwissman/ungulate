using Microsoft.Owin;

namespace Ungulate.Application
{
    public interface IHttpResponseBuilder
    {
        IHttpResponse Build(IOwinRequest context);
    }
}