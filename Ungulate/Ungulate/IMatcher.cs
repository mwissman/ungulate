using Microsoft.Owin;

namespace Ungulate
{
    public interface IMatcher
    {
        bool DoesMatch(IOwinRequest context);
    }
}