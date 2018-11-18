using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Cookies;
using RestSharp;
using Login.Models.Responses;
using KwikMedical.Shared;
using Newtonsoft.Json;

namespace ApiGateway.Modules
{
    public class GatewayModule : NancyModule
    {
        public GatewayModule(ClientFactory clientFactory)
        {
            Get("", _ => 
            {
                return View["Views/Home.sshtml", Authenticate(clientFactory.GetClient(Services.LOGIN))];
            });

            Get("/register", _ => 
            {
                return View["Views/Register.sshtml"];
            });

            Post("/register", async _ => 
            {
                var request = new RestRequest("/register", Method.POST);
                request.AddQueryParameter("user", Request.Form["user"].Value);
                request.AddQueryParameter("pass", Request.Form["pass"].Value);
                var response = clientFactory.GetClient(Services.LOGIN).Execute<UserLoginResponse>(request);
                var model = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content);

                if (model is null)
                    return HttpStatusCode.InternalServerError;

                if (model.Success)
                {
                    Request.Cookies.Add("token", model.User.Token);
                }
                else
                {
                    return View["Views/Register.sshtml", model];
                }

                return HttpStatusCode.OK;
            });

            Get("/login", _ =>
            {
                return View["Views/Login.sshtml"];
            });

            Post("/login", async _ =>
            {
                var request = new RestRequest("/login", Method.POST);
                request.AddQueryParameter("user", Request.Form["user"].Value);
                request.AddQueryParameter("pass", Request.Form["pass"].Value);

                var response = clientFactory.GetClient(Services.LOGIN).Execute<UserLoginResponse>(request);
                var model = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content);

                if (model is null)
                    return HttpStatusCode.InternalServerError;
                
                if (model.Success)
                {
                    return await Response.AsRedirect("/").WithCookie(new NancyCookie("token", model.User.Token));
                }
                else
                {
                    return View["Views/Login.sshtml", model];
                }
            });
        }
        
        private AuthResponse Authenticate(RestClient client)
        {
            if(!Request.Cookies.ContainsKey("token"))
                return new AuthResponse();


            var request = new RestRequest("/auth", Method.POST);
            request.AddQueryParameter("token", Request.Cookies["token"]);

            var response = client.Execute(request, Method.POST);

            return JsonConvert.DeserializeObject<AuthResponse>(response.Content);
        }       
    }
}
