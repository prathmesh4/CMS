using CustMgmSys.Abstract;
using CustMgmSys.Data;
using CustMgmSys.Models;
using CustMgmSys.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustMgmSys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var configuration = builder.Configuration;
            builder.Services.AddDbContext<CMSDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
             
            // For Identity  
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CMSDbContext>()
                .AddDefaultTokenProviders();
            var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            builder.Services.AddSingleton(emailSettings);
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Login/Login");
             
            //add services to container
            builder.Services.AddScoped<IUserLoginService, UserLoginService>();

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
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
