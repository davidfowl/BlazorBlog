var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgresContainer("pg").AddDatabase("db");

// Azd doesn't currently support storage
//var blobs = builder.AddAzureStorage("storage")
//                   .UseEmulator()
//                   .AddBlobs("blobs");

var api = builder.AddProject<Projects.MyBlog_Api>("myblogapi")
                 .WithReference(db)
                 .WithLaunchProfile("https");

builder.AddProject<Projects.MyBlog>("myblog")
       .WithReference(api)
       // .WithReference(blobs)
       .WithEnvironment("ASPNETCORE_FORWARDEDHEADERS_ENABLED", "true")
       .WithLaunchProfile("https");

builder.Build().Run();