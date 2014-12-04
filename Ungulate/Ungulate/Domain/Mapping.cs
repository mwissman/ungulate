using System.Collections.Generic;

namespace Ungulate.Domain
{
    public class Mapping
    {
        public Request Request { get; set; }
        public Response Response { get; set; }
    }

    public class Request
    {
        public string UrlPattern { get; set; }
        public string Method { get; set; }
    }

    public class Response
    {
        public int Status { get; set; }
        public string BodyFileName { get; set; }
        public string Body { get; set; }
//        public List<Header> Headers { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }

    public class Header
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}