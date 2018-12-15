using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace NStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
//                .ConfigureServices(s => s.AddMvcCore())
//                .Configure(app =>
//                {
//                    app.UseRouter(r =>
//                    {
//                        r.MapGet("/hi", async (request, response, data) =>
//                            {
//                                await response.WriteAsync("Hi!");
//                            }
//                        );
//                    });
//                })
                .UseSerilog((ctx, cfg) =>
                {
                    cfg.WriteTo.ColoredConsole()
                        .MinimumLevel.Is(LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Environment",
                            ctx.HostingEnvironment.EnvironmentName);
                })
                .UseStartup<Startup>();
    }
}
