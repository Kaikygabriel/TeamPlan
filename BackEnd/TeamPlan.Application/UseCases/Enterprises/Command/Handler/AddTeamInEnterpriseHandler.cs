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
        
        var manager = await _unitOfWork.MemberRepository.GetByPredicate(x => x.Id == request.ManageId);
        if(manager is null)
            return Result.Failure(new("Manege.NotFound", "Manege no Not Found!"));
        
        UpdateMemberForManage(manager);

        var resultCreateTeam =await _mediator.Send
            (new CreateTeamRequest(request.NameTeam, manager, request.EnterpriseId));
        
        if(!resultCreateTeam.IsSuccess)
            return Result.Failure(resultCreateTeam.Error);

        enterprise.AddTeam(resultCreateTeam.Value);
        _unitOfWork.EnterpriseRepository.Update(enterprise);

        await _unitOfWork.CommitAsync();
        return Result.Success();
    }

    private void UpdateMemberForManage(Member member)
        => member.UpdateRole(Roles.Manager);
}