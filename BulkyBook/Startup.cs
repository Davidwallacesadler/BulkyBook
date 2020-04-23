using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using BulkyBook.DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using BulkyBook.Utility;
using Microsoft.CodeAnalysis.Options;

namespace BulkyBook
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
            // Db Context Setup:
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Default Identity setup: -- Note: this does not have support for identity role (which we need for roleManagement)
            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Registering our EmailSender Class:
            services.AddSingleton<IEmailSender, EmailSender>();

            // Configure our EmailOptions class with the sendGrid keys from appsettings
            // NOTE: this configuration method will try and match up whatever keys we had in the app settings with properties in our email options.
            services.Configure<EmailOptions>(Configuration);

            // Repository and Unit of work setup: (this will be added to the project as dependency injection)
            // This makes it so in any controller we can access the unit of work and it's functionality.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // MVC Controllers with Views setup with added razorpages runtime compilation:
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            // Razor pages setup: (Used for Areas)
            services.AddRazorPages();

            // Congfigure redirection if the user attempts to access a page in outside of their area
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            // setup facebook authentication:
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "901015027037493";
                options.AppSecret = "8ed72993a5e0a0463746c5002d0467d7";
            });

            // Setup google authentication:
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "272211179688-9cu7en6lcqn7cpb1dck96chd9aage41b.apps.googleusercontent.com";
                options.ClientSecret = "D3ryg_yLgMMAcc0YeV-hHoo3";

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/{area=Customer}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
