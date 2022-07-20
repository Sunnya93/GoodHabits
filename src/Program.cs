using GoodHabits;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSqliteWasmDbContextFactory<AppDbContext>(opt => opt.UseSqlite("Data Source=GoodHabits.sqlite3"));
builder.Services.AddScoped<PrayService>();
builder.Services.AddScoped<TodoService>();


await builder.Build().RunAsync();
