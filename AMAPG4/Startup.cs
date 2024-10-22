using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using AMAPG4.Models.Command;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AMAPG4.Models.ContactForm;


namespace AMAPG4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Index";
            });

            services.AddControllersWithViews();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Initialisation des données
            
            IndividualDal individualDal = new IndividualDal();
            individualDal.Initialize();

            CEDal ceDal = new CEDal();
            ceDal.Initialize();

            ProducerDal producerDal = new ProducerDal();
            producerDal.Initialize();

            ProductDal productDal = new ProductDal();
            productDal.InitializeDataBase();

            UserAccountDal userAccountDal = new UserAccountDal();
            userAccountDal.InitializeDataBase();

            OrderLineDal orderLineDal = new OrderLineDal();
                orderLineDal.Initialize();

            ContactService contactService = new ContactService();
            contactService.InitializeDataBase();




            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
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