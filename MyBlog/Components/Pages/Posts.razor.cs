namespace MyBlog.Components.Pages;

public partial class Posts
{
    private Post[]? posts;

    protected override async Task OnInitializedAsync()
    {
        posts = await Client.GetPostsAsync();
    }

    async Task DeletePost(int id)
    {
        await Client.DeletePostAsync(id);

        Nav.NavigateTo("/posts");
    }
}
