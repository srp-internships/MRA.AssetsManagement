using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using MRA.Identity.Application.Contract.User.Responses;

namespace MRA.AssetsManagement.Web.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly CustomAuthStateProvider _customAuthStateProvider;
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorageService;
    private readonly string _baseAddress; 
    
    public AuthService(IConfiguration configuration,
        CustomAuthStateProvider customAuthStateProvider,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService,
        IWebAssemblyHostEnvironment environment)
    {
        _configuration = configuration;
        _customAuthStateProvider = customAuthStateProvider;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _baseAddress = environment.BaseAddress;
    }
    public async Task AddTokenToLocal()
    {
        JwtTokenResponse token = new();
        var currentUri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
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
            await _localStorageService.SetItemAsync("authToken", token);
            var userName = await GetUserNameFromToken();
            await _localStorageService.SetItemAsync("userName", userName);
            await _customAuthStateProvider.GetAuthenticationStateAsync();
        }
    }
    public async Task LogoutUser()
    {
        await _localStorageService.RemoveItemAsync("authToken");
        await _localStorageService.RemoveItemAsync("userName");
        _navigationManager.NavigateTo($"{_configuration["IdentityClient"]}logout?callback=/login?callback={_baseAddress}");
    }
    public async Task<string?> GetUserNameFromToken()
    {
        var token = await _localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token.AccessToken) as JwtSecurityToken;
        var claimList = jsonToken.Claims.ToList();
        var username = claimList.FirstOrDefault(cl => cl.Type.Contains("username"))?.Value;
        
        return username;
    }
}