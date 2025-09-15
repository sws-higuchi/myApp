using myApp.Client.Pages;
using myApp.Components;

var builder = WebApplication.CreateBuilder(args);

//Render on a specific port if specified in the environment
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(int.Parse(port));
    });
}

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

//Render a simple health check at /health
//app.MapGet("/health", () => "OK");
app.MapGet("/health", () => Results.Ok("OK"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Render 環境では HTTPS リダイレクトを無効化
var disableHttps = Environment.GetEnvironmentVariable("DISABLE_HTTPS_REDIRECT");
var isHttpsDisabled = string.Equals(disableHttps, "true", StringComparison.OrdinalIgnoreCase);

if (!isHttpsDisabled)
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(myApp.Client._Imports).Assembly);

app.Run();
