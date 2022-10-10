namespace Sample.Account.Api.ViewModels
{
    public class JwtTokenViewModel
    {
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
