# Learning Blazor

Learning in public for GraphQL and Blazor.

## Run Locally

Create appsettings.json file under wwwroot with the below structure:

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Debug",
            "System": "Information",
            "Microsoft": "Information"
        }
    },
    "apiEndpoint": "<APP_SYNC_ID>.appsync-api.us-east-1.amazonaws.com",
    "webSocketsEndpoint": "<APP_SYNC_ID>.appsync-realtime-api.us-east-1.amazonaws.com",
    "apiKey": "<APP_SYNC_API_KEY>"
}
```