using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Configuration;
using Nancy.Bootstrapper;
using Nancy.Owin;
using Nancy.TinyIoc;
using HospitalsService.Infrastructure;

namespace HospitalsService
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IHospitalStore, HospitalStore>().AsSingleton();
        }
    }
}
