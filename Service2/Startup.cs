using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Service2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MessagingQueueService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                var message = app.ApplicationServices.GetRequiredService<MessagingQueueService>();

                endpoints.MapGet("/", async context =>
                {
                    using var scope = app.ApplicationServices.CreateScope();
                    var messagequeue = scope.ServiceProvider.GetRequiredService<MessagingQueueService>();

                    var content = messagequeue.Message.ToString();
                    await context.Response.WriteAsync(content);
                });
            });
        }
    }
}
