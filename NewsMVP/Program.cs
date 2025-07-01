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

            // کلید برای JWT
            var key = Encoding.UTF8.GetBytes("137c514cc904eb0cc089aca19fdab93c68e859249a335331368c893818c64b91");

            // 🔐 احراز هویت با JWT
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

            // ⚙️ Razor + MVC
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            // 💾 پایگاه داده SQL Server
            builder.Services.AddDbContext<NewsContext>(options =>
                options.UseSqlServer("Data Source=.;Initial Catalog=NewsDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

            // 🌐 HttpClient برای دریافت خبر از API خارجی
            builder.Services.AddHttpClient("externalnews", client =>
            {
                client.BaseAddress = new Uri("https://newsapi.org/v2/");
            });

            // 💡 سرویس دریافت اخبار خارجی
            builder.Services.AddScoped<ExternalNewsService>();

            var app = builder.Build();

            // ⚠️ هندل کردن خطاها در محیط production
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // 🔐 فعال‌سازی احراز هویت
            app.UseAuthentication();
            app.UseAuthorization();

            // مسیردهی
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
