using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LearningBlazor;
using LearningBlazor.DataPoints;

using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

AppSyncHelper.InitializeSettings(builder.Configuration);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddGraphQlDataPointClient(builder.Configuration);
builder.Services.AddGraphQlWebSockets(builder.Configuration);
builder.Services.AddSingleton<IDataPointService, DataPointService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
