using HealthCheck.API.Services;
using HealthCheck.Repository;
using HealthCheck.Web.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.Cookie.Name = "user";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Events.OnRedirectToLogin = (context) =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddMvc().AddControllersAsServices();
            services.AddTransient(typeof(IEFRepository<>), typeof(EFRepository<>));
            services.AddScoped<AnswerRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<SessionService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<SessionCategoryService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<ExcelExportService>();
            services.AddSignalR();

            var connection = Configuration.GetConnectionString("SQLConnectionString");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
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
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("DefaultApi", "api/{controller}/{id?}");
            });
            app.UseSignalR(routes =>
            {
                routes.MapHub<AnswerHub>("/answerHub");
                routes.MapHub<SessionHub>("/sessionHub");
                routes.MapHub<ChatHub>("/chatHub");
                routes.MapHub<CategoryHub>("/categoryHub");
            });
        }
    }
}
