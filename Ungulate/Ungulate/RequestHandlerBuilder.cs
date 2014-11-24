using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate
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

    public class MappingHandler : AbstractHandler
    {
        private readonly Mapping _mapping;
        private readonly IMatcherBuilder _matcherBuilder;

        public MappingHandler(Mapping mapping, IMatcherBuilder matcherBuilder)
        {
            _mapping = mapping;
            _matcherBuilder = matcherBuilder;
        }

        public override Response Process(IOwinRequest context)
        {
            var matcher = _matcherBuilder.CreateFrom(_mapping);
            if (matcher.DoesMatch(context))
            {
                return _mapping.Response;
            }
            else if (_successor != null)
            {
                _successor.Process(context);
            }
            return new EmptyResponse();
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