using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamPlan.Application.UseCases.Comment.Command.Request;
using TeamPlan.Application.UseCases.Kanban.Command.Request;
using TeamPlan.Application.UseCases.Tasks.Command.Request;
using TeamPlan.Application.UseCases.Tasks.Query.Request;
using TeamPlan.Application.UseCases.Teams.Query.Request;

namespace TeamPlan.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult> ReportByMonth([FromQuery]GetByIdTaskRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpGet("ReportByMonth")]
    public async Task<ActionResult> ReportByMonth([FromQuery]GetReportByMonthRequest request)
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
    
    [HttpPost("CreateComment")]
    public async Task<ActionResult> CreateComment(CreateCommentRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    
    [HttpPost("AddKanbanInTeam")]
    public async Task<ActionResult> AddKanbanInTeam(AddKanbanInTaskRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    
    [HttpPost("CreateSubComment")]
    public async Task<ActionResult> CreateSubComment(AddSubCommentRequest request)
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
    
    [HttpPut("FinishTask")]
    public async Task<ActionResult> FinishTask(FinishTaskRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
    
  
}