using AzureExamples.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<ExampleDatabaseContext>();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapRazorPages();

app.Run();
