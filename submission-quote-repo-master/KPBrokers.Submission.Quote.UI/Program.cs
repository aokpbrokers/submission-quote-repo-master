using KPBrokers.Submission.Quote.UI.Areas.Identity.Data;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using KPBrokers.Submission.Quote.UI.Services.Caching;
using KPBrokers.Submission.Quote.UI.Services.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
namespace KPBrokers.Submission.Quote.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("KPBDbContextConnection") ?? throw new InvalidOperationException("Connection string 'KPBDbContextConnection' not found.");

            builder.Services.AddDbContext<KPBDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<KPBDbContext>()                
                .AddDefaultUI()
                .AddDefaultTokenProviders();           

            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();
            
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddScoped<IUrlHelper>(x => {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ICacheService, RuntimeCacheService>();

            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IClientFactoryService, ClientFactoryService>();
            builder.Services.AddScoped<IShortMessagingService, ShortMessagingService>();
            builder.Services.AddTransient<ISenderEmailService, SenderEmailService>();   

            var configuration = builder.Configuration;

            builder.Services.AddHttpClient("ApiHttpClient", client =>
            {
                client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!);
            });
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            } 
            else
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
