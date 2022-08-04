namespace GoodHabits.JavascriptInterop
{
    public class IndexedDBInterop : IAsyncDisposable
    {
        private BaseJsInterop jsInterop;

        public IndexedDBInterop(IJSRuntime jsRuntime)
        {
            this.jsInterop = new BaseJsInterop(jsRuntime, "localdb.js");
        }

        public async ValueTask DisposeAsync()
        {
            var module = await jsInterop.GetModule();
            await module.InvokeVoidAsync("disposeModel");
            await jsInterop.DisposeAsync();
        }

        public async ValueTask<bool> CreateDatabase()
        {
            var module = await jsInterop.GetModule();

            if(module is not null)
            {
                await module.InvokeVoidAsync("CreateDatabase");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
