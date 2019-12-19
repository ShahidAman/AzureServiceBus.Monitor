# AzureServiceBus.Monitor
A windows service to detect network issues between your on-prem applications and azure servicebus. This service creates a dummy topic on a given servicebus namepspace and publishes messages every 10 minutes.
It raises an email alert when not able to publish.
