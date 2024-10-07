using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Extensions;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapster();
MapsterConfig.VacancyMappings();

builder.Services.AddSwaggerWithJwtAuthentication();
builder.Services.AddAuthenticationWithJwtTokenSettings(builder.Configuration);
builder.Services.AddIdentityCore<User>(
        options =>
        {
            var passwordSettings = builder.Configuration.GetSection("PasswordValidation");

            options.Password.RequiredUniqueChars = passwordSettings.GetValue<int>("RequiredUniqueChars");
            options.Password.RequireUppercase = passwordSettings.GetValue<bool>("RequireUppercase");
            options.Password.RequiredLength = passwordSettings.GetValue<int>("RequiredLength");
        })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });

    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
    });

    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter the JWT token",
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
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
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();
app.Run();