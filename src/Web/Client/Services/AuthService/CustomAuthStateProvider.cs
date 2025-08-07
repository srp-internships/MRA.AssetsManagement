using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;

namespace MRA.AssetsManagement.Web.Client.Services.AuthService;

public class CustomAuthStateProvider(
    ILocalStorageService localStorageService,
    HttpClient http,
    IWebAssemblyHostEnvironment environment,
    NavigationManager navigationManager)
    : AuthenticationStateProvider
{
    private readonly string _baseAddress = environment.BaseAddress;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await GetTokenAsync();
        var identity = new ClaimsIdentity();

        if (authToken != null && !string.IsNullOrEmpty(authToken.AccessToken))
        {
            try
            {
                // Check if token is expired
                if (authToken.AccessTokenValidateTo <= DateTime.UtcNow)
                {
                    // Token is expired, clear it
                    await localStorageService.RemoveItemAsync("authToken");
                    await localStorageService.RemoveItemAsync("userName");
                }
                else
                {
                    var claims = ParseClaimsFromJwt(authToken.AccessToken);
                    
                    // Create ClaimsIdentity with proper authentication type
                    identity = new ClaimsIdentity(claims, "jwt", ClaimTypes.Name, ClaimTypes.Email);
                    
                    // Set authorization header for HTTP requests
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        authToken.AccessToken.Replace("\"", ""));
                }
            }
            catch (Exception)
            {
                // Token is invalid, clear it
                await localStorageService.RemoveItemAsync("authToken");
                await localStorageService.RemoveItemAsync("userName");
            }
        }
        else
        {
            // No token found
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        return state;
    }

    private async Task<JwtTokenResponse?> GetTokenAsync()
    {
        var token = await localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
        return token;
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        if (string.IsNullOrEmpty(jwt))
            return Enumerable.Empty<Claim>();
            
        try
        {
            var jwtObject = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            var claims = new List<Claim>();
            
            foreach (var claim in jwtObject.Claims)
            {
                // Map JWT claim types to .NET claim types
                var claimType = claim.Type switch
                {
                    // Standard JWT claim names
                    "nameid" => ClaimTypes.NameIdentifier,
                    "unique_name" => ClaimTypes.Name,
                    "email" => ClaimTypes.Email,
                    
                    // .NET ClaimTypes (these are the actual names in the JWT)
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" => ClaimTypes.NameIdentifier,
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" => ClaimTypes.Name,
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress" => ClaimTypes.Email,
                    
                    // Fallback to original type
                    _ => claim.Type
                };
                
                claims.Add(new Claim(claimType, claim.Value));
            }
            
            return claims;
        }
        catch (Exception)
        {
            return Enumerable.Empty<Claim>();
        }
    }

    private void NavigateToLoginPage()
    {
        navigationManager.NavigateTo("/login", forceLoad: true);
    }

    public async Task<string?> GetUserNameFromToken()
    {
        var token = await localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
        if (token == null || string.IsNullOrEmpty(token.AccessToken)) return null;
        
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.AccessToken) as JwtSecurityToken;
            var claimList = jsonToken?.Claims.ToList() ?? new List<Claim>();
            var username = claimList.FirstOrDefault(cl => cl.Type.Contains("name"))?.Value;

            return username;
        }
        catch
        {
            return null;
        }
    }

    public async Task LogoutUser()
    {
        await localStorageService.RemoveItemAsync("authToken");
        await localStorageService.RemoveItemAsync("userName");
        http.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged();
        navigationManager.NavigateTo("/login", forceLoad: true);
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}