using System.Collections.Generic;
using Ungulate.Domain;

namespace Ungulate
{
    public interface IRequestHandlerBuilder
    {
        IRequestHandler Create(IEnumerable<Mapping> mappings);
    }
}