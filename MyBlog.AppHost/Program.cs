var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MyBlog>("myblog");

builder.Build().Run();
