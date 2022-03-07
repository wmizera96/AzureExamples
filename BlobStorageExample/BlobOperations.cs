using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BlobStorageExample
{
    internal static class BlobOperations
    {
        public static async Task CreateContainer(BlobServiceClient client, string name)
        {
            var response = await client.CreateBlobContainerAsync(name);
            var container = response.Value;
            Console.WriteLine(container.Name);
            Console.WriteLine(container.Uri);
            Console.WriteLine(container.AccountName);
        }

        public static async Task UploadFile(BlobServiceClient client, string containerName, string filePath)
        {
            var containerClient = client.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(filePath);

            using var fileStream = File.OpenRead(filePath);

            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        public static async Task ListBlobs(BlobServiceClient client, string containerName)
        {
            var containerClient = client.GetBlobContainerClient(containerName);
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine(blobItem.Name);
            }
        }

        public static async Task DeleteContainer(BlobServiceClient client, string containerName)
        {
            var containerClient = client.GetBlobContainerClient(containerName);
            await containerClient.DeleteAsync();
        }

        public static async Task DownloadBlob(BlobServiceClient client, string containerName, string sourcePath, string targetPath)
        {
            var blobContainerClient = client.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(sourcePath);
            var downloadInfo = await blobClient.DownloadAsync();


            using (FileStream downloadFileStream = File.OpenWrite(targetPath))
            {
                await downloadInfo.Value.Content.CopyToAsync(downloadFileStream);
                downloadFileStream.Close();
            }
        }

        public static async Task ReadContainerPropertiesAsync(BlobServiceClient client, string containerName)
        {
            var container = client.GetBlobContainerClient(containerName);
            try
            {
                // Fetch some container properties and write out their values.
                var properties = await container.GetPropertiesAsync();
                Console.WriteLine($"Properties for container {container.Uri}");
                Console.WriteLine($"Public access level: {properties.Value.PublicAccess}");
                Console.WriteLine($"Last modified time in UTC: {properties.Value.LastModified}");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        public static async Task AddContainerMetadataAsync(BlobServiceClient client, string containerName, IDictionary<string, string> metadata)
        {
            var container = client.GetBlobContainerClient(containerName);
            try
            {
                await container.SetMetadataAsync(metadata);
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        public static async Task ReadContainerMetadataAsync(BlobServiceClient client, string containerName)
        {
            var container = client.GetBlobContainerClient(containerName);
            try
            {
                var properties = await container.GetPropertiesAsync();

                // Enumerate the container's metadata.
                Console.WriteLine("Container metadata:");
                foreach (var metadataItem in properties.Value.Metadata)
                {
                    Console.WriteLine($"\tKey: {metadataItem.Key}");
                    Console.WriteLine($"\tValue: {metadataItem.Value}");
                }
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
