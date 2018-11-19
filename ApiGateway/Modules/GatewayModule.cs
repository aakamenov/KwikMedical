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
            Get("", async _ => 
            {
                if (Request.Cookies.ContainsKey("token"))
                    return await Response.AsRedirect("/dashboard");

                return View["Views/Home.sshtml"];
            });

            Get("/register", _ => 
            {
                return View["Views/Register.sshtml", new UserLoginResponse()];
            });

            Post("/register", async _ => 
            {
                var request = new RestRequest("/register", Method.POST);
                request.AddQueryParameter("user", Request.Form["user"].Value);
                request.AddQueryParameter("pass", Request.Form["pass"].Value);

                var response = clientFactory.GetClient(Services.LOGIN).Execute<UserLoginResponse>(request);
                var model = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content);

                if (model is null || response.StatusCode != System.Net.HttpStatusCode.OK)
                    return HttpStatusCode.InternalServerError;

                if (model.Success)
                {
                    return await Response.AsRedirect("/dashboard").WithCookie(new NancyCookie("token", model.User.Token));
                }
                else
                {
                    return View["Views/Register.sshtml", model];
                }
            });

            Get("/login", _ =>
            {
                return View["Views/Login.sshtml", new UserLoginResponse()];
            });

            Post("/login", async _ =>
            {
                var request = new RestRequest("/login", Method.POST);
                request.AddQueryParameter("user", Request.Form["user"].Value);
                request.AddQueryParameter("pass", Request.Form["pass"].Value);

                var response = clientFactory.GetClient(Services.LOGIN).Execute<UserLoginResponse>(request);
                var model = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content);

                if (model is null || response.StatusCode != System.Net.HttpStatusCode.OK)
                    return HttpStatusCode.InternalServerError;
                
                if (model.Success)
                {
                    return await Response.AsRedirect("/dashboard").WithCookie(new NancyCookie("token", model.User.Token));
                }
                else
                {
                    return View["Views/Login.sshtml", model];
                }
            });

            Get("/logout", async _ => 
            {
                if (!Request.Cookies.ContainsKey("token"))
                    return await Response.AsRedirect("/");

                var token = Request.Cookies["token"];
                var request = new RestRequest("/logout", Method.POST);
                request.AddQueryParameter("token", token);

                var response = clientFactory.GetClient(Services.LOGIN).Execute<UserLoginResponse>(request);
                Request.Cookies.Remove("token");

                return await Response.AsRedirect("/");
            });
        }          
    }
}
