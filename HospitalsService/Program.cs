using System;
using KwikMedical.Shared;
using Nancy.Hosting.Self;

namespace HospitalsService
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

            using (var host = new NancyHost(hostConfig, new Uri(Services.HOSPITALS)))
            {
                host.Start();

                Console.WriteLine($"Running hospitals service on {Services.HOSPITALS}");
                Console.ReadLine();
            }
        }
    }
}
