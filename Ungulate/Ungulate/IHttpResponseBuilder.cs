using Microsoft.Owin;

namespace Ungulate
{
    public interface IHttpResponseBuilder
    {
        IHttpResponse Build(IOwinRequest context);
    }
}