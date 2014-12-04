using System.Collections.Generic;

namespace Ungulate.Domain
{
    public class EmptyMapping : Mapping
    {
        public EmptyMapping()
        {
            Response=new EmptyResponse();
        }
    }

    public class EmptyResponse : Response
    {
        public EmptyResponse()
        {
            Body = "404 not found";
            Status = 404;
            Headers=new Dictionary<string, string>();
        }

       
    }
}