using Microsoft.AspNetCore.Components;

namespace MyBlog.Components.Pages;

public partial class Posts
{
    private Post[]? _posts;

    [SupplyParameterFromForm]
    public int? PostToDelete { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (PostToDelete.HasValue)
        {
            await Client.DeletePostAsync(PostToDelete.Value);
        }

        _posts = await Client.GetPostsAsync();
    }
}
