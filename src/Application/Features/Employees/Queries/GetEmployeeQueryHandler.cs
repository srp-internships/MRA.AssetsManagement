using MediatR;
using MRA.AssetsManagement.Domain.Entities;

public class GetEmployeesQuery : IRequest<List<EmployeeResponse>>
{
}

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeesQuery,List<EmployeeResponse>>
{
    private readonly IEmployeeService _employeeService;
    public GetEmployeeQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    public async Task<List<EmployeeResponse>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetAll();
        return response;
    }
}