using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using MRA.Identity.Application.Contract.User.Responses;

namespace MRA.AssetsManagement.Web.Client.Services.AuthService;

public class CustomAuthStateProvider(
    ILocalStorageService localStorageService,
    HttpClient http,
    IConfiguration configuration,
    IWebAssemblyHostEnvironment environment,
    NavigationManager navigationManager)
    : AuthenticationStateProvider
{
    private readonly string _baseAddress = environment.BaseAddress;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await ProcessAuthTokens();
        var authToken = await GetTokenAsync();
        var identity = new ClaimsIdentity();

        if (authToken != null)
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken.AccessToken), "jwt");
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                authToken.AccessToken.Replace("\"", ""));
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        if (!state.User.Identity?.IsAuthenticated ?? true)
        {
            NavigateToLoginPage();
        }

        return state;
    }
    private async Task ProcessAuthTokens()
    {
        var currentUri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        var query = QueryHelpers.ParseQuery(currentUri.Query);
        JwtTokenResponse token = new();
        if (query.TryGetValue("atoken", out var atoken))
        {
            token.AccessToken = atoken;
        }

        if (query.TryGetValue("rtoken", out var rtoken))
        {
            token.RefreshToken = rtoken;
        }

        if (query.TryGetValue("vdate", out var vdate))
        {
            DateTime oDate = Convert.ToDateTime(vdate);
            token.AccessTokenValidateTo = oDate;
            await localStorageService.SetItemAsync("authToken", token);
            var userName = await GetUserNameFromToken();
            await localStorageService.SetItemAsync("userName", userName);
        var uriWithoutQuery = new UriBuilder(currentUri.Scheme, currentUri.Host, currentUri.Port, currentUri.AbsolutePath).Uri;
        navigationManager.NavigateTo(uriWithoutQuery.ToString(), forceLoad: true);
        }
    }

    private async Task<JwtTokenResponse?> GetTokenAsync()
    {
        var token = await localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
        return token;
    }
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var jwtObject = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
        return jwtObject.Claims;
    }
    private void NavigateToLoginPage()
    {
        navigationManager.NavigateTo($"{configuration["IdentityClient"]}/login?callback={_baseAddress}", forceLoad: true);
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

    public async Task LogoutUser()
    {
        await localStorageService.RemoveItemAsync("authToken");
        await localStorageService.RemoveItemAsync("userName");
        navigationManager.NavigateTo(
            $"{configuration["IdentityClient"]}/logout?callback=/login?callback={_baseAddress}");
    }
}

