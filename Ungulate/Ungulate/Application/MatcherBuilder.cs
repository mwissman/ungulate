using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Owin;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public class MatcherBuilder : IMatcherBuilder
    {
        public IMatcher CreateFrom(Mapping mapping)
        {
            var matchers = new List<IMatcher>
            {
                new MethodEqualityMatcher(mapping.Request),
                new UrlMatternMatcher(mapping.Request)
            };

            return new AllMatch(matchers);
        }
    }

    public class MethodEqualityMatcher : IMatcher
    {
        private readonly Request _request;

        public MethodEqualityMatcher(Request request)
        {
            _request = request;
        }

        public bool DoesMatch(IOwinRequest context)
        {
            return context.Method == _request.Method;
        }
    }

    public class UrlMatternMatcher : IMatcher
    {
         private readonly Request _request;

         public UrlMatternMatcher(Request request)
        {
            _request = request;
        }

        public bool DoesMatch(IOwinRequest context)
        {
            return Regex.IsMatch(context.Uri.PathAndQuery, _request.UrlPathPattern);
        }
    }

    public class AllMatch : IMatcher
    {
        private readonly IList<IMatcher> _matchers;

        public AllMatch(IList<IMatcher> matchers)
        {
            _matchers = matchers;
        }

        public bool DoesMatch(IOwinRequest context)
        {
            return _matchers.All(m => m.DoesMatch(context));
        }
    }
}