<<<<<<< HEAD
﻿using MediatR;

=======
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[ApiController, Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}