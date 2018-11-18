using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace PatientsService.Modules
{
    public class PatientsModule : NancyModule
    {
        public PatientsModule() : base("/api")
        {
            Get("/patient/{nhsNumber:int}", async parameters => 
            {
                var nhsNumber = parameters.nhsNumber;
            });
        }
    }
}
