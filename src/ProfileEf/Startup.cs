using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyWeb {

    public class AppSettings {
        public string ConnectionString { set; get; }
        public bool EnableProfiler { set; get; }
    }

    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {

            var settings = Configuration.Get<AppSettings>();

            services.AddSingleton<AppSettings>(settings);
            services.AddMemoryCache();
            services.AddDbContext<MyContext>(options =>
                options.UseNpgsql(settings.ConnectionString, x => {
                    x.SetPostgresVersion(9, 3);
                })
            );
            services.AddControllers();

            if (settings.EnableProfiler) {
                services.AddMiniProfiler(opitons =>
                    opitons.RouteBasePath = "/profiler"
                ).AddEntityFramework();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext context, AppSettings settings) {
            context.Database.EnsureCreated();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            if (settings.EnableProfiler) {
                app.UseMiniProfiler();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
