using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Buffers.Text;
using System.IO;
using Nancy;
using Nancy.Responses;
using Nancy.Json.Simple;
using PatientsService.Infrastructure;
using PatientsService.Models;
using PatientsService.Models.Responses;

namespace PatientsService.Modules
{
    public class PatientsModule : NancyModule
    {
        public PatientsModule(IPatientsStore patientsStore) : base("/api")
        {
            Post("/patient", async _ => 
            {               
                var response = new AddPatientResponse();

                var patient = await DeserializePatientRecord();
                response.Success = await patientsStore.AddPatient(patient);

                return new JsonResponse<AddPatientResponse>(response, new DefaultJsonSerializer(Context.Environment), Context.Environment);
            });

            Put("/patient", async _ => 
            {
                var response = new AddPatientResponse();

                var patient = await DeserializePatientRecord();
                response.Success = await patientsStore.UpdatePatient(patient);

                return new JsonResponse<AddPatientResponse>(response, new DefaultJsonSerializer(Context.Environment), Context.Environment);
            });

            Get("/patient/{nhsNumber}", async parameters => 
            {
                var nhsNumber = parameters.nhsNumber.Value;

                var patient = new GetPatientResponse();
                patient.Patient = await patientsStore.GetPatient(nhsNumber);

                return new JsonResponse<GetPatientResponse>(patient, new DefaultJsonSerializer(Context.Environment), Context.Environment);
            });
        }

        private async Task<PatientRecord> DeserializePatientRecord()
        {
            string json = null;

            using (var reader = new StreamReader(Request.Body))
            {
                json = await reader.ReadToEndAsync();
            }

            return SimpleJson.DeserializeObject<PatientRecord>(json);
        }
    }
}
