using Azure.Storage.Blobs;
using Microsoft.AspNetCore.DataProtection;

public static class Extensions
{
    public static void AddStorageBasedDataProtection(this IHostApplicationBuilder builder)
    {
        // Make this optional
        if (builder.Configuration.GetConnectionString("blobs") is null)
        {
            return;
        }

        builder.AddAzureBlobService("blobs");

        builder.Services.AddDataProtection()
            .PersistKeysToAzureBlobStorage(sp =>
            {
                var containerClient = sp.GetRequiredService<BlobServiceClient>().GetBlobContainerClient("container");
                containerClient.CreateIfNotExists();
                var blobClient = containerClient.GetBlobClient("data-protection-keys");
                return blobClient;
            });

    }
}
