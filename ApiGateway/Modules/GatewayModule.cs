using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using RestSharp.Authenticators.OAuth;

namespace ApiGateway.Modules
{
    public class GatewayModule : NancyModule
    {
        public GatewayModule(ClientFactory clientFactory)
        {

        }
        /*
        private async Task<bool> IsValidToken(string token)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "token", token }
            };
            var content = new FormUrlEncodedContent(parameters);

            var result = await httpClient.PostAsync("/auth", content);
            var response = JsonConvert.DeserializeObject<AuthResponse>(await result.Content.ReadAsStringAsync());

            return response.Success;
        }
        */
    }
}
