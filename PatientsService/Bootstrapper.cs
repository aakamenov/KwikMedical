using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using PatientsService.Infrastructure;
using KwikMedical.Shared;

namespace PatientsService
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IPatientsStore, PatientsStore>().AsSingleton();
        }
    }
}
