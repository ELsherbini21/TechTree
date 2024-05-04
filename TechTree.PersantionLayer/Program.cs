using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechTree.BLL.Repositories;
using TechTree.DAL.Data;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.Extensions;
using TechTree.PersantionLayer.Helpers.interfaces;
using TechTree.PersantionLayer.Helpers.Models;

namespace TechTree.PersantionLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("TechTreeDbConnectionString"));
                options.UseLazyLoadingProxies();
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                       .AddRoles<IdentityRole>()
                       .AddEntityFrameworkStores<AppDbContext>()
                       .AddDefaultTokenProviders();

            // => i will make map at Service 'mail Settings '
            // here i am inside configuration . 
            builder.Services.Configure<MailSettings>(
                builder.Configuration.GetSection("MailSettings")
                );

            builder.Services.AddTransient<IEmailSettings, EmailSettings>();
            // here i Said , Create From Class Email Settings 

            // External Login For Google 
            builder.Services.AddAuthentication(
                o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }
            )
                .AddCookie()
                
                .AddGoogle(o =>
            {
                // here i get Google that inside in Section authentication .
                var googleAuth = builder.Configuration.GetSection("Authentication:Google");
                o.ClientId = googleAuth["ClientID"];
                o.ClientSecret = googleAuth["ClientSecret"];
                o.SaveTokens = true;
            })
            .AddFacebook(facebookOption =>
            {
                var facebookAuth = builder.Configuration.GetSection("Authentication:Facebook");
                facebookOption.AppId = facebookAuth["AppID"];
                facebookOption.AppSecret = facebookAuth["AppSecret"];
                // if when i make click to Not now button at facebook
                facebookOption.AccessDeniedPath = "/Home/Index";
            });

            builder.Services.AddCors( options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
                  
                
            }) ;

            
            builder.Services.AddApplicationServices();

            builder.Services.AddRazorPages();







            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}