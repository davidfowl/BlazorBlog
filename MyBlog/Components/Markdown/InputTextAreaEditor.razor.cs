using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyBlog.Extensions;

namespace MyBlog.Components.Markdown;

public sealed partial class InputTextAreaEditor : IAsyncDisposable
{
    private readonly string _id = $"input-{Guid.NewGuid().GetHashCode():X}";
    private InitializationState _initializationState = InitializationState.Initializing;

    private string VisibilityClass => _initializationState switch
    {
        InitializationState.Initializing => "visually-hidden",
        _ => ""
    };

    [Inject]
    public required IJSRuntime JavaScript { get; set; }

    [Inject]
    public required ILogger<InputTextAreaEditor> Log { get; set; }

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
                this, $"#{_id}", Value, nameof(InitializedAsync));

            Log.LogInformation("" +
                "OnAfterRenderAsync: Element with id='{Id}' initialized", _id);

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

            StateHasChanged();
        });
    }

    [JSInvokable]
    public async Task OnValueChanged(string? value)
    {
        await InvokeAsync(async () =>
        {
            Value = value;
            await ValueChanged.InvokeAsync(value);
        });
    }   

    public ValueTask DisposeAsync()
    {
        return _initializationState is InitializationState.Initialized
            ? JavaScript.DisposeMarkdownEditorAsync()
            : ValueTask.CompletedTask;
    }
}

internal enum InitializationState
{
    Initializing,
    Initialized,
    Error,
}