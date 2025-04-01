using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public int Age { get; set; }

    }
}