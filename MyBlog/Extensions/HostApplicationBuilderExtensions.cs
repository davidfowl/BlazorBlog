using Azure.Storage.Blobs;
using Microsoft.AspNetCore.DataProtection;

public static class HostApplicationBuilderExtensions
{
    public static void AddStorageBasedDataProtection(this IHostApplicationBuilder builder)
    {
        // Make this optional
        if (builder.Configuration.GetConnectionString("blobs") is null)
        {
            return;
        }

        // TODO: We need to support creating a key in the app model to make this work seamlessly
        //var keyVaultUri = builder.Configuration.GetConnectionString("vault") is string keyVaultConnectionString
        //    ? new Uri(keyVaultConnectionString)
        //    : null;

        builder.AddAzureBlobService("blobs");

        var dpBuilder = builder.Services.AddDataProtection()
            .PersistKeysToAzureBlobStorage(sp =>
            {
                var containerClient = sp.GetRequiredService<BlobServiceClient>().GetBlobContainerClient("container");
                containerClient.CreateIfNotExists();
                var blobClient = containerClient.GetBlobClient("data-protection-keys");
                return blobClient;
            });

        // TODO: We need to support creating a key in the app model to make this work seamlessly
        //if (keyVaultUri is not null)
        //{
        //    dpBuilder.ProtectKeysWithAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
        //}
    }
}
