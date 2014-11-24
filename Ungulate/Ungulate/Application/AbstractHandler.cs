using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public abstract class AbstractHandler : IRequestHandler
    {
        protected IRequestHandler _successor;

        public void SetSuccessor(IRequestHandler successor)
        {
            _successor = successor;
        }

        public abstract Response Process(IOwinRequest context);
    
    }
}