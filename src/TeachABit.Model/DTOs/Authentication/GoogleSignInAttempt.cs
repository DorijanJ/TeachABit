namespace TeachABit.Model.DTOs.Authentication
{
    public class GoogleSignInAttempt
    {
        public string Token { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
    }
}
