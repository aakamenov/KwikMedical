using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using KwikMedical.Shared;

namespace ApiGateway
{
    public class ClientFactory
    {
        private static Dictionary<string, RestClient> clients;

        public ClientFactory()
        {
            clients = new Dictionary<string, RestClient>()
            {
                { Services.LOGIN, new RestClient(Services.LOGIN + "/api") },
                { Services.PATIENTS, new RestClient(Services.PATIENTS + "/api") }
            };
        }

        public RestClient GetClient(string baseUrl)
        {
            return clients[baseUrl];
        }
    }
}
