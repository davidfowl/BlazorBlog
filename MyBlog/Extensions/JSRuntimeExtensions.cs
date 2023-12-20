using Microsoft.JSInterop;

namespace MyBlog.Extensions;

internal static class JSRuntimeExtensions
{
    public static ValueTask InitializeMarkdownEditorAsync<TComponent>(
        this IJSRuntime js,
        TComponent component,
        string selector,
        string? initialValue,
        string onInitializedMethodName) where TComponent : class =>
        js.InvokeVoidAsync(
            "app.initializeEditor",
            DotNetObjectReference.Create(component),
            selector,
            initialValue,
            onInitializedMethodName);

    public static ValueTask DisposeMarkdownEditorAsync(
        this IJSRuntime js) =>
        js.InvokeVoidAsync("app.disposeEditor");

    public static ValueTask<string> GetMarkdownEditorValueAsync(
        this IJSRuntime js) =>
        js.InvokeAsync<string>("app.getValue");

    public static ValueTask SetMarkdownEditorValueAsync(
        this IJSRuntime js, string value) =>
        js.InvokeVoidAsync("app.setValue", value);
}
