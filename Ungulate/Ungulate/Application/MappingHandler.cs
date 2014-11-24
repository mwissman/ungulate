using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public class MappingHandler : AbstractHandler
    {
        private readonly Mapping _mapping;
        private readonly IMatcherBuilder _matcherBuilder;

        public MappingHandler(Mapping mapping, IMatcherBuilder matcherBuilder)
        {
            _mapping = mapping;
            _matcherBuilder = matcherBuilder;
        }

        public override Mapping Process(IOwinRequest context)
        {
            var matcher = _matcherBuilder.CreateFrom(_mapping);
            if (matcher.DoesMatch(context))
            {
                return _mapping;
            }
            if (_successor != null)
            {
                _successor.Process(context);
            }
            return new EmptyMapping();
        }
    }
}