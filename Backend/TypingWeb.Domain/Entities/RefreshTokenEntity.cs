namespace TypingWeb.Domain.Models.Entities
{
    public class RefreshTokenEntity
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;
    }
}
