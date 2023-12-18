var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("pg").AddDatabase("db");

var blobs = builder.AddAzureStorage("storage").UseEmulator()
                   .AddBlobs("blobs");

var api = builder.AddProject<Projects.MyBlog_Api>("myblogapi")
                 .WithReference(db);

builder.AddProject<Projects.MyBlog>("myblog")
       .WithReference(api)
       .WithReference(blobs)
       .WithEnvironment("ASPNETCORE_FORWARDEDHEADERS_ENABLED", "true");
;
builder.Build().Run();