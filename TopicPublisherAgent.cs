using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace AzureServiceBus.Monitor
{
    public  class TopicPublisherAgent
    {
        private readonly ITopicClient topicClient;

        private readonly string serviceBusConnectionString = ConfigurationManager.ConnectionStrings["ServiceBus.ConnectionString"]?.ConnectionString;

        private readonly string topicName = ConfigurationManager.AppSettings["ServiceBus.Topic"].ToString();

        private readonly string fromAddress = ConfigurationManager.AppSettings["EmailAlert.From"].ToString();
        private readonly string toAddresses = ConfigurationManager.AppSettings["EmailAlert.To"].ToString();


        public TopicPublisherAgent()
        {
            if (string.IsNullOrEmpty(serviceBusConnectionString))
            {
                throw new ConfigurationErrorsException(nameof(serviceBusConnectionString));
            }

            topicClient = new TopicClient(serviceBusConnectionString, topicName);
        }

        public async Task PublishToTopicAsync()
        {
            var messageBody = "foo";

            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            try
            {
                await topicClient.SendAsync(message);
                await topicClient.CloseAsync();
            }
            catch(Exception ex)
            {
                RaiseEmailAlert();
                await Task.FromException(ex);
            }
        }

        private void RaiseEmailAlert()
        {
            var smtpClient = new SmtpClient();
            var message = new MailMessage(fromAddress, toAddresses)
            {
                Subject = "Service Bus Monitor Alert",
                Body = "Could not publish message to service bus topic. Check application logs."
            };

            smtpClient.Send(message);
        }
    }
}