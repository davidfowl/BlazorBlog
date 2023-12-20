using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MyBlog.Components.Pages;

public partial class CreatePost
{
    private EditContext _editContext = default!;

    [SupplyParameterFromForm]
    private PostViewModel Post { get; set; } = default!;

    protected override void OnInitialized()
    {
        Post ??= new PostViewModel();
        _editContext = new EditContext(Post);
    }

    private async Task HandleSubmitAsync()
    {
        if (_editContext.Validate())
        {
            await Client.CreatePostAsync(Post.Title, Post.Content);

            Nav.NavigateTo("/posts");
        }
    }
}

public class PostViewModel
{
    [Required]
    [StringLength(100, ErrorMessage = "Title is too long.")]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(10_000, ErrorMessage = "Content is too long.")]
    public string Content { get; set; } = "My amazing start of a blog...";
}