using Microsoft.AspNetCore.Components;

namespace MyBlog.Components.Pages;

public partial class Posts
{
    private Post[]? posts;

    [SupplyParameterFromForm]
    public int? PostId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        posts = await Db.Posts.ToArrayAsync();
    }

    async Task DeletePost()
    {
        await Db.Posts.Where(p => p.Id == PostId).ExecuteDeleteAsync();

        Nav.NavigateTo("/posts");
    }
}