namespace Infrastructure.ValidationOptions
{
    public class JwtOptions
    {
        public required string SecretKey { get; set; }

        public required string Issuer { get; set; }

        public required string Audience { get; set; }

        public int AccessTokenExpiryMinutes { get; set; }

        public int RefreshTokenExpiryMinutes { get; set; }
    }
}