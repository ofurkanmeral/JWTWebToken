namespace JWTWebToken.Security
{
    public class JwtTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresTime { get; set; }
    }
}
