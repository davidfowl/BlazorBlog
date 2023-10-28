var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgresContainer("pg").AddDatabase("db");

builder.AddProject<Projects.MyBlog>("myblog")
       .WithReference(db);

builder.Build().Run();