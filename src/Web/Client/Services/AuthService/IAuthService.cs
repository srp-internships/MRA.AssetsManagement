namespace MRA.AssetsManagement.Web.Client.Services.AuthService;

public interface IAuthService
{
    Task AddTokenToLocal();
    Task LogoutUser();
    Task<string?> GetUserNameFromToken();
}