using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using KwikMedical.Shared;
using Nancy.Hosting.Self;

namespace PatientsService
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

            using (var host = new NancyHost(hostConfig, new Uri(Services.PATIENTS)))
            {
                host.Start();

                Console.WriteLine($"Running patients service on {Services.PATIENTS}");
                Console.ReadLine();
            }
        }
    }
}
