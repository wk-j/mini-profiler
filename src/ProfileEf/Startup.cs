using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProfileEf {
    public class Student {
        public int Id { set; get; }
        public string Name { set; get; }
    }

    public class MyContext : DbContext {
        public MyContext(DbContextOptions options) : base(options) {

        }
        public DbSet<Student> Students { set; get; }
    }

    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMemoryCache();
            services.AddDbContext<MyContext>(options =>
                options.UseNpgsql(Configuration["ConnectionString"], x => {
                    x.SetPostgresVersion(9, 3);
                })

            );
            services.AddControllers();
            services.AddMiniProfiler(opitons =>
                opitons.RouteBasePath = "/profiler"
            ).AddEntityFramework();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext context) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            context.Database.EnsureCreated();

            app.UseMiniProfiler();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
