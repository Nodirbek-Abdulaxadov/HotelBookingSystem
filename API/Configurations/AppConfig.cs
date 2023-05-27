using API.Identity;
using API.Interfaces;
using Datalayer.Context;
using Entities;
using Datalayer.Interfaces;
using Datalayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BLL.Interfaces;
using BLL.Services;

namespace API.Configurations
{
    public static class AppConfig
    {
        static readonly string CORSOpenPolicy = "OpenCORSPolicy";
        public static void AddServices(this WebApplicationBuilder builder)
        {
            //API Services
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add DI Services
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IRoomTypeInterface, RoomTypeRepository>();
            builder.Services.AddTransient<IRoomInterface, RoomRepository>();
            builder.Services.AddTransient<IOrderInterface, OrderRepository>();
            builder.Services.AddTransient<IServiceInterface, ServiceRepository>();
            builder.Services.AddTransient<IReceiptInterface, ReceiptRepository>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddTransient<IImageService, ImageService>();
            builder.Services.AddTransient<IRoomTypeService, RoomTypeService>();
            builder.Services.AddTransient<IRoomService, RoomService>();
            builder.Services.AddTransient<IOrderService, OrderService>();

            //Add dbContext
            builder.Services.AddDbContext<HotelDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDB")));

            //Add Identity
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HotelDbContext>()
            .AddDefaultTokenProviders();

            //Add Authentication
            var tokenParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:securityKey"])),

                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["JwtSettings:Audence"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            builder.Services.AddSingleton(tokenParameters);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = tokenParameters;
                });

            //Add cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                  name: CORSOpenPolicy,
                  builder => {
                      builder.WithOrigins("https://hotel.1kb.uz/", "http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                  });
            });
        }

        public static void AddMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.UseStaticFiles();

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors(CORSOpenPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Auth}/{action=Login}/{id?}"
                );
                endpoints.MapControllers();
            });
            app.SeedRolesToDatabase().Wait();

            app.Run();
        }
    }
}
