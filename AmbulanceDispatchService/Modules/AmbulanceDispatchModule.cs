using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Nancy;
using Nancy.Json.Simple;
using PatientsService.Models;

namespace AmbulanceDispatchService.Modules
{
    public class AmbulanceDispatchModule : NancyModule
    {
        public AmbulanceDispatchModule() : base("/api")
        {
            Post("/dispatch/ambulance", async _ => 
            {
                var json = await ReadString(Request.Body);
                var patient = SimpleJson.DeserializeObject<PatientRecord>(json);
                Console.WriteLine($"Sending details to ambulance for patient {patient.Name} {patient.Surname}");

                return HttpStatusCode.OK;
            });
        }

        private async Task<string> ReadString(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
