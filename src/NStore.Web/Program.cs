using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

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
                .UseStartup<Startup>();
    }
}
