using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Infrastructure;
using Login.Models.Responses;
using Nancy;
using Nancy.Responses;

namespace Login.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule(IUserStore userStore) : base("/api")
        {
            Post("/register", async parameters => 
            {
                var user = Request.Query["user"].Value;
                var pass = Request.Query["pass"].Value;
                var response = new UserLoginResponse();

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    response.Errors.Add("Please enter username and password");
                    return new JsonResponse<UserLoginResponse>(response, new DefaultJsonSerializer(Context.Environment), Context.Environment);
                }

                if (await userStore.AddUser(user, pass))
                {
                    response.User = userStore.GetUser(user, pass);
                    response.Success = true;
                }
                else
                    response.Errors.Add($"A user with username \"{user}\" already exists!");

                return new JsonResponse<UserLoginResponse>(response, new DefaultJsonSerializer(Context.Environment), Context.Environment);
            });

            Post("/auth", async parameters => 
            {
                var token = Request.Query["token"].Value;

                if (string.IsNullOrEmpty(token))
                    return HttpStatusCode.BadRequest;

                var response = new AuthResponse();

                try
                {
                    response.Success = await userStore.AuthenticateUser(token);                    
                }
                catch(Exception e)
                {
                    response.Errors.Add(e.Message);
                }

                return new JsonResponse<AuthResponse>(response, new DefaultJsonSerializer(Context.Environment), Context.Environment);
            });

            Post("/login", async parameters => 
            {
                var user = Request.Query["user"].Value;
                var pass = Request.Query["pass"].Value;

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                    return HttpStatusCode.BadRequest;

                var response = new UserLoginResponse();
                
                try
                {
                    response.User = await userStore.GetUser(user, pass);
                    response.Success = true;
                }
                catch (Exception e)
                {
                    response.Errors.Add(e.Message);
                }

                return new JsonResponse<UserLoginResponse>(response, new DefaultJsonSerializer(Context.Environment), Context.Environment);
            });
        }
    }
}
