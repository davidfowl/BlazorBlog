using Microsoft.AspNetCore.Components;

namespace MyBlog.Components.Pages;

public partial class Posts
{
    private Post[]? posts;

    [SupplyParameterFromForm]
    public int? PostId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        posts = await Client.GetPostsAsync();
    }

    async Task DeletePost()
    {
        if (PostId is int id)
        {
            await Client.DeletePostAsync(id);

            Nav.NavigateTo("/posts");
        }
    }
}