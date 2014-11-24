using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate
{
    public interface IRequestHandler
    {
        Response Process(IOwinRequest context);
    }
}