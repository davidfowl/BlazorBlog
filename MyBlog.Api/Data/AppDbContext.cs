using Microsoft.EntityFrameworkCore;

namespace MyBlog.Data;

// Write an AppDbContext class that inherits from DbContext and implements a model for a blog with posts and comments.

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime Created { get; set; } = DateTime.Now;
    public List<Comment> Comments { get; set; } = [];
}

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public DateTime Created { get; set; } = DateTime.Now;
    public Post Post { get; set; } = null!;
}