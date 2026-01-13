using Azure.Storage.Blobs;

namespace WebApp
{
    public class BlobService
    {
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string connectionString)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient("faturas");
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);

            return blobClient.Uri.ToString();
        }
    }
}