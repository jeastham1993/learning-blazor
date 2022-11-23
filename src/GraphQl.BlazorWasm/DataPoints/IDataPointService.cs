namespace GraphQl.BlazorWasm.DataPoints;

using GraphQL;

using GraphQl.BlazorWasm.Client.DataPoint;

using StrawberryShake;

public interface IDataPointService
{
    IObservable<DataPointItem> SubscribeToOnCreateDataPoint();
    
    Task AddNewDataPoint(string name, int value);
    
    IObservable<IOperationResult<IListDataPointsResult>>? ListDataPoints();
}