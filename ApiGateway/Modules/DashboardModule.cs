using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Cookies;
using Nancy.Responses;
using RestSharp;
using Login.Models;
using Login.Models.Responses;
using KwikMedical.Shared;
using ApiGateway.ViewModels;
using PatientsService.Models;
using PatientsService.Models.Responses;
using Newtonsoft.Json;

namespace ApiGateway.Modules
{
    public class DashboardModule : NancyModule
    {
        public DashboardModule(ClientFactory clientFactory) : base("/dashboard")
        {
            Before.AddItemToStartOfPipeline(ctx => 
            {
                if (!Request.Cookies.ContainsKey("token"))
                    return new RedirectResponse("/login");

                var request = new RestRequest("/auth", Method.POST);
                request.AddQueryParameter("token", Request.Cookies["token"]);

                var response = clientFactory.GetClient(Services.LOGIN).Execute(request, Method.POST);
                var result = JsonConvert.DeserializeObject<AuthResponse>(response.Content);
                
                if (!result.Success)
                    return new RedirectResponse("/login");

                Context.Items["user"] = result.User;
                return null; // continue to route
            });

            Get("/", _ =>
            {
                return View["Views/Dashboard.sshtml", new DashboardViewModel(Context.Items["user"] as AuthenticatedUser)];
            });

            Get("/emergency", _ => 
            {             
                return View["Views/SubmitEmergency.sshtml", new DashboardViewModel(Context.Items["user"] as AuthenticatedUser)];
            });

            Post("/emergency", _ => 
            {
                return HttpStatusCode.InternalServerError;
            });

            Get("/emergency/{nhsNumber}", parameters =>
            {
                var nhsNumber = parameters.nhsNumber.Value;
                var vm = new SubmitEmergencyViewModel(Context.Items["user"] as AuthenticatedUser, GetPatient(nhsNumber));

                return View["Views/SubmitEmergencyExisting.sshtml", vm];
            });

            Post("/emergency/{nhsNumber}", _ =>
            {
                return HttpStatusCode.InternalServerError;
            });

            Get("/search/nhs", _ => 
            {
                return View["Views/SearchByNhs.sshtml", new DashboardViewModel(Context.Items["user"] as AuthenticatedUser)];
            });

            Get("/search/results", _ => 
            {
                var nhsNumber = Request.Query["nhsNumber"].Value;
                var vm = new PatientSearchResultsViewModel(Context.Items["user"] as AuthenticatedUser);

                if(!string.IsNullOrEmpty(nhsNumber))
                {
                    vm.Results = new PatientRecord[] { GetPatient(nhsNumber) };
                }

                return View["Views/PatientSearchResults.sshtml", vm];
            });

            PatientRecord GetPatient(string nhsNumber)
            {
                var request = new RestRequest($"/patient/{nhsNumber}");
                var response = clientFactory.GetClient(Services.PATIENTS).Execute(request, Method.GET);

                return JsonConvert.DeserializeObject<GetPatientResponse>(response.Content).Patient;
            }
        }
    }
}
