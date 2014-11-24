using System.Collections.Generic;
using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public class RequestHandlerBuilder : IRequestHandlerBuilder
    {
        private readonly IMatcherBuilder _matcherbuilder;

        public RequestHandlerBuilder(IMatcherBuilder matcherbuilder)
        {
            _matcherbuilder = matcherbuilder;
        }

        public IRequestHandler Create(IEnumerable<Mapping> mappings)
        {
            AbstractHandler firstHandler = null;
            AbstractHandler previousHandler = null;

            foreach (var mapping in mappings)
            {
                var currentHandler = new MappingHandler(mapping, _matcherbuilder);

                if (firstHandler == null)
                {
                    firstHandler = currentHandler;
                }
                else if (previousHandler==null)
                {
                    previousHandler = currentHandler;
                }
                else
                {
                    previousHandler.SetSuccessor(currentHandler);
                }
            }
            return firstHandler;
        }
       
    }

    public interface IMatcherBuilder
    {
        IMatcher CreateFrom(Mapping mapping);
    }

    public class EmptyResponse : Response
    {
    }

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