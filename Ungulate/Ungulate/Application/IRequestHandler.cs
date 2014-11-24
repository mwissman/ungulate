using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public interface IRequestHandler
    {
        Mapping Process(IOwinRequest context);
    }
}