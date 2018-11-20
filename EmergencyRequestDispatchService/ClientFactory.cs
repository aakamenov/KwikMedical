using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using KwikMedical.Shared;

namespace EmergencyRequestDispatchService
{
    public class ClientFactory
    {
        private static Dictionary<string, RestClient> clients;

        public ClientFactory()
        {
            clients = new Dictionary<string, RestClient>()
            {
                { Services.PATIENTS, new RestClient(Services.PATIENTS + "/api") },
                { Services.HOSPITALS, new RestClient(Services.HOSPITALS + "/api") },
                { Services.AMBULANCE, new RestClient(Services.AMBULANCE + "/api") }
            };
        }

        public RestClient GetClient(string baseUrl)
        {
            return clients[baseUrl];
        }
    }
}
