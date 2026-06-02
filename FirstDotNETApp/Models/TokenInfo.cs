namespace FirstDotNETApp.Models
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string UserId { get; set; }
    }
}