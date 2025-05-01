namespace UrlShortener.AppHost
{
    using Aspire.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = DistributedApplication.CreateBuilder(args);

            var apiService = builder.AddProject<Projects.UrlShortener_ApiService>("apiservice");

            builder.AddProject<Projects.UrlShortener_Web>("webfrontend")
                .WithExternalHttpEndpoints()
                .WithReference(apiService)
                .WaitFor(apiService);

            builder.Build().Run();
        }
    }
}