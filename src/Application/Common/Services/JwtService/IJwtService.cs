using System.Security.Claims;

namespace MRA.AssetsManagement.Application.Common.Services.JwtService;

public interface IJwtService
{
    string GenerateAccessToken(string userId, string username, string email);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
    string GetUserIdFromToken(string token);
    string GetUsernameFromToken(string token);
} 