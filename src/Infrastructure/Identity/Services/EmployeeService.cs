using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Identity.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _configuration;
    private readonly string token;

    public EmployeeService(HttpClient http,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        _http = http;
        _configuration = configuration;
        token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");
    }

    public async Task<List<EmployeeResponse>> GetAll()
    {

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response =
            await _http.GetFromJsonAsync<List<EmployeeResponse>>(
                "https://localhost:7245/api/User/GetListUsers/ByFilter");
        return response;
    }

    public async Task<EmployeeResponse> GetById(string id)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _http.GetFromJsonAsync<EmployeeResponse>($"https://localhost:7245/api/User/{id}");

        if (response != null)
        {
            return response;
        }
        else
        {
            throw new Exception("Error: Unable to retrieve user data.");
        }
    }

    public async Task<EmployeeResponse> GetByEmail(string email)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response =
            await _http.GetFromJsonAsync<List<EmployeeResponse>>(
                $"https://localhost:7245/api/User/GetListUsers/ByFilter?Email={email}");

        if (response != null)
        {
            return response.FirstOrDefault();
        }
        else
        {
            throw new Exception("Error: Unable to retrieve user data.");
        }
    }

    public async Task<string> Create(RegisterEmployee registerEmployee)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _http.PostAsJsonAsync("https://localhost:7245/api/Auth/register", registerEmployee);

        if (response.IsSuccessStatusCode)
        {
            var userId = await response.Content.ReadAsStringAsync();
            return userId;
        }
        else
        {
            throw new HttpRequestException($"Failed to create employee. Status code: {response.StatusCode}");
        }
    }
}