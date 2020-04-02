using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service1
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

                endpoints.MapGet("/", async context =>
                {
                    //Microsoft.Extensions.Primitives.StringValues queryString;

                    //using var scope = app.ApplicationServices.CreateScope();
                    //var messagequeue = scope.ServiceProvider.GetRequiredService<MessagingQueueService>();

                    //if (context.Request.Query.TryGetValue("message", out queryString))
                    //{
                    //    messagequeue.Enqueue(queryString);

                    //    await context.Response.WriteAsync(queryString);
                    //}
                    await context.Response.WriteAsync("try again with message querystring ex : /test");

                });
                endpoints.MapGet("/{message:alpha}", async context =>
                {
                    using var scope = app.ApplicationServices.CreateScope();
                    var messagequeue = scope.ServiceProvider.GetRequiredService<MessagingQueueService>();

                    var queryString = context.Request.RouteValues["message"].ToString();
                    messagequeue.Enqueue(queryString);

                    await context.Response.WriteAsync(queryString);
                });


            });
        }
    }
}
