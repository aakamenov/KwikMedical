using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Owin.Hosting;
using KwikMedical.Shared;
using Nancy.Hosting.Self;

namespace Login
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

            using (var host = new NancyHost(hostConfig, new Uri(Services.LOGIN)))
            {
                host.Start();

                Console.WriteLine($"Running login service on {Services.LOGIN}");
                Console.ReadLine();

                host.Stop();
            }
        }
    }
}
