using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamPlan.Application.UseCases.Owners.Commands.Request;

namespace TeamPlan.Api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class OwnersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OwnersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterOwnerRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginOwnerRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}