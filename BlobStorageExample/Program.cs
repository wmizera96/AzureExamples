using Azure.Storage.Blobs;
using BlobStorageExample;
using Microsoft.Extensions.Configuration;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();


var blobServiceClient = new BlobServiceClient(configuration["ConnectionStrings:BlobStorage"]);

var guid = "b0175793-9c42-4a2f-a00d-eeffd5335621";
var containerName = $"wmizera-container-{guid}";
var filePath = "./data/text.txt";

//await Operations.CreateContainer(blobServiceClient, containerName);

//await Operations.UploadFile(blobServiceClient, containerName, filePath);

//await Operations.ListBlobs(blobServiceClient, containerName);

//await Operations.DownloadBlob(blobServiceClient, containerName, filePath, "./downloaded.txt");

//await Operations.DeleteContainer(blobServiceClient, containerName);

//await Operations.ReadContainerPropertiesAsync(blobServiceClient, containerName);


IDictionary<string, string> metadata =
                   new Dictionary<string, string>
                   {
                       ["doctype"] = "img",
                       ["category"] = "guidance"
                   };

await BlobOperations.AddContainerMetadataAsync(blobServiceClient, containerName, metadata);
await BlobOperations.ReadContainerMetadataAsync(blobServiceClient, containerName);
