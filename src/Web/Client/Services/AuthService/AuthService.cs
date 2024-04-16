using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using MRA.Identity.Application.Contract.User.Responses;

namespace MRA.AssetsManagement.Web.Client.Services.AuthService;

public class AuthService(
    IConfiguration configuration,
    CustomAuthStateProvider customAuthStateProvider,
    NavigationManager navigationManager,
    ILocalStorageService localStorageService,
    IWebAssemblyHostEnvironment environment)
    : IAuthService
{
    private readonly string _baseAddress = environment.BaseAddress;

    public async Task AddTokenToLocal()
    {
        JwtTokenResponse token = new();
        var currentUri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        if (QueryHelpers.ParseQuery(currentUri.Query).TryGetValue("atoken", out var atoken))
        {
            token.AccessToken = atoken;
        }
        
        if (QueryHelpers.ParseQuery(currentUri.Query).TryGetValue("rtoken", out var rtoken))
        {
            token.RefreshToken = rtoken;
        }
        
        if (QueryHelpers.ParseQuery(currentUri.Query).TryGetValue("vdate", out var vdate))
        {
            DateTime oDate = Convert.ToDateTime(vdate);
            token.AccessTokenValidateTo = oDate;
            await localStorageService.SetItemAsync("authToken", token);
            var userName = await GetUserNameFromToken();
            await localStorageService.SetItemAsync("userName", userName);
            await customAuthStateProvider.GetAuthenticationStateAsync();
        }
    }
    public async Task LogoutUser()
    {
        await localStorageService.RemoveItemAsync("authToken");
        await localStorageService.RemoveItemAsync("userName");
        navigationManager.NavigateTo($"{configuration["IdentityClient"]}/logout?callback=/login?callback={_baseAddress}");
    }
    public async Task<string?> GetUserNameFromToken()
    {
        var token = await localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token!.AccessToken) as JwtSecurityToken;
        var claimList = jsonToken!.Claims.ToList();
        var username = claimList.FirstOrDefault(cl => cl.Type.Contains("username"))?.Value;
        
        return username;
    }
}