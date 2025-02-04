using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.BusinessLogic.Concretes;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.Common.Concretes;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.Concretes;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.Services.Abstracts;
using KPBrokers.Submission.Quote.Services.Concretes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using System.Text;
using AdminBusinessLogic = KPBrokers.Submission.Quote.BusinessLogic.Concretes.AdminBusinessLogic;

namespace KPBrokers.Submission.Quote.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure logging
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            builder.Logging.AddNLog();

            // Configure database context and services
            ConfigureDatabase(builder);
            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure middleware
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureDatabase(WebApplicationBuilder builder)
        {
            // Register DbContext with connection string
            builder.Services.AddDbContext<KPBDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("KPBQuoteSubmissionDBContext")));          

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {                      
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,                      
                      ValidIssuer = builder.Configuration["Jwt:Issuer"],
                      ValidAudience = builder.Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
                  };
              });
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Register controllers
            services.AddControllers();

            services.AddScoped<ILoggerService, LoggerService>();            
            services.AddScoped<IAuthenticationBusinessLogic, AuthenticationBusinessLogic>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<ILookupRepository, LookupRepository>();
            services.AddScoped<ILookUpBusinessLogic, LookUpBusinessLogic>();
            services.AddScoped<IEmailService, EmailService>();            
            services.AddScoped<IAdminBusinessLogic, AdminBusinessLogic>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IAgentBusinessLogic, AgentBusinessLogic>();
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<IUserAccountBusinessLogic, UserAccountBusinessLogic>();
            services.AddScoped<IUserAccountService, UserAccountService>();

            // Add Swagger for API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KPBrokers API", Version = "v1" });
            });
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable Swagger UI for development
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "KPBrokers API V1");
                    options.RoutePrefix = string.Empty; // Serve Swagger at the app's root
                });
            }
            else
            {
                app.UseExceptionHandler("/error"); // Custom error handling for production
            }

            // Enable routing, authentication, and authorization
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controller routes
            app.MapControllers();
        }
    }
}
