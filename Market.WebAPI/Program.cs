using Market.BLL.AuthBL.Services;
using Market.BLL.Helpers;
using Market.BLL.MarketBL.Services;
using Market.DAL.Data;
using Market.DAL.Data.Repositories;
using Market.DAL.Entities.Identity;
using Market.DAL.Entities.Market;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Добавление db context и настройка библиотеки Identity для работы с пользователями и ролями.
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddIdentity<User, Role>().
    AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//Добавление экземпляров репозитория для каждой нужной сущности
builder.Services.AddTransient<IRepository<Shop>, Repository<Shop>>();
builder.Services.AddTransient<IRepository<Product>, Repository<Product>>();
builder.Services.AddTransient<IRepository<User>, Repository<User>>();
builder.Services.AddTransient<IRepository<Role>, Repository<Role>>();
builder.Services.AddTransient<IRepository<IdentityUserRole<int>>, Repository<IdentityUserRole<int>>>();
builder.Services.AddTransient<ProductRepository>();

//Добавление экземпляров сервисов для работы с контроллерами
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<GetService>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<AdminService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<SellerManagerService>();

//Добавление Automapper и добавление к нему класса содержащего конфигурацию
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//Настройка Swagger для работы с Bearer token
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.0",
        Title = "MaxSoftMarket API",
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
//Добавил некоторую политику корс, разрешающую использовать все скрипты и виды сообщений
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
//Простенькая настройка аутентификации для JWT токенов
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
            ValidAudience = builder.Configuration["Jwt:Audience"]!,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy =
    new AuthorizationPolicyBuilder
            (JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

app.UseSwagger();
//Добавил тут эндпоинт для страницы со сваггером (для веб-деплоя)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaxSoftMarket");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
