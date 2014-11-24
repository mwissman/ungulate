using Microsoft.Owin;

namespace Ungulate.Application
{
    public interface IHttpResponse
    {
        void ApplyTo(IOwinResponse owinResponse);
    }
}