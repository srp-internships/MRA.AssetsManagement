using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MRA.AssetsManagement.Domain.Entities;

public class CreateEmployeeCommand : IRequest<string>
{
    public RegisterEmployee RegisterEmployee { get; }
    public string Token { get; }

    public CreateEmployeeCommand(RegisterEmployee registerEmployee, string token)
    {
        RegisterEmployee = registerEmployee;
        Token = token;
    }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
{
    private readonly HttpClient _httpClient;

    public CreateEmployeeCommandHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token.Replace("Bearer ", ""));

        var response = await _httpClient.PostAsJsonAsync("https://localhost:7245/api/Auth/register", request.RegisterEmployee);

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