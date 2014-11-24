using Microsoft.Owin;

namespace Ungulate
{
    public interface IHttpResponse
    {
        void ApplyTo(IOwinResponse owinResponse);
    }
}