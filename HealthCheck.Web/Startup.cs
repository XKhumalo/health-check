using HealthCheck.API.Services;
using HealthCheck.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCheck.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().AddControllersAsServices();
            services.AddScoped<AnswerService>();
            services.AddScoped<UserService>();
            services.AddScoped<SessionService>();
            services.AddScoped<CategoryService>();
            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("DefaultApi", "api/{controller}/{id?}");
            });
            app.UseSignalR(routes =>
            {
                routes.MapHub<AnswerHub>("/answerHub");
                routes.MapHub<SessionHub>("/sessionHub");
                routes.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
