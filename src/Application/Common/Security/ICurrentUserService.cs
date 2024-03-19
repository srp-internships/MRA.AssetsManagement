﻿namespace MRA.AssetsManagement.Application.Common.Security;
public interface ICurrentUserService
{
    Guid? GetUserId();
    string GetEmail();
    string GetUserName();
    List<string> GetRoles();

    string GetAuthToken();
}