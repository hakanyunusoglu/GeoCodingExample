using GeoCodingExample.Models;
using GeoCodingExample.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.IO;

namespace GeoCodingExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<GeocodeHelper, GeocodeHelper>();
            //HttpClient i dependency injection a dahil edebilmek için bu servisi eklememiz gerekiyor
            services.AddHttpClient();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext datacontext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
