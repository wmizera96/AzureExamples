using AzureExamples.Data;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<ExampleDatabaseContext>();
builder.Services.AddFeatureManagement();

builder.Configuration.AddAzureAppConfiguration(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("AppConfig");
    opt.Connect(connectionString);
    opt.UseFeatureFlags();
});

var app = builder.Build();

app.UseRouting();
app.MapRazorPages();

app.Run();
