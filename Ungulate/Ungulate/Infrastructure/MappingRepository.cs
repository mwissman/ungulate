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
            string[] strings = Directory.GetFiles(_settings.MappingPath, "*.json");
            var files = strings
                .Select(s => ReadAllText(s))
                .Select(s=> DeserializeObject(s))
                .ToList();
            return files;
        }

        private static Mapping DeserializeObject(string s)
        {
            return JsonConvert.DeserializeObject<Mapping>(s);
        }

        private static string ReadAllText(string s)
        {
            return File.ReadAllText(s);
        }
    }
}