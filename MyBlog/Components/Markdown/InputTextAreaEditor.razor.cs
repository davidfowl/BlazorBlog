using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyBlog.Extensions;

namespace MyBlog.Components.Markdown;

public sealed partial class InputTextAreaEditor : IAsyncDisposable
{
    private readonly string _id = $"input-{Guid.NewGuid().GetHashCode():X}";
    private InitializationState _initializationState = InitializationState.Initializing;

    private string LabelClass => _initializationState switch
    {
        InitializationState.Initializing => "visually-hidden",
        _ => ""
    };

    private string InputClass => _initializationState switch
    {
        InitializationState.Initializing => "form-control visually-hidden",
        InitializationState.Initialized => "form-control",
        _ => "form-control is-invalid",
    };

    [Inject]
    public required IJSRuntime JavaScript { get; set; }

    [Inject]
    public required ILogger<InputTextAreaEditor> Logger { get; set; }

    [EditorRequired, Parameter]
    public required string Label { get; set; }

    [EditorRequired, Parameter]
    public string? Value { get; set; }

    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await JavaScript.InitializeMarkdownEditorAsync(
                $"#{_id}", Value, nameof(InitializedAsync));

            Logger.LogInformation("Element with id='{Id}' initialized", _id);

            StateHasChanged();
        }
    }

    [JSInvokable]
    public async Task InitializedAsync(bool initialized)
    {
        await InvokeAsync(() =>
        {
            _initializationState = initialized
                ? InitializationState.Initialized
                : InitializationState.Error;

            Logger.LogInformation(
                "Element with id='{Id}' is: {State}", _id, _initializationState);

            StateHasChanged();
        });
    }

    public ValueTask DisposeAsync()
    {
        return JavaScript.DisposeMarkdownEditorAsync();
    }
}

internal enum InitializationState
{
    Initializing,
    Initialized,
    Error,
}