using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Core.Mappings;
using ECommerceAPI.Core.Services;
using ECommerceAPI.Core.Validators.User;
using ECommerceAPI.Domain.Interfaces;
using ECommerceAPI.Infrastructure.Data;
using ECommerceAPI.Infrastructure.Repositories;
using ECommerceAPI.UI.Filters;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(
    typeof(MappingProfile).Assembly,
    typeof(Program).Assembly
);

builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();


//DI LIFETIME FOR REPOSITORY
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

//DI LIFETIME FOR SERVICE
builder.Services.AddScoped<IProductWriteService, ProductService>();
builder.Services.AddScoped<IProductReadService, ProductService>();
builder.Services.AddScoped<ICategoryWriteService, CategoryService>();
builder.Services.AddScoped<ICategoryReadService, CategoryService>();
builder.Services.AddScoped<IOrderWriteService, OrderService>();
builder.Services.AddScoped<IOrderReadService, OrderService>();
builder.Services.AddScoped<ICartWriteService, CartService>();
builder.Services.AddScoped<ICartReadService, CartService>();
builder.Services.AddScoped<IUserWriteService, UserService>();
builder.Services.AddScoped<IUserReadService, UserService>();
builder.Services.AddScoped<IAddressWriteService, AddressService>();
builder.Services.AddScoped<IAddressReadService, AddressService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"]!;

if (string.IsNullOrWhiteSpace(secretKey)) throw new Exception("JWT SecretKey is missing");

if (secretKey.Length < 32) throw new Exception("JWT SecretKey must be at least 32 characters long");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Bearer {token}"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
