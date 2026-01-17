using MediatR;
using TeamPlan.Application.UseCases.Enterprises.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Enterprises.Command.Handler;

internal class CreateEnterpriseHandler: HandlerBase,IRequestHandler<CreateEnterpriseRequest,Result>
{
    public CreateEnterpriseHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(CreateEnterpriseRequest request, CancellationToken cancellationToken)
    {
        Owner? owner = await _unitOfWork.OwnerRepository.GetByPredicate(x=>x.Id == request.OwnerId);
        if (owner is null)
            return Result.Failure(new("Owner.NoExisting", "Owner No Existing!"));
        var resultCreateEnterprise = Enterprise.Factories.Create(request.Name, owner);
        if(!resultCreateEnterprise.IsSuccess)
            return Result.Failure(new("Create.Enterprise", "Create enterprise invalid!"));
        _unitOfWork.EnterpriseRepository.Create(resultCreateEnterprise.Value);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
} 