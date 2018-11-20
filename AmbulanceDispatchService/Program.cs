using System;
using KwikMedical.Shared;
using Nancy.Hosting.Self;

namespace AmbulanceDispatchService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostConfig = new HostConfiguration
            {
                UrlReservations = new UrlReservations
                {
                    CreateAutomatically = true
                },
            };

            using (var host = new NancyHost(hostConfig, new Uri(Services.AMBULANCE)))
            {
                host.Start();

                Console.WriteLine($"Running ambulance dispatch service on {Services.AMBULANCE}");
                Console.ReadLine();
            }
        }
    }
}
