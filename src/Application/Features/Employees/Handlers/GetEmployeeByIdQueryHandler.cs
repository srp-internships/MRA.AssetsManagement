using System.Net.Http.Headers;
using System.Net.Http.Json;
using MediatR;
using MRA.AssetsManagement.Domain.Entities;

public class GetEmployeeByIdQuery : IRequest<EmployeeResponse>
{
    public readonly string _id;
    public readonly string _token;

    public GetEmployeeByIdQuery(string id, string token)
    {
        _id = id;
        _token = token;
    }
}

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponse>
{
    private readonly HttpClient _http;

    public GetEmployeeByIdQueryHandler(HttpClient http)
    {
        _http = http;
    }

    public async Task<EmployeeResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
            request._token.Replace("Bearer ",""));
        
        var response = await _http.GetFromJsonAsync<EmployeeResponse>($"https://localhost:7245/api/User/{request._id}");
        
        if (response != null)
        {
            return response;
        }
        else
        {
            throw new Exception("Error: Unable to retrieve user data.");
        }
    }
}