using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin.Hosting;

namespace AzureServiceBus.Monitor
{
    public class MonitoringService
    {
        private BackgroundJobServer backgroundJobServer;

        public void Start()
        {
            BootstrapHangfire();
            RecurringJob.AddOrUpdate(() => PublishMessage(), Cron.MinuteInterval(10));

            backgroundJobServer = new BackgroundJobServer();
        }

        public void Stop()
        {
            backgroundJobServer.Dispose();
        }

        [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public async Task PublishMessage()
        {
            var agent = new TopicPublisherAgent();
            await agent.PublishToTopicAsync();
        }


        private void BootstrapHangfire()
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireDb");
            var options = new StartOptions();

            options.Urls.Add("http://localhost:9095");

            options.Urls.Add("http://127.0.0.1:9095");

            options.Urls.Add($"http://{Environment.MachineName}:9095");

            WebApp.Start<Startup>(options);
        }

    }
}
