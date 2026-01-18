using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamPlan.Application.DTOs.RecurringTask;
using TeamPlan.Application.UseCases.Marks.Command.Request;
using TeamPlan.Application.UseCases.Tasks.Command.Request;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Application.UseCases.Teams.Query.Request;

namespace TeamPlan.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("DashBoard")]
    public async Task<ActionResult> GetDashBoard([FromQuery]DashBoardTeamRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    [HttpGet("HistoryTask")]
    public async Task<ActionResult> HistoryTask([FromQuery]GetHistoryTaskByTeamRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    [HttpPost("AddMemberInTeam")]
    public async Task<ActionResult> AddMemberInTeam(AddMemberInTeamRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
    [HttpPost("CreateRecurringTask")]
    public async Task<ActionResult> CreateRecurringTask(AddRecurringTaskRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    [HttpPost("CreateTask")]
    public async Task<ActionResult> CreateTask(CreateTaskRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    [HttpPost("CreateMark")]
    public async Task<ActionResult> CreateMark(CreateMarkRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    [HttpPut("FinishTask")]
    public async Task<ActionResult> FinishTask(FinishTaskRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpDelete("RemoveMember")]
    public async Task<ActionResult> RemoveMember([FromQuery] RemoveMemberInTeamRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(): BadRequest(result.Error);
    }
} 