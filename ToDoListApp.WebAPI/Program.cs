namespace ToDoListApp.WebAPI;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using ToDoListApp.BLL.Interfaces;
using ToDoListApp.BLL.Options;
using ToDoListApp.BLL.Services;
using ToDoListApp.DAL.Models;
using ToDoListApp.DAL.Persistence;
using ToDoListApp.DAL.Repositories.Interfaces;
using ToDoListApp.DAL.Repositories.Realizations;
using Serilog;

public static class Program
{
    public static void Main(string[] args)
    {
        var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
        var config = configBuilder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection") ?? string.Empty;

        var builder = WebApplication.CreateBuilder(args);

        JwtSettings jwtSettings = new JwtSettings();
        builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
        var jwtSection = builder.Configuration.GetSection(nameof(JwtSettings));
        builder.Services.Configure<JwtSettings>(jwtSection);

        Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();

        // Add services to the container.
        builder.Services.AddSingleton<ILogger>(Log.Logger);

        builder.Services.AddDbContext<ToDoListAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITasklistRepository, TasklistRepository>();
        builder.Services.AddScoped<ITaskToDoRepository, TaskToDoRepository>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddSingleton<ITokenService, TokenService>();
        builder.Services.AddAuthentication(
        a =>
        {
            a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwt =>
        {
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey ?? throw new InvalidOperationException())),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience?.FirstOrDefault(),
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };
            jwt.Audience = jwtSettings.Audience?.FirstOrDefault();
            jwt.ClaimsIssuer = jwtSettings.Issuer;
        });

        builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
                options.User.RequireUniqueEmail = true;
            })
            .AddSignInManager()
            .AddEntityFrameworkStores<ToDoListAppDbContext>();

        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        builder.Services.AddControllers().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                opts.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoListApp.WebAPI", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    Array.Empty<string>()
                },
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173", "https://present-dodo-equipped.ngrok-free.app/");
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}