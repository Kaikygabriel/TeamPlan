using MediatR;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Command.Handler;

internal class CreateTeamHandler : HandlerBase,IRequestHandler<CreateTeamRequest,Result<Guid>>
{
    public CreateTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<Guid>> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        var manager = await _unitOfWork.MemberRepository.GetByPredicate(x=>x.Id == request.ManageId);
        if (manager is null)
            return new Error("Member.NotFound", "Not Found");

        var resultCreateTeam = Team.Factories.Create(request.Name, manager);
        if (!resultCreateTeam.IsSuccess)
            return resultCreateTeam.Error;

        var team = resultCreateTeam.Value;

        manager.AddTeam(team, Roles.Manager);
        _unitOfWork.TeamRepository.Create(team);
        await _unitOfWork.CommitAsync();
        
        return Result<Guid>.Success(team.Id);
    }
}