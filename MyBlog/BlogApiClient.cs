namespace MyBlog;

public class BlogApiClient(HttpClient client)
{
    public async Task<Post[]> GetPostsAsync() => 
        await client.GetFromJsonAsync<Post[]>("/api/posts") ?? [];

    public Task<Post?> GetPostAsync(int id) => 
        client.GetFromJsonAsync<Post>($"/api/posts/{id}");

    public async Task CreatePostAsync(string title, string content) =>
        await client.PostAsJsonAsync("/api/posts", new CreatePost(title, content));

    public async Task DeletePostAsync(int id) => 
        await client.DeleteAsync($"/api/posts/{id}");

    private record CreatePost(string Title, string Content);
}
