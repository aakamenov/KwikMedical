using Nancy.Owin;
using Owin;

namespace EmergencyRequestDispatchService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
