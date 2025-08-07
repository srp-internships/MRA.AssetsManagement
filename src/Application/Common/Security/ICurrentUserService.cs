namespace MRA.AssetsManagement.Application.Common.Security;
public interface ICurrentUserService
{
    string GetUserId();
    string GetUserName();
    string GetUserEmail();
}