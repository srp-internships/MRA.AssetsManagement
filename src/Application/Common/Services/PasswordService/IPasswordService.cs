namespace MRA.AssetsManagement.Application.Common.Services.PasswordService;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
} 