namespace UrlShortener.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using UrlShortener.Web.Components;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add service defaults & Aspire client integrations.
            builder.AddServiceDefaults();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddOutputCache();

            builder.Services.AddHttpClient<WeatherApiClient>(client =>
            {
                // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
                // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
                client.BaseAddress = new("https+http://apiservice");
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseOutputCache();

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            app.MapDefaultEndpoints();

            app.Run();
        }
    }
}