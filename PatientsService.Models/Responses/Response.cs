using System;
using System.Collections.Generic;
using System.Text;

namespace PatientsService.Models.Responses
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
