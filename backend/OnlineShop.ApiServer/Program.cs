using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services;
using OnlineShop.Data.EntityFramework;
using OnlineShop.Data.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;
using IdentityPasswordHasherLib;
using Microsoft.AspNetCore.HttpLogging;
using OnlineShop.WebApi;
using OnlineShop.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.WebApi.Configurations;
using OnlineShop.WebApi.Services;

namespace OnlineShop.ApiServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors();

            JwtConfig jwtConfig = builder.Configuration.GetRequiredSection("JwtConfig").Get<JwtConfig>()!;
            if (jwtConfig is null) { throw new InvalidOperationException("JwtConfig is not configured"); }
            builder.Services.AddSingleton(jwtConfig);
            builder.Services.AddSingleton<ITokenService, TokenService>();

            var dbPath = "myapp.db";
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepositoryEf>();
            builder.Services.AddScoped<IAccountRepository, AccountRepositoryEf>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddSingleton<IApplicationPasswordHasher, IdentityPasswordHasher>();

            builder.Services.AddHttpLogging(options => //настройка
            {
                options.LoggingFields = HttpLoggingFields.RequestHeaders
                                        | HttpLoggingFields.ResponseHeaders
                                        | HttpLoggingFields.RequestBody
                                        | HttpLoggingFields.ResponseBody;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,

                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudiences = new[] { jwtConfig.Audience },
                    ValidIssuer = jwtConfig.Issuer
                };
            });
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                } catch (Exception ex)
                {
                    await context.Response.WriteAsync("Server error");
                    return;
                }
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });

            app.MapControllers();

            app.UseHttpLogging();

            app.UseMiddleware<TransitionCounterMiddleware>();

            app.Run();
        }
    }
}