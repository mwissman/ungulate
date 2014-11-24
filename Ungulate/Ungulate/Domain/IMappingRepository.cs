using System.Collections.Generic;

namespace Ungulate.Domain
{
    public interface IMappingRepository
    {
        IList<Mapping> All();
    }
}