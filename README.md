# AzureServiceBus.Monitor
A windows service to detect network issues between your on-prem applications and azure servicebus. This service creates a dummy topic on a given servicebus namepspace and publishes messages every 10 minutes.
It raises an email alert when not able to publish.

## Usage
Just add these three in your app.config
1. ServiceBus connection string
2. An sql server connection string for Hangfire db
3. SMTP server name for email alerts
