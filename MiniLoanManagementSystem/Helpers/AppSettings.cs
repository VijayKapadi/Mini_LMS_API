namespace MiniLoanManagementSystem.Helpers
{
    public class JWTSettings
    {
        public string Secret { get; set; }

        public string JwtTokenIssuer { get; set; }
        public string JwtTokenAudience { get; set; }
        public int JwtTokenExpires { get; set; }
        public string JwtTokenSecretekey { get; set; }
    }
   
}
