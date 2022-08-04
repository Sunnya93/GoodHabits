using GoodHabits;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddIndexedDB(dbStore =>
{
    dbStore.DbName = "GoodHabits";
    dbStore.Version = 1;

    dbStore.Stores.Add(new StoreSchema
    {
        Name = "Todo",
        PrimaryKey = new IndexSpec { Name = "id", KeyPath = "id", Auto = true },
        Indexes = new List<IndexSpec>
        {
            new IndexSpec{Name = "title", KeyPath = "title"},
            new IndexSpec{Name = "content", KeyPath = "content"},
            new IndexSpec{Name = "todoDate", KeyPath = "todoDate"},
            new IndexSpec{Name = "insertTime", KeyPath = "insertTime"},
            new IndexSpec{Name = "updateTime", KeyPath = "updateTime"}
        }
    });

    dbStore.Stores.Add(new StoreSchema
    {
        Name = "Pray",
        PrimaryKey = new IndexSpec { Name = "id", KeyPath = "id", Auto = true },
        Indexes = new List<IndexSpec>
        {
            new IndexSpec{Name = "type", KeyPath = "type"},
            new IndexSpec{Name = "content", KeyPath = "content"},
            new IndexSpec{Name = "prayDate", KeyPath = "prayDate"},
            new IndexSpec{Name = "insertTime", KeyPath = "insertTime"},
            new IndexSpec{Name = "updateTime", KeyPath = "updateTime"}
        }
    });
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSqliteWasmDbContextFactory<AppDbContext>(opt => opt.UseSqlite("Data Source=GoodHabits.sqlite3"));
builder.Services.AddScoped<PrayService>();
builder.Services.AddScoped<TodoService>();


await builder.Build().RunAsync();
