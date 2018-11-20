using Nancy.Owin;
using Owin;

namespace AmbulanceDispatchService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
