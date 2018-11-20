using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Nancy;
using Nancy.Json.Simple;
using RestSharp;
using PatientsService.Models;
using KwikMedical.Shared;
using HospitalsService.Models;
using Newtonsoft.Json;

namespace EmergencyRequestDispatchService.Modules
{
    public class DispatchModule : NancyModule
    {
        public DispatchModule(ClientFactory clientFactory) : base("/api")
        {
            Post("/dispatch", async _ => 
            {
                var json = await ReadString(Request.Body);
                var patient = SimpleJson.SimpleJson.DeserializeObject<PatientRecord>(json);
                Console.WriteLine($"Sending emergency request for patient {patient.Name} {patient.Surname}");

                var ambulanceRequest = new RestRequest("/dispatch/ambulance", Method.POST);
                ambulanceRequest.AddJsonBody(patient);
                var ambulanceResponse = clientFactory.GetClient(Services.AMBULANCE).Execute(ambulanceRequest, Method.POST);
                Console.WriteLine("Sent patient details to ambulance");

                var hospitalRequest = new RestRequest("/hospital/12312312");
                var hospitalResponse = clientFactory.GetClient(Services.HOSPITALS).Execute(hospitalRequest);
                var hospital = JsonConvert.DeserializeObject<HospitalDetails>(hospitalResponse.Content);
                Console.WriteLine($"Sent details to hospital {hospital.Name}\n");

                return HttpStatusCode.OK;
            });

            Post("/receive", async _ => 
            {
                var json = await ReadString(Request.Body);
                var patient = SimpleJson.SimpleJson.DeserializeObject<PatientRecord>(json);
                Console.WriteLine($"Receiving and updating detials for patient {patient.Name} {patient.Surname}");

                var request = new RestRequest("/patient", Method.PUT);
                request.AddJsonBody(patient);
                var response = clientFactory.GetClient(Services.PATIENTS).Execute(request, Method.PUT);

                return response.StatusCode;
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
