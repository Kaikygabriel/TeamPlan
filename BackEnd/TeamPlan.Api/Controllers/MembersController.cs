using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamPlan.Application.UseCases.Members.Command.Request;

namespace TeamPlan.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterMemberRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginMemberRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}