using MediatR;
using MRA.AssetsManagement.Domain.Entities;

public class GetEmployeeByIdQuery : IRequest<EmployeeResponse>
{
    public readonly string _id;

    public GetEmployeeByIdQuery(string id)
    {
        _id = id;
    }
}
public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponse>
{
    private readonly IEmployeeService _employeeService;
    public GetEmployeeByIdQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<EmployeeResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetById(request._id);
        return response;
    }
}