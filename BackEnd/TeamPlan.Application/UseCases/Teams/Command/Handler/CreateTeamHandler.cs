using MediatR;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using Task = System.Threading.Tasks.Task;

namespace TeamPlan.Application.UseCases.Teams.Command.Handler;

internal class CreateTeamHandler : HandlerBase,IRequestHandler<CreateTeamRequest,Result<Team>>
{
    public CreateTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<Team>> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        var resultCreateTeam = Team.Factories.Create(request.Name,request.Manage);
        if (!resultCreateTeam.IsSuccess)
            return Result<Team>.Failure(resultCreateTeam.Error);
        
        _unitOfWork.TeamRepository.Create(resultCreateTeam.Value);
        
        return await Task.FromResult(Result<Team>.Success(resultCreateTeam.Value));
    }
}