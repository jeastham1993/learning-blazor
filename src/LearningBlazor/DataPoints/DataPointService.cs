namespace LearningBlazor.DataPoints;

using System.Reactive.Linq;
using System.Text.Json;

using GraphQL;

using GraphQL.Client.Http;

using StrawberryShake;

public class DataPointService : IDataPointService
{
    private readonly GraphQLHttpClient _graphQlClient;
    private readonly DataPointClient _dataPointClient;

    public DataPointService(
        GraphQLHttpClient graphQlClient,
        DataPointClient dataPointClient)
    {
        this._graphQlClient = graphQlClient;
        this._dataPointClient = dataPointClient;
    }

    /// <inheritdoc/>
    public IObservable<DataPointItem> SubscribeToOnCreateDataPoint()
    {
        return this._graphQlClient.CreateSubscriptionStream<OnDataPointAddedSubscriptionResult>(
                new GraphQLRequest
                {
                    ["data"] = JsonSerializer.Serialize(
                        new GraphQLRequest
                        {
                            Query = @"
                  subscription OnCreateDataPoint {
                  onCreateDataPoint {
                    createdAt
                    name
                    value
                  }
                }
        "
                        }),
                    ["extensions"] = new
                    {
                        authorization = AppSyncHelper.AppSyncAuthHeader
                    }
                })
            .Select(p => p.Data.OnCreateDataPoint);
    }

    /// <inheritdoc/>
    public async Task AddNewDataPoint(string name, int value)
    {
        var result = await this._dataPointClient.CreateDataPoint.ExecuteAsync(
            new CreateDataPointInput
            {
                Name = name,
                Value = value,
                CreatedAt = DateTime.UtcNow.ToString("o")
            });

        if (result.Errors.Any())
        {
            throw new Exception("Failure adding data point");
        }
    }

    /// <inheritdoc/>
    public IObservable<IOperationResult<IListDataPointsResult>>? ListDataPoints()
    {
        return this._dataPointClient
            .ListDataPoints
            .Watch(ExecutionStrategy.CacheFirst)
            .Where(p => !p.Errors.Any());
    }
}