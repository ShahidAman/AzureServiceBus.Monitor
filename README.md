# AzureServiceBus.Monitor
A windows service to detect network issues between your on-prem applications and azure servicebus. This service creates a dummy topic on a given servicebus namepspace and publishes messages every 10 minutes.
It raises an email alert when not able to publish.

Hangfire dashboard is available @ http://localhost:9095. A list of executed and scheduled jobs can be seen, useful for looking up errors.

## Usage
Just add these three in app.config, hit F5 to run
1. ServiceBus connection string
2. An sql server connection string for Hangfire db
3. SMTP server name for email alerts
