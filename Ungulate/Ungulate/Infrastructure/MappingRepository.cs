using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ungulate.Domain;
using Newtonsoft.Json;

namespace Ungulate.Infrastructure
{
    public class MappingRepository : IMappingRepository
    {
        private readonly ISettings _settings;

        public MappingRepository(ISettings settings)
        {
            _settings = settings;
        }

        public IList<Mapping> All()
        {
            var files = Directory.GetFiles(_settings.MappingPath, "*.json")
                .Select(File.ReadAllText)
                .Select(JsonConvert.DeserializeObject<Mapping>)
                .ToList();
            return files;
        }
    }
}