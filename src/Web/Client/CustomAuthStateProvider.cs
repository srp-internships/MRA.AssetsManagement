using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MRA.Identity.Application.Contract.User.Responses;

namespace MRA.AssetsManagement.Web.Client;

public class CustomAuthStateProvider
    : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    
    private readonly HttpClient _http;

    public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
    {
        this._localStorageService = localStorageService;
        _http = http;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await GetTokenAsync();
        var identity = new ClaimsIdentity();
        _http.DefaultRequestHeaders.Authorization = null;
        if (authToken != null)
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken.AccessToken), "jwt");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.AccessToken.Replace("\"", ""));
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }
    
    private async Task<JwtTokenResponse?> GetTokenAsync()
    {
        var token = await _localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
        if (token == null!)
        {
            return null;
        }

        return token;
    }
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var jwtObject = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
        return jwtObject.Claims;
    }
}

