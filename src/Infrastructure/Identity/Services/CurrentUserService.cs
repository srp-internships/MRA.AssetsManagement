using Microsoft.AspNetCore.Http;
using MRA.AssetsManagement.Application.Common.Security;
using System.Security.Claims;

namespace MRA.AssetsManagement.Infrastructure.Identity.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }

    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
    }

    public string GetUserEmail()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public string GetAuthToken()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers.Authorization;
            if (!string.IsNullOrEmpty(token))
            {
                return token.ToString();
            }
        }
        return string.Empty;
    }
}