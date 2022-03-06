using Azure.Storage.Blobs;
using BlobStorageExample;
using Microsoft.Extensions.Configuration;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();


var blobClient = new BlobServiceClient(configuration["ConnectionStrings:BlobStorage"]);

var guid = "b0175793-9c42-4a2f-a00d-eeffd5335621";


await Operations.CreateContainer(blobClient, $"wmizera-container-{guid}");


Console.WriteLine("Container created");





