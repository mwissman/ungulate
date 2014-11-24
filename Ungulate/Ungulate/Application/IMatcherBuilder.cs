using Ungulate.Domain;

namespace Ungulate.Application
{
    public interface IMatcherBuilder
    {
        IMatcher CreateFrom(Mapping mapping);
    }
}