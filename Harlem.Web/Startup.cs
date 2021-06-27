using Harlem.BLL.Abstract;
using Harlem.BLL.Concrete.Services;
using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.Context;
using Harlem.DAL.Concrete.DataAccesLayers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace Harlem.Web
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
            services.AddMvc();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddScoped<ICategoryDAL, CategoryDAL>();
            services.AddScoped<IProductDAL, ProductDAL>();
            services.AddScoped<IProductImageDAL, ProductImageDAL>();
            services.AddScoped<IUserDAL, UserDAL>();
            services.AddScoped<IAccountUserDAL, AccountUserDAL>();
            services.AddScoped<IAccountUserAddressDAL, AccountUserAddressDAL>();
            services.AddScoped<IBasketDAL, BasketDAL>();
            services.AddScoped<IOrderDAL, OrderDAL>();
            services.AddScoped<IOrderItemDAL, OrderItemDAL>();
            services.AddScoped<IBasketItemDAL, BasketItemDAL>();

            services.AddScoped<ICategoryService,CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductImageService, ProductImageMenager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IAccountUserService, AccountUserManager>();
            services.AddScoped<IAccountUserAddressService, AccountUserAddressManager>();
            services.AddScoped<IBasketService, BasketManager>();
            services.AddScoped<IBasketItemService, BasketItemManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderItemService, OrderItemManager>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var client = new HarlemContext())
                {
                    client.Database.EnsureDeleted();
                    client.Database.EnsureCreated();
                    HarlemDBInitilazier.SeedData(client);
                    app.UseBrowserLink();

                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var supportedCultures = new[]{new CultureInfo("en-US"),new CultureInfo("es"),};

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // Localized UI strings.
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
               name: "areas",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
             );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
