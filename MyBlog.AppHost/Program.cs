var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureProvisioning();

var db = builder.AddPostgresContainer("pg").AddDatabase("db");

var blobs = builder.AddAzureStorage("storage")
                   .UseEmulator()
                   .AddBlobs("blobs");

var kv = builder.AddAzureKeyVault("vault");

var api = builder.AddProject<Projects.MyBlog_Api>("myblogapi")
                 .WithReference(db)
                 .WithReference(kv)
                 .WithLaunchProfile("https");

builder.AddProject<Projects.MyBlog>("myblog")
       .WithReference(api)
       .WithReference(kv)
       .WithReference(blobs)
       .WithEnvironment("ASPNETCORE_FORWARDEDHEADERS_ENABLED", "true")
       .WithLaunchProfile("https");

builder.Build().Run();