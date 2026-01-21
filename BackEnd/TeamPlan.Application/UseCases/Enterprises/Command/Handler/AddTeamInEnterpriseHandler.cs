using MediatR;
using TeamPlan.Application.UseCases.Enterprises.Command.Request;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Enterprises.Command.Handler;

internal class AddTeamInEnterpriseHandler : HandlerBase,IRequestHandler<AddTeamInEnterpriseRequest,Result>
{
    private readonly IMediator _mediator;
    public AddTeamInEnterpriseHandler(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork)
    {
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddTeamInEnterpriseRequest request, CancellationToken cancellationToken)
    {
        var enterprise = await _unitOfWork.EnterpriseRepository
            .GetByPredicate(x => x.Id == request.EnterpriseId);
        if (enterprise is null || enterprise.IdOwner != request.OwnerId)
            return Result.Failure(new("Enterprise.IsInvalid", "Enterprise or owner is invalid!"));
        
        var manager = await _unitOfWork.MemberRepository.GetByEmail(request.EmailManager);
        if(manager is null)
            return Result.Failure(new("Manege.NotFound", "Manege no Not Found!"));
        

        var resultCreateTeam =CreateTeam(request.NameTeam, manager);
        
        if(!resultCreateTeam.IsSuccess)
            return Result.Failure(resultCreateTeam.Error);

        var team = resultCreateTeam.Value;

        UpdateMemberForManage(manager,team);

        enterprise.AddTeam(team);
        _unitOfWork.EnterpriseRepository.Update(enterprise);
        
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }

    private void UpdateMemberForManage(Member member,Team team)
            => member.UpdateForManager(team);
    
    private Result<Team> CreateTeam(string name,Member manage)
    {
        var resultCreateTeam = Team.Factories.Create(name,manage);
        if (!resultCreateTeam.IsSuccess)
            return Result<Team>.Failure(resultCreateTeam.Error);
                
        _unitOfWork.TeamRepository.Create(resultCreateTeam.Value);
        return Result<Team>.Success(resultCreateTeam.Value);
    }
}