using System.Data.SqlClient;
using HealthCheck.API.Services;
using HealthCheck.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCheck.API
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
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
            });

            services.AddMvc();
            services.AddTransient(typeof(IEFRepository<>), typeof(EFRepository<>));
            services.AddScoped<AnswerRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<SessionRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<SessionCategoryRepository>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<ExcelExportService>();

            var connection = Configuration.GetConnectionString("SQLConnectionString");
            var sqlConnection = new SqlConnection("Data Source=ESSQLSERVER02\\SQL2017;Initial Catalog=ServiceDesk_HealthCheck;User ID=servicedesk-hcadmin;Password=8wn_2]_HbXe^CCA");

            services.AddDbContext<DatabaseContext>(options => options.UseLazyLoadingProxies().UseSqlServer(sqlConnection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors("CorsPolicy");

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}");
            });
        }
    }
}
