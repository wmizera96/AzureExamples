using Azure.Storage.Blobs;

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
    }
}
