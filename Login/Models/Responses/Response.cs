using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Models.Responses
{
    public abstract class Response
    {
        public IList<string> Errors { get; set; }

        public Response()
        {
            Errors = new List<string>();
        }
    }
}
