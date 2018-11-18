using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Cookies;
using Nancy.Responses;
using RestSharp;
using Login.Models.Responses;
using KwikMedical.Shared;
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

                return null; // continue to route
            });

            Get("/", _ => 
            {
                return View["Views/Dashboard.sshtml"];
            });
        }
    }
}
