using System.Collections.Generic;
using Ungulate.Domain;

namespace Ungulate.Application
{
    public interface IRequestHandlerBuilder
    {
        IRequestHandler Create(IEnumerable<Mapping> mappings);
    }
}