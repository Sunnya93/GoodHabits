namespace GoodHabits.JavascriptInterop
{
    public class BaseJsInterop : IAsyncDisposable
    {
        private const string JS_BASE_URL = "./js/";

        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public BaseJsInterop(IJSRuntime jsRuntime, string jsFilePath)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", $"{JS_BASE_URL}{jsFilePath}").AsTask());
        }

        public async Task<IJSObjectReference> GetModule()
        {
            return await moduleTask.Value;
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
