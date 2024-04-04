using System.Net.Http.Headers;
using System.Net.Http.Json;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MRA.AssetsManagement.Application.Common.Security;
using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Application.Features.Employees;
using MRA.AssetsManagement.Domain.Entities.Employee;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Infrastructure.Identity.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _http;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly string _apiBaseUrl;
    public EmployeeService(HttpClient http,
        ICurrentUserService currentUserService,
        IConfiguration configuration,
        IMapper mapper)
    {
        _http = http;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _apiBaseUrl = configuration.GetValue<string>("IdentityServer")!;
    }

    public async Task<List<Employee>> GetAll()
    {
        SetAuthorizationHeader();
        var response = await _http.GetFromJsonAsync<List<EmployeeResponse>>(
            $"{_apiBaseUrl}User/GetListUsers/ByFilter");
        if (response is not null)
        {
            var emplooyes = _mapper.Map<List<Employee>>(response);
            return emplooyes;
        }
        return new List<Employee>();
    }

    public async Task<Employee?> GetById(string id)
    {
        SetAuthorizationHeader();
        var response = await _http.GetFromJsonAsync<EmployeeResponse>($"{_apiBaseUrl}User/{id}");
        var employee = _mapper.Map<Employee>(response);
        return employee;
    }

    public async Task<Employee?> GetByEmail(string email)
    {
        SetAuthorizationHeader();
        var response = await _http.GetFromJsonAsync<List<EmployeeResponse>>(
                $"{_apiBaseUrl}User/GetListUsers/ByFilter?Email={email}");
        return response is null ? null :  _mapper.Map<Employee>( response.FirstOrDefault());         
        
    }

    public async Task<string> Create(CreateEmployeeRequest createEmployeeRequest)
    {
        var response = await _http.PostAsJsonAsync($"{_apiBaseUrl}Auth/register", createEmployeeRequest);
        var userId = await response.Content.ReadAsStringAsync();
        return userId;
    }
    private void SetAuthorizationHeader()
    {
        var token = _currentUserService.GetAuthToken().Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}