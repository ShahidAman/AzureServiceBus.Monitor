using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AzureServiceBus.Monitor.Startup))]

namespace AzureServiceBus.Monitor
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard();
        }
    }
}
