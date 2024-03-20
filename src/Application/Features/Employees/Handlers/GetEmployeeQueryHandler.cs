using System.Net.Http.Headers;
using System.Net.Http.Json;
using MediatR;
using MRA.AssetsManagement.Domain.Entities;

public class GetEmployeesQuery : IRequest<List<EmployeeResponse>>
{
    public readonly string _token;

    public GetEmployeesQuery(string token)
    {
        _token = token;
    }   
}

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeesQuery,List<EmployeeResponse>>
{
    private readonly HttpClient _http;

    public GetEmployeeQueryHandler(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<EmployeeResponse>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
            request._token.Replace("Bearer ",""));
        var response = await _http.GetFromJsonAsync<List<EmployeeResponse>>("https://localhost:7245/api/User/GetListUsers/ByFilter");

        return response;
    }
}