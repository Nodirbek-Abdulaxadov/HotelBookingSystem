namespace API.ViewModels.Identity
{
    public class AuthResultViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
