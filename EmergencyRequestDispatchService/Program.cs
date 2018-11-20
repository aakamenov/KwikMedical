using System;
using KwikMedical.Shared;
using Nancy.Hosting.Self;

namespace EmergencyRequestDispatchService
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

            using (var host = new NancyHost(hostConfig, new Uri(Services.DISPATCH)))
            {
                host.Start();

                Console.WriteLine($"Running emergency request dispatch service on {Services.DISPATCH}");
                Console.ReadLine();
            }
        }
    }
}
