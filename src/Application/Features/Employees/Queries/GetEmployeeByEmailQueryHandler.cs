﻿using MediatR;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Application.Features.Employees.Queries;
public class GetEmployeeByEmailQuery : IRequest<Employee>
{
    public string Email { get; set; }

    public GetEmployeeByEmailQuery(string email)
    {
        Email = email;
    }
}

public class GetEmployeeByEmailQueryHandler : IRequestHandler<GetEmployeeByEmailQuery, Employee>
{
    private readonly IEmployeeService _employeeService;

    public GetEmployeeByEmailQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    public async Task<Employee> Handle(GetEmployeeByEmailQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetByEmail(request.Email);
        return response;
    }
}