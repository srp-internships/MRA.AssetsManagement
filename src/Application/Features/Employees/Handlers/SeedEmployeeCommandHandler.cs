using System.Net.Http.Json;
using MediatR;
using MRA.AssetsManagement.Domain.Entities;
public class SeedEmployeeCommand : IRequest<string>
{
    public readonly List<RegisterEmployee> _registerEmployeeCommands;

    public SeedEmployeeCommand(List<RegisterEmployee> registerEmployeeCommands)
    {
        _registerEmployeeCommands = registerEmployeeCommands;
    }
}

public class SeedEmployeeCommandHandler : IRequestHandler<SeedEmployeeCommand, string>
{
    private readonly HttpClient _http;

    public SeedEmployeeCommandHandler(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> Handle(SeedEmployeeCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request._registerEmployeeCommands)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7245/api/Auth/register",
                item); 
            if (response.IsSuccessStatusCode)
            {
                var createdEmployee = await response.Content.ReadAsStringAsync();
            }
        }
        return "Ok";
    }
}