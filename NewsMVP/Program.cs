using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsMVP.Models;
using NewsMVP.MOdels;
using NewsMVP.Utilities;
using System.Text;

namespace NewsMVP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var key = Encoding.UTF8.GetBytes("8NxPhOITEpqy-wdoLkLia0zfuZ53J9KjcPwT0kdYwiI");

  
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

      
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

       
            builder.Services.AddDbContext<NewsContext>(options =>
                options.UseSqlServer("Data Source=.;Initial Catalog=NewsDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

            builder.Services.AddHttpClient("externalnews", client =>
            {
                client.BaseAddress = new Uri("https://newsapi.org/v2/");
            });

            builder.Services.AddScoped<ExternalNewsService>();

            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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

            app.MapRazorPages();

            app.Run();
        }
    }
}
