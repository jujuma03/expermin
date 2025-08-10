using EXPERMIN.CORE.Helpers;
using EXPERMIN.DATABASE.Data;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Implementations;
using EXPERMIN.REPOSITORY.Repositories.Base.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.Jwt;
using EXPERMIN.REPOSITORY.Repositories.User.Implementations;
using EXPERMIN.REPOSITORY.Repositories.User.Interfaces;
using EXPERMIN.SERVICE.Mappings;
using EXPERMIN.SERVICE.Security.Implementations;
using EXPERMIN.SERVICE.Security.Interfaces;
using EXPERMIN.SERVICE.Services.User.Implementations;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;

namespace CLUBACCESS.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region DATABASE
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
            builder.Services.AddDbContext<ExperminContext>(options =>
                options.UseSqlServer(connectionString));
            #endregion

            #region IDENTITY
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(op =>
            {
                op.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ExperminContext>()
            .AddDefaultTokenProviders();
            #endregion


            #region JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var tokenService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();
                        var token = context.SecurityToken as JwtSecurityToken;

                        if (token != null)
                        {
                            bool isRevoked = await tokenService.IsTokenRevoked(token.RawData);
                            if (isRevoked)
                            {
                                context.Fail("Token revocado.");
                            }
                        }
                    }
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", ConstantHelpers.ROLES.ADMIN));
            });
            #endregion

            #region REPOSITORY / SERVICE
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IJwtRepository, JwtRepository>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserServices>();

            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            builder.Services.AddHttpContextAccessor();

            #endregion

            // Add services to the container.
            builder.Services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddJsonOptions(op =>
            {
                op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(cfg =>
            {
            }, typeof(AutoMapperProfile).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

}