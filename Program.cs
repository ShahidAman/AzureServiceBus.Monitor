using System;
using Topshelf;

namespace AzureServiceBus.Monitor
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = HostFactory.New(x =>
            {
                x.Service<MonitoringService>(svc =>
                {
                    svc.ConstructUsing(mon => new MonitoringService());
                    svc.WhenStarted(sbm => sbm.Start());
                    svc.WhenStopped(sbm => sbm.Stop());
                }).RunAsNetworkService();

                x.EnableShutdown();
                
                x.SetDescription("Azure Service Bus Monitoring App");
                x.SetDisplayName("Azure ServiceBus Monitor");
                x.SetServiceName("AzureServiceBus.Monitor");

            });

            var rc = host.Run();
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
