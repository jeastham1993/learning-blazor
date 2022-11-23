namespace LearningBlazor.Pages;

using LearningBlazor.DataPoints;

using Microsoft.AspNetCore.Components;

public partial class IndexComponent : ComponentBase, IDisposable
{
    private IDisposable storeSession;
    private IDisposable subscription;

    [Inject]
    public IDataPointService DataPointService { get; set; }
    
    public bool Loading = true;
    public List<DataPointItem> DataPointNames = new List<DataPointItem>();

    protected override async Task OnInitializedAsync()
    {
        storeSession = DataPointService.ListDataPoints()?
            .Subscribe(result =>
            {
                var results = result.Data.ListDataPoints.Items.OrderByDescending(p => p.CreatedAt).ToList();

                this.DataPointNames = results.Select(p => new DataPointItem(p.Name, DateTime.Parse(p.CreatedAt), p.Value)).ToList();
                
                StateHasChanged();
            });

        subscription = DataPointService
            .SubscribeToOnCreateDataPoint()
            .Subscribe(create =>
            {
                DataPointNames.Add(new DataPointItem(create.Name, create.CreatedAt, create.Value));
                DataPointNames = DataPointNames.OrderByDescending(p => p.CreatedAt).ToList();
                
                StateHasChanged();
            });

        Loading = false;
    }

    public void Dispose()
    {
        storeSession?.Dispose();
        subscription?.Dispose();
    }
}