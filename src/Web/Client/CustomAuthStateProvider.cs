using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MRA.Identity.Application.Contract.User.Responses;

namespace MRA.AssetsManagement.Web.Client;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _http;
    private readonly IConfiguration _configuration;
    private readonly NavigationManager _navigationManager;
    private readonly string _baseAddress;

    public CustomAuthStateProvider(ILocalStorageService localStorageService,
        HttpClient http,
        IConfiguration configuration,
        IWebAssemblyHostEnvironment environment,
        NavigationManager navigationManager)
    {
        this._localStorageService = localStorageService;
        _http = http;
        _configuration = configuration;
        _navigationManager = navigationManager;
        _baseAddress = environment.BaseAddress;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var path = $"{_configuration["IdentityClient"]}login?callback={_baseAddress}";
        var authToken = await GetTokenAsync();
        var identity = new ClaimsIdentity();
        _http.DefaultRequestHeaders.Authorization = null;
        if (authToken != null)
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken.AccessToken), "jwt");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                authToken.AccessToken.Replace("\"", ""));
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        if (state.User.Identity == null || !state.User.Identity.IsAuthenticated)
        {
            _navigationManager.NavigateTo(path, forceLoad: true);
        }
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

