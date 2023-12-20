using Microsoft.JSInterop;

namespace MyBlog.Extensions;

internal static class JSRuntimeExtensions
{
    public static ValueTask InitializeMarkdownEditorAsync(
        this IJSRuntime js, string selector, string? initialValue, string onInitializedMethodName) =>
        js.InvokeVoidAsync("initializeEditor", selector, initialValue, onInitializedMethodName);

    public static ValueTask DisposeMarkdownEditorAsync(
        this IJSRuntime js) =>
        js.InvokeVoidAsync("disposeEditor");

    public static ValueTask<string> GetMarkdownEditorValueAsync(
        this IJSRuntime js) =>
        js.InvokeAsync<string>("getValue");

    public static ValueTask SetMarkdownEditorValueAsync(
        this IJSRuntime js, string value) =>
        js.InvokeVoidAsync("setValue", value);
}
