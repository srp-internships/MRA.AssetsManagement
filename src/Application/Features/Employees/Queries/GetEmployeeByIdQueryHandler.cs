using MediatR;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Application.Features.Employees.Queries;

public class GetEmployeeByIdQuery : IRequest<Employee>
{
    public string Id { get; set; }
    public GetEmployeeByIdQuery(string id)
    {
        Id = id;
    }
}
public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IEmployeeService _employeeService;
    public GetEmployeeByIdQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetById(request.Id);
        return response;
    }
}