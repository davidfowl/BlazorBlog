using Microsoft.AspNetCore.Http.HttpResults;
using MyBlog.Data;

namespace MyBlog.Api;

public static class PostsApi
{
    public static void MapPostsApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/posts");

        group.MapGet("", async (AppDbContext db) =>
        {
            var posts = await db.Posts
            .Select(p => new PostWithoutComments(p.Id, p.Title, p.Content, p.Created))
            .ToListAsync();

            return posts;
        });

        group.MapGet("{id}", async Task<Results<Ok<PostWithoutComments>, NotFound>> (AppDbContext db, int id) =>
        {
            if (await db.Posts.FindAsync(id) is Post post)
            {
                return TypedResults.Ok(new PostWithoutComments(post.Id, post.Title, post.Content, post.Created));
            }
            return TypedResults.NotFound();
        });

        group.MapPost("", async Task<Results<Created, BadRequest>> (AppDbContext db, CreatePost model) =>
        {
            var post = new Post { Title = model.Title, Content = model.Content, Created = DateTime.UtcNow };
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/posts/{post.Id}");
        });

        group.MapDelete("{id}", async Task<Results<NoContent, NotFound>> (AppDbContext db, int id) =>
        {
            if (await db.Posts.Where(p => p.Id == id).ExecuteDeleteAsync() > 0)
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        });
    }

    public record PostWithoutComments(int Id, string Title, string Content, DateTime Created);
    public record CreatePost(string Title, string Content);
}

