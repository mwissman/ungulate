using Microsoft.Owin;

namespace Ungulate.Application
{
    public interface IMatcher
    {
        bool DoesMatch(IOwinRequest context);
    }
}