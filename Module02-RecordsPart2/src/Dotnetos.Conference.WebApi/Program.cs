using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Dotnetos.Conference.WebApi
{
    public class Program
    {
        /// <summary>
        /// TODO: Use your knowledge about init-only properties, records, inheritance and (de)construction to rewrite flow of retrieving speakers from the repository.
        /// Your changes should not introduce a breaking change to the web-api JSON contract. After the refactoring, the `speakers` endpoint located under `https://localhost:5001/speakers` should return the same data as now.
        /// There is no single solution to this task. Experiment, be creative and try to see how much you know about records.
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
