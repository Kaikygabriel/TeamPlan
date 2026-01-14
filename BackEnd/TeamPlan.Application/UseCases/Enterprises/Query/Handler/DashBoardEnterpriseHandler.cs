using MediatR;
using TeamPlan.Application.DTOs.Enterprises;
using TeamPlan.Application.UseCases.Enterprises.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Enterprises.Query.Handler;

internal class DashBoardEnterpriseHandler : HandlerBase,IRequestHandler<DashBoardEnterprise,Result<EnterpriseDashBoardDTO>>
{
    public DashBoardEnterpriseHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<EnterpriseDashBoardDTO>> Handle(DashBoardEnterprise request, CancellationToken cancellationToken = default)
    {
        var enterprise = await _unitOfWork.EnterpriseRepository.GetByIdWithTasks(request.EnterpriseId);
        if (enterprise is null)
            return Result<EnterpriseDashBoardDTO>.Failure(new("Enterprise.NotFound", "Not found!"));
        if(enterprise.IdOwner == request.OwnerId)
            return Result<EnterpriseDashBoardDTO>.Failure(new("Owner.NotFound", "Not found!"));

        var percentageTeams = enterprise.Teams.Select(x => x.PercentageByMonthCurrent);
        var percentageEnterprise = (ushort)percentageTeams.Average(x=>x);

        var teamsInOrder = enterprise.Teams.OrderBy(x => x.PercentageByMonthCurrent);
        return Result<EnterpriseDashBoardDTO>.Success
            (new(enterprise.Name, teamsInOrder, percentageEnterprise));
    }

}