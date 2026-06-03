namespace FirstDotNETApp.Models
{
    public class UserToken
    {
        public int TokenId { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime ExpiryAt { get; set; }

        public bool IsActive { get; set; }
    }
}
