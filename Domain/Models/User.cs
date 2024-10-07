using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}