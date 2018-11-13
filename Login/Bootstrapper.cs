using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Infrastructure;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Login
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IUserStore, UserStore>().AsSingleton();
        }
    }
}
