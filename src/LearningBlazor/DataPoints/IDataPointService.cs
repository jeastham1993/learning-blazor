namespace LearningBlazor.DataPoints;

using StrawberryShake;

public interface IDataPointService
{
    IObservable<DataPointItem> SubscribeToOnCreateDataPoint();
    
    Task AddNewDataPoint(string name, int value);
    
    IObservable<IOperationResult<IListDataPointsResult>>? ListDataPoints();
}