using Application.Dtos;
using Application.Dtos.User;
using Application.Dtos.Vacancy;
using Application.Initializers;
using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using Application.Validators;
using Domain.Models;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.ValidationOptions;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.Extensions;
using Presentation.Middlewares;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMapster();
MapsterConfig.VacancyMappings();
MapsterConfig.UserMappings();
MapsterConfig.ResumeMappings();

builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddSwaggerWithJwtAuthentication();
builder.Services.AddAuthenticationWithJwtTokenSettings(builder.Configuration);
builder.Services.AddIdentityCore<User>(
        options =>
        {
            options.Password.RequiredUniqueChars = 2;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 10;
        })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUser, CurrentUserService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<ICrmService, CrmService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<GlobalExceptionHandler>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpOptions"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5176")
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
await RolesInitializer.InitializeRolesAsync(app.Services);
await SystemAdministratorInitializer.InitializeSystemAdministratorAsync(app.Services, builder.Configuration);
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();
app.Run();