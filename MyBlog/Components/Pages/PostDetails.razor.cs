using Microsoft.AspNetCore.Components;

namespace MyBlog.Components.Pages;

public partial class PostDetails
{
    public Post? Post { get; set; }

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Post = await Client.GetPostAsync(Id);
    }
}