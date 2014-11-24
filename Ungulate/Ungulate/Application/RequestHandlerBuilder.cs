using System.Collections.Generic;
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
}