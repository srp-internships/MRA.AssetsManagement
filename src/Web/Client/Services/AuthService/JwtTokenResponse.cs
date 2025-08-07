namespace MRA.AssetsManagement.Web.Client.Services.AuthService;

public class JwtTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenValidateTo { get; set; }
} 