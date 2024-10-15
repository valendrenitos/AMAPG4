using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMAPG4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Index";

            });

            services.AddControllersWithViews();

            services.AddScoped<UserAccountDal>();
            services.AddScoped<ProductDal>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            // Initialisez les bases de données ici en utilisant un fournisseur de services scoped
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                UserAccountDal userAccountDal = scope.ServiceProvider.GetRequiredService<UserAccountDal>();
                userAccountDal.InitializeDataBase();

                ProductDal productDal = scope.ServiceProvider.GetRequiredService<ProductDal>();
                productDal.InitializeDataBase();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
