using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using AutoMapper;

using Microsoft.Extensions.Configuration;

using MRA.AssetsManagement.Application.Common.Security;
using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Application.Features.Employees;
using MRA.AssetsManagement.Domain.Entities.Employee;
using MRA.AssetsManagement.Web.Shared.Employees;
using MRA.Identity.Application.Contract.User.Commands.UsersByApplications;

namespace MRA.AssetsManagement.Infrastructure.Identity.Services;

public class EmployeeService(
    HttpClient http,
    IHttpClientFactory httpClientFactory,
    ICurrentUserService currentUserService,
    IConfiguration configuration,
    IMapper mapper)
    : IEmployeeService
{
    private readonly string _apiBaseUrl = configuration["IdentityServer"]!;
    
    public async Task<List<Employee>> GetAll()
    {
        var httpClient = httpClientFactory.CreateClient("MraAssetsManagement");
        SetAuthorizationHeader();
        var command = new GetListUsersCommand()
        {
            ApplicationId = Guid.Parse(configuration["Application:Id"]),
            ApplicationClientSecret = configuration["Application:ClientSecret"]
        };

        var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/User/GetListUsersCommand/ByFilter", command);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<List<EmployeeResponse>>();
            if (result is not null)
            {
                var employees = mapper.Map<List<Employee>>(result);
                return employees;
            }
        }

        if (response.StatusCode == HttpStatusCode.InternalServerError)
            throw new Exception("Server is not responding");

        return new List<Employee>();
    }

    public async Task<Employee?> GetByUserName(string userName)
    {
        var httpClient = httpClientFactory.CreateClient("MraAssetsManagement");
        var command = new GetUserByKeyCommand
        {
            ApplicationId = Guid.Parse(configuration["Application:Id"]),
            ApplicationClientSecret = configuration["Application:ClientSecret"],
            Key = userName
        };
        SetAuthorizationHeader();
        var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/User/{userName}", command);
        var employee = new Employee();
        if (response.StatusCode != HttpStatusCode.OK)
            return employee;

        var result = await response.Content.ReadFromJsonAsync<EmployeeResponse>();
        employee = mapper.Map<Employee>(result);

        return employee;
    }

    public async Task<Employee?> GetByEmail(string email)
    {
        SetAuthorizationHeader();
        var response = await http.GetFromJsonAsync<List<EmployeeResponse>>(
            $"{_apiBaseUrl}/User/GetListUsers/ByFilter?Email={email}");
        return response is null ? null : mapper.Map<Employee>(response.FirstOrDefault());
    }

    public async Task<string> Create(CreateEmployeeRequest createEmployeeRequest)
    {
       
        SetAuthorizationHeader();
        var response = await http.PostAsJsonAsync($"{_apiBaseUrl}/User/CreateEmployee", createEmployeeRequest);
        var userId = await response.Content.ReadAsStringAsync();
        return userId;
    }

    private void SetAuthorizationHeader()
    {
        var token = currentUserService.GetAuthToken().Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

public class GetUserByKeyCommand
{
    public string Key { get; set; }
    public Guid? ApplicationId { get; set; }
    public string ApplicationClientSecret { get; set; }
}