namespace GraphQl.BlazorWasm.Components;

using GraphQl.BlazorWasm.DataPoints;

using Microsoft.AspNetCore.Components;

public partial class AddDataPointComponent : LayoutComponentBase
{
    public string Name { get; set; } = "";

    public int Value { get; set; } = 0;
    
    [Inject]
    public IDataPointService DataPointService { get; set; }

    public async Task OnSaveDataPoint()
    {
        await DataPointService.AddNewDataPoint(Name, Value);

        Name = "";
        Value = 0;
        
        this.StateHasChanged();
    }
}