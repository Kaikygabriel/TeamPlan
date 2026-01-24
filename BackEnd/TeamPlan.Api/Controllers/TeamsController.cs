using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamPlan.Application.UseCases.Kanban.Command.Request;
using TeamPlan.Application.UseCases.Marks.Command.Request;
using TeamPlan.Application.UseCases.RecurringTask.Command.Request;
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
    [HttpPost("CreateKanban")]
    public async Task<ActionResult> CreateKanban(AddKanbanRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    [HttpPost("CreateTeam")]
    public async Task<ActionResult> CreateTeam(CreateTeamRequest request)
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

    [HttpDelete("RemoveMember")]
    public async Task<ActionResult> RemoveMember([FromQuery] RemoveMemberInTeamRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(): BadRequest(result.Error);
    }
    [HttpDelete("RemoveRecurringTask")]
    public async Task<ActionResult> RemoveRecurringTask([FromQuery] RemoveRecurringTaskRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(): BadRequest(result.Error);
    }
    
    [HttpDelete("RemoveTeam")]
    public async Task<ActionResult> RemoveteTeam([FromQuery]RemoveTeamRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(): BadRequest(result.Error);
    }
    [HttpDelete("RemoveKanban")]
    public async Task<ActionResult> DeleteKanban(RemoveKanbanRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
} 