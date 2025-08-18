using EXPERMIN.CORE.Helpers;
using EXPERMIN.CORE.Services.Implementations;
using EXPERMIN.CORE.Services.Interfaces;
using EXPERMIN.DATABASE.Data;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Implementations;
using EXPERMIN.REPOSITORY.Repositories.Base.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.Jwt;
using EXPERMIN.REPOSITORY.Repositories.Portal.Implementations;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.Storage.Implementations;
using EXPERMIN.REPOSITORY.Repositories.Storage.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.User.Implementations;
using EXPERMIN.REPOSITORY.Repositories.User.Interfaces;
using EXPERMIN.SERIVICE.Services.Implementations;
using EXPERMIN.SERIVICE.Services.Interfaces;
using EXPERMIN.SERVICE.Mappings;
using EXPERMIN.SERVICE.Security.Implementations;
using EXPERMIN.SERVICE.Security.Interfaces;
using EXPERMIN.SERVICE.Services.Portal.Implementations;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Implementations;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using EXPERMIN.SERVICE.Storage.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace EXPERMIN.API
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

                        // Leer el token directamente del header
                        var authHeader = context.Request.Headers["Authorization"].ToString();
                        var rawToken = authHeader.StartsWith("Bearer ") ? authHeader.Substring("Bearer ".Length) : null;

                        Console.WriteLine(">>>> Entró a OnTokenValidated con token: " + rawToken);

                        if (rawToken != null)
                        {
                            bool isRevoked = await tokenService.IsTokenRevoked(rawToken);
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

            #region STORAGE
            var storageConfig = builder.Configuration.GetSection("Storage");
            var provider = storageConfig.GetValue<int>("Provider");

            switch (provider)
            {
                case ConstantHelpers.FILESTORAGE.LOCAL:
                    builder.Services.AddScoped<IFileStorageService, LocalStorageServices>();
                    break;
                case ConstantHelpers.FILESTORAGE.AZURE:
                    //builder.Services.AddScoped<IFileStorageService, S3FileStorageService>();
                    break;
                case ConstantHelpers.FILESTORAGE.AMAZONS3:
                    //builder.Services.AddScoped<IFileStorageService, S3FileStorageService>();
                    break;
                default: // Local
                    builder.Services.AddScoped<IFileStorageService, LocalStorageServices>();
                    break;
            };

            builder.Services.Configure<StorageOptions>(
                 builder.Configuration.GetSection("Storage:Local"));

            #endregion
            #region REPOSITORY / SERVICE
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IJwtRepository, JwtRepository>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserServices>();
            builder.Services.AddScoped<IUserValidationService, UserValidationService>();
            
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            builder.Services.AddScoped<IBannerRepository, BannerRepository>();
            builder.Services.AddScoped<IBannerService, BannerService>();

            builder.Services.AddScoped<IMediaFileRepository, MediaFileRepository>();
            builder.Services.AddScoped<IMediaFileService, MediaFileService>();
            builder.Services.AddScoped<IFileValidatorServices, FileValidatorServices>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

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

            // Servir archivos estáticos desde el storage local
            var localStoragePath = builder.Configuration.GetSection("Storage:Local:BasePath").Value;
            if (!string.IsNullOrEmpty(localStoragePath))
            {
                var absolutePath = Path.GetFullPath(localStoragePath);
                if (!Directory.Exists(absolutePath))
                    Directory.CreateDirectory(absolutePath);

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(localStoragePath),
                    RequestPath = "/uploads" // URL pública -> https://localhost:7020/uploads/...
                });
            }


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

}