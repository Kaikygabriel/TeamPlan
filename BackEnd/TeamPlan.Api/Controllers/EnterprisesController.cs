using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamPlan.Application.UseCases.Enterprises.Command.Request;
using TeamPlan.Application.UseCases.Enterprises.Query.Request;

namespace TeamPlan.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EnterprisesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnterprisesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateEnterpriseRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    [HttpPost("AddTeam")]
    public async Task<ActionResult> AddTeam(AddTeamInEnterpriseRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    [HttpGet("DashBoard")]
    public async Task<ActionResult> GetDashBoard([FromQuery]DashBoardEnterprise request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    [HttpDelete("RemoveTeam")]
    public async Task<ActionResult> RemoveMember([FromQuery] RemoveTeamRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(): BadRequest(result.Error);
    }
}