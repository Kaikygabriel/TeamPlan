using MediatR;
using TeamPlan.Application.UseCases.Tasks.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Tasks.Command.Handler;

internal class CreateTaskHandler : HandlerBase,IRequestHandler<CreateTaskRequest,Result>
{
    public CreateTaskHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var teamCheckResult = await TeamCheck(request.CreateTaskDto.TeamId);
        if (!teamCheckResult.IsSuccess) 
            return teamCheckResult;
        
        var resultCreatedTask = request.ToEntity();
        if(!resultCreatedTask.IsSuccess)
            return resultCreatedTask;

        var task = resultCreatedTask.Value;
        
        if (request.MemberId is not null)
        {
            var member = await GetMemberByIdOrNull((Guid)request.MemberId);
            if (member is null)
                return Result.Failure(new("Member.NotFound", "Member not found!"));
            var resultAddMember = task.AddMember(member);
            if (!resultAddMember.IsSuccess) return resultAddMember;
        }
        _unitOfWork.TaskRepository.Create(task);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }

    private async Task<Result> TeamCheck(Guid teamId)
    {
        var team = await _unitOfWork.TeamRepository.GetByPredicate(x=>x.Id ==teamId);
        if (team is null) 
            return Result.Failure(new Error("Team.NotFound", "Time não existe."));
        return Result.Success();
    }
    private async Task<Member?> GetMemberByIdOrNull(Guid memberId)
        =>await _unitOfWork.MemberRepository.GetByPredicate(x => x.Id == memberId);
}