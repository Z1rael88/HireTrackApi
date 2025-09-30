using System.Security.Claims;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Initializers
{
    public static class SystemAdministratorInitializer
    {
        public static async ValueTask InitializeSystemAdministratorAsync(
            IServiceProvider serviceProvider,
            IConfiguration config)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var userRepository = unitOfWork.Repository<User>();

            var email = config["Logging:SystemAdminSettings:Email"]!;
            var userName = config["Logging:SystemAdminSettings:UserName"]!;
            var password = config["Logging:SystemAdminSettings:Password"]!;

            var systemAdminRole = await roleManager.FindByNameAsync(Role.SystemAdministrator.ToString());
            if (systemAdminRole == null)
            {
                throw new NullReferenceException("System Administration role creation failed.");
            }

            var existingAdminUser = await userManager.FindByNameAsync(userName!);
            if (existingAdminUser != null)
            {
                return;
            }

            var adminUserId = 1;

            var adminUser = new User
            {
                Id = adminUserId,
                Email = email,
                UserName = userName,
            };

            httpContextAccessor.HttpContext = new DefaultHttpContext();
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Sid, adminUserId.ToString()));
            httpContextAccessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

            await userManager.CreateAsync(adminUser, password);
            await userManager.AddToRoleAsync(adminUser, Role.SystemAdministrator.ToString());
        }
    }
}