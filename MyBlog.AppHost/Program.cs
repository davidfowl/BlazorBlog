var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgresContainer("pg").AddDatabase("db");

var api = builder.AddProject<Projects.MyBlog_Api>("myblogapi")
                 .WithReference(db);

builder.AddProject<Projects.MyBlog>("myblog")
       .WithReference(api)
       .WithEnvironment("ASPNETCORE_FORWARDEDHEADERS_ENABLED", "true");

builder.Build().Run();