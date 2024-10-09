using Microsoft.AspNetCore.Identity;

namespace Application.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException(string message, IEnumerable<IdentityError> errors)
            : base(message)
        {
            Errors = errors.Select(x => x.Description).ToList();
        }

        public ICollection<string> Errors { get; }
    }
}