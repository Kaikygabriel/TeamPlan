using MediatR;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Command.Handler ;

public class AddMemberInTeamHandler : HandlerBase,IRequestHandler<AddMemberInTeamRequest,Result>
{
    public AddMemberInTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(AddMemberInTeamRequest request, CancellationToken cancellationToken)
    {
        Member? member = await _unitOfWork.MemberRepository.GetByEmail(request.EmailMember);
        if (member is null)
            return Result.Failure(new("Member.NotFound", "Member not found!"));
        
        Team? team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        if (team is null)
            return Result.Failure(new("Team.NotFound", "Team not found!"));
        if(team.ManagerId != request.ManagerId)
            return Result.Failure(new("ManagerId.IsNotEquals", "ManagerId Is Not Equals Team.ManagerId!"));
        
        team.AddMember(member);
        _unitOfWork.TeamRepository.Update(team);
        await _unitOfWork.CommitAsync();
        
        return Result.Success();
    }
}