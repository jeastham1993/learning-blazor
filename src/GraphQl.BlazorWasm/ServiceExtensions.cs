namespace GraphQl.BlazorWasm;

using System.Text;
using System.Text.Json;

using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

public static class ServiceExtensions
{
    public static IServiceCollection AddGraphQlWebSockets(this IServiceCollection services, IConfiguration configuration)
    {
        var client = new GraphQLHttpClient(options =>
        {
            var auth = Convert.ToBase64String(Encoding.Default.GetBytes(JsonSerializer.Serialize(AppSyncHelper.AppSyncAuthHeader)));
            
            options.EndPoint = new Uri($"https://{configuration["apiEndpoint"]}/graphql/");
            options.WebSocketEndPoint = new Uri($"wss://{configuration["webSocketsEndpoint"]}/graphql?header={auth}&payload=e30=");
        }, new SystemTextJsonSerializer());

        services.AddSingleton(client);

        return services;
    }
    
    public static IServiceCollection AddGraphQlDataPointClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataPointClient()
            .ConfigureHttpClient(
                client =>
                {
                    client.BaseAddress = new Uri(
                        $"https://{configuration["apiEndpoint"]}/graphql/");
                    client.DefaultRequestHeaders.Add(
                        "x-api-key",
                        configuration["apiKey"]);
                })
            .ConfigureWebSocketClient(
                client =>
                {
                    var auth = Convert.ToBase64String(Encoding.Default.GetBytes(JsonSerializer.Serialize(AppSyncHelper.AppSyncAuthHeader)));
                    var fullUri =
                        $"wss://{configuration["webSocketsEndpoint"]}/graphql?header={auth}&payload=e30=";
            
                    client.Uri = new Uri(fullUri);
                    client.OpenAsync();
                });

        return services;
    }
}