using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BlobStorageExample
{
    internal static class Operations
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
    }
}
