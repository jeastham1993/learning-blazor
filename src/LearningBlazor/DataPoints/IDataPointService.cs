namespace LearningBlazor.DataPoints;

using GraphQL;

using LearningBlazor.Client.DataPoint;

using StrawberryShake;

public interface IDataPointService
{
    IObservable<DataPointItem> SubscribeToOnCreateDataPoint();
    
    Task AddNewDataPoint(string name, int value);
    
    IObservable<IOperationResult<IListDataPointsResult>>? ListDataPoints();
}