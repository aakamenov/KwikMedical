using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using HospitalsService.Infrastructure;
using HospitalsService.Models;
using Nancy.Responses;

namespace HospitalsService.Modules
{
    public class HospitalsModule : NancyModule
    {
        public HospitalsModule(IHospitalStore hospitalStore) : base("/api")
        {
            Get("/hospital/{location}", parameters => 
            {
                Console.WriteLine($"Fetching nearest hospital.");
                var location = parameters.location.Value;
                var hospital = hospitalStore.GetClosestHospital(location);

                return new JsonResponse<HospitalDetails>(hospital, new DefaultJsonSerializer(Context.Environment), Context.Environment);
            });
        }
    }
}
