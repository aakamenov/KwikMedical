using Nancy.Owin;
using Owin;

namespace HospitalsService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
