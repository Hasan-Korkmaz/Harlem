using Harlem.BLL.Abstract;
using Harlem.BLL.Concrete.Services;
using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.Context;
using Harlem.DAL.Concrete.DataAccesLayers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;

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

            services.AddSession();



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

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductImageService, ProductImageMenager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IAccountUserService, AccountUserManager>();
            services.AddScoped<IAccountUserAddressService, AccountUserAddressManager>();
            services.AddScoped<IBasketService, BasketManager>();
            services.AddScoped<IBasketItemService, BasketItemManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderItemService, OrderItemManager>();

            services.AddScoped<Security.UserManager>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie("CustomerCookie", options =>
            {
                options.Cookie.Name = "CustomerCookie";
                options.LoginPath = "/Home/Login?login=1";
                options.LogoutPath = "/Account/Logout";
            }).AddCookie("BackofficeCookie",
                           options =>
                           {
                               options.Cookie.Name = "BackOffice";
                               options.LoginPath = "/BackOffice/Account/Login";
                               options.LogoutPath = "/BackOffice/Account/Logout";
                           });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

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
            var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("es"), };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // Localized UI strings.
                SupportedUICultures = supportedCultures
            });

            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
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
        private void OnShutdown()
        {
            //Aplication Closing
            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine("\t*******************************************" + Environment.NewLine +
                                    "\t*                                         *" + Environment.NewLine +
                                    "\t*                                         *" + Environment.NewLine +
                                    "\t*                                         *" + Environment.NewLine +
                                    "\t*           ...Harlem System...           *" + Environment.NewLine +
                                    "\t*            ...ShutDown...               *" + Environment.NewLine +
                                    "\t*                  "+i+"                      *" + Environment.NewLine +
                                    "\t*                                         *" + Environment.NewLine +
                                    "\t*                                         *" + Environment.NewLine +
                                    "\t*******************************************");
                Thread.Sleep(1000);
            }
        }
    }
}
