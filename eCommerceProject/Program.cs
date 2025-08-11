using eCommerceProject.BLL.Service.Classes;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.Model;
using eCommerceProject.DAL.Repositories.Classes;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.DAL.Utilts;
using eCommerceProject.Data;
using eCommerceProject.Model;
using eCommerceProject.PL.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Globalization;
using System.Text;

namespace eCommerceProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //  Add services to the container
            builder.Services.AddControllers();

            //  Localization
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resource");
            var supportedCultures = new[] { new CultureInfo("en-GB"), new CultureInfo("ar") };
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-GB");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider { QueryStringKey = "Lang" }
                };
            });

            //  Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //  Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireDigit = true;
                Options.Password.RequiredLength = 8;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequireNonAlphanumeric = false;
                Options.User.RequireUniqueEmail = true; 
            })
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //  Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //  Register Repositories & Services
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();


            //  Register SeedData
            builder.Services.AddScoped<ISeedData, SeedData>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IEmailSender, EmailSetting>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
            };
        });

            builder.Services.AddOpenApi();

            var app = builder.Build();

            //  Apply Seeding
                var scope = app.Services.CreateScope();
                var objectOfSeedData = scope.ServiceProvider.GetRequiredService<ISeedData>();
                await objectOfSeedData.DataSeedingAsync();
                await objectOfSeedData.IdentityDataSeedingAsync();


            //  Use Localization
            var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            //  Swagger UI
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapScalarApiReference();

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
