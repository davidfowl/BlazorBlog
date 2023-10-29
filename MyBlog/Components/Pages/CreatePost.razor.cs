using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MyBlog.Components.Pages;

public partial class CreatePost
{
    private EditContext editContext = default!;

    [SupplyParameterFromForm]
    private PostViewModel Post { get; set; } = default!;

    protected override void OnInitialized()
    {
        Post ??= new PostViewModel();
        editContext = new EditContext(Post);
    }

    private async Task HandleSubmitAsync()
    {
        if (editContext.Validate())
        {
            Db.Posts.Add(new Post
            {
                Title = Post.Title,
                Content = Post.Content,
                Created = DateTime.UtcNow
            });

            await Db.SaveChangesAsync();

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
    [StringLength(10000, ErrorMessage = "Content is too long.")]
    public string Content { get; set; } = "";
}